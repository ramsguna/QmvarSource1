Imports iFont = iTextSharp.text.Font
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO
Imports System.Text
Imports System.Data.SqlClient
Public Class Class_money
    '売上計上画面からの入力情報（GSPN用）
    Public Structure ADD_DATA
        Dim rec_datetime As String
        Dim rec_yuser As String
        Dim rpt_counter As String
        Dim rpt_repair As String
        Dim close_datetime As String
        Dim denomi As String
        Dim amount As String
        Dim asc_c_num As String
        Dim sam_c_num As String
        Dim po_no As String
        Dim comment As String
        Dim Consumer_Name As String
    End Structure
    '売上計上画面からの入力情報（その他）
    Public Structure OTHER_DATA
        Dim rec_datetime As String
        Dim rec_yuser As String
        Dim rpt_counter As String
        Dim rpt_repair As String
        Dim close_datetime As String
        Dim denomi As String
        Dim amount As String
        Dim po_no As String
        Dim comment As String
        Dim comment2 As String
        Dim Consumer_Name As String
        Dim Consumer_Addr1 As String
        Dim Consumer_Addr2 As String
        Dim Consumer_Telephone As String
        Dim Customer_mail_address As String
        Dim Consumer_Fax As String
        Dim Postal_Code As String
        Dim State_Name As String
        Dim Model As String
        Dim Serial_No As String
        Dim IMEI_No As String
        Dim Defect_Type As String
        Dim Product_Type As String
        Dim Maker As String
        Dim warranty As String
        Dim Repair_Description As String
        Dim Repair_Received_Date As String
        Dim Completed_Date As String
        Dim Delivery_Date As String
        Dim Labor_Amount As String
        Dim Labor_No As String
        Dim Labor_Qty As String
        Dim Other_Freight_Amount As String
        Dim Other_No As String
        Dim Other_Qty As String
        Dim Other_Price As String
        Dim ShipMent_No As String
        Dim ShipMent_Qty As String
        Dim ShipMent_Price As String
        Dim Parts_Amount As String
        Dim Parts_SGST As String
        Dim Parts_IGST As String
        Dim Parts_CGST As String
        Dim Total_Invoice_Amount As String
        Dim Part_1 As String
        Dim Qty_1 As String
        Dim GUnit_Price_1 As String
        Dim Part_2 As String
        Dim Qty_2 As String
        Dim GUnit_Price_2 As String
        Dim Part_3 As String
        Dim Qty_3 As String
        Dim GUnit_Price_3 As String
        Dim Part_4 As String
        Dim Qty_4 As String
        Dim GUnit_Price_4 As String
        Dim Part_5 As String
        Dim Qty_5 As String
        Dim GUnit_Price_5 As String
        Dim SGST As String
        Dim IGST As String
        Dim CGST As String
        Dim Other_Freight_SGST As String
        Dim Other_Freight_IGST As String
        Dim Other_Freight_CGST As String

        '単価　⇓
        Dim gUnitPriceLabor As String
        Dim gUnitPriceParts1 As String
        Dim gUnitPriceParts2 As String
        Dim gUnitPriceParts3 As String
        Dim gUnitPriceParts4 As String
        Dim gUnitPriceParts5 As String
        Dim gUnitPriceOther As String
        Dim gUnitPriceShipMent As String

        '名称
        Dim LaborName As String
        Dim OtherName As String
        Dim Parts1Name As String
        Dim Parts2Name As String
        Dim Parts3Name As String
        Dim Parts4Name As String
        Dim Parts5Name As String
        Dim ShipMentName As String

        '店舗情報
        Dim Ship_Addr1 As String
        Dim Ship_Tel As String
        Dim Ship_Mail As String
        Dim GSTIN As String
        Dim ship_Name As String

    End Structure
    '売り上げ集計情報
    Public Structure Aggregation
        Dim OOWCashSum As Decimal
        Dim OOWCardSum As Decimal
        Dim IWSum As Decimal
        Dim OOWCashTaxIn As Decimal
        Dim OOWCardTaxIn As Decimal
        Dim IWTaxIn As Decimal
        Dim otherCashSum As Decimal
        Dim otherCardSum As Decimal
        Dim otherCashTaxIn As Decimal
        Dim otherCardTaxIn As Decimal
        Dim OOWCashCnt As Integer
        Dim OOWCardCnt As Integer
        Dim IWCnt As Integer
        Dim otherCashCnt As Integer
        Dim otherCardCnt As Integer
        Dim reserve As Decimal
        Dim deposit As Decimal
        Dim cashTotal As Decimal
        Dim depositSum As Decimal
        Dim changeSum As Decimal
        Dim otherDeposit As Decimal
        Dim otherChange As Decimal
        Dim OOWDeposit As Decimal
        Dim OOWChange As Decimal
        Dim Count As Integer
        Dim shipCode As String
        Dim dataNo As Integer
        'custodyテーブルよりの集計をセット
        Dim customerTotalCash As Decimal
        Dim CustomerTotalNumber As Integer
        'discount情報
        Dim discountNum As Integer
        Dim discountAmount As Decimal
        Dim fullDiscountNum As Integer
        Dim fullDiscountAmount As Decimal
        'cash and credit
        Dim OOWCashCreditCount As Integer
        Dim otherCashCreditCount As Integer
    End Structure
    'レジ点検
    Public Structure T_Reserve
        Dim ship_code As String
        Dim maxDate As String
        Dim total As Decimal
        Dim status As String
        Dim youser_name As String
        Dim datetime As DateTime
        Dim diff As Decimal
        Dim reserve As Decimal
        Dim open_youser_name As String
        Dim open_datetime As DateTime
        Dim open_diff As Decimal
        Dim open_maxDate As String
        Dim close_youser_name As String
        Dim close_datetime As DateTime
        Dim close_diff As Decimal
        Dim close_maxDate As String
        Dim ins1_youser_name As String
        Dim ins1_datetime As DateTime
        Dim ins1_diff As Decimal
        Dim ins1_maxDate As String
        Dim ins2_youser_name As String
        Dim ins2_datetime As DateTime
        Dim ins2_diff As Decimal
        Dim ins2_maxDate As String
        Dim ins3_youser_name As String
        Dim ins3_datetime As DateTime
        Dim ins3_diff As Decimal
        Dim ins3_maxDate As String
        Dim dataNo As Integer
        Dim conf_user As String
        Dim conf_datetime As DateTime
        Dim conf_ip As String
        Dim M_2000 As String
        Dim M_500 As String
        Dim M_200 As String
        Dim M_100 As String
        Dim M_50 As String
        Dim M_20 As String
        Dim M_10 As String
        Dim Coin_10 As String
        Dim Coin_5 As String
        Dim Coin_2 As String
        Dim Coin_1 As String
    End Structure
    'サーバ側に保存するパス
    Public saveCsvPass As String = "C:\money\CSV\"
    Public savePDFPass As String = "C:\money\PDF\"

    'logo
    Public logoQuickGarage As String = "C:\money\QuickGarage_logo.jpg"

    'GSPN,その他売り上げデータCSVファイルの項目数
    Public Const GSPNCol As Integer = 138
    Public Const InputCol As Integer = 10
    Public Const otherCol As Integer = 27

    '税率
    Public Const SGST As Decimal = 0.09
    Public Const IGST As Decimal = 0.18
    Public Const CGST As Decimal = 0.09

    '****************************************************
    '処理概要：金種毎件数と金種毎合計金額を取得
    '引数：Result　　    集計結果をセット
    '      shipCode      拠点コード
    '      startDate     invoice_date
    '      errMsg　　    エラーメッセージをセット　　
    '****************************************************
    Public Sub setSyukei(ByRef Result As Aggregation, ByVal shipCode As String, ByVal startDate As String, ByRef errMsg As String)

        Dim dscash_track As New DataSet
        Dim errFlg As Integer
        Dim strSQL As String = ""

        '■***該当件数取得***
        strSQL = "SELECT COUNT(*) AS COUNT "
        strSQL &= "FROM dbo.cash_track WHERE DELFG = 0 AND location = '" & shipCode & "' "
        strSQL &= "AND FALSE = '0' "
        strSQL &= "AND (CONVERT(VARCHAR,invoice_date, 111)) = '" & startDate & "' "
        strSQL &= "AND (payment_kind = '1' OR payment_kind is NULL)"

        dscash_track = DBCommon.Get_DS(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = ("The failed to acquire the number of data items")
            Exit Sub
        End If

        If dscash_track IsNot Nothing Then

            Dim dr As DataRow = dscash_track.Tables(0).Rows(0)
            If dr("COUNT") IsNot DBNull.Value Then
                Result.Count = dr("COUNT")
            Else
                Result.Count = 0
            End If
        End If

        '■***denomi毎、amount(taxin)/deposit/change合計 ***
        strSQL = ""
        strSQL = "SELECT payment_kind, Warranty, payment, SUM(CONVERT(decimal(13,2),total_amount)) AS SUM_total_amount, "
        strSQL &= "CASE WHEN payment = 'Credit' then SUM(CONVERT(decimal(13,2),claim_card)) WHEN payment = 'Cash' then SUM(CONVERT(decimal(13,2),claim)) ELSE SUM(CONVERT(decimal(13,2),claim)) END AS SUM_claim, "
        strSQL &= "SUM(CONVERT(decimal(13,2),deposit)) AS SUM_deposit, SUM(CONVERT(decimal(13,2),change)) AS SUM_change "
        strSQL &= "FROM dbo.cash_track WHERE DELFG = 0 AND location = '" & shipCode & "' "
        strSQL &= "AND (CONVERT(VARCHAR,invoice_date, 111)) = '" & startDate & "' "
        strSQL &= "AND FALSE = '0' "
        strSQL &= "GROUP BY payment_kind, Warranty, payment ORDER BY payment_kind, Warranty, payment;"

        dscash_track = DBCommon.Get_DS(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = ("The failed to acquire summary information (denomi, total amount).")
            Exit Sub
        End If

        If dscash_track IsNot Nothing Then

            For i = 0 To dscash_track.Tables(0).Rows.Count - 1

                Dim dr As DataRow = dscash_track.Tables(0).Rows(i)

                '金種（下記二つの組み合わせで５種類）
                Dim warranty As String
                Dim denomi As String
                Dim paymentKind As String 'cash and creditで登録あり　1:cash 2:credit

                '税込み
                Dim amount As Decimal = 0

                Dim amountClaim As Decimal = 0

                Dim depositSum As Decimal = 0

                Dim changeSum As Decimal = 0

                If dr("Warranty") IsNot DBNull.Value Then
                    warranty = dr("Warranty")
                Else
                    warranty = ""
                End If

                If dr("payment") IsNot DBNull.Value Then
                    denomi = dr("payment")
                Else
                    denomi = ""
                End If

                If dr("payment_kind") IsNot DBNull.Value Then
                    paymentKind = dr("payment_kind")
                Else
                    paymentKind = ""
                End If

                If dr("SUM_total_amount") IsNot DBNull.Value Then
                    amount = dr("SUM_total_amount")
                Else
                    amount = 0.00
                End If

                If dr("SUM_claim") IsNot DBNull.Value Then
                    amountClaim = dr("SUM_claim")
                Else
                    amountClaim = 0.00
                End If

                If dr("SUM_deposit") IsNot DBNull.Value Then
                    depositSum = dr("SUM_deposit")
                Else
                    depositSum = 0.00
                End If

                If dr("SUM_change") IsNot DBNull.Value Then
                    changeSum = dr("SUM_change")
                Else
                    changeSum = 0.00
                End If

                If paymentKind = "" Then

                    If warranty = "IW" Then

                        Result.IWTaxIn = amount
                        Result.IWSum = amountClaim

                    ElseIf warranty = "OOW" Then

                        If denomi = "Cash" Then

                            Result.OOWCashTaxIn = amount
                            Result.OOWCashSum = amountClaim
                            Result.OOWDeposit = depositSum
                            Result.OOWChange = changeSum

                        ElseIf denomi = "Credit" Then

                            Result.OOWCardTaxIn = amount
                            Result.OOWCardSum = amountClaim

                        End If

                    ElseIf warranty = "Other" Then

                        If denomi = "Cash" Then

                            Result.otherCashTaxIn = amount
                            Result.otherCashSum = amountClaim
                            Result.otherDeposit = depositSum
                            Result.otherChange = changeSum

                        ElseIf denomi = "Credit" Then

                            Result.otherCardTaxIn = amount
                            Result.otherCardSum = amountClaim

                        End If

                    End If

                Else
                    '以下、cash and creditで登録あり
                    If warranty = "OOW" Then

                        If denomi = "Cash" Then

                            Result.OOWCashTaxIn = Result.OOWCashTaxIn + amount
                            Result.OOWCashSum = Result.OOWCashSum + amountClaim
                            Result.OOWDeposit = Result.OOWDeposit + depositSum
                            Result.OOWChange = Result.OOWChange + changeSum

                        ElseIf denomi = "Credit" Then

                            'Result.OOWCardTaxIn = amount
                            Result.OOWCardSum = Result.OOWCardSum + amountClaim

                        End If

                    ElseIf warranty = "Other" Then

                        If denomi = "Cash" Then

                            Result.otherCashTaxIn = Result.otherCashTaxIn + amount
                            Result.otherCashSum = Result.otherCashSum + amountClaim
                            Result.otherDeposit = Result.otherDeposit + depositSum
                            Result.otherChange = Result.otherChange + changeSum

                        ElseIf denomi = "Credit" Then

                            'Result.otherCardTaxIn = amount
                            Result.otherCardSum = Result.otherCardSum + amountClaim

                        End If

                    End If

                End If

            Next i

        End If

        '■***denomi毎件数***
        strSQL = ""
        strSQL = "SELECT payment_kind, Warranty, payment, count(*) AS SUM_total_count "
        strSQL &= "FROM dbo.cash_track WHERE DELFG = 0 AND location = '" & shipCode & "' "
        strSQL &= "AND (CONVERT(VARCHAR,invoice_date, 111)) = '" & startDate & "' "
        strSQL &= "AND FALSE = '0' "
        strSQL &= "GROUP BY payment_kind, Warranty, payment ORDER BY payment_kind, Warranty, payment;"

        dscash_track = DBCommon.Get_DS(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = ("Failed to acquire summary information (number of each denomi).")
            Exit Sub
        End If

        If dscash_track IsNot Nothing Then

            For i = 0 To dscash_track.Tables(0).Rows.Count - 1

                Dim dr As DataRow = dscash_track.Tables(0).Rows(i)

                Dim sumTotalCount As Integer

                '金種（下記二つの組み合わせで５種類）
                Dim warranty As String = ""
                Dim denomi As String = ""
                Dim paymentKind As String = ""

                If dr("Warranty") IsNot DBNull.Value Then
                    warranty = dr("Warranty")
                End If

                If dr("payment") IsNot DBNull.Value Then
                    denomi = dr("payment")
                End If

                If dr("payment_kind") IsNot DBNull.Value Then
                    paymentKind = dr("payment_kind")
                End If

                If dr("SUM_total_count") IsNot DBNull.Value Then
                    sumTotalCount = dr("SUM_total_count")
                Else
                    sumTotalCount = 0
                End If

                If paymentKind = "" Then

                    If warranty = "IW" Then

                        Result.IWCnt = sumTotalCount

                    ElseIf warranty = "OOW" Then

                        If denomi = "Cash" Then

                            Result.OOWCashCnt = sumTotalCount

                        ElseIf denomi = "Credit" Then

                            Result.OOWCardCnt = sumTotalCount

                        End If

                    ElseIf warranty = "Other" Then

                        If denomi = "Cash" Then

                            Result.otherCashCnt = sumTotalCount

                        ElseIf denomi = "Credit" Then

                            Result.otherCardCnt = sumTotalCount

                        End If

                    End If

                Else
                    'cash and creditで登録あり
                    If warranty = "OOW" Then

                        If paymentKind = "1" Then

                            Result.OOWCashCreditCount = sumTotalCount

                        End If

                    ElseIf warranty = "Other" Then

                        If paymentKind = "1" Then

                            Result.otherCashCreditCount = sumTotalCount

                        End If

                    End If

                End If

            Next i

        End If

        '■***discount情報***
        'discountの件数と合計金額
        strSQL = "SELECT SUM(discount) AS disSum, COUNT(discount) AS disCnt "
        strSQL &= "FROM dbo.cash_track WHERE DELFG = 0 AND location = '" & shipCode & "' "
        strSQL &= "AND FALSE = '0' "
        strSQL &= "AND (CONVERT(VARCHAR,invoice_date, 111)) = '" & startDate & "' "
        strSQL &= "AND discount <> '0.00' "
        strSQL &= "AND full_discount = 0"
        strSQL &= "AND (payment_kind = '1' or payment_kind IS NULL);"

        dscash_track = DBCommon.Get_DS(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = ("データ件数の取得に失敗しました。")
            Exit Sub
        End If

        If dscash_track IsNot Nothing Then

            Dim dr As DataRow = dscash_track.Tables(0).Rows(0)

            If dr("disSum") IsNot DBNull.Value Then
                Result.discountAmount = dr("disSum")
            Else
                Result.discountAmount = 0.00
            End If

            If dr("disCnt") IsNot DBNull.Value Then
                Result.discountNum = dr("disCnt")
            Else
                Result.discountNum = 0
            End If

        End If

        '■***fulldiscount情報***
        strSQL = "SELECT COUNT(*) AS fulldisCnt, SUM(discount) AS fulldisSum "
        strSQL &= "FROM dbo.cash_track WHERE DELFG = 0 AND location = '" & shipCode & "' "
        strSQL &= "AND FALSE = '0' "
        strSQL &= "AND (CONVERT(VARCHAR,invoice_date, 111)) = '" & startDate & "' "
        strSQL &= "AND full_discount = 1;"

        dscash_track = DBCommon.Get_DS(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = ("Failed to acquire the number of data items.")
            Exit Sub
        End If

        If dscash_track IsNot Nothing Then

            Dim dr As DataRow = dscash_track.Tables(0).Rows(0)

            If dr("fulldisCnt") IsNot DBNull.Value Then
                Result.fullDiscountNum = dr("fulldisCnt")
            Else
                Result.fullDiscountNum = 0
            End If

            If dr("fulldisSum") IsNot DBNull.Value Then
                Result.fullDiscountAmount = dr("fulldisSum")
            Else
                Result.fullDiscountAmount = 0.00
            End If

        End If
        'Comment by India on 2018-12-28 change request of 12/12
        ''■準備金の設定(Open Reserve)
        'Dim dsM_ship_base As New DataSet
        'strSQL = ""
        'strSQL = "SELECT regi_deposit FROM dbo.M_ship_base WHERE DELFG = 0 AND ship_code = '" & shipCode & "';"

        'dsM_ship_base = DBCommon.Get_DS(strSQL, errFlg)

        'If errFlg = 1 Then
        '    errMsg = ("Reserve情報の取得に失敗しました。")
        '    Exit Sub
        'End If

        'If dsM_ship_base Is Nothing Then
        '    errMsg = ("M_ship_baseにregi_deposit（準備金の設定）がありませんでした。")
        '    Exit Sub
        'Else
        '    Dim dr As DataRow = dsM_ship_base.Tables(0).Rows(0)
        '    If dr("regi_deposit") IsNot DBNull.Value Then
        '        Result.reserve = dr("regi_deposit")
        '    End If
        'End If


        '■準備金の設定(Open Reserve)
        Dim dsT_Reserve As New DataSet
        strSQL = ""
        strSQL = "SELECT total FROM dbo.T_Reserve WHERE DELFG = 0 AND ship_code = '" & shipCode & "' "
        strSQL &= "AND (CONVERT(VARCHAR,datetime, 111)) = '" & startDate & "' "
        strSQL &= "AND status='Open' "
        dsT_Reserve = DBCommon.Get_DS(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = ("Reserve information acquisition failed.")
            Exit Sub
        End If

        If dsT_Reserve Is Nothing Then
            errMsg = ("Reserve information acquisition failed.")
            Exit Sub
        Else
            Dim dr As DataRow = dsT_Reserve.Tables(0).Rows(0)
            If dr("total") IsNot DBNull.Value Then
                Result.reserve = dr("total")
            End If
        End If


        '■準備金の設定(Open Deposit)
        Dim dsT_Reserve1 As New DataSet
        strSQL = ""
        strSQL = "SELECT TOP 1 total FROM dbo.T_Reserve WHERE DELFG = 0 AND ship_code = '" & shipCode & "' AND status = 'open' "
        strSQL &= "AND (CONVERT(VARCHAR,datetime, 111)) = '" & startDate & "' "
        strSQL &= "ORDER BY datetime DESC;"

        dsT_Reserve1 = DBCommon.Get_DS(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = ("Failed to get Deposit information.")
            Exit Sub
        End If

        If dsT_Reserve1 IsNot Nothing Then

            Dim dr As DataRow = dsT_Reserve1.Tables(0).Rows(0)
            If dr("total") IsNot DBNull.Value Then
                Result.deposit = dr("total")
            End If

        Else
            Result.deposit = 0.00
        End If

        '■cash total
        '※deposit＋totalの金額(tax含む)
        strSQL = ""
        strSQL = "SELECT TOP 1 total FROM dbo.T_Reserve WHERE DELFG = 0 AND ship_code = '" & shipCode & "' "
        strSQL &= "AND (CONVERT(VARCHAR,datetime, 111)) = '" & startDate & "' "
        strSQL &= "ORDER BY datetime DESC;"

        dsT_Reserve = DBCommon.Get_DS(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = ("Failed to acquire cash total information.")
            Exit Sub
        End If

        If dsT_Reserve IsNot Nothing Then

            Dim dr As DataRow = dsT_Reserve.Tables(0).Rows(0)
            If dr("total") IsNot DBNull.Value Then
                Result.cashTotal = dr("total")
            End If
        Else
            Result.cashTotal = 0.00
        End If

        '■お客様の製品と金銭のお預かりの集計
        strSQL = ""
        strSQL &= "SELECT SUM(cash) AS cash_SUM, COUNT(*) AS COUNT "
        strSQL &= "FROM dbo.custody WHERE DELFG = 0 AND ship_code = '" & shipCode & "' AND takeout = 1 "
        strSQL &= "AND (CONVERT(VARCHAR,CRTDT, 111)) = '" & startDate & "' "

        Dim DT_custody As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = ("Failed to acquire total cash, total number than custody.")
            Exit Sub
        End If

        If DT_custody IsNot Nothing Then

            If DT_custody.Rows(0)("cash_SUM") IsNot DBNull.Value Then
                Result.customerTotalCash = DT_custody.Rows(0)("cash_SUM")
            End If

            If DT_custody.Rows(0)("COUNT") IsNot DBNull.Value Then
                Result.CustomerTotalNumber = DT_custody.Rows(0)("COUNT")
            End If

        End If

    End Sub
    '****************************************************
    '処理概要：金種毎件数と金種毎合計金額を取得
    '引数：Result　　    全拠点の集計結果をセット
    '      ResultALL()   拠点毎の集計情報をセット
    '      shipAllString 拠点コード（カンマ区切り）
    '      shipCodeAll() 拠点コード（配列）
    '      startDate     invoice_date
    '      errMsg　　    エラーメッセージをセット　　
    '****************************************************
    Public Sub setSyukei2(ByRef Result As Aggregation, ByRef ResultALL() As Aggregation, ByVal shipAllString As String, ByVal shipCodeAll() As String, ByVal startDate As String, ByRef errMsg As String)

        Dim dscash_track As New DataSet
        Dim dsT_Reserve As New DataSet
        Dim errFlg As Integer
        Dim strSQL As String = ""

        '■■全件■■ cash_track
        '■***denomi毎、amount(taxin)/deposit/change合計 ***
        strSQL = ""
        strSQL = "SELECT payment_kind, Warranty, payment, SUM(CONVERT(decimal(13,2),total_amount)) AS SUM_total_amount, "
        strSQL &= "CASE WHEN payment = 'Credit' then SUM(CONVERT(decimal(13,2),claim_card)) WHEN payment = 'Cash' then SUM(CONVERT(decimal(13,2),claim)) ELSE SUM(CONVERT(decimal(13,2),claim)) END AS SUM_claim, "
        strSQL &= "SUM(CONVERT(decimal(13,2),deposit)) AS SUM_deposit, SUM(CONVERT(decimal(13,2),change)) AS SUM_change "
        strSQL &= "FROM dbo.cash_track WHERE DELFG = 0 AND location IN (" & shipAllString & ") "
        strSQL &= "AND (CONVERT(VARCHAR,invoice_date, 111)) = '" & startDate & "' "
        strSQL &= "AND FALSE = '0' "
        strSQL &= "GROUP BY payment_kind, Warranty, payment ORDER BY payment_kind, Warranty, payment;"

        dscash_track = DBCommon.Get_DS(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = ("Failed to acquire summary information (denomi, total amount).")
            Exit Sub
        End If

        If dscash_track IsNot Nothing Then

            For i = 0 To dscash_track.Tables(0).Rows.Count - 1

                Dim dr As DataRow = dscash_track.Tables(0).Rows(i)

                '金種（下記二つの組み合わせで５種類）
                Dim warranty As String = ""
                Dim denomi As String = ""
                Dim paymentKind As String = "" 'cash and creditで登録あり　1:cash 2:credit

                '税込み
                Dim amount As Decimal

                Dim amountClaim As Decimal

                Dim depositSum As Decimal

                Dim changeSum As Decimal

                If dr("Warranty") IsNot DBNull.Value Then
                    warranty = dr("Warranty")
                End If

                If dr("payment_kind") IsNot DBNull.Value Then
                    paymentKind = dr("payment_kind")
                End If

                If dr("payment") IsNot DBNull.Value Then
                    denomi = dr("payment")
                End If

                If dr("SUM_total_amount") IsNot DBNull.Value Then
                    amount = dr("SUM_total_amount")
                Else
                    amount = 0.00
                End If

                If dr("SUM_claim") IsNot DBNull.Value Then
                    amountClaim = dr("SUM_claim")
                Else
                    amountClaim = 0.00
                End If

                If dr("SUM_deposit") IsNot DBNull.Value Then
                    depositSum = dr("SUM_deposit")
                Else
                    depositSum = 0.00
                End If

                If dr("SUM_change") IsNot DBNull.Value Then
                    changeSum = dr("SUM_change")
                Else
                    changeSum = 0.00
                End If

                If paymentKind = "" Then

                    If warranty = "IW" Then

                        Result.IWSum = amountClaim
                        Result.IWTaxIn = amount

                    ElseIf warranty = "OOW" Then

                        If denomi = "Cash" Then

                            Result.OOWCashSum = amountClaim
                            Result.OOWCashTaxIn = amount
                            Result.OOWDeposit = depositSum
                            Result.OOWChange = changeSum

                        ElseIf denomi = "Credit" Then

                            Result.OOWCardSum = amountClaim
                            Result.OOWCardTaxIn = amount

                        End If

                    ElseIf warranty = "Other" Then

                        If denomi = "Cash" Then

                            Result.otherCashSum = amountClaim
                            Result.otherCashTaxIn = amount
                            Result.otherDeposit = depositSum
                            Result.otherChange = changeSum

                        ElseIf denomi = "Credit" Then

                            Result.otherCardSum = amountClaim
                            Result.otherCardTaxIn = amount

                        End If

                    End If

                Else
                    '以下、cash and creditで登録あり
                    If warranty = "OOW" Then

                        If denomi = "Cash" Then

                            Result.OOWCashTaxIn = Result.OOWCashTaxIn + amount
                            Result.OOWCashSum = Result.OOWCashSum + amountClaim
                            Result.OOWDeposit = Result.OOWDeposit + depositSum
                            Result.OOWChange = Result.OOWChange + changeSum

                        ElseIf denomi = "Credit" Then

                            'Result.OOWCardTaxIn = amount
                            Result.OOWCardSum = Result.OOWCardSum + amountClaim

                        End If

                    ElseIf warranty = "Other" Then

                        If denomi = "Cash" Then

                            Result.otherCashTaxIn = Result.otherCashTaxIn + amount
                            Result.otherCashSum = Result.otherCashSum + amountClaim
                            Result.otherDeposit = Result.otherDeposit + depositSum
                            Result.otherChange = Result.otherChange + changeSum

                        ElseIf denomi = "Credit" Then

                            'Result.otherCardTaxIn = amount
                            Result.otherCardSum = Result.otherCardSum + amountClaim

                        End If

                    End If

                End If

            Next i

        End If

        '■■全件■■ cash_track
        '■***denomi毎件数***
        strSQL = ""
        strSQL = "SELECT payment_kind, Warranty, payment, count(*) AS SUM_total_count "
        strSQL &= "FROM dbo.cash_track WHERE DELFG = 0 AND location IN (" & shipAllString & ") "
        strSQL &= "AND (CONVERT(VARCHAR,invoice_date, 111)) = '" & startDate & "' "
        strSQL &= "AND FALSE = '0' "
        strSQL &= "GROUP BY payment_kind, Warranty, payment ORDER BY payment_kind, Warranty, payment;"

        dscash_track = DBCommon.Get_DS(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = ("Failed to acquire summary information (number of each denomi).")
            Exit Sub
        End If

        If dscash_track IsNot Nothing Then

            For i = 0 To dscash_track.Tables(0).Rows.Count - 1

                Dim dr As DataRow = dscash_track.Tables(0).Rows(i)

                Dim sumTotalCount As Integer

                Dim location As String = ""

                '金種（下記二つの組み合わせで５種類）
                Dim warranty As String = ""
                Dim denomi As String = ""
                Dim paymentKind As String = "" 'cash and creditで登録あり　1:cash 2:credit

                If dr("Warranty") IsNot DBNull.Value Then
                    warranty = dr("Warranty")
                End If

                If dr("payment_kind") IsNot DBNull.Value Then
                    paymentKind = dr("payment_kind")
                End If

                If dr("payment") IsNot DBNull.Value Then
                    denomi = dr("payment")
                End If

                If dr("SUM_total_count") IsNot DBNull.Value Then
                    sumTotalCount = dr("SUM_total_count")
                Else
                    sumTotalCount = 0
                End If

                If paymentKind = "" Then

                    If warranty = "IW" Then

                        Result.IWCnt = sumTotalCount

                    ElseIf warranty = "OOW" Then

                        If denomi = "Cash" Then

                            Result.OOWCashCnt = sumTotalCount

                        ElseIf denomi = "Credit" Then

                            Result.OOWCardCnt = sumTotalCount

                        End If

                    ElseIf warranty = "Other" Then

                        If denomi = "Cash" Then

                            Result.otherCashCnt = sumTotalCount

                        ElseIf denomi = "Credit" Then

                            Result.otherCardCnt = sumTotalCount

                        End If

                    End If

                Else
                    'cash and creditで登録あり
                    If warranty = "OOW" Then

                        If paymentKind = "1" Then

                            Result.OOWCashCreditCount = sumTotalCount

                        End If

                    ElseIf warranty = "Other" Then

                        If paymentKind = "1" Then

                            Result.otherCashCreditCount = sumTotalCount

                        End If

                    End If

                End If

            Next i

        End If

        '■■拠点毎■■ cash_track
        '■***denomi毎件数***
        Dim shipCnt As Integer
        Dim CntNo As Integer

        For shipCnt = 0 To shipCodeAll.Length - 1

            strSQL = ""
            strSQL = "SELECT payment_kind, Warranty, payment, count(*) AS SUM_total_count "
            strSQL &= "FROM dbo.cash_track WHERE DELFG = 0 AND location = '" & shipCodeAll(shipCnt) & "' "
            strSQL &= "AND (CONVERT(VARCHAR,invoice_date, 111)) = '" & startDate & "' "
            strSQL &= "AND FALSE = '0' "
            strSQL &= "GROUP BY payment_kind, Warranty, payment ORDER BY payment_kind, Warranty, payment;"

            dscash_track = DBCommon.Get_DS(strSQL, errFlg)

            If errFlg = 1 Then
                errMsg = ("Failed to acquire summary information (number of each denomi).")
                Exit Sub
            End If

            CntNo = CntNo + 1

            ResultALL(shipCnt).shipCode = shipCodeAll(shipCnt)
            ResultALL(shipCnt).dataNo = CntNo

            If dscash_track IsNot Nothing Then

                For i = 0 To dscash_track.Tables(0).Rows.Count - 1

                    Dim dr As DataRow = dscash_track.Tables(0).Rows(i)

                    Dim sumTotalCount As Integer

                    '金種（下記二つの組み合わせで５種類）
                    Dim warranty As String = ""
                    Dim denomi As String = ""
                    Dim paymentKind As String = "" 'cash and creditで登録あり　1:cash 2:credit

                    If dr("Warranty") IsNot DBNull.Value Then
                        warranty = dr("Warranty")
                    End If

                    If dr("payment_kind") IsNot DBNull.Value Then
                        paymentKind = dr("payment_kind")
                    End If

                    If dr("payment") IsNot DBNull.Value Then
                        denomi = dr("payment")
                    End If

                    If dr("SUM_total_count") IsNot DBNull.Value Then
                        sumTotalCount = dr("SUM_total_count")
                    Else
                        sumTotalCount = 0
                    End If

                    If paymentKind = "" Then

                        If warranty = "IW" Then

                            ResultALL(shipCnt).IWCnt = sumTotalCount

                        ElseIf warranty = "OOW" Then

                            If denomi = "Cash" Then

                                ResultALL(shipCnt).OOWCashCnt = sumTotalCount

                            ElseIf denomi = "Credit" Then

                                ResultALL(shipCnt).OOWCardCnt = sumTotalCount

                            End If

                        ElseIf warranty = "Other" Then

                            If denomi = "Cash" Then

                                ResultALL(shipCnt).otherCashCnt = sumTotalCount

                            ElseIf denomi = "Credit" Then

                                ResultALL(shipCnt).otherCardCnt = sumTotalCount

                            End If

                        End If

                    Else
                        'cash and creditで登録あり
                        If warranty = "OOW" Then

                            If paymentKind = "1" Then

                                ResultALL(shipCnt).OOWCashCreditCount = sumTotalCount

                            End If

                        ElseIf warranty = "Other" Then

                            If paymentKind = "1" Then

                                ResultALL(shipCnt).otherCashCreditCount = sumTotalCount

                            End If

                        End If

                    End If

                Next i

            End If

        Next shipCnt

        '■■拠点毎■■ cash_track
        '■***denomi毎、amount(taxin)/deposit/change合計 ***
        For shipCnt = 0 To shipCodeAll.Length - 1

            strSQL = ""
            strSQL = "SELECT payment_kind, Warranty, payment, SUM(CONVERT(decimal(13,2),total_amount)) AS SUM_total_amount, "
            strSQL &= "CASE WHEN payment = 'Credit' then SUM(CONVERT(decimal(13,2),claim_card)) WHEN payment = 'Cash' then SUM(CONVERT(decimal(13,2),claim)) ELSE SUM(CONVERT(decimal(13,2),claim)) END AS SUM_claim, "
            strSQL &= "SUM(CONVERT(decimal(13,2),deposit)) AS SUM_deposit, SUM(CONVERT(decimal(13,2),change)) AS SUM_change "
            strSQL &= "FROM dbo.cash_track WHERE DELFG = 0 AND location = '" & shipCodeAll(shipCnt) & "' "
            strSQL &= "AND (CONVERT(VARCHAR,invoice_date, 111)) = '" & startDate & "' "
            strSQL &= "AND FALSE = '0' "
            strSQL &= "GROUP BY payment_kind, Warranty, payment ORDER BY payment_kind, Warranty, payment;"

            dscash_track = DBCommon.Get_DS(strSQL, errFlg)

            If errFlg = 1 Then
                errMsg = ("Failed to acquire summary information (denomi, total amount).")
                Exit Sub
            End If

            If dscash_track IsNot Nothing Then

                For i = 0 To dscash_track.Tables(0).Rows.Count - 1

                    Dim dr As DataRow = dscash_track.Tables(0).Rows(i)

                    '金種（下記二つの組み合わせで５種類）
                    Dim warranty As String = ""
                    Dim denomi As String = ""
                    Dim paymentKind As String = "" 'cash and creditで登録あり　1:cash 2:credit

                    '税込み
                    Dim amount As Decimal

                    Dim amountClaim As Decimal

                    Dim depositSum As Decimal

                    Dim changeSum As Decimal

                    If dr("Warranty") IsNot DBNull.Value Then
                        warranty = dr("Warranty")
                    End If

                    If dr("payment_kind") IsNot DBNull.Value Then
                        paymentKind = dr("payment_kind")
                    End If

                    If dr("payment") IsNot DBNull.Value Then
                        denomi = dr("payment")
                    End If

                    If dr("SUM_total_amount") IsNot DBNull.Value Then
                        amount = dr("SUM_total_amount")
                    Else
                        amount = 0.00
                    End If

                    If dr("SUM_claim") IsNot DBNull.Value Then
                        amountClaim = dr("SUM_claim")
                    Else
                        amountClaim = 0.00
                    End If

                    If dr("SUM_deposit") IsNot DBNull.Value Then
                        depositSum = dr("SUM_deposit")
                    Else
                        depositSum = 0.00
                    End If

                    If dr("SUM_change") IsNot DBNull.Value Then
                        changeSum = dr("SUM_change")
                    Else
                        changeSum = 0.00
                    End If

                    If paymentKind = "" Then

                        If warranty = "IW" Then

                            ResultALL(shipCnt).IWSum = amountClaim
                            ResultALL(shipCnt).IWTaxIn = amount

                        ElseIf warranty = "OOW" Then

                            If denomi = "Cash" Then

                                ResultALL(shipCnt).OOWCashSum = amountClaim
                                ResultALL(shipCnt).OOWCashTaxIn = amount
                                ResultALL(shipCnt).OOWDeposit = depositSum
                                ResultALL(shipCnt).OOWChange = changeSum

                            ElseIf denomi = "Credit" Then

                                ResultALL(shipCnt).OOWCardSum = amountClaim
                                ResultALL(shipCnt).OOWCardTaxIn = amount

                            End If

                        ElseIf warranty = "Other" Then

                            If denomi = "Cash" Then

                                ResultALL(shipCnt).otherCashSum = amountClaim
                                ResultALL(shipCnt).otherCashTaxIn = amount
                                ResultALL(shipCnt).otherDeposit = depositSum
                                ResultALL(shipCnt).otherChange = changeSum

                            ElseIf denomi = "Credit" Then

                                ResultALL(shipCnt).otherCardSum = amountClaim
                                ResultALL(shipCnt).otherCardTaxIn = amount

                            End If

                        End If

                    Else
                        '以下、cash and creditで登録あり
                        If warranty = "OOW" Then

                            If denomi = "Cash" Then

                                ResultALL(shipCnt).OOWCashTaxIn = ResultALL(shipCnt).OOWCashTaxIn + amount
                                ResultALL(shipCnt).OOWCashSum = ResultALL(shipCnt).OOWCashSum + amountClaim
                                ResultALL(shipCnt).OOWDeposit = ResultALL(shipCnt).OOWDeposit + depositSum
                                ResultALL(shipCnt).OOWChange = ResultALL(shipCnt).OOWChange + changeSum

                            ElseIf denomi = "Credit" Then

                                'Result.OOWCardTaxIn = amount
                                ResultALL(shipCnt).OOWCardSum = ResultALL(shipCnt).OOWCardSum + amountClaim

                            End If

                        ElseIf warranty = "Other" Then

                            If denomi = "Cash" Then

                                ResultALL(shipCnt).otherCashTaxIn = ResultALL(shipCnt).otherCashTaxIn + amount
                                ResultALL(shipCnt).otherCashSum = ResultALL(shipCnt).otherCashSum + amountClaim
                                ResultALL(shipCnt).otherDeposit = ResultALL(shipCnt).otherDeposit + depositSum
                                ResultALL(shipCnt).otherChange = ResultALL(shipCnt).otherChange + changeSum

                            ElseIf denomi = "Credit" Then

                                'Result.otherCardTaxIn = amount
                                ResultALL(shipCnt).otherCardSum = ResultALL(shipCnt).otherCardSum + amountClaim

                            End If

                        End If

                    End If

                Next i

            End If

        Next shipCnt

        '■■拠点毎■■ T_Reserve
        '■最新のcash total
        '※deposit＋totalの金額(tax含む)
        For shipCnt = 0 To shipCodeAll.Length - 1

            strSQL = ""
            strSQL = "SELECT TOP 1 total FROM dbo.T_Reserve WHERE DELFG = 0 AND ship_code = '" & shipCodeAll(shipCnt) & "' "
            strSQL &= "AND (CONVERT(VARCHAR,datetime, 111)) = '" & startDate & "' "
            strSQL &= "ORDER BY datetime DESC;"

            dsT_Reserve = DBCommon.Get_DS(strSQL, errFlg)

            If errFlg = 1 Then
                errMsg = ("Failed to acquire cash total information.")
                Exit Sub
            End If

            If dsT_Reserve IsNot Nothing Then

                Dim dr As DataRow = dsT_Reserve.Tables(0).Rows(0)
                If dr("total") IsNot DBNull.Value Then
                    ResultALL(shipCnt).cashTotal = dr("total")
                Else
                    ResultALL(shipCnt).cashTotal = 0.00
                End If

            Else

                ResultALL(shipCnt).cashTotal = 0.00

            End If

        Next shipCnt

        '■■全件■■ T_Reserve
        '■cash total
        '※deposit,tax含む
        '※拠点毎の最新レジ点検時刻を取得
        Dim workSyukei() As T_Reserve
        strSQL = ""
        strSQL = "SELECT ship_code, MAX(datetime) AS maxDate FROM dbo.T_Reserve WHERE DELFG = 0 AND ship_code IN (" & shipAllString & ") "
        strSQL &= "AND (CONVERT(VARCHAR,datetime, 111)) = '" & startDate & "' "
        strSQL &= "GROUP BY ship_code;"

        dsT_Reserve = DBCommon.Get_DS(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = ("Failed to acquire cash total information from T_Reserve.")
            Exit Sub
        End If

        If dsT_Reserve IsNot Nothing Then

            If dsT_Reserve.Tables(0).Rows.Count <> 0 Then

                ReDim workSyukei(dsT_Reserve.Tables(0).Rows.Count - 1)

                For i = 0 To dsT_Reserve.Tables(0).Rows.Count - 1

                    Dim dr As DataRow = dsT_Reserve.Tables(0).Rows(i)

                    Dim workDt As DateTime

                    If dr("maxDate") IsNot DBNull.Value Then
                        workDt = dr("maxDate")
                        workSyukei(i).maxDate = workDt.ToString("yyyy/MM/dd HH:mm:ss")
                    End If

                    If dr("ship_code") IsNot DBNull.Value Then
                        workSyukei(i).ship_code = dr("ship_code")
                    End If

                Next i

            End If

        End If

        '※拠点毎のレジ点検最新時刻よりtotal情報を取得
        '※拠点毎のtotal情報を集計
        Dim cashTotal As Decimal

        If workSyukei IsNot Nothing Then

            For i = 0 To workSyukei.Length - 1

                strSQL = ""
                strSQL = "SELECT * FROM dbo.T_Reserve WHERE DELFG = 0 AND ship_code = '" & workSyukei(i).ship_code & "' "
                strSQL &= "AND (CONVERT(VARCHAR, datetime, 111)) = '" & startDate & "' "
                strSQL &= "AND FORMAT(datetime,'yyyy/MM/dd HH:mm:ss') = '" & workSyukei(i).maxDate & "';"

                dsT_Reserve = DBCommon.Get_DS(strSQL, errFlg)

                If errFlg = 1 Then
                    errMsg = ("Failed to acquire cash total information from T_Reserve.")
                    Exit Sub
                End If

                If dsT_Reserve IsNot Nothing Then

                    If dsT_Reserve.Tables(0).Rows.Count = 1 Then

                        Dim dr As DataRow = dsT_Reserve.Tables(0).Rows(0)

                        If dr("total") IsNot DBNull.Value Then
                            workSyukei(i).total = dr("total")
                        Else
                            workSyukei(i).total = "0.00"
                        End If

                        cashTotal = cashTotal + workSyukei(i).total
                        Result.cashTotal = cashTotal

                    End If

                End If

            Next i

        End If

        '■■全件■■ T_Reserve
        '■open deposit　全ての拠点の準備金の合計を取得（T_Reserveのstatuがopenの最新のtotal情報を取得）
        Dim workSyukei2() As T_Reserve
        strSQL = ""
        strSQL = "SELECT ship_code, MAX(datetime) AS maxDate FROM dbo.T_Reserve WHERE DELFG = 0 AND ship_code IN (" & shipAllString & ") "
        strSQL &= "AND status = 'open' "
        strSQL &= "AND (CONVERT(VARCHAR,datetime, 111)) = '" & startDate & "' "
        strSQL &= "GROUP BY ship_code;"

        dsT_Reserve = DBCommon.Get_DS(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = ("The failed to acquire open deposit information.")
            Exit Sub
        End If

        If dsT_Reserve IsNot Nothing Then

            If dsT_Reserve.Tables(0).Rows.Count <> 0 Then

                ReDim workSyukei2(dsT_Reserve.Tables(0).Rows.Count - 1)

                For i = 0 To dsT_Reserve.Tables(0).Rows.Count - 1

                    Dim dr As DataRow = dsT_Reserve.Tables(0).Rows(i)

                    Dim workDt As DateTime

                    If dr("maxDate") IsNot DBNull.Value Then
                        workDt = dr("maxDate")
                        workSyukei2(i).maxDate = workDt.ToString("yyyy/MM/dd HH:mm:ss")
                    End If

                    If dr("ship_code") IsNot DBNull.Value Then
                        workSyukei2(i).ship_code = dr("ship_code")
                    End If

                Next i

            End If

        End If

        'total情報を取得して集計
        cashTotal = 0
        If workSyukei2 IsNot Nothing Then

            For i = 0 To workSyukei2.Length - 1

                strSQL = ""
                strSQL = "SELECT * FROM dbo.T_Reserve WHERE DELFG = 0 AND ship_code = '" & workSyukei2(i).ship_code & "' "
                strSQL &= "AND (CONVERT(VARCHAR, datetime, 111)) = '" & startDate & "' "
                strSQL &= "AND FORMAT(datetime,'yyyy/MM/dd HH:mm:ss') = '" & workSyukei2(i).maxDate & "';"

                dsT_Reserve = DBCommon.Get_DS(strSQL, errFlg)

                If errFlg = 1 Then
                    errMsg = ("Failed to acquire cash total information from T_Reserve.")
                    Exit Sub
                End If

                If dsT_Reserve IsNot Nothing Then

                    If dsT_Reserve.Tables(0).Rows.Count = 1 Then

                        Dim dr As DataRow = dsT_Reserve.Tables(0).Rows(0)

                        If dr("total") IsNot DBNull.Value Then
                            workSyukei2(i).total = dr("total")
                        Else
                            workSyukei2(i).total = "0.00"
                        End If

                        cashTotal = cashTotal + workSyukei2(i).total
                        Result.deposit = cashTotal

                    End If

                End If

            Next i

        End If

        '■■拠点毎■■
        '■open deposit
        For shipCnt = 0 To shipCodeAll.Length - 1

            strSQL = ""
            strSQL = "SELECT TOP 1 total FROM dbo.T_Reserve WHERE DELFG = 0 AND ship_code = '" & shipCodeAll(shipCnt) & "' "
            strSQL &= "AND (CONVERT(VARCHAR,datetime, 111)) = '" & startDate & "' "
            strSQL &= "AND status = 'open' "
            strSQL &= "ORDER BY datetime DESC;"

            dsT_Reserve = DBCommon.Get_DS(strSQL, errFlg)

            If errFlg = 1 Then
                errMsg = ("Failed to acquire open deposit information ")
                Exit Sub
            End If

            If dsT_Reserve IsNot Nothing Then

                Dim dr As DataRow = dsT_Reserve.Tables(0).Rows(0)
                If dr("total") IsNot DBNull.Value Then
                    ResultALL(shipCnt).deposit = dr("total")
                Else
                    ResultALL(shipCnt).deposit = 0.00
                End If

            Else
                ResultALL(shipCnt).deposit = 0.00
            End If

        Next shipCnt

        '■■全件■■
        '■お客様の製品と金銭のお預かりの集計  
        strSQL = ""
        strSQL &= "SELECT SUM(cash) AS cash_SUM, COUNT(*) AS COUNT "
        strSQL &= "FROM dbo.custody WHERE DELFG = 0 AND ship_code IN (" & shipAllString & ") "
        strSQL &= "AND takeout = 1 AND (CONVERT(VARCHAR,CRTDT, 111)) = '" & startDate & "' "

        Dim DT_custody As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = ("Failed to acquire total cash, total number from custody (all cases)")
            Exit Sub
        End If

        If DT_custody IsNot Nothing Then

            If DT_custody.Rows(0)("cash_SUM") IsNot DBNull.Value Then
                Result.customerTotalCash = DT_custody.Rows(0)("cash_SUM")
            End If

            If DT_custody.Rows(0)("COUNT") IsNot DBNull.Value Then
                Result.CustomerTotalNumber = DT_custody.Rows(0)("COUNT")
            End If

        End If

        '■■拠点毎■■
        '■お客様の製品と金銭のお預かりの集計  
        For shipCnt = 0 To shipCodeAll.Length - 1

            strSQL = ""
            strSQL &= "SELECT SUM(cash) AS cash_SUM, COUNT(*) AS COUNT "
            strSQL &= "FROM dbo.custody WHERE DELFG = 0 AND ship_code = '" & shipCodeAll(shipCnt) & "' "
            strSQL &= "AND takeout = 1 AND (CONVERT(VARCHAR,CRTDT, 111)) = '" & startDate & "' "

            DT_custody = DBCommon.ExecuteGetDT(strSQL, errFlg)

            If errFlg = 1 Then
                errMsg = ("Failed to acquire total cash, total number from custody")
                Exit Sub
            End If

            If DT_custody IsNot Nothing Then

                If DT_custody.Rows(0)("cash_SUM") IsNot DBNull.Value Then
                    ResultALL(shipCnt).customerTotalCash = DT_custody.Rows(0)("cash_SUM")
                End If

                If DT_custody.Rows(0)("COUNT") IsNot DBNull.Value Then
                    ResultALL(shipCnt).CustomerTotalNumber = DT_custody.Rows(0)("COUNT")
                End If

            End If

        Next shipCnt

    End Sub

    '****************************************************
    '処理概要：拠点毎のレジチェック点検情報を取得（List表示用）
    '引数：ResultList　　 T_Reserve情報をセット
    '      ResultListALL  拠点毎の T_Reserve情報をセット
    '      shipAllString  全拠点コード（カンマ区切り）
    '      shipCodeAll    全拠点コード
    '      startDate      invoice_date
    '      errMsg　　     エラーメッセージをセット　　
    '****************************************************
    Public Sub setSyukeiList(ByRef ResultList As T_Reserve, ByRef ResultListALL() As T_Reserve, ByVal shipAllString As String, ByVal shipCodeAll() As String, ByVal startDate As String, ByRef errMsg As String)

        '■■拠点毎■■
        Dim shipCnt As Integer
        Dim CntNo As Integer
        Dim strSQL As String = ""
        Dim errFlg As Integer

        'ステータス毎の最新時刻を取得
        For shipCnt = 0 To shipCodeAll.Length - 1

            strSQL = ""
            strSQL = "SELECT status, MAX(datetime) AS maxDate FROM dbo.T_Reserve WHERE DELFG = 0 AND ship_code = '" & shipCodeAll(shipCnt) & "' "
            strSQL &= "AND (CONVERT(VARCHAR,datetime, 111)) = '" & startDate & "' "
            strSQL &= "GROUP BY status ORDER BY status;"

            Dim dsT_Reserve As New DataSet
            dsT_Reserve = DBCommon.Get_DS(strSQL, errFlg)

            If errFlg = 1 Then
                errMsg = ("Failed to acquire cash total information from T_Reserve")
                Exit Sub
            End If

            CntNo = CntNo + 1

            ResultListALL(shipCnt).ship_code = shipCodeAll(shipCnt)
            ResultListALL(shipCnt).dataNo = CntNo

            If dsT_Reserve IsNot Nothing Then

                If dsT_Reserve.Tables(0).Rows.Count <> 0 Then

                    Dim maxDate As DateTime
                    Dim maxDateStr As String
                    Dim status As String

                    For i = 0 To dsT_Reserve.Tables(0).Rows.Count - 1

                        Dim dr As DataRow = dsT_Reserve.Tables(0).Rows(i)

                        If dr("maxDate") IsNot DBNull.Value Then
                            maxDate = dr("maxDate")
                            maxDateStr = maxDate.ToString("yyyy/MM/dd HH:mm:ss")
                        End If

                        If dr("status") IsNot DBNull.Value Then
                            status = dr("status")
                        End If

                        If status = "close" Then

                            ResultListALL(shipCnt).close_maxDate = maxDateStr

                        ElseIf status = "inspection1" Then

                            ResultListALL(shipCnt).ins1_maxDate = maxDateStr

                        ElseIf status = "inspection2" Then

                            ResultListALL(shipCnt).ins2_maxDate = maxDateStr

                        ElseIf status = "inspection3" Then

                            ResultListALL(shipCnt).ins3_maxDate = maxDateStr

                        ElseIf status = "open" Then

                            ResultListALL(shipCnt).open_maxDate = maxDateStr

                        End If

                    Next i

                End If

            End If

        Next shipCnt

        '最新のopen情報取得
        For i = 0 To ResultListALL.Length - 1

            If ResultListALL(i).open_maxDate <> "" Then
                strSQL = ""
                strSQL = "SELECT * FROM dbo.T_Reserve WHERE DELFG = 0 AND ship_code = '" & ResultListALL(i).ship_code & "' "
                strSQL &= "AND (CONVERT(VARCHAR,datetime, 111)) = '" & startDate & "' "
                strSQL &= "AND FORMAT(datetime,'yyyy/MM/dd HH:mm:ss') = '" & ResultListALL(i).open_maxDate & "';"

                Dim dsT_Reserve As New DataSet
                dsT_Reserve = DBCommon.Get_DS(strSQL, errFlg)

                If errFlg = 1 Then
                    errMsg = ("Failed to acquire Open information from T_Reserve")
                    Exit Sub
                End If

                If dsT_Reserve IsNot Nothing Then

                    If dsT_Reserve.Tables(0).Rows.Count = 1 Then

                        Dim dr As DataRow = dsT_Reserve.Tables(0).Rows(0)

                        If dr("diff") IsNot DBNull.Value Then
                            ResultListALL(i).open_diff = dr("diff")
                        End If

                        If dr("youser_name") IsNot DBNull.Value Then
                            ResultListALL(i).open_youser_name = dr("youser_name")
                        End If

                    End If

                End If

            End If

        Next i

        '最新のclose情報取得
        For i = 0 To ResultListALL.Length - 1

            If ResultListALL(i).close_maxDate <> "" Then
                strSQL = ""
                strSQL = "SELECT * FROM dbo.T_Reserve WHERE DELFG = 0 AND ship_code = '" & ResultListALL(i).ship_code & "' "
                strSQL &= "AND (CONVERT(VARCHAR,datetime, 111)) = '" & startDate & "' "
                strSQL &= "AND FORMAT(datetime,'yyyy/MM/dd HH:mm:ss') = '" & ResultListALL(i).close_maxDate & "';"

                Dim dsT_Reserve As New DataSet
                dsT_Reserve = DBCommon.Get_DS(strSQL, errFlg)

                If errFlg = 1 Then
                    errMsg = ("Failed to acquire Open information from T_Reserve.")
                    Exit Sub
                End If

                If dsT_Reserve IsNot Nothing Then

                    If dsT_Reserve.Tables(0).Rows.Count = 1 Then

                        Dim dr As DataRow = dsT_Reserve.Tables(0).Rows(0)

                        If dr("diff") IsNot DBNull.Value Then
                            ResultListALL(i).close_diff = dr("diff")
                        End If

                        If dr("youser_name") IsNot DBNull.Value Then
                            ResultListALL(i).close_youser_name = dr("youser_name")
                        End If

                    End If

                End If

            End If

        Next i

        '最新のレジ点検１回目情報取得
        For i = 0 To ResultListALL.Length - 1

            If ResultListALL(i).ins1_maxDate <> "" Then
                strSQL = ""
                strSQL = "SELECT * FROM dbo.T_Reserve WHERE DELFG = 0 AND ship_code = '" & ResultListALL(i).ship_code & "' "
                strSQL &= "AND (CONVERT(VARCHAR,datetime, 111)) = '" & startDate & "' "
                strSQL &= "AND FORMAT(datetime,'yyyy/MM/dd HH:mm:ss') = '" & ResultListALL(i).ins1_maxDate & "';"

                Dim dsT_Reserve As New DataSet
                dsT_Reserve = DBCommon.Get_DS(strSQL, errFlg)

                If errFlg = 1 Then
                    errMsg = ("Failed to acquire Open information from T_Reserve.")
                    Exit Sub
                End If

                If dsT_Reserve IsNot Nothing Then

                    If dsT_Reserve.Tables(0).Rows.Count = 1 Then

                        Dim dr As DataRow = dsT_Reserve.Tables(0).Rows(0)

                        If dr("diff") IsNot DBNull.Value Then
                            ResultListALL(i).ins1_diff = dr("diff")
                        End If

                        If dr("youser_name") IsNot DBNull.Value Then
                            ResultListALL(i).ins1_youser_name = dr("youser_name")
                        End If

                    End If

                End If

            End If

        Next i

        '最新のレジ点検２回目情報取得
        For i = 0 To ResultListALL.Length - 1

            If ResultListALL(i).ins2_maxDate <> "" Then
                strSQL = ""
                strSQL = "SELECT * FROM dbo.T_Reserve WHERE DELFG = 0 AND ship_code = '" & ResultListALL(i).ship_code & "' "
                strSQL &= "AND (CONVERT(VARCHAR,datetime, 111)) = '" & startDate & "' "
                strSQL &= "AND FORMAT(datetime,'yyyy/MM/dd HH:mm:ss') = '" & ResultListALL(i).ins2_maxDate.ToString & "';"

                Dim dsT_Reserve As New DataSet
                dsT_Reserve = DBCommon.Get_DS(strSQL, errFlg)

                If errFlg = 1 Then
                    errMsg = ("Failed to acquire Open information from T_Reserve")
                    Exit Sub
                End If

                If dsT_Reserve IsNot Nothing Then

                    If dsT_Reserve.Tables(0).Rows.Count = 1 Then

                        Dim dr As DataRow = dsT_Reserve.Tables(0).Rows(0)

                        If dr("diff") IsNot DBNull.Value Then
                            ResultListALL(i).ins2_diff = dr("diff")
                        End If

                        If dr("youser_name") IsNot DBNull.Value Then
                            ResultListALL(i).ins2_youser_name = dr("youser_name")
                        End If

                    End If

                End If

            End If

        Next i

        '最新のレジ点検３回目情報取得
        For i = 0 To ResultListALL.Length - 1

            If ResultListALL(i).ins3_maxDate <> "" Then
                strSQL = ""
                strSQL = "SELECT * FROM dbo.T_Reserve WHERE DELFG = 0 AND ship_code = '" & ResultListALL(i).ship_code & "' "
                strSQL &= "AND (CONVERT(VARCHAR,datetime, 111)) = '" & startDate & "' "
                strSQL &= "AND FORMAT(datetime,'yyyy/MM/dd HH:mm:ss') = '" & ResultListALL(i).ins3_maxDate & "';"

                Dim dsT_Reserve As New DataSet
                dsT_Reserve = DBCommon.Get_DS(strSQL, errFlg)

                If errFlg = 1 Then
                    errMsg = ("Failed to acquire Open information from T_Reserve")
                    Exit Sub
                End If

                If dsT_Reserve IsNot Nothing Then

                    If dsT_Reserve.Tables(0).Rows.Count = 1 Then

                        Dim dr As DataRow = dsT_Reserve.Tables(0).Rows(0)

                        If dr("diff") IsNot DBNull.Value Then
                            ResultListALL(i).ins3_diff = dr("diff")
                        End If

                        If dr("youser_name") IsNot DBNull.Value Then
                            ResultListALL(i).ins3_youser_name = dr("youser_name")
                        End If

                    End If

                End If

            End If

        Next i

    End Sub

    '****************************************************
    '処理概要：Money型の小数点３、４桁目を切り捨てる
    '引数： moneyInr　金額
    '返却： moneyInr　金額の小数点以下を２桁にセットして返す　
    '****************************************************
    Public Function setINR(ByVal moneyInr As String) As String

        If moneyInr = "" Then
            Exit Function
        End If

        Dim amountDelimit() As String

        amountDelimit = Split(moneyInr, ".")


        If amountDelimit.Length = 1 Then

            '小数点なしは、.00を付加　　例）30 ⇒　30.00
            moneyInr = moneyInr & ".00"

        ElseIf amountDelimit.Length = 2 Then

            '小数点ありは、３桁以上切り捨て 例）30.2300 ⇒　30.23
            moneyInr = amountDelimit(0) & "." & Left(amountDelimit(1), 2)

        End If

        Return moneyInr

    End Function
    '****************************************************
    '処理概要：decimal型の小数点３、４桁目を切り捨てる
    '引数： moneyInr　金額
    '返却： moneyInr　金額の小数点以下を２桁にセットして返す　
    '****************************************************
    Public Function setINR2(ByVal moneyInr As Decimal) As Decimal

        Dim amountDelimit() As String
        Dim moneyInrStr As String

        amountDelimit = Split(moneyInr.ToString, ".")

        If amountDelimit.Length = 1 Then

            '小数点なしは、.00を付加　　例）30 ⇒　30.00
            moneyInrStr = moneyInr.ToString & ".00"

        ElseIf amountDelimit.Length = 2 Then

            '小数点ありは、３桁以上切り捨て 例）30.2300 ⇒　30.23
            moneyInrStr = amountDelimit(0) & "." & Left(amountDelimit(1), 2)

        End If

        Return Convert.ToDecimal(moneyInrStr)

    End Function

    '****************************************************
    '処理概要：CSVデータを配列にセット
    '引数：filePass CSVファイル名(pass付き)
    '      colLen 戻り値  項目数をセット
    '      errFlg 戻り値　0:正常　1:異常
    '      csvChk 戻り値  ヘッダ情報チェック結果をセット NG:False OK:True
    '      kindCsv        CSVファイルの種類を指定  
    '返却：配列にセットしたCSVデータ
    '****************************************************
    Public Function getCsvData(ByVal filePass As String, ByRef colLen As Integer, ByRef errFlg As Integer, ByRef csvChk As String, ByVal kindCsv As String) As String()()

        Try
            'StreamReader の新しいインスタンスを生成する
            Dim cReader As New System.IO.StreamReader(filePass, System.Text.Encoding.Default)
            Dim textLines As New List(Of String())
            Dim strArr()() As String
            Dim rowCnt As Integer

            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String

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

                textLines.Add(cols)

                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく
                If rowCnt = 1 Then
                    colsHead = Split(stBuffer, ",")
                End If

            End While

            colLen = cols.Length

            cReader.Close()

            'ヘッダ情報確認
            If kindCsv = "GSPNData" Then
                If colLen = GSPNCol Then
                    If chkHead(colsHead, "GSPN") = False Then
                        csvChk = "False"
                    End If
                Else
                    csvChk = "False"
                End If
            ElseIf kindCsv = "inputData" Then
                If colLen = InputCol Then
                    If chkHead(colsHead, "inputData") = False Then
                        csvChk = "False"
                    End If
                Else
                    csvChk = "False"
                End If
            ElseIf kindCsv = "otherData" Then
                If colLen = otherCol Then
                    If chkHead(colsHead, "otherData") = False Then
                        csvChk = "False"
                    End If
                Else
                    csvChk = "False"
                End If
            End If

            'CSVデータセット
            strArr = textLines.ToArray

            Return strArr

        Catch ex As Exception
            Return Nothing
            errFlg = 1
        End Try

    End Function
    '****************************************************
    '処理：GSPNデータの登録
    '引数：csvData CSVデータ
    '　　　errFlg      戻り値　0:正常　1:異常
    '      addData     入力画面からの情報 
    '      addDataPdf  出力pdfの項目
    '      userid      ログインユーザID
    '      shipCode    ログインユーザ拠点コード
    '      poM         poの種類名 
    '      poNoMax     poNoの最終番号　
    '      kindCsv     CSVデータの種類
    '      userLevel   管理者権限　1：管理者
    '      tourokuFlg  登録確認フラグ　-1:変更なし　1:変更あり 
    '****************************************************
    Public Sub setCsvData(ByVal csvData()() As String, ByRef errFlg As Integer, ByRef addData As ADD_DATA, ByRef addDataPdf() As ADD_DATA, ByVal userid As String, ByVal shipCode As String, ByVal poM As String, ByVal poNoMax As Long, ByVal kindCsv As String, ByVal userLevel As String, ByRef tourokuFlg As Integer)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Dim i As Long
        Dim newData As Integer = -1 '新規追加登録確認用 
        Dim add As Integer = -1　　 '追加登録確認用　　
        Dim newDataCnt As Long
        Dim dt As DateTime
        Dim dtNow As DateTime = DateTime.Now
        Dim chkCompletedDate As DateTime

        Try
            'GSPNのデータ登録
            If csvData IsNot Nothing And kindCsv = "GSPNData" Then
                For i = 0 To csvData.Length - 1
                    newData = -1
                    add = -1
                    '①番目SQL：インポート済のデータか確認
                    Dim select_sql1 As String = ""
                    select_sql1 &= "SELECT * FROM dbo.T_repair1 WHERE ASC_Claim_No = '" & csvData(i)(2) & "' AND Samsung_Claim_No = '" & csvData(i)(3) & "' AND DELFG = 0 and Branch_Code = '" & shipCode & "';"
                    Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
                    Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
                    Dim Builder1 As New SqlCommandBuilder(Adapter1)
                    Dim ds1 As New DataSet
                    Dim dr1 As DataRow
                    Adapter1.Fill(ds1)
                    If i <> 0 Then
                        If ds1.Tables(0).Rows.Count = 1 Then
                            'インポート済の為上書き登録
                            dr1 = ds1.Tables(0).Rows(0)
                        Else
                            '②番目のSQL：入力画面から登録済のデータか確認
                            Dim select_sql0 As String = ""
                            select_sql0 &= "SELECT * FROM dbo.T_repair1 WHERE asc_c_num = '" & csvData(i)(2) & "' AND sam_c_num = '" & csvData(i)(3) & "' AND DELFG = 0 and Branch_Code = '" & shipCode & "';"
                            Dim sqlSelect0 As New SqlCommand(select_sql0, con, trn)
                            Dim Adapter0 As New SqlDataAdapter(sqlSelect0)
                            Dim Builder0 As New SqlCommandBuilder(Adapter0)
                            Dim ds0 As New DataSet
                            Adapter0.Fill(ds0)
                            If ds0.Tables(0).Rows.Count = 1 Then
                                '入力画面から登録済の為追加登録
                                dr1 = ds0.Tables(0).Rows(0)
                                add = 1
                                tourokuFlg = 1
                                dr1("CRTDT") = DateTime.Now
                                dr1("CRTCD") = userid
                                dr1("UPDDT") = DateTime.Now
                                dr1("UPDCD") = userid
                                dr1("UPDPG") = "systemName"
                                dr1("DELFG") = 0
                                dr1("ASC_Code") = csvData(i)(0)
                                dr1("Branch_Code") = csvData(i)(1)
                                dr1("ASC_Claim_No") = csvData(i)(2)
                                dr1("Samsung_Claim_No") = csvData(i)(3)
                                dr1("Service_Type") = csvData(i)(4)
                                dr1("Consumer_Name") = csvData(i)(5)
                                dr1("Consumer_Addr1") = csvData(i)(6)
                                dr1("Consumer_Addr2") = csvData(i)(7)
                                dr1("Consumer_Telephone") = csvData(i)(8)
                                dr1("Consumer_Fax") = csvData(i)(9)
                                dr1("Postal_Code") = csvData(i)(10)
                                dr1("Model") = csvData(i)(11)
                                dr1("Serial_No") = csvData(i)(12)
                                dr1("IMEI_No") = csvData(i)(13)
                                dr1("Defect_Type") = csvData(i)(14)
                                dr1("Condition") = csvData(i)(15)
                                dr1("Symptom") = csvData(i)(16)
                                dr1("Defect_Code") = csvData(i)(17)
                                dr1("Repair_Code") = csvData(i)(18)
                                dr1("Defect_Desc") = csvData(i)(19)
                                dr1("Repair_Description") = csvData(i)(20)

                                If DateTime.TryParse(csvData(i)(21), dt) Then
                                    dr1("Purchase_Date") = csvData(i)(21)
                                Else
                                    If csvData(i)(21) = "" Then
                                        dr1("Purchase_Date") = DBNull.Value
                                    ElseIf csvData(i)(21) = 0 Then
                                        dr1("Purchase_Date") = DBNull.Value
                                    Else
                                        dr1("Purchase_Date") = DateTime.ParseExact(csvData(i)(21) & "000000", "yyyyMMddHHmmss", Nothing)
                                    End If
                                End If

                                If DateTime.TryParse(csvData(i)(22), dt) Then
                                    dr1("Repair_Received_Date") = csvData(i)(22)
                                Else
                                    If csvData(i)(22) = "" Then
                                        dr1("Repair_Received_Date") = DBNull.Value
                                    ElseIf csvData(i)(22) = 0 Then
                                        dr1("Repair_Received_Date") = DBNull.Value
                                    Else
                                        dr1("Repair_Received_Date") = DateTime.ParseExact(csvData(i)(22) & "000000", "yyyyMMddHHmmss", Nothing)
                                    End If
                                End If

                                If DateTime.TryParse(csvData(i)(23), dt) Then
                                    dr1("Completed_Date") = csvData(i)(23)
                                Else
                                    If csvData(i)(23) = "" Then
                                        dr1("Completed_Date") = DBNull.Value
                                    ElseIf csvData(i)(23) = 0 Then
                                        dr1("Completed_Date") = DBNull.Value
                                    Else
                                        dr1("Completed_Date") = DateTime.ParseExact(csvData(i)(23) & "000000", "yyyyMMddHHmmss", Nothing)
                                    End If
                                End If

                                If DateTime.TryParse(csvData(i)(24), dt) Then
                                    dr1("Delivery_Date") = csvData(i)(24)
                                Else
                                    If csvData(i)(24) = "" Then
                                        dr1("Delivery_Date") = DBNull.Value
                                    ElseIf csvData(i)(24) = 0 Then
                                        dr1("Delivery_Date") = DBNull.Value
                                    Else
                                        dr1("Delivery_Date") = DateTime.ParseExact(csvData(i)(24) & "000000", "yyyyMMddHHmmss", Nothing)
                                    End If
                                End If

                                If DateTime.TryParse(csvData(i)(25), dt) Then
                                    dr1("Production_Date") = csvData(i)(25)
                                Else
                                    If csvData(i)(25) = "" Then
                                        dr1("Production_Date") = DBNull.Value
                                    ElseIf csvData(i)(25) = 0 Then
                                        dr1("Production_Date") = DBNull.Value
                                    Else
                                        dr1("Production_Date") = DateTime.ParseExact(csvData(i)(25) & "000000", "yyyyMMddHHmmss", Nothing)
                                    End If
                                End If

                                If csvData(i)(26) = "" Then
                                    dr1("Labor_Amount") = 0
                                Else
                                    dr1("Labor_Amount") = csvData(i)(26)
                                End If
                                If csvData(i)(27) = "" Then
                                    dr1("Parts_Amount") = 0
                                Else
                                    dr1("Parts_Amount") = csvData(i)(27)
                                End If
                                dr1("Freight") = csvData(i)(28)
                                dr1("Other") = csvData(i)(29)
                                If csvData(i)(30) = "" Then
                                    dr1("Parts_SGST") = 0
                                Else
                                    dr1("Parts_SGST") = csvData(i)(30)
                                End If
                                dr1("Parts_UTGST") = csvData(i)(31)
                                If csvData(i)(32) = "" Then
                                    dr1("Parts_CGST") = 0
                                Else
                                    dr1("Parts_CGST") = csvData(i)(32)
                                End If
                                If csvData(i)(33) = "" Then
                                    dr1("Parts_IGST") = 0
                                Else
                                    dr1("Parts_IGST") = csvData(i)(33)
                                End If
                                dr1("Parts_Cess") = csvData(i)(34)
                                If csvData(i)(35) = "" Then
                                    dr1("SGST") = 0
                                Else
                                    dr1("SGST") = csvData(i)(35)
                                End If
                                dr1("UTGST") = csvData(i)(36)
                                If csvData(i)(37) = "" Then
                                    dr1("CGST") = 0
                                Else
                                    dr1("CGST") = csvData(i)(37)
                                End If
                                If csvData(i)(38) = "" Then
                                    dr1("IGST") = 0
                                Else
                                    dr1("IGST") = csvData(i)(38)
                                End If
                                dr1("Cess") = csvData(i)(39)
                                If csvData(i)(40) = "" Then
                                    dr1("Total_Invoice_Amount") = 0
                                Else
                                    dr1("Total_Invoice_Amount") = csvData(i)(40)
                                End If
                                dr1("Remark") = csvData(i)(41)
                                dr1("Tr_No") = csvData(i)(42)
                                dr1("Tr_Type") = csvData(i)(43)
                                dr1("Status") = csvData(i)(44)
                                dr1("Engineer") = csvData(i)(45)
                                dr1("Collection_Point") = csvData(i)(46)
                                dr1("Collection_Point_Name") = csvData(i)(47)
                                dr1("Location_1") = csvData(i)(48)
                                dr1("Part_1") = csvData(i)(49)
                                dr1("Qty_1") = csvData(i)(50)
                                If csvData(i)(51) = "" Then
                                    dr1("Unit_Price_1") = 0
                                Else
                                    dr1("Unit_Price_1") = csvData(i)(51)
                                End If
                                dr1("Doc_Num_1") = csvData(i)(52)
                                dr1("Matrial_Serial_1") = csvData(i)(53)
                                dr1("Location_2") = csvData(i)(54)
                                dr1("Part_2") = csvData(i)(55)
                                dr1("Qty_2") = csvData(i)(56)
                                If csvData(i)(57) = "" Then
                                    dr1("Unit_Price_2") = 0
                                Else
                                    dr1("Unit_Price_2") = csvData(i)(57)
                                End If
                                dr1("Doc_Num_2") = csvData(i)(58)
                                dr1("Matrial_Serial_2") = csvData(i)(59)
                                dr1("Location_3") = csvData(i)(60)
                                dr1("Part_3") = csvData(i)(61)
                                dr1("Qty_3") = csvData(i)(62)
                                If csvData(i)(63) = "" Then
                                    dr1("Unit_Price_3") = 0
                                Else
                                    dr1("Unit_Price_3") = csvData(i)(63)
                                End If
                                dr1("Doc_Num_3") = csvData(i)(64)
                                dr1("Matrial_Serial_3") = csvData(i)(65)
                                dr1("Location_4") = csvData(i)(66)
                                dr1("Part_4") = csvData(i)(67)
                                dr1("Qty_4") = csvData(i)(68)
                                If csvData(i)(69) = "" Then
                                    dr1("Unit_Price_4") = 0
                                Else
                                    dr1("Unit_Price_4") = csvData(i)(69)
                                End If
                                dr1("Doc_Num_4") = csvData(i)(70)
                                dr1("Matrial_Serial_4") = csvData(i)(71)
                                dr1("Location_5") = csvData(i)(72)
                                dr1("Part_5") = csvData(i)(73)
                                dr1("Qty_5") = csvData(i)(74)
                                If csvData(i)(75) = "" Then
                                    dr1("Unit_Price_5") = 0
                                Else
                                    dr1("Unit_Price_5") = csvData(i)(75)
                                End If
                                dr1("Doc_Num_5") = csvData(i)(76)
                                dr1("Matrial_Serial_5") = csvData(i)(77)
                                dr1("Location_6") = csvData(i)(78)
                                dr1("Part_6") = csvData(i)(79)
                                dr1("Qty_6") = csvData(i)(80)
                                If csvData(i)(81) = "" Then
                                    dr1("Unit_Price_6") = 0
                                Else
                                    dr1("Unit_Price_6") = csvData(i)(81)
                                End If
                                dr1("Doc_Num_6") = csvData(i)(82)
                                dr1("Matrial_Serial_6") = csvData(i)(83)
                                dr1("Location_7") = csvData(i)(84)
                                dr1("Part_7") = csvData(i)(85)
                                dr1("Qty_7") = csvData(i)(86)
                                If csvData(i)(87) = "" Then
                                    dr1("Unit_Price_7") = 0
                                Else
                                    dr1("Unit_Price_7") = csvData(i)(87)
                                End If
                                dr1("Doc_Num_7") = csvData(i)(88)
                                dr1("Matrial_Serial_7") = csvData(i)(89)
                                dr1("Location_8") = csvData(i)(90)
                                dr1("Part_8") = csvData(i)(91)
                                dr1("Qty_8") = csvData(i)(92)
                                If csvData(i)(93) = "" Then
                                    dr1("Unit_Price_8") = 0
                                Else
                                    dr1("Unit_Price_8") = csvData(i)(93)
                                End If
                                dr1("Doc_Num_8") = csvData(i)(94)
                                dr1("Matrial_Serial_8") = csvData(i)(95)
                                dr1("Location_9") = csvData(i)(96)
                                dr1("Part_9") = csvData(i)(97)
                                dr1("Qty_9") = csvData(i)(98)
                                If csvData(i)(99) = "" Then
                                    dr1("Unit_Price_9") = 0
                                Else
                                    dr1("Unit_Price_9") = csvData(i)(99)
                                End If
                                dr1("Doc_Num_9") = csvData(i)(100)
                                dr1("Matrial_Serial_9") = csvData(i)(101)
                                dr1("Location_10") = csvData(i)(102)
                                dr1("Part_10") = csvData(i)(103)
                                dr1("Qty_10") = csvData(i)(104)
                                If csvData(i)(105) = "" Then
                                    dr1("Unit_Price_10") = 0
                                Else
                                    dr1("Unit_Price_10") = csvData(i)(105)
                                End If
                                dr1("Doc_Num_10") = csvData(i)(106)
                                dr1("Matrial_Serial_10") = csvData(i)(107)
                                dr1("Location_11") = csvData(i)(108)
                                dr1("Part_11") = csvData(i)(109)
                                dr1("Qty_11") = csvData(i)(110)
                                If csvData(i)(111) = "" Then
                                    dr1("Unit_Price_11") = 0
                                Else
                                    dr1("Unit_Price_11") = csvData(i)(111)
                                End If
                                dr1("Doc_Num_11") = csvData(i)(112)
                                dr1("Matrial_Serial_11") = csvData(i)(113)
                                dr1("Location_12") = csvData(i)(114)
                                dr1("Part_12") = csvData(i)(115)
                                dr1("Qty_12") = csvData(i)(116)
                                If csvData(i)(117) = "" Then
                                    dr1("Unit_Price_12") = 0
                                Else
                                    dr1("Unit_Price_12") = csvData(i)(117)
                                End If
                                dr1("Doc_Num_12") = csvData(i)(118)
                                dr1("Matrial_Serial_12") = csvData(i)(119)
                                dr1("Location_13") = csvData(i)(120)
                                dr1("Part_13") = csvData(i)(121)
                                dr1("Qty_13") = csvData(i)(122)
                                If csvData(i)(123) = "" Then
                                    dr1("Unit_Price_13") = 0
                                Else
                                    dr1("Unit_Price_13") = csvData(i)(123)
                                End If
                                dr1("Doc_Num_13") = csvData(i)(124)
                                dr1("Matrial_Serial_13") = csvData(i)(125)
                                dr1("Location_14") = csvData(i)(126)
                                dr1("Part_14") = csvData(i)(127)
                                dr1("Qty_14") = csvData(i)(128)
                                If csvData(i)(129) = "" Then
                                    dr1("Unit_Price_14") = 0
                                Else
                                    dr1("Unit_Price_14") = csvData(i)(129)
                                End If
                                dr1("Doc_Num_14") = csvData(i)(130)
                                dr1("Matrial_Serial_14") = csvData(i)(131)
                                dr1("Location_15") = csvData(i)(132)
                                dr1("Part_15") = csvData(i)(133)
                                dr1("Qty_15") = csvData(i)(134)
                                If csvData(i)(135) = "" Then
                                    dr1("Unit_Price_15") = 0
                                Else
                                    dr1("Unit_Price_15") = csvData(i)(135)
                                End If
                                dr1("Doc_Num_15") = csvData(i)(136)
                                dr1("Matrial_Serial_15") = csvData(i)(137)
                                Adapter0.Update(ds0)
                            Else
                                '新規追加登録
                                dr1 = ds1.Tables(0).NewRow
                                newData = 1
                            End If
                        End If

                        '追加登録以外（新規登録 or 上書き登録）
                        '※管理者以外は、完了日に記載があるデータは変更登録不可
                        '※管理者であっても、完了日が当月以外は変更登録不可
                        'Left(chkCompletedDate.ToShortDateString, 7) <> Left(dtnow.ToShortDateString, 7)
                        If dr1("Completed_Date") IsNot DBNull.Value Then
                            chkCompletedDate = dr1("Completed_Date")
                        End If
                        If add = -1 Then
                            If (dr1("Completed_Date") Is DBNull.Value) Or (dr1("Completed_Date") IsNot DBNull.Value And userLevel = "1" And (Left(chkCompletedDate.ToShortDateString, 7) = Left(dtNow.ToShortDateString, 7))) Then
                                tourokuFlg = 1
                                dr1("CRTDT") = DateTime.Now
                                dr1("CRTCD") = userid
                                dr1("UPDDT") = DateTime.Now
                                dr1("UPDCD") = userid
                                dr1("UPDPG") = "systemName"
                                dr1("DELFG") = 0
                                dr1("ASC_Code") = csvData(i)(0)
                                dr1("Branch_Code") = csvData(i)(1)
                                dr1("ASC_Claim_No") = csvData(i)(2)
                                dr1("Samsung_Claim_No") = csvData(i)(3)
                                If newData = 1 Then
                                    dr1("po_no") = poM & (poNoMax + newDataCnt).ToString("00000000")
                                    newDataCnt = newDataCnt + 1
                                End If
                                'dr1("comment") = ""
                                dr1("Service_Type") = csvData(i)(4)
                                dr1("Consumer_Name") = csvData(i)(5)
                                dr1("Consumer_Addr1") = csvData(i)(6)
                                dr1("Consumer_Addr2") = csvData(i)(7)
                                dr1("Consumer_Telephone") = csvData(i)(8)
                                dr1("Consumer_Fax") = csvData(i)(9)
                                dr1("Postal_Code") = csvData(i)(10)
                                dr1("Model") = csvData(i)(11)
                                dr1("Serial_No") = csvData(i)(12)
                                dr1("IMEI_No") = csvData(i)(13)
                                dr1("Defect_Type") = csvData(i)(14)
                                dr1("Condition") = csvData(i)(15)
                                dr1("Symptom") = csvData(i)(16)
                                dr1("Defect_Code") = csvData(i)(17)
                                dr1("Repair_Code") = csvData(i)(18)
                                dr1("Defect_Desc") = csvData(i)(19)
                                dr1("Repair_Description") = csvData(i)(20)

                                If DateTime.TryParse(csvData(i)(21), dt) Then
                                    dr1("Purchase_Date") = csvData(i)(21)
                                Else
                                    If csvData(i)(21) = "" Then
                                        dr1("Purchase_Date") = DBNull.Value
                                    ElseIf csvData(i)(21) = 0 Then
                                        dr1("Purchase_Date") = DBNull.Value
                                    Else
                                        dr1("Purchase_Date") = DateTime.ParseExact(csvData(i)(21) & "000000", "yyyyMMddHHmmss", Nothing)
                                    End If
                                End If

                                If DateTime.TryParse(csvData(i)(22), dt) Then
                                    dr1("Repair_Received_Date") = csvData(i)(22)
                                Else
                                    If csvData(i)(22) = "" Then
                                        dr1("Repair_Received_Date") = DBNull.Value
                                    ElseIf csvData(i)(22) = 0 Then
                                        dr1("Repair_Received_Date") = DBNull.Value
                                    Else
                                        dr1("Repair_Received_Date") = DateTime.ParseExact(csvData(i)(22) & "000000", "yyyyMMddHHmmss", Nothing)
                                    End If
                                End If

                                If DateTime.TryParse(csvData(i)(23), dt) Then
                                    dr1("Completed_Date") = csvData(i)(23)
                                Else
                                    If csvData(i)(23) = "" Then
                                        dr1("Completed_Date") = DBNull.Value
                                    ElseIf csvData(i)(23) = 0 Then
                                        dr1("Completed_Date") = DBNull.Value
                                    Else
                                        dr1("Completed_Date") = DateTime.ParseExact(csvData(i)(23) & "000000", "yyyyMMddHHmmss", Nothing)
                                    End If
                                End If

                                If DateTime.TryParse(csvData(i)(24), dt) Then
                                    dr1("Delivery_Date") = csvData(i)(24)
                                Else
                                    If csvData(i)(24) = "" Then
                                        dr1("Delivery_Date") = DBNull.Value
                                    ElseIf csvData(i)(24) = 0 Then
                                        dr1("Delivery_Date") = DBNull.Value
                                    Else
                                        dr1("Delivery_Date") = DateTime.ParseExact(csvData(i)(24) & "000000", "yyyyMMddHHmmss", Nothing)
                                    End If
                                End If

                                If DateTime.TryParse(csvData(i)(25), dt) Then
                                    dr1("Production_Date") = csvData(i)(25)
                                Else
                                    If csvData(i)(25) = "" Then
                                        dr1("Production_Date") = DBNull.Value
                                    ElseIf csvData(i)(25) = 0 Then
                                        dr1("Production_Date") = DBNull.Value
                                    Else
                                        dr1("Production_Date") = DateTime.ParseExact(csvData(i)(25) & "000000", "yyyyMMddHHmmss", Nothing)
                                    End If
                                End If

                                If csvData(i)(26) = "" Then
                                    dr1("Labor_Amount") = 0
                                Else
                                    dr1("Labor_Amount") = csvData(i)(26)
                                End If
                                If csvData(i)(27) = "" Then
                                    dr1("Parts_Amount") = 0
                                Else
                                    dr1("Parts_Amount") = csvData(i)(27)
                                End If
                                dr1("Freight") = csvData(i)(28)
                                dr1("Other") = csvData(i)(29)
                                If csvData(i)(30) = "" Then
                                    dr1("Parts_SGST") = 0
                                Else
                                    dr1("Parts_SGST") = csvData(i)(30)
                                End If
                                dr1("Parts_UTGST") = csvData(i)(31)
                                If csvData(i)(32) = "" Then
                                    dr1("Parts_CGST") = 0
                                Else
                                    dr1("Parts_CGST") = csvData(i)(32)
                                End If
                                If csvData(i)(33) = "" Then
                                    dr1("Parts_IGST") = 0
                                Else
                                    dr1("Parts_IGST") = csvData(i)(33)
                                End If
                                dr1("Parts_Cess") = csvData(i)(34)
                                If csvData(i)(35) = "" Then
                                    dr1("SGST") = 0
                                Else
                                    dr1("SGST") = csvData(i)(35)
                                End If
                                dr1("UTGST") = csvData(i)(36)
                                If csvData(i)(37) = "" Then
                                    dr1("CGST") = 0
                                Else
                                    dr1("CGST") = csvData(i)(37)
                                End If
                                If csvData(i)(38) = "" Then
                                    dr1("IGST") = 0
                                Else
                                    dr1("IGST") = csvData(i)(38)
                                End If
                                dr1("Cess") = csvData(i)(39)
                                If csvData(i)(40) = "" Then
                                    dr1("Total_Invoice_Amount") = 0
                                Else
                                    dr1("Total_Invoice_Amount") = csvData(i)(40)
                                End If
                                dr1("Remark") = csvData(i)(41)
                                dr1("Tr_No") = csvData(i)(42)
                                dr1("Tr_Type") = csvData(i)(43)
                                dr1("Status") = csvData(i)(44)
                                dr1("Engineer") = csvData(i)(45)
                                dr1("Collection_Point") = csvData(i)(46)
                                dr1("Collection_Point_Name") = csvData(i)(47)
                                dr1("Location_1") = csvData(i)(48)
                                dr1("Part_1") = csvData(i)(49)
                                dr1("Qty_1") = csvData(i)(50)
                                If csvData(i)(51) = "" Then
                                    dr1("Unit_Price_1") = 0
                                Else
                                    dr1("Unit_Price_1") = csvData(i)(51)
                                End If
                                dr1("Doc_Num_1") = csvData(i)(52)
                                dr1("Matrial_Serial_1") = csvData(i)(53)
                                dr1("Location_2") = csvData(i)(54)
                                dr1("Part_2") = csvData(i)(55)
                                dr1("Qty_2") = csvData(i)(56)
                                If csvData(i)(57) = "" Then
                                    dr1("Unit_Price_2") = 0
                                Else
                                    dr1("Unit_Price_2") = csvData(i)(57)
                                End If
                                dr1("Doc_Num_2") = csvData(i)(58)
                                dr1("Matrial_Serial_2") = csvData(i)(59)
                                dr1("Location_3") = csvData(i)(60)
                                dr1("Part_3") = csvData(i)(61)
                                dr1("Qty_3") = csvData(i)(62)
                                If csvData(i)(63) = "" Then
                                    dr1("Unit_Price_3") = 0
                                Else
                                    dr1("Unit_Price_3") = csvData(i)(63)
                                End If
                                dr1("Doc_Num_3") = csvData(i)(64)
                                dr1("Matrial_Serial_3") = csvData(i)(65)
                                dr1("Location_4") = csvData(i)(66)
                                dr1("Part_4") = csvData(i)(67)
                                dr1("Qty_4") = csvData(i)(68)
                                If csvData(i)(69) = "" Then
                                    dr1("Unit_Price_4") = 0
                                Else
                                    dr1("Unit_Price_4") = csvData(i)(69)
                                End If
                                dr1("Doc_Num_4") = csvData(i)(70)
                                dr1("Matrial_Serial_4") = csvData(i)(71)
                                dr1("Location_5") = csvData(i)(72)
                                dr1("Part_5") = csvData(i)(73)
                                dr1("Qty_5") = csvData(i)(74)
                                If csvData(i)(75) = "" Then
                                    dr1("Unit_Price_5") = 0
                                Else
                                    dr1("Unit_Price_5") = csvData(i)(75)
                                End If
                                dr1("Doc_Num_5") = csvData(i)(76)
                                dr1("Matrial_Serial_5") = csvData(i)(77)
                                dr1("Location_6") = csvData(i)(78)
                                dr1("Part_6") = csvData(i)(79)
                                dr1("Qty_6") = csvData(i)(80)
                                If csvData(i)(81) = "" Then
                                    dr1("Unit_Price_6") = 0
                                Else
                                    dr1("Unit_Price_6") = csvData(i)(81)
                                End If
                                dr1("Doc_Num_6") = csvData(i)(82)
                                dr1("Matrial_Serial_6") = csvData(i)(83)
                                dr1("Location_7") = csvData(i)(84)
                                dr1("Part_7") = csvData(i)(85)
                                dr1("Qty_7") = csvData(i)(86)
                                If csvData(i)(87) = "" Then
                                    dr1("Unit_Price_7") = 0
                                Else
                                    dr1("Unit_Price_7") = csvData(i)(87)
                                End If
                                dr1("Doc_Num_7") = csvData(i)(88)
                                dr1("Matrial_Serial_7") = csvData(i)(89)
                                dr1("Location_8") = csvData(i)(90)
                                dr1("Part_8") = csvData(i)(91)
                                dr1("Qty_8") = csvData(i)(92)
                                If csvData(i)(93) = "" Then
                                    dr1("Unit_Price_8") = 0
                                Else
                                    dr1("Unit_Price_8") = csvData(i)(93)
                                End If
                                dr1("Doc_Num_8") = csvData(i)(94)
                                dr1("Matrial_Serial_8") = csvData(i)(95)
                                dr1("Location_9") = csvData(i)(96)
                                dr1("Part_9") = csvData(i)(97)
                                dr1("Qty_9") = csvData(i)(98)
                                If csvData(i)(99) = "" Then
                                    dr1("Unit_Price_9") = 0
                                Else
                                    dr1("Unit_Price_9") = csvData(i)(99)
                                End If
                                dr1("Doc_Num_9") = csvData(i)(100)
                                dr1("Matrial_Serial_9") = csvData(i)(101)
                                dr1("Location_10") = csvData(i)(102)
                                dr1("Part_10") = csvData(i)(103)
                                dr1("Qty_10") = csvData(i)(104)
                                If csvData(i)(105) = "" Then
                                    dr1("Unit_Price_10") = 0
                                Else
                                    dr1("Unit_Price_10") = csvData(i)(105)
                                End If
                                dr1("Doc_Num_10") = csvData(i)(106)
                                dr1("Matrial_Serial_10") = csvData(i)(107)
                                dr1("Location_11") = csvData(i)(108)
                                dr1("Part_11") = csvData(i)(109)
                                dr1("Qty_11") = csvData(i)(110)
                                If csvData(i)(111) = "" Then
                                    dr1("Unit_Price_11") = 0
                                Else
                                    dr1("Unit_Price_11") = csvData(i)(111)
                                End If
                                dr1("Doc_Num_11") = csvData(i)(112)
                                dr1("Matrial_Serial_11") = csvData(i)(113)
                                dr1("Location_12") = csvData(i)(114)
                                dr1("Part_12") = csvData(i)(115)
                                dr1("Qty_12") = csvData(i)(116)
                                If csvData(i)(117) = "" Then
                                    dr1("Unit_Price_12") = 0
                                Else
                                    dr1("Unit_Price_12") = csvData(i)(117)
                                End If
                                dr1("Doc_Num_12") = csvData(i)(118)
                                dr1("Matrial_Serial_12") = csvData(i)(119)
                                dr1("Location_13") = csvData(i)(120)
                                dr1("Part_13") = csvData(i)(121)
                                dr1("Qty_13") = csvData(i)(122)
                                If csvData(i)(123) = "" Then
                                    dr1("Unit_Price_13") = 0
                                Else
                                    dr1("Unit_Price_13") = csvData(i)(123)
                                End If
                                dr1("Doc_Num_13") = csvData(i)(124)
                                dr1("Matrial_Serial_13") = csvData(i)(125)
                                dr1("Location_14") = csvData(i)(126)
                                dr1("Part_14") = csvData(i)(127)
                                dr1("Qty_14") = csvData(i)(128)
                                If csvData(i)(129) = "" Then
                                    dr1("Unit_Price_14") = 0
                                Else
                                    dr1("Unit_Price_14") = csvData(i)(129)
                                End If
                                dr1("Doc_Num_14") = csvData(i)(130)
                                dr1("Matrial_Serial_14") = csvData(i)(131)
                                dr1("Location_15") = csvData(i)(132)
                                dr1("Part_15") = csvData(i)(133)
                                dr1("Qty_15") = csvData(i)(134)
                                If csvData(i)(135) = "" Then
                                    dr1("Unit_Price_15") = 0
                                Else
                                    dr1("Unit_Price_15") = csvData(i)(135)
                                End If
                                dr1("Doc_Num_15") = csvData(i)(136)
                                dr1("Matrial_Serial_15") = csvData(i)(137)
                                If newData = 1 Then
                                    '新規DRをDataTableに追加
                                    ds1.Tables(0).Rows.Add(dr1)
                                End If
                                Adapter1.Update(ds1)
                            End If
                        End If
                    End If
                Next i
            Else
                '入力画面からの登録
                Dim add2 As Integer = -1
                If kindCsv = "" Then
                    Dim select_sql1 As String = ""
                    select_sql1 &= "SELECT * FROM dbo.T_repair1 WHERE ASC_Claim_No = '" & addData.asc_c_num & "' AND Samsung_Claim_No = '" & addData.sam_c_num & "' AND DELFG = 0 and Branch_Code = '" & shipCode & "';"
                    Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
                    Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
                    Dim Builder1 As New SqlCommandBuilder(Adapter1)
                    Dim ds1 As New DataSet
                    Dim dr1 As DataRow
                    Adapter1.Fill(ds1)
                    If ds1.Tables(0).Rows.Count = 1 Then
                        '上書き登録
                        dr1 = ds1.Tables(0).Rows(0)
                    Else
                        Dim select_sql2 As String = ""
                        select_sql2 &= "SELECT * FROM dbo.T_repair1 WHERE asc_c_num = '" & addData.asc_c_num & "' AND sam_c_num = '" & addData.sam_c_num & "' AND DELFG = 0 and Branch_Code = '" & shipCode & "';"
                        Dim sqlSelect2 As New SqlCommand(select_sql2, con, trn)
                        Dim Adapter2 As New SqlDataAdapter(sqlSelect2)
                        Dim Builder2 As New SqlCommandBuilder(Adapter2)
                        Dim ds2 As New DataSet
                        Adapter2.Fill(ds2)
                        If ds2.Tables(0).Rows.Count = 1 Then
                            '上書き登録(入力用データのみ)
                            dr1 = ds2.Tables(0).Rows(0)
                            add2 = 1
                            dr1("CRTDT") = DateTime.Now
                            dr1("CRTCD") = userid
                            dr1("UPDDT") = DateTime.Now
                            dr1("UPDCD") = userid
                            dr1("UPDPG") = "systemName"
                            dr1("DELFG") = 0
                            dr1("Branch_Code") = shipCode

                            If addData.rec_datetime = "" Then
                                dr1("rec_datetime") = DBNull.Value
                            Else
                                dr1("rec_datetime") = DateTime.Parse(addData.rec_datetime)
                            End If

                            dr1("rec_yuser") = addData.rec_yuser
                            dr1("rpt_counter") = addData.rpt_counter
                            dr1("rpt_repair") = addData.rpt_repair

                            If addData.close_datetime = "" Then
                                dr1("close_datetime") = DBNull.Value
                            Else
                                dr1("close_datetime") = DateTime.Parse(addData.close_datetime)
                            End If

                            dr1("denomi") = addData.denomi
                            dr1("amount") = addData.amount
                            dr1("asc_c_num") = addData.asc_c_num
                            dr1("sam_c_num") = addData.sam_c_num
                            dr1("comment") = addData.comment
                            'po_noを取得　※後でPDF出力用
                            If dr1("po_no") IsNot DBNull.Value Then
                                addData.po_no = dr1("po_no")
                            End If
                            Adapter2.Update(ds2)
                        Else
                            '新規追加登録
                            dr1 = ds1.Tables(0).NewRow
                            newData = 1
                        End If
                    End If
                    '新規追加登録 or 上書き登録（※asc_c_numに値がなけれは、追加登録
                    If dr1("Completed_Date") IsNot DBNull.Value Then
                        chkCompletedDate = dr1("Completed_Date")
                    End If
                    If add2 = -1 Then
                        If (dr1("Completed_Date") Is DBNull.Value) Then
                            dr1("CRTDT") = DateTime.Now
                            dr1("CRTCD") = userid
                            dr1("UPDDT") = DateTime.Now
                            dr1("UPDCD") = userid
                            dr1("UPDPG") = "systemName"
                            dr1("DELFG") = 0
                            If newData = 1 Then
                                dr1("po_no") = poM & (poNoMax + newDataCnt).ToString("00000000")
                                newDataCnt = newDataCnt + 1
                            End If
                            dr1("Branch_Code") = shipCode

                            If addData.rec_datetime = "" Then
                                dr1("rec_datetime") = DBNull.Value
                            Else
                                dr1("rec_datetime") = DateTime.Parse(addData.rec_datetime)
                            End If

                            dr1("rec_yuser") = addData.rec_yuser
                            dr1("rpt_counter") = addData.rpt_counter
                            dr1("rpt_repair") = addData.rpt_repair

                            If addData.close_datetime = "" Then
                                dr1("close_datetime") = DBNull.Value
                            Else
                                dr1("close_datetime") = DateTime.Parse(addData.close_datetime)
                            End If

                            dr1("denomi") = addData.denomi
                            dr1("amount") = addData.amount
                            dr1("asc_c_num") = addData.asc_c_num
                            dr1("sam_c_num") = addData.sam_c_num
                            dr1("comment") = addData.comment
                            If newData = 1 Then
                                '新規DRをDataTableに追加 
                                ds1.Tables(0).Rows.Add(dr1)
                            End If
                            'po_noを取得　※後でPDF出力用
                            If dr1("po_no") IsNot DBNull.Value Then
                                addData.po_no = dr1("po_no")
                            End If
                            Adapter1.Update(ds1)
                        Else
                            '完了日の記載があっても、入力情報が未登録の場合は登録OK
                            '※入力情報が登録済であっても、管理者は変更登録OK
                            '※管理者であっても、完了日が当月以外は変更登録不可
                            If (dr1("asc_c_num") Is DBNull.Value) Or (dr1("asc_c_num") IsNot DBNull.Value And userLevel = "1" And (Left(chkCompletedDate.ToShortDateString, 7) = Left(dtNow.ToShortDateString, 7))) Then
                                dr1("CRTDT") = DateTime.Now
                                dr1("CRTCD") = userid
                                dr1("UPDDT") = DateTime.Now
                                dr1("UPDCD") = userid
                                dr1("UPDPG") = "systemName"
                                dr1("DELFG") = 0
                                If newData = 1 Then
                                    dr1("po_no") = poM & (poNoMax + newDataCnt).ToString("00000000")
                                    newDataCnt = newDataCnt + 1
                                End If
                                dr1("Branch_Code") = shipCode

                                If addData.rec_datetime = "" Then
                                    dr1("rec_datetime") = DBNull.Value
                                Else
                                    dr1("rec_datetime") = DateTime.Parse(addData.rec_datetime)
                                End If

                                dr1("rec_yuser") = addData.rec_yuser
                                dr1("rpt_counter") = addData.rpt_counter
                                dr1("rpt_repair") = addData.rpt_repair

                                If addData.close_datetime = "" Then
                                    dr1("close_datetime") = DBNull.Value
                                Else
                                    dr1("close_datetime") = DateTime.Parse(addData.close_datetime)
                                End If

                                dr1("denomi") = addData.denomi
                                dr1("amount") = addData.amount
                                dr1("asc_c_num") = addData.asc_c_num
                                dr1("sam_c_num") = addData.sam_c_num
                                dr1("comment") = addData.comment
                                If newData = 1 Then
                                    '新規DRをDataTableに追加 
                                    ds1.Tables(0).Rows.Add(dr1)
                                End If
                                'po_noを取得　※後でPDF出力用
                                If dr1("po_no") IsNot DBNull.Value Then
                                    addData.po_no = dr1("po_no")
                                End If
                                Adapter1.Update(ds1)
                            End If
                        End If
                    End If
                Else
                    '追加データをCSVより登録
                    If kindCsv = "INPUTData" Then
                        For i = 0 To csvData.Length - 1
                            If i <> 0 Then
                                newData = -1
                                add2 = -1
                                Dim select_sql1 As String = ""
                                select_sql1 &= "SELECT * FROM dbo.T_repair1 WHERE ASC_Claim_No = '" & csvData(i)(7) & "' AND Samsung_Claim_No = '" & csvData(i)(8) & "' AND DELFG = 0 and Branch_Code = '" & shipCode & "';"
                                Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
                                Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
                                Dim Builder1 As New SqlCommandBuilder(Adapter1)
                                Dim ds1 As New DataSet
                                Dim dr1 As DataRow
                                Adapter1.Fill(ds1)
                                If ds1.Tables(0).Rows.Count = 1 Then
                                    '上書き登録
                                    dr1 = ds1.Tables(0).Rows(0)
                                Else
                                    Dim select_sql2 As String = ""
                                    select_sql2 &= "SELECT * FROM dbo.T_repair1 WHERE asc_c_num = '" & csvData(i)(7) & "' AND sam_c_num = '" & csvData(i)(8) & "' AND DELFG = 0 and Branch_Code = '" & shipCode & "';"
                                    Dim sqlSelect2 As New SqlCommand(select_sql2, con, trn)
                                    Dim Adapter2 As New SqlDataAdapter(sqlSelect2)
                                    Dim Builder2 As New SqlCommandBuilder(Adapter2)
                                    Dim ds2 As New DataSet
                                    Adapter2.Fill(ds2)
                                    If ds2.Tables(0).Rows.Count = 1 Then
                                        '上書き登録(入力用データのみ)
                                        dr1 = ds2.Tables(0).Rows(0)
                                        add2 = 1
                                        tourokuFlg = 1
                                        dr1("CRTDT") = DateTime.Now
                                        dr1("CRTCD") = userid
                                        dr1("UPDDT") = DateTime.Now
                                        dr1("UPDCD") = userid
                                        dr1("UPDPG") = "systemName"
                                        dr1("DELFG") = 0
                                        dr1("Branch_Code") = shipCode
                                        If csvData(i)(0) = "" Then
                                            dr1("rec_datetime") = DBNull.Value
                                        Else
                                            dr1("rec_datetime") = DateTime.Parse(csvData(i)(0))
                                        End If
                                        dr1("rec_yuser") = csvData(i)(1)
                                        dr1("rpt_counter") = csvData(i)(2)
                                        dr1("rpt_repair") = csvData(i)(3)
                                        If csvData(i)(4) = "" Then
                                            dr1("close_datetime") = DBNull.Value
                                        Else
                                            dr1("close_datetime") = DateTime.Parse(csvData(i)(4))
                                        End If
                                        dr1("denomi") = csvData(i)(5)
                                        If csvData(i)(6) = "" Then
                                            dr1("amount") = 0
                                        Else
                                            dr1("amount") = csvData(i)(6)
                                        End If
                                        dr1("asc_c_num") = csvData(i)(7)
                                        dr1("sam_c_num") = csvData(i)(8)
                                        dr1("comment") = csvData(i)(9)
                                        'PDF出力用にデータの構造体にセット（po_no等
                                        If dr1("po_no") IsNot DBNull.Value Then
                                            addDataPdf(i).po_no = dr1("po_no")
                                        End If
                                        addDataPdf(i).sam_c_num = csvData(i)(8)
                                        addDataPdf(i).asc_c_num = csvData(i)(7)
                                        addDataPdf(i).amount = csvData(i)(6)
                                        addDataPdf(i).rec_yuser = csvData(i)(1)
                                        Adapter2.Update(ds2)
                                    Else
                                        '新規追加登録
                                        dr1 = ds1.Tables(0).NewRow
                                        newData = 1
                                    End If
                                End If
                                If dr1("Completed_Date") IsNot DBNull.Value Then
                                    chkCompletedDate = dr1("Completed_Date")
                                End If
                                '新規追加登録 or 上書き登録（※asc_c_numに値がなけれは、追加登録）
                                If add2 = -1 Then
                                    '完了日の記載がないとき登録OK
                                    If dr1("Completed_Date") Is DBNull.Value Then
                                        tourokuFlg = 1
                                        dr1("CRTDT") = DateTime.Now
                                        dr1("CRTCD") = userid
                                        dr1("UPDDT") = DateTime.Now
                                        dr1("UPDCD") = userid
                                        dr1("UPDPG") = "systemName"
                                        dr1("DELFG") = 0
                                        If newData = 1 Then
                                            dr1("po_no") = poM & (poNoMax + newDataCnt).ToString("00000000")
                                            newDataCnt = newDataCnt + 1
                                        End If
                                        dr1("Branch_Code") = shipCode

                                        If csvData(i)(0) = "" Then
                                            dr1("rec_datetime") = DBNull.Value
                                        Else
                                            dr1("rec_datetime") = DateTime.Parse(csvData(i)(0))
                                        End If

                                        dr1("rec_yuser") = csvData(i)(1)
                                        dr1("rpt_counter") = csvData(i)(2)
                                        dr1("rpt_repair") = csvData(i)(3)
                                        If csvData(i)(4) = "" Then
                                            dr1("close_datetime") = DBNull.Value
                                        Else
                                            dr1("close_datetime") = DateTime.Parse(csvData(i)(4))
                                        End If
                                        dr1("denomi") = csvData(i)(5)
                                        If csvData(i)(6) = "" Then
                                            dr1("amount") = 0
                                        Else
                                            dr1("amount") = csvData(i)(6)
                                        End If
                                        dr1("asc_c_num") = csvData(i)(7)
                                        dr1("sam_c_num") = csvData(i)(8)
                                        dr1("comment") = csvData(i)(9)

                                        If newData = 1 Then
                                            '新規DRをDataTableに追加 
                                            ds1.Tables(0).Rows.Add(dr1)
                                        End If

                                        'PDF出力用にデータの構造体にセット（po_no等
                                        If dr1("po_no") IsNot DBNull.Value Then
                                            addDataPdf(i).po_no = dr1("po_no")
                                        End If
                                        addDataPdf(i).sam_c_num = csvData(i)(8)
                                        addDataPdf(i).asc_c_num = csvData(i)(7)
                                        addDataPdf(i).amount = csvData(i)(6)
                                        addDataPdf(i).rec_yuser = csvData(i)(1)
                                        Adapter1.Update(ds1)
                                    Else
                                        '完了日の記載があっても、入力情報が未登録の場合は登録OK
                                        '※入力情報が登録済であっても、管理者は変更登録OK
                                        '※管理者であっても、完了日が当月以外は変更登録不可
                                        If (dr1("asc_c_num") Is DBNull.Value) Or (dr1("asc_c_num") IsNot DBNull.Value And userLevel = "1" And (Left(chkCompletedDate.ToShortDateString, 7) = Left(dtNow.ToShortDateString, 7))) Then
                                            tourokuFlg = 1
                                            dr1("CRTDT") = DateTime.Now
                                            dr1("CRTCD") = userid
                                            dr1("UPDDT") = DateTime.Now
                                            dr1("UPDCD") = userid
                                            dr1("UPDPG") = "systemName"
                                            dr1("DELFG") = 0
                                            dr1("Branch_Code") = shipCode
                                            If csvData(i)(0) = "" Then
                                                dr1("rec_datetime") = DBNull.Value
                                            Else
                                                dr1("rec_datetime") = DateTime.Parse(csvData(i)(0))
                                            End If
                                            dr1("rec_yuser") = csvData(i)(1)
                                            dr1("rpt_counter") = csvData(i)(2)
                                            dr1("rpt_repair") = csvData(i)(3)
                                            If csvData(i)(4) = "" Then
                                                dr1("close_datetime") = DBNull.Value
                                            Else
                                                dr1("close_datetime") = DateTime.Parse(csvData(i)(4))
                                            End If
                                            dr1("denomi") = csvData(i)(5)
                                            If csvData(i)(6) = "" Then
                                                dr1("amount") = 0
                                            Else
                                                dr1("amount") = csvData(i)(6)
                                            End If
                                            dr1("asc_c_num") = csvData(i)(7)
                                            dr1("sam_c_num") = csvData(i)(8)
                                            dr1("comment") = csvData(i)(9)

                                            'PDF出力用にデータの構造体にセット（po_no等
                                            If dr1("po_no") IsNot DBNull.Value Then
                                                addDataPdf(i).po_no = dr1("po_no")
                                            End If
                                            addDataPdf(i).sam_c_num = csvData(i)(8)
                                            addDataPdf(i).asc_c_num = csvData(i)(7)
                                            addDataPdf(i).amount = csvData(i)(6)
                                            addDataPdf(i).rec_yuser = csvData(i)(1)
                                            Adapter1.Update(ds1)
                                        End If
                                    End If
                                End If
                            End If
                        Next i
                    End If
                End If
            End If
            trn.Commit()
        Catch ex As Exception
            trn.Rollback()
            errFlg = 1
        Finally
            'DB接続クローズ
            If con.State <> ConnectionState.Closed Then
                con.Close()
            End If
        End Try

    End Sub
    '****************************************************
    '処理：GSPN以外のデータの登録
    '引数：csvData CSVデータ
    '　　　errFlg        戻り値　0:正常　1:異常
    '      otherData     入力画面からの情報 
    '      otherDataPdf  出力pdfの項目
    '      userid        ログインユーザID
    '      shipCode      ログインユーザ拠点コード
    '      poM           poの種類名 
    '      poNoMax       poNoの最終番号　
    '      kindCsv       CSVデータの種類
    '      levelFlg      管理者権限　userLevel 0 or 1 or 2 or adminflg = true
    '      tourokuFlg    登録確認フラグ　-1:変更なし　1:変更あり 
    '****************************************************
    Public Sub setCsvData2(ByVal csvData()() As String, ByRef errFlg As Integer, ByRef otherData As OTHER_DATA, ByRef otherDataPdf() As OTHER_DATA, ByVal userid As String, ByVal shipCode As String, ByVal poM As String, ByVal poNoMax As Long, ByVal kindCsv As String, ByVal levelFlg As String, ByRef tourokuFlg As Integer)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Dim i As Long
        Dim newData As Integer = -1 '新規追加登録確認用 

        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia

        Try
            '入力画面からの登録
            If csvData Is Nothing Then

                Dim select_sql1 As String = ""
                select_sql1 &= "SELECT * FROM dbo.T_repair1 WHERE po_no = '" & otherData.po_no & "' AND DELFG = 0 AND Branch_Code = '" & shipCode & "';"
                Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
                Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
                Dim Builder1 As New SqlCommandBuilder(Adapter1)
                Dim ds1 As New DataSet
                Dim dr1 As DataRow
                Adapter1.Fill(ds1)

                If ds1.Tables(0).Rows.Count = 1 Then
                    '上書き登録
                    dr1 = ds1.Tables(0).Rows(0)
                Else
                    '新規追加登録
                    dr1 = ds1.Tables(0).NewRow
                    newData = 1
                End If

                If (dr1("Completed_Date") Is DBNull.Value) Or (dr1("Completed_Date") IsNot DBNull.Value And (levelFlg = "1")) Then

                    If newData = 1 Then
                        dr1("CRTDT") = dtNow
                        dr1("CRTCD") = userid
                        dr1("po_no") = poM & poNoMax.ToString("00000000")
                    Else
                        dr1("UPDDT") = dtNow
                        dr1("UPDCD") = userid
                    End If

                    dr1("UPDPG") = "Class_common.vb"
                    dr1("DELFG") = 0
                    dr1("Branch_Code") = shipCode

                    If otherData.rec_datetime = "" Then
                        dr1("rec_datetime") = DBNull.Value
                    Else
                        dr1("rec_datetime") = otherData.rec_datetime
                    End If

                    dr1("rec_yuser") = otherData.rec_yuser

                    dr1("denomi") = otherData.denomi

                    If otherData.amount Is Nothing Or otherData.amount = "" Then
                        dr1("amount") = 0.00
                    Else
                        dr1("amount") = otherData.amount
                    End If

                    dr1("comment") = otherData.comment
                    dr1("comment2") = otherData.comment2
                    dr1("Consumer_Name") = otherData.Consumer_Name
                    dr1("Consumer_Addr1") = otherData.Consumer_Addr1
                    dr1("Consumer_Addr2") = otherData.Consumer_Addr2
                    dr1("Consumer_MailAddress") = otherData.Customer_mail_address
                    dr1("Consumer_Telephone") = otherData.Consumer_Telephone
                    dr1("Consumer_Fax") = otherData.Consumer_Fax
                    dr1("Postal_Code") = otherData.Postal_Code
                    dr1("State_Name") = otherData.State_Name
                    dr1("Model") = otherData.Model
                    dr1("Serial_No") = otherData.Serial_No
                    dr1("IMEI_No") = otherData.IMEI_No
                    dr1("Maker") = otherData.Maker
                    dr1("Product_Type") = otherData.Product_Type
                    dr1("warranty") = otherData.warranty
                    dr1("Repair_Description") = otherData.Repair_Description

                    If otherData.Repair_Received_Date = "" Then
                        dr1("Repair_Received_Date") = DBNull.Value
                    Else
                        dr1("Repair_Received_Date") = otherData.Repair_Received_Date
                    End If

                    If otherData.Completed_Date = "" Then
                        dr1("Completed_Date") = DBNull.Value
                    Else
                        dr1("Completed_Date") = otherData.Completed_Date
                    End If

                    If otherData.Delivery_Date = "" Then
                        dr1("Delivery_Date") = DBNull.Value
                    Else
                        dr1("Delivery_Date") = otherData.Delivery_Date
                    End If

                    If otherData.Labor_Amount Is Nothing Or otherData.Labor_Amount = "" Then
                        dr1("Labor_Amount") = 0.00
                    Else
                        dr1("Labor_Amount") = otherData.Labor_Amount
                    End If

                    dr1("Labor_No") = otherData.Labor_No
                    dr1("Labor_Qty") = otherData.Labor_Qty

                    If otherData.Parts_Amount Is Nothing Or otherData.Parts_Amount = "" Then
                        dr1("Parts_Amount") = 0.00
                    Else
                        dr1("Parts_Amount") = otherData.Parts_Amount
                    End If

                    dr1("Freight") = otherData.ShipMent_No
                    dr1("Freight_Qty") = otherData.ShipMent_Qty

                    If otherData.ShipMent_Price Is Nothing Or otherData.ShipMent_Price = "" Then
                        dr1("Freight_Price") = 0.00
                    Else
                        dr1("Freight_Price") = otherData.ShipMent_Price
                    End If

                    dr1("Other") = otherData.Other_No
                    dr1("Other_Qty") = otherData.Other_Qty

                    If otherData.Other_Price Is Nothing Or otherData.Other_Price = "" Then
                        dr1("Other_Price") = 0.00
                    Else
                        dr1("Other_Price") = otherData.Other_Price
                    End If

                    If otherData.Other_Freight_Amount Is Nothing Or otherData.Other_Freight_Amount = "" Then
                        dr1("Other_Freight_Amount") = 0.00
                    Else
                        dr1("Other_Freight_Amount") = otherData.Other_Freight_Amount
                    End If

                    If otherData.Other_Freight_SGST Is Nothing Or otherData.Other_Freight_SGST = "" Then
                        dr1("Other_Freight_SGST") = 0.00
                    Else
                        dr1("Other_Freight_SGST") = otherData.Other_Freight_SGST
                    End If

                    If otherData.Other_Freight_CGST Is Nothing Or otherData.Other_Freight_CGST = "" Then
                        dr1("Other_Freight_CGST") = 0.00
                    Else
                        dr1("Other_Freight_CGST") = otherData.Other_Freight_CGST
                    End If

                    If otherData.Other_Freight_IGST Is Nothing Or otherData.Other_Freight_IGST = "" Then
                        dr1("Other_Freight_IGST") = 0.00
                    Else
                        dr1("Other_Freight_IGST") = otherData.Other_Freight_IGST
                    End If

                    If otherData.Parts_SGST Is Nothing Or otherData.Parts_SGST = "" Then
                        dr1("Parts_SGST") = 0.00
                    Else
                        dr1("Parts_SGST") = otherData.Parts_SGST
                    End If

                    If otherData.Parts_IGST Is Nothing Or otherData.Parts_IGST = "" Then
                        dr1("Parts_IGST") = 0.00
                    Else
                        dr1("Parts_IGST") = otherData.Parts_IGST
                    End If

                    If otherData.Parts_CGST Is Nothing Or otherData.Parts_CGST = "" Then
                        dr1("Parts_CGST") = 0.00
                    Else
                        dr1("Parts_CGST") = otherData.Parts_CGST
                    End If

                    If otherData.Total_Invoice_Amount Is Nothing Or otherData.Total_Invoice_Amount = "" Then
                        dr1("Total_Invoice_Amount") = 0.00
                    Else
                        dr1("Total_Invoice_Amount") = otherData.Total_Invoice_Amount
                    End If

                    dr1("Part_1") = otherData.Part_1
                    dr1("Qty_1") = otherData.Qty_1

                    If otherData.GUnit_Price_1 Is Nothing Or otherData.GUnit_Price_1 = "" Then
                        dr1("Unit_Price_1") = 0.00
                    Else
                        dr1("Unit_Price_1") = otherData.GUnit_Price_1
                    End If

                    dr1("Part_2") = otherData.Part_2
                    dr1("Qty_2") = otherData.Qty_2

                    If otherData.GUnit_Price_2 Is Nothing Or otherData.GUnit_Price_2 = "" Then
                        dr1("Unit_Price_2") = 0.00
                    Else
                        dr1("Unit_Price_2") = otherData.GUnit_Price_2
                    End If

                    dr1("Part_3") = otherData.Part_3
                    dr1("Qty_3") = otherData.Qty_3

                    If otherData.GUnit_Price_3 Is Nothing Or otherData.GUnit_Price_3 = "" Then
                        dr1("Unit_Price_3") = 0.00
                    Else
                        dr1("Unit_Price_3") = otherData.GUnit_Price_3
                    End If

                    dr1("Part_4") = otherData.Part_4
                    dr1("Qty_4") = otherData.Qty_4

                    If otherData.GUnit_Price_4 Is Nothing Or otherData.GUnit_Price_4 = "" Then
                        dr1("Unit_Price_4") = 0.00
                    Else
                        dr1("Unit_Price_4") = otherData.GUnit_Price_4
                    End If

                    dr1("Part_5") = otherData.Part_5
                    dr1("Qty_5") = otherData.Qty_5

                    If otherData.GUnit_Price_5 Is Nothing Or otherData.GUnit_Price_5 = "" Then
                        dr1("Unit_Price_5") = 0.00
                    Else
                        dr1("Unit_Price_5") = otherData.GUnit_Price_5
                    End If

                    If otherData.SGST Is Nothing Or otherData.SGST = "" Then
                        dr1("SGST") = 0.00
                    Else
                        dr1("SGST") = otherData.SGST
                    End If

                    If otherData.IGST Is Nothing Or otherData.IGST = "" Then
                        dr1("IGST") = 0.00
                    Else
                        dr1("IGST") = otherData.IGST
                    End If

                    If otherData.CGST Is Nothing Or otherData.CGST = "" Then
                        dr1("CGST") = 0.00
                    Else
                        dr1("CGST") = otherData.CGST
                    End If

                    If newData = 1 Then
                        '新規DRをDataTableに追加 
                        ds1.Tables(0).Rows.Add(dr1)
                    End If

                    'po_noを取得　※後でPDF出力用
                    If dr1("po_no") IsNot DBNull.Value Then
                        otherData.po_no = dr1("po_no")
                    End If

                    Adapter1.Update(ds1)

                End If

            Else

                ''CSVより登録
                'Dim dt As DateTime
                'Dim chkCompletedDate As DateTime
                'Dim newDataCnt As Long
                'For i = 0 To csvData.Length - 1
                '    If i <> 0 Then
                '        tourokuFlg = -1
                '        newData = -1
                '        Dim select_sql1 As String = ""
                '        select_sql1 &= "SELECT * FROM dbo.T_repair1 WHERE Consumer_Name = '" & csvData(i)(8) & "'AND Serial_No = '" & csvData(i)(15) & "' AND DELFG = 0 and Branch_Code = '" & shipCode & "';"
                '        Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
                '        Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
                '        Dim Builder1 As New SqlCommandBuilder(Adapter1)
                '        Dim ds1 As New DataSet
                '        Dim dr1 As DataRow
                '        Adapter1.Fill(ds1)
                '        If ds1.Tables(0).Rows.Count = 1 Then
                '            '上書き登録
                '            dr1 = ds1.Tables(0).Rows(0)
                '        Else
                '            '新規追加登録
                '            dr1 = ds1.Tables(0).NewRow
                '            newData = 1
                '        End If
                '        '完了日に記載があれば、管理者以外は、変更登録不可
                '        '※管理者であっても、完了日が当月以外は変更登録不可
                '        If dr1("Completed_Date") IsNot DBNull.Value Then
                '            chkCompletedDate = dr1("Completed_Date")
                '        End If
                '        If (dr1("Completed_Date") Is DBNull.Value) Or (dr1("Completed_Date") IsNot DBNull.Value And userLevel = "1" And (Left(chkCompletedDate.ToShortDateString, 7) = Left(dtNow.ToShortDateString, 7))) Then
                '            tourokuFlg = 1
                '            dr1("CRTDT") = DateTime.Now
                '            dr1("CRTCD") = userid
                '            dr1("UPDDT") = DateTime.Now
                '            dr1("UPDCD") = userid
                '            dr1("UPDPG") = "systemName"
                '            dr1("DELFG") = 0
                '            If newData = 1 Then
                '                dr1("po_no") = poM & (poNoMax + newDataCnt).ToString("00000000")
                '                newDataCnt = newDataCnt + 1
                '            End If
                '            dr1("Branch_Code") = shipCode
                '            If DateTime.TryParse(csvData(i)(0), dt) Then
                '                dr1("rec_datetime") = DateTime.Parse(csvData(i)(0))
                '            Else
                '                dr1("rec_datetime") = DBNull.Value
                '            End If
                '            dr1("rec_yuser") = csvData(i)(1)
                '            dr1("rpt_counter") = csvData(i)(2)
                '            dr1("rpt_repair") = csvData(i)(3)
                '            If DateTime.TryParse(csvData(i)(4), dt) Then
                '                dr1("close_datetime") = DateTime.Parse(csvData(i)(4))
                '            Else
                '                dr1("close_datetime") = DBNull.Value
                '            End If
                '            dr1("denomi") = csvData(i)(5)
                '            If csvData(i)(6) = "" Then
                '                dr1("amount") = 0
                '            Else
                '                dr1("amount") = csvData(i)(6)
                '            End If
                '            dr1("comment") = csvData(i)(7)
                '            dr1("Consumer_Name") = csvData(i)(8)
                '            dr1("Consumer_Addr1") = csvData(i)(9)
                '            dr1("Consumer_Addr2") = csvData(i)(10)
                '            dr1("Consumer_Telephone") = csvData(i)(11)
                '            dr1("Consumer_Fax") = csvData(i)(12)
                '            dr1("Postal_Code") = csvData(i)(13)
                '            dr1("Model") = csvData(i)(14)
                '            dr1("Serial_No") = csvData(i)(15)
                '            dr1("IMEI_No") = csvData(i)(16)
                '            dr1("Defect_Type") = csvData(i)(17)
                '            dr1("Repair_Description") = csvData(i)(18)
                '            If DateTime.TryParse(csvData(i)(19), dt) Then
                '                dr1("Repair_Received_Date") = DateTime.Parse(csvData(i)(19))
                '            Else
                '                dr1("Repair_Received_Date") = DBNull.Value
                '            End If
                '            If DateTime.TryParse(csvData(i)(20), dt) Then
                '                dr1("Completed_Date") = DateTime.Parse(csvData(i)(20))
                '            Else
                '                dr1("Completed_Date") = DBNull.Value
                '            End If
                '            If DateTime.TryParse(csvData(i)(21), dt) Then
                '                dr1("Delivery_Date") = DateTime.Parse(csvData(i)(21))
                '            Else
                '                dr1("Delivery_Date") = DBNull.Value
                '            End If
                '            If csvData(i)(22) = "" Then
                '                dr1("Labor_Amount") = 0
                '            Else
                '                dr1("Labor_Amount") = csvData(i)(22)
                '            End If
                '            If csvData(i)(23) = "" Then
                '                dr1("Parts_Amount") = 0
                '            Else
                '                dr1("Parts_Amount") = csvData(i)(23)
                '            End If
                '            If csvData(i)(24) = "" Then
                '                dr1("Parts_SGST") = 0
                '            Else
                '                dr1("Parts_SGST") = csvData(i)(24)
                '            End If
                '            If csvData(i)(25) = "" Then
                '                dr1("Parts_CGST") = 0
                '            Else
                '                dr1("Parts_CGST") = csvData(i)(25)
                '            End If
                '            If csvData(i)(26) = "" Then
                '                dr1("Total_Invoice_Amount") = 0
                '            Else
                '                dr1("Total_Invoice_Amount") = csvData(i)(26)
                '            End If

                '            If newData = 1 Then
                '                '新規DRをDataTableに追加 
                '                ds1.Tables(0).Rows.Add(dr1)
                '            End If

                '            'PDF出力用にデータの構造体にセット
                '            If dr1("po_no") IsNot DBNull.Value Then
                '                otherDataPdf(i).po_no = dr1("po_no")
                '            End If
                '            otherDataPdf(i).amount = csvData(i)(6)
                '            otherDataPdf(i).rec_yuser = csvData(i)(1)
                '            otherDataPdf(i).Consumer_Name = csvData(i)(8)
                '            otherDataPdf(i).Serial_No = csvData(i)(15)
                '            Adapter1.Update(ds1)
                '        End If
                '    End If
                'Next i

            End If

            trn.Commit()

        Catch ex As Exception
            trn.Rollback()
            errFlg = 1
        Finally
            'DB接続クローズ
            If con.State <> ConnectionState.Closed Then
                con.Close()
            End If
        End Try

    End Sub
    '****************************************************
    '処理：店舗の開店情報確認
    '引数：shipCode  　ログイン者の拠点コード　
    '　　　openTime    戻り値  開店時間
    '      closeTime   戻り値  閉店時間
    '      errMsg      戻り値　エラー内容
    '返却：FALSE 閉店 TRRUE 開店
    '          ※開店の判断：Open処理開始済　閉店の判断：締め処理済（Confirmation Reportが完了済）
    '****************************************************
    Public Function chkOpen(ByVal shipCode As String, ByRef openTime As String, ByRef closeTime As String, ByRef errMsg As String) As Boolean

        Dim strSQL = "SELECT * FROM dbo.M_ship_base WHERE ship_code = '" & shipCode & "';"

        Dim errFlg As Integer
        Dim openingDate As Date
        Dim closingDate As Date

        Dim clsSetCommon As New Class_common
        Dim dt As DateTime = clsSetCommon.dtIndia

        Dim DT_M_ship_base As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = "Failed to acquire information from DT_M_ship_base"
            Exit Function
        End If

        If DT_M_ship_base IsNot Nothing Then

            If DT_M_ship_base.Rows(0)("open_time") IsNot DBNull.Value Then
                openTime = DT_M_ship_base.Rows(0)("open_time")
            End If

            If DT_M_ship_base.Rows(0)("opening_date") IsNot DBNull.Value Then
                openingDate = DT_M_ship_base.Rows(0)("opening_date")
            End If

            If DT_M_ship_base.Rows(0)("closing_date") IsNot DBNull.Value Then
                closingDate = DT_M_ship_base.Rows(0)("closing_date")
                ''''''''''''''''''''If dt.ToShortDateString > closingDate.ToShortDateString Then
                ''''''''''''''''''''    errMsg = "閉店しているお店です。"
                ''''''''''''''''''''    Exit Function
                ''''''''''''''''''''End If
            End If

        Else
            errMsg = "Open store information can not be acquired from M_ship_base."
            Exit Function
        End If

        If openTime = "True " Then
            Return True
        Else
            Return False
        End If

    End Function
    '****************************************************
    '処理：レジ点検処理可能時刻を取得
    '引数：shipCode  　　　ログイン者の拠点コード　
    '　　　syoriStatus　　 戻り値  処理ステータス
    '      syoriStart  　　戻り値  点検処理可能開始時間をセット
    '      syoriEnd        戻り値　点検処理期限時間をセット　
    '      errMsg      　　戻り値　エラー内容
    '****************************************************
    Public Sub syoriOkTime(ByVal shipCode As String, ByVal syoriStatus As String, ByRef syoriStart As String, ByRef syoriEnd As String, ByRef errMsg As String)

        Dim strSQL = "SELECT * FROM dbo.M_ship_base WHERE DELFG = 0 AND ship_code = '" & shipCode & "';"

        Dim errFlg As Integer

        Dim DT_M_ship_base As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = "Failed to get information from M_ship_base"
            Exit Sub
        End If

        If DT_M_ship_base IsNot Nothing Then

            If syoriStatus = "open" Then

                If DT_M_ship_base.Rows(0)("open_start") IsNot DBNull.Value Then
                    syoriStart = DT_M_ship_base.Rows(0)("open_start")
                Else
                    syoriStart = "00:00"
                End If

                If DT_M_ship_base.Rows(0)("open_end") IsNot DBNull.Value Then
                    syoriEnd = DT_M_ship_base.Rows(0)("open_end")
                Else
                    syoriEnd = "24:00"
                End If

            ElseIf syoriStatus = "inspection1" Then

                If DT_M_ship_base.Rows(0)("inspection1_start") IsNot DBNull.Value Then
                    syoriStart = DT_M_ship_base.Rows(0)("inspection1_start")
                Else
                    syoriStart = "00:00"
                End If

                If DT_M_ship_base.Rows(0)("inspection1_end") IsNot DBNull.Value Then
                    syoriEnd = DT_M_ship_base.Rows(0)("inspection1_end")
                Else
                    syoriEnd = "24:00"
                End If

            ElseIf syoriStatus = "inspection2" Then

                If DT_M_ship_base.Rows(0)("inspection2_start") IsNot DBNull.Value Then
                    syoriStart = DT_M_ship_base.Rows(0)("inspection2_start")
                Else
                    syoriStart = "00:00"
                End If

                If DT_M_ship_base.Rows(0)("inspection2_end") IsNot DBNull.Value Then
                    syoriEnd = DT_M_ship_base.Rows(0)("inspection2_end")
                Else
                    syoriEnd = "24:00"
                End If

            ElseIf syoriStatus = "inspection3" Then

                If DT_M_ship_base.Rows(0)("inspection3_start") IsNot DBNull.Value Then
                    syoriStart = DT_M_ship_base.Rows(0)("inspection3_start")
                Else
                    syoriStart = "00:00"
                End If

                If DT_M_ship_base.Rows(0)("inspection3_end") IsNot DBNull.Value Then
                    syoriEnd = DT_M_ship_base.Rows(0)("inspection3_end")
                Else
                    syoriEnd = "24:00"
                End If

            End If

        End If






    End Sub

    '****************************************************
    '処理：GSPN売上げ登録情報をPDFとして出力処理
    '引数：addData   　画面からの入力情報　
    '　　　addDataPdf  CSVからの入力情報
    '      errMsg      戻り値　エラー内容
    '      errFlg      戻り値  1:エラー　0：正常 
    '****************************************************
    Public Sub outPDF(ByVal addData As ADD_DATA, ByVal addDataPdf() As ADD_DATA, ByRef errMsg As String, ByRef errFlg As Integer)

        '***バーコードのPDF出力処理***
        Dim doc As Document
        Dim fileStream As FileStream
        Dim pdfWriter As PdfWriter
        Dim pdfFileName As String = DateTime.Now.ToString("yyyyMMddHHmmss") & ".pdf"

        Try
            'FileStreamを生成 
            fileStream = New FileStream(savePDFPass & pdfFileName, FileMode.Create)

            'Documentを生成 
            doc = New Document(PageSize.A4)

            'PdfWriter生成 
            pdfWriter = PdfWriter.GetInstance(doc, fileStream)

            'Documentのオープン 
            doc.Open()

            '項目の書き出し
            doc.Add(New Paragraph("PO No,ASC_Claim_no,samsung_Claim_no,yousername,Amount"))

            'CSVからの入力情報をPDF出力
            If addDataPdf IsNot Nothing Then
                For i = 0 To addDataPdf.Length - 1
                    If addDataPdf(i).po_no IsNot Nothing Then
                        doc.Add(New Paragraph(addDataPdf(i).po_no & "," & addDataPdf(i).asc_c_num & "," & addDataPdf(i).sam_c_num & "," & addDataPdf(i).rec_yuser & "," & addDataPdf(i).amount))
                    End If
                Next i
            End If

            '画面からの入力情報をPDF出力
            If addData.po_no IsNot Nothing Then
                doc.Add(New Paragraph(addData.po_no & "," & addData.asc_c_num & "," & addData.sam_c_num & "," & addData.rec_yuser & "," & addData.amount))
            End If

            'クローズ 
            doc.Close()

        Catch ex As Exception
            errMsg = "Data registration is complete. The PDF output for confirming the difference failed."
            Exit Sub
        End Try

    End Sub
    '****************************************************
    '処理：その他売上げ登録情報をPDFとして出力処理
    '引数：otherData   　画面からの入力情報　
    '　　　otherDataPdf  CSVからの入力情報
    '      errMsg        戻り値　エラー内容
    '      errFlg        戻り値  1:エラー　0：正常 
    '****************************************************
    Public Sub outPDF2(ByVal otherData As OTHER_DATA, ByVal otherDataPdf() As OTHER_DATA, ByRef errMsg As String, ByRef errFlg As Integer)

        '***バーコードのPDF出力処理***
        Dim doc As Document
        Dim fileStream As FileStream
        Dim pdfWriter As PdfWriter
        Dim pdfFileName As String = DateTime.Now.ToString("yyyyMMddHHmmss") & ".pdf"

        Try
            'FileStreamを生成 
            fileStream = New FileStream(savePDFPass & pdfFileName, FileMode.Create)

            'Documentを生成 
            doc = New Document(PageSize.A4)

            'PdfWriter生成 
            pdfWriter = PdfWriter.GetInstance(doc, fileStream)

            'Documentのオープン 
            doc.Open()

            '項目の書き出し
            doc.Add(New Paragraph("PO No,Serial_No,Consumer_Name,yousername,Amount"))

            'CSVからの入力情報をPDF出力
            If otherDataPdf IsNot Nothing Then
                For i = 0 To otherDataPdf.Length - 1
                    If otherDataPdf(i).po_no IsNot Nothing Then
                        doc.Add(New Paragraph(otherDataPdf(i).po_no & "," & otherDataPdf(i).Serial_No & "," & otherDataPdf(i).Consumer_Name & "," & otherDataPdf(i).rec_yuser & "," & otherDataPdf(i).amount))
                    End If
                Next i
            End If

            '画面からの入力情報をPDF出力
            If otherData.po_no IsNot Nothing Then
                doc.Add(New Paragraph(otherData.po_no & "," & otherData.Serial_No & "," & otherData.Consumer_Name & "," & otherData.rec_yuser & "," & otherData.amount))
            End If

            'クローズ 
            doc.Close()

        Catch ex As Exception
            errMsg = "Data registration is complete. The PDF output for confirming the difference failed."
            Exit Sub
        End Try

    End Sub
    '****************************************************
    '処理：受付時、PDF出力処理
    '引数：otherData   　画面からの入力情報　
    '　　　pdfFileName   ファイル名
    '      errFlg        戻り値  1:エラー　0：正常
    '      shipName      拠点名称
    '****************************************************
    Public Sub ReceptionistPDF(ByVal otherData As OTHER_DATA, ByVal pdfFileName As String, ByRef errFlg As Integer, ByVal shipName As String)

        '***PDF出力処理***
        Dim fileStream As FileStream

        Try
            Dim doc As Document
            Dim pdfWriter As PdfWriter
            Dim image1 As Image = Image.GetInstance(logoQuickGarage) '画像
            image1.ScalePercent(7) '大きさ

            'フォントの設定
            Dim fnt1 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 10)
            Dim fnt As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 12)
            Dim fntBa2 As New Font(BaseFont.CreateFont("c:\windows\fonts\CODE39.ttf", BaseFont.IDENTITY_H, True), 12) 'バーコード
            Dim fnt2 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 24, iTextSharp.text.Font.BOLD)
            Dim fnt3 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 12, iTextSharp.text.Font.UNDERLINE)
            Dim fnt4 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 16, iTextSharp.text.Font.UNDERLINE)
            Dim fnt5 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 16, iTextSharp.text.Font.BOLD)

            'FileStreamを生成 
            fileStream = New FileStream(savePDFPass & pdfFileName, FileMode.Create)

            'Documentを生成 
            doc = New Document(PageSize.A4, 5, 5, 5, 5)

            'PdfWriter生成 
            pdfWriter = PdfWriter.GetInstance(doc, fileStream)

            'Documentのオープン 
            doc.Open()

            'PDF表示行列設定
            Const pdfRow As Integer = 10

            Dim pcb As PdfContentByte = pdfWriter.DirectContent

            'テーブル作成
            Dim table As PdfPTable
            table = New PdfPTable(pdfRow)
            table.TotalWidth = 100%

            '列の設定（10列）
            Dim widths As Single()
            widths = New Single() {1.0F, 1.0F, 1.0F, 0.5F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 0.5F}
            table.SetWidths(widths)

            'セル作成
            Dim cell As PdfPCell

            '件名
            doc.Add(New Paragraph("   Reception sheet", fnt2))

            'Quick Garageのアイコンをセット
            Dim p As New Paragraph()
            p.Add(New Chunk(image1, 0, 0))
            p.Alignment = Element.ALIGN_RIGHT
            p.Add(" ")
            doc.Add(p)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '受付日
            cell = New PdfPCell(New Paragraph("Reception Date：" & otherData.Repair_Received_Date, fnt))
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'Po番号
            cell = New PdfPCell(New Paragraph("PO : " & otherData.po_no, fnt2))
            cell.FixedHeight = 26.0F
            cell.Colspan = 6
            table.AddCell(cell)

            cell = New PdfPCell(New Paragraph("*" & Right(otherData.po_no, Len(otherData.po_no) - 2) & "*", fntBa2))
            cell.FixedHeight = 26.0F
            cell.Colspan = 4
            table.AddCell(cell)

            '拠点名称
            cell = New PdfPCell(New Paragraph(shipName, fnt2))
            cell.FixedHeight = 26.0F
            cell.Colspan = 6
            table.AddCell(cell)

            'ユーザ(受付担当者)
            cell = New PdfPCell(New Paragraph(otherData.rec_yuser, fnt5))
            cell.FixedHeight = 26.0F
            cell.Colspan = 4
            table.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'お客様の申告状況
            cell = New PdfPCell(New Paragraph("Customer declaration contents", fnt))
            cell.GrayFill = 0.8F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            cell = New PdfPCell(New Paragraph(otherData.Repair_Description, fnt))
            cell.FixedHeight = 80.0F
            cell.Colspan = 10
            table.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'お客様情報
            cell = New PdfPCell(New Paragraph("Customer Information", fnt))
            cell.GrayFill = 0.8F
            cell.Colspan = 3
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            cell = New PdfPCell(New Paragraph("Warranty status:in warranty(out of warranty)", fnt))
            cell.HorizontalAlignment = Element.ALIGN_RIGHT
            cell.GrayFill = 0.8F
            cell.Colspan = 7
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'product type
            cell = New PdfPCell(New Paragraph("product type : " & otherData.Product_Type, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 5
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'tel
            cell = New PdfPCell(New Paragraph("telephone : " & otherData.Consumer_Telephone, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 4
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'name
            cell = New PdfPCell(New Paragraph("name : " & otherData.Consumer_Name, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 9
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'mail address
            cell = New PdfPCell(New Paragraph("mail address : " & otherData.Customer_mail_address, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 9
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'serial No
            cell = New PdfPCell(New Paragraph("serial No : " & otherData.Serial_No, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 9
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'Maker
            cell = New PdfPCell(New Paragraph("Maker : " & otherData.Maker, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 9
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'Model
            cell = New PdfPCell(New Paragraph("Model : " & otherData.Model, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 9
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'address1
            cell = New PdfPCell(New Paragraph("address1 : " & otherData.Consumer_Addr1, fnt))
            cell.FixedHeight = 32.0F
            cell.Colspan = 9
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 32.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'address2
            cell = New PdfPCell(New Paragraph("address2 : " & otherData.Consumer_Addr2, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 9
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'address
            'cell = New PdfPCell(New Paragraph("address : " & otherData.Consumer_Addr1, fnt1))
            'cell.FixedHeight = 48.0F
            'cell.Colspan = 10
            'table.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'ブルーセル：used parts
            cell = New PdfPCell(New Paragraph("used parts", fnt))
            cell.BackgroundColor = New BaseColor(188, 200, 219)
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '項目
            cell = New PdfPCell(New Paragraph("item", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table.AddCell(cell)

            'parts number
            cell = New PdfPCell(New Paragraph("parts number", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table.AddCell(cell)

            'parts serial
            cell = New PdfPCell(New Paragraph("parts serial", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table.AddCell(cell)

            'qty
            cell = New PdfPCell(New Paragraph("qty", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table.AddCell(cell)

            'price
            cell = New PdfPCell(New Paragraph("price", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table.AddCell(cell)

            'parts 1
            cell = New PdfPCell(New Paragraph("parts 1", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table.AddCell(cell)

            'parts number
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table.AddCell(cell)

            'parts serial
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table.AddCell(cell)

            'qty
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table.AddCell(cell)

            'price
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table.AddCell(cell)

            'parts 2
            cell = New PdfPCell(New Paragraph("parts 2", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table.AddCell(cell)

            'parts number
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table.AddCell(cell)

            'parts serial
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table.AddCell(cell)

            'qty
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table.AddCell(cell)

            'price
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table.AddCell(cell)

            'parts 3
            cell = New PdfPCell(New Paragraph("parts 3", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table.AddCell(cell)

            'parts number
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table.AddCell(cell)

            'parts serial
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table.AddCell(cell)

            'qty
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table.AddCell(cell)

            'price
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table.AddCell(cell)

            'parts 4
            cell = New PdfPCell(New Paragraph("parts 4", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table.AddCell(cell)

            'parts number
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table.AddCell(cell)

            'parts serial
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table.AddCell(cell)

            'qty
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table.AddCell(cell)

            'price
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table.AddCell(cell)

            'parts 5
            cell = New PdfPCell(New Paragraph("parts 5", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table.AddCell(cell)

            'parts number
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table.AddCell(cell)

            'parts serial
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table.AddCell(cell)

            'qty
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table.AddCell(cell)

            'price
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table.AddCell(cell)

            'Labor
            cell = New PdfPCell(New Paragraph("Labor", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table.AddCell(cell)

            'parts number
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table.AddCell(cell)

            'parts serial
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table.AddCell(cell)

            'qty
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table.AddCell(cell)

            'price
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table.AddCell(cell)

            doc.Add(table)

            '二つ目のテーブル作成
            Dim table2 As PdfPTable
            table2 = New PdfPTable(8)
            table2.TotalWidth = 100%

            '列の設定（8列）
            Dim widths2 As Single()
            widths2 = New Single() {0.25F, 0.25F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F}
            table2.SetWidths(widths2)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            'ピンクセル：check point
            cell = New PdfPCell(New Paragraph("check point", fnt))
            cell.BackgroundColor = New BaseColor(239, 193, 196)
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            cell.Colspan = 5
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            'オレンジセル：作業内容(memo)
            cell = New PdfPCell(New Paragraph("Work content.", fnt))
            cell.BackgroundColor = New BaseColor(251, 236, 53)
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            cell.Colspan = 3
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            cell = New PdfPCell(New Paragraph("1", fnt))
            cell.Colspan = 1
            table2.AddCell(cell)

            cell = New PdfPCell(New Paragraph("", fnt))
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            cell.Colspan = 1
            table2.AddCell(cell)

            cell = New PdfPCell(New Paragraph("", fnt))
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            cell.Colspan = 3
            table2.AddCell(cell)

            cell = New PdfPCell(New Paragraph("", fnt))
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            cell.Colspan = 3
            cell.Rowspan = 8
            table2.AddCell(cell)

            'チェックポイント８項目分
            For i = 0 To 6
                cell = New PdfPCell(New Paragraph(i + 2, fnt))
                cell.Colspan = 1
                table2.AddCell(cell)

                cell = New PdfPCell(New Paragraph("", fnt))
                cell.VerticalAlignment = Element.ALIGN_MIDDLE
                cell.Colspan = 1
                table2.AddCell(cell)

                cell = New PdfPCell(New Paragraph("", fnt))
                cell.VerticalAlignment = Element.ALIGN_MIDDLE
                cell.Colspan = 3
                table2.AddCell(cell)
            Next i

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            'ブルーセル：used parts
            cell = New PdfPCell(New Paragraph("test tool result", fnt))
            cell.BackgroundColor = New BaseColor(207, 226, 212)
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            'Comment
            cell = New PdfPCell(New Paragraph("Comment: ", fnt))
            cell.FixedHeight = 48.0F
            cell.Colspan = 8
            table2.AddCell(cell)

            doc.Add(table2)

            '三つ目のテーブル作成
            Dim table3 As PdfPTable
            table3 = New PdfPTable(8)
            table3.TotalWidth = 100%

            '列の設定（8列）
            Dim widths3 As Single()
            widths3 = New Single() {1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F}
            table3.SetWidths(widths3)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table3.AddCell(cell)

            'グレーセル：store information
            cell = New PdfPCell(New Paragraph("store information", fnt))
            cell.GrayFill = 0.8F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table3.AddCell(cell)

            'ship name:
            cell = New PdfPCell(New Paragraph("name:", fnt))
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table3.AddCell(cell)

            cell = New PdfPCell(New Paragraph(otherData.ship_Name, fnt))
            cell.Colspan = 3
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table3.AddCell(cell)

            'GSTIN:
            cell = New PdfPCell(New Paragraph("GSTIN:", fnt))
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table3.AddCell(cell)

            cell = New PdfPCell(New Paragraph(otherData.GSTIN, fnt))
            cell.Colspan = 3
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table3.AddCell(cell)

            'e-mail:
            cell = New PdfPCell(New Paragraph("e-mail:", fnt))
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table3.AddCell(cell)

            cell = New PdfPCell(New Paragraph(otherData.Ship_Mail, fnt))
            cell.Colspan = 3
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table3.AddCell(cell)

            'tel:
            cell = New PdfPCell(New Paragraph("tel:", fnt))
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table3.AddCell(cell)

            cell = New PdfPCell(New Paragraph(otherData.Ship_Tel, fnt))
            cell.Colspan = 3
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table3.AddCell(cell)

            'address:
            cell = New PdfPCell(New Paragraph("address:", fnt))
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table3.AddCell(cell)

            cell = New PdfPCell(New Paragraph(otherData.Ship_Addr1, fnt))
            cell.Colspan = 7
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table3.AddCell(cell)

            doc.Add(table3)

            'クローズ 
            doc.Close()

        Catch ex As Exception
            errFlg = 1
            Exit Sub
        Finally
            If fileStream IsNot Nothing Then
                fileStream.Close()
            End If
        End Try

    End Sub
    '****************************************************
    '処理：見積書をPDFとして出力処理
    '引数：otherData   　画面からの入力情報　
    '　　　pdfFileName   PDFファイル名
    '      errFlg        戻り値  1:エラー　0：正常
    '      shipName      拠点名称 
    '****************************************************
    Public Sub EstimatesPDF2(ByVal otherData As OTHER_DATA, ByVal pdfFileName As String, ByRef errFlg As Integer, ByVal shipName As String)

        '***PDF出力処理***

        Dim fileStream As FileStream

        Try
            Dim doc As Document
            Dim pdfWriter As PdfWriter
            Dim image1 As Image = Image.GetInstance(logoQuickGarage) '画像
            image1.ScalePercent(7) '大きさ

            'フォントの設定
            Dim fnt1 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 10)
            Dim fnt As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 12)
            Dim fntBa2 As New Font(BaseFont.CreateFont("c:\windows\fonts\CODE39.ttf", BaseFont.IDENTITY_H, True), 12) 'バーコード
            Dim fnt2 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 24, iTextSharp.text.Font.BOLD)
            Dim fnt3 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 12, iTextSharp.text.Font.UNDERLINE)
            Dim fnt4 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 16, iTextSharp.text.Font.UNDERLINE)
            Dim fnt6 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 8)
            Dim fnt7 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 16)

            'FileStreamを生成 
            fileStream = New FileStream(savePDFPass & pdfFileName, FileMode.Create)

            'Documentを生成 
            doc = New Document(PageSize.A4, 5, 5, 5, 5)

            'PdfWriter生成 
            pdfWriter = PdfWriter.GetInstance(doc, fileStream)

            'Documentのオープン 
            doc.Open()

            Dim pcb As PdfContentByte = pdfWriter.DirectContent

            'PDF表示行列設定
            Const pdfRow As Integer = 10

            'テーブル作成
            Dim table As PdfPTable
            table = New PdfPTable(pdfRow)
            table.TotalWidth = 100%

            '列の設定（10列）
            Dim widths As Single()
            widths = New Single() {1.0F, 1.0F, 1.0F, 0.5F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 0.5F}
            table.SetWidths(widths)

            '二つ目のPDF表示行列設定
            Const pdfRow2 As Integer = 8

            '二つ目のテーブル作成
            Dim table2 As PdfPTable
            table2 = New PdfPTable(pdfRow2)
            table2.TotalWidth = 100%

            '二つ目の列の設定（８列）
            Dim widths2 As Single()
            widths2 = New Single() {1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F}
            table2.SetWidths(widths2)

            'セル作成
            Dim cell As PdfPCell

            '件名
            doc.Add(New Paragraph("   Estimate", fnt2))

            'Quick Garageのアイコンをセット
            Dim p As New Paragraph()
            p.Add(New Chunk(image1, 0, 0))
            p.Alignment = Element.ALIGN_RIGHT
            p.Add(" ")
            doc.Add(p)

            Dim msg1 As String = "Since it is an estimate, the contents of the quotation may be changed."
            Dim msg2 As String = "In that case, I will check the repair request again."

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '見積書の説明
            cell = New PdfPCell(New Paragraph(msg1, fnt))
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            cell = New PdfPCell(New Paragraph(msg2, fnt))
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '受付日
            cell = New PdfPCell(New Paragraph("Reception Date：" & otherData.Repair_Received_Date, fnt))
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'Po番号
            cell = New PdfPCell(New Paragraph("PO : " & otherData.po_no, fnt))
            'cell.FixedHeight = 26.0F
            cell.Colspan = 6
            table.AddCell(cell)

            cell = New PdfPCell(New Paragraph("*" & Right(otherData.po_no, Len(otherData.po_no) - 2) & "*", fntBa2))
            'cell.FixedHeight = 26.0F
            cell.Colspan = 4
            table.AddCell(cell)

            'グレーセル
            cell = New PdfPCell(New Paragraph("store information", fnt))
            cell.GrayFill = 0.8F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'ship name:
            cell = New PdfPCell(New Paragraph("name:", fnt))
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            cell = New PdfPCell(New Paragraph(otherData.ship_Name, fnt))
            cell.Colspan = 4
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'GSTIN:
            cell = New PdfPCell(New Paragraph("GSTIN:", fnt))
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            cell = New PdfPCell(New Paragraph(otherData.GSTIN, fnt))
            cell.Colspan = 4
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'e-mail:
            cell = New PdfPCell(New Paragraph("e-mail:", fnt))
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            cell = New PdfPCell(New Paragraph(otherData.Ship_Mail, fnt))
            cell.Colspan = 4
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'tel:
            cell = New PdfPCell(New Paragraph("tel:", fnt))
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            cell = New PdfPCell(New Paragraph(otherData.Ship_Tel, fnt))
            cell.Colspan = 4
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'address:
            cell = New PdfPCell(New Paragraph("address:", fnt))
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            cell = New PdfPCell(New Paragraph(otherData.Ship_Addr1, fnt))
            cell.Colspan = 9
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'お客様情報
            cell = New PdfPCell(New Paragraph("Customer Information", fnt))
            cell.GrayFill = 0.8F
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '保証有無
            cell = New PdfPCell(New Paragraph("Warranty status : " & otherData.warranty, fnt))
            cell.HorizontalAlignment = Element.ALIGN_RIGHT
            cell.GrayFill = 0.8F
            cell.FixedHeight = 16.0F
            cell.Colspan = 7
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'product type
            cell = New PdfPCell(New Paragraph("product type : " & otherData.Product_Type, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 5
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'お支払方法
            cell = New PdfPCell(New Paragraph("Payment : " & otherData.denomi, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 4
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'name
            cell = New PdfPCell(New Paragraph("name : " & Left(otherData.Consumer_Name, 30), fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 5
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'tel
            cell = New PdfPCell(New Paragraph("telephone : " & otherData.Consumer_Telephone, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 4
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'mail address
            cell = New PdfPCell(New Paragraph("mail address : " & otherData.Customer_mail_address, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 9
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'serial No
            cell = New PdfPCell(New Paragraph("serial No : " & otherData.Serial_No, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 9
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'Maker
            cell = New PdfPCell(New Paragraph("Maker : " & otherData.Maker, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 9
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'Model
            cell = New PdfPCell(New Paragraph("Model : " & otherData.Model, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 9
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'address1
            cell = New PdfPCell(New Paragraph("address1 : " & otherData.Consumer_Addr1, fnt))
            cell.FixedHeight = 32.0F
            cell.Colspan = 9
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 32.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'address2
            cell = New PdfPCell(New Paragraph("address2 : " & otherData.Consumer_Addr2, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 9
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            ''address
            'cell = New PdfPCell(New Paragraph("address : " & otherData.Consumer_Addr1, fnt))
            'cell.FixedHeight = 48.0F
            'cell.Colspan = 10
            'table.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            doc.Add(table)

            'ブルーセル：Service costs
            cell = New PdfPCell(New Paragraph("Service costs", fnt))
            cell.BackgroundColor = New BaseColor(167, 219, 162)
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            '項目
            cell = New PdfPCell(New Paragraph("item", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '名称
            cell = New PdfPCell(New Paragraph("name", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table2.AddCell(cell)

            '単価
            cell = New PdfPCell(New Paragraph("unit price", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '数量
            cell = New PdfPCell(New Paragraph("qty", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '金額
            cell = New PdfPCell(New Paragraph("Amount of money", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            '作業費用
            cell = New PdfPCell(New Paragraph("Labor", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '作業費名称
            cell = New PdfPCell(New Paragraph(otherData.LaborName, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table2.AddCell(cell)

            '単価
            cell = New PdfPCell(New Paragraph(otherData.gUnitPriceLabor, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '数量
            cell = New PdfPCell(New Paragraph(otherData.Labor_Qty, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '金額 保証修理は０円を設定
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.Labor_Amount, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            '郵送費用
            cell = New PdfPCell(New Paragraph("Postage", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '名称
            cell = New PdfPCell(New Paragraph(otherData.ShipMentName, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table2.AddCell(cell)

            '単価
            cell = New PdfPCell(New Paragraph(otherData.gUnitPriceShipMent, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '数量
            cell = New PdfPCell(New Paragraph(otherData.ShipMent_Qty, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '金額
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.ShipMent_Price, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'その他費用
            cell = New PdfPCell(New Paragraph("Other", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '名称
            cell = New PdfPCell(New Paragraph(otherData.OtherName, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table2.AddCell(cell)

            '単価
            cell = New PdfPCell(New Paragraph(otherData.gUnitPriceOther, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '数量
            cell = New PdfPCell(New Paragraph(otherData.Other_Qty, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '金額
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.Other_Price, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            '④TAX
            cell = New PdfPCell(New Paragraph("TAX", fnt))
            cell.FixedHeight = 16.0F
            cell.GrayFill = 0.8F
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            cell.Colspan = 2
            cell.Rowspan = 2
            table2.AddCell(cell)

            'CGST
            cell = New PdfPCell(New Paragraph("CGST", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'SGST
            cell = New PdfPCell(New Paragraph("SGST", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'IGST
            cell = New PdfPCell(New Paragraph("IGST", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'CGST
            '計算用
            Dim tmp_CGST As Decimal
            Dim tmp_Other_Freight_CGST As Decimal

            tmp_CGST = otherData.CGST
            tmp_Other_Freight_CGST = otherData.Other_Freight_CGST

            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph((tmp_CGST + tmp_Other_Freight_CGST).ToString, fnt))
            End If

            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'SGST
            '計算用
            Dim tmp_SGST As Decimal
            Dim tmp_Other_Freight_SGST As Decimal

            tmp_SGST = otherData.SGST
            tmp_Other_Freight_SGST = otherData.Other_Freight_SGST

            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph((tmp_SGST + tmp_Other_Freight_SGST).ToString, fnt))
            End If

            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'IGST
            '計算用
            Dim tmp_IGST As Decimal
            Dim tmp_Other_Freight_IGST As Decimal

            tmp_IGST = otherData.IGST
            tmp_Other_Freight_IGST = otherData.Other_Freight_IGST

            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph((tmp_IGST + tmp_Other_Freight_IGST).ToString, fnt))
            End If

            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            '合計
            '計算用
            Dim tmp_Labor_Amount As Decimal = otherData.Labor_Amount
            Dim tmp_Other_Freight_Amount As Decimal = otherData.Other_Freight_Amount

            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("amount : 0.00", fnt3))
            Else
                cell = New PdfPCell(New Paragraph("amount : " & ((tmp_CGST + tmp_Other_Freight_CGST) + (tmp_SGST + tmp_Other_Freight_SGST) + (tmp_IGST + tmp_Other_Freight_IGST) + (tmp_Labor_Amount + tmp_Other_Freight_Amount)).ToString, fnt3))
            End If

            cell.HorizontalAlignment = Element.ALIGN_RIGHT
            cell.FixedHeight = 16.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            'ピンクセル：Parts costs
            cell = New PdfPCell(New Paragraph("Parts costs", fnt))
            cell.BackgroundColor = New BaseColor(239, 193, 196)
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            '項目
            cell = New PdfPCell(New Paragraph("item", fnt))
            cell.Colspan = 1
            table2.AddCell(cell)

            '名称
            cell = New PdfPCell(New Paragraph("name ", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table2.AddCell(cell)

            '単価
            cell = New PdfPCell(New Paragraph("unit price", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '数量
            cell = New PdfPCell(New Paragraph("qty", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '金額
            cell = New PdfPCell(New Paragraph("Amount of money", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'parts 1
            cell = New PdfPCell(New Paragraph("parts 1", fnt))
            cell.Colspan = 1
            table2.AddCell(cell)

            '名称
            cell = New PdfPCell(New Paragraph(Left(otherData.Parts1Name, 38), fnt6))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table2.AddCell(cell)

            '単価
            cell = New PdfPCell(New Paragraph(otherData.gUnitPriceParts1, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '数量
            cell = New PdfPCell(New Paragraph(otherData.Qty_1, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '金額
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.GUnit_Price_1, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'parts 2
            cell = New PdfPCell(New Paragraph("parts 2", fnt))
            cell.Colspan = 1
            table2.AddCell(cell)

            '部品名称
            cell = New PdfPCell(New Paragraph(Left(otherData.Parts2Name, 38), fnt6))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table2.AddCell(cell)

            '単価
            cell = New PdfPCell(New Paragraph(otherData.gUnitPriceParts2, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '数量
            cell = New PdfPCell(New Paragraph(otherData.Qty_2, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '金額
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.GUnit_Price_2, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'parts 3
            cell = New PdfPCell(New Paragraph("parts 3", fnt))
            cell.Colspan = 1
            table2.AddCell(cell)

            '部品名称
            cell = New PdfPCell(New Paragraph(Left(otherData.Parts3Name, 38), fnt6))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table2.AddCell(cell)

            '単価
            cell = New PdfPCell(New Paragraph(otherData.gUnitPriceParts3, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '数量
            cell = New PdfPCell(New Paragraph(otherData.Qty_3, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '金額
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.GUnit_Price_3, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'parts 4
            cell = New PdfPCell(New Paragraph("parts 4", fnt))
            cell.Colspan = 1
            table2.AddCell(cell)

            '部品名称
            cell = New PdfPCell(New Paragraph(Left(otherData.Parts4Name, 38), fnt6))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table2.AddCell(cell)

            '単価
            cell = New PdfPCell(New Paragraph(otherData.gUnitPriceParts4, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '数量
            cell = New PdfPCell(New Paragraph(otherData.Qty_4, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '金額
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.GUnit_Price_4, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'parts 5
            cell = New PdfPCell(New Paragraph("parts 5", fnt))
            cell.Colspan = 1
            table2.AddCell(cell)

            '部品名称
            cell = New PdfPCell(New Paragraph(Left(otherData.Parts5Name, 38), fnt6))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table2.AddCell(cell)

            '単価
            cell = New PdfPCell(New Paragraph(otherData.gUnitPriceParts5, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '数量
            cell = New PdfPCell(New Paragraph(otherData.Qty_5, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '金額
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.GUnit_Price_5, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'TAX
            cell = New PdfPCell(New Paragraph("TAX", fnt))
            cell.FixedHeight = 16.0F
            cell.GrayFill = 0.8F
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            cell.Colspan = 2
            cell.Rowspan = 2
            table2.AddCell(cell)

            'CGST
            cell = New PdfPCell(New Paragraph("CGST", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'SGST
            cell = New PdfPCell(New Paragraph("SGST", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'IGST
            cell = New PdfPCell(New Paragraph("IGST", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'CGST
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.Parts_CGST, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'SGST
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.Parts_SGST, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'IGST
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.Parts_IGST, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            '合計
            Dim tmp_Parts_CGST As Decimal = otherData.Parts_CGST
            Dim tmp_Parts_SGST As Decimal = otherData.Parts_SGST
            Dim tmp_Parts_IGST As Decimal = otherData.Parts_IGST
            Dim tmp_Parts_Amount As Decimal = otherData.Parts_Amount

            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("amount : 0.00", fnt3))
            Else
                cell = New PdfPCell(New Paragraph("amount : " & (tmp_Parts_CGST + tmp_Parts_SGST + tmp_Parts_IGST + tmp_Parts_Amount).ToString, fnt3))
            End If
            cell.HorizontalAlignment = Element.ALIGN_RIGHT
            cell.FixedHeight = 16.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 6.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            'toatal
            cell = New PdfPCell(New Paragraph("total amount(Service costs+Parts costs+TAX)", fnt))
            cell.BackgroundColor = New BaseColor(186, 183, 224)
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            Dim dtNow As DateTime = DateTime.Now
            cell = New PdfPCell(New Paragraph("Quotation issue date: " & dtNow.ToShortDateString, fnt))
            cell.HorizontalAlignment = Element.ALIGN_RIGHT
            cell.FixedHeight = 16.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            cell = New PdfPCell(New Paragraph("　　I will request the repair with the above contents.", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            cell = New PdfPCell(New Paragraph("　　The expiration date of the quotation is  7days from the issue date.", fnt))
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            '最終合計金額
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("amount : 0.00INR ", fnt4))
            Else
                cell = New PdfPCell(New Paragraph("amount : " & ((tmp_Parts_CGST + tmp_Parts_SGST + tmp_Parts_IGST + tmp_Parts_Amount) + (tmp_CGST + tmp_Other_Freight_CGST) + (tmp_SGST + tmp_Other_Freight_SGST) + (tmp_IGST + tmp_Other_Freight_IGST) + (tmp_Labor_Amount + tmp_Other_Freight_Amount)).ToString & "INR", fnt4))
            End If
            cell.HorizontalAlignment = Element.ALIGN_RIGHT
            cell.FixedHeight = 23.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 6.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            'cell = New PdfPCell(New Paragraph("", fnt))
            'cell.FixedHeight = 45.0F
            'cell.Colspan = 8
            ''cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            ''cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            ''cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            ''cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            'table2.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 6.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            'サイン
            cell = New PdfPCell(New Paragraph("signature : ", fnt7))
            cell.HorizontalAlignment = Element.ALIGN_RIGHT
            cell.Colspan = 5
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            'サイン
            cell = New PdfPCell(New Paragraph("", fnt7))
            cell.Colspan = 3
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            doc.Add(table2)

            'クローズ 
            doc.Close()

        Catch ex As Exception
            errFlg = 1
        Finally
            If fileStream IsNot Nothing Then
                fileStream.Close()
            End If
        End Try

    End Sub

    '****************************************************
    '処理：作業報告書をPDFとして出力処理
    '引数：otherData   　画面からの入力情報　
    '　　　pdfFileName   PDFファイル名
    '      errFlg        戻り値  1:エラー　0：正常
    '      shipName      拠点名称 
    '****************************************************
    Public Sub WorkReportPDF(ByVal otherData As OTHER_DATA, ByVal pdfFileName As String, ByRef errFlg As Integer, ByVal shipName As String)

        '***PDF出力処理***
        Dim fileStream As FileStream

        Try
            Dim doc As Document
            Dim pdfWriter As PdfWriter
            Dim image1 As Image = Image.GetInstance(logoQuickGarage) '画像
            image1.ScalePercent(7) '大きさ

            'フォントの設定
            Dim fnt As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 12)
            Dim fntBa2 As New Font(BaseFont.CreateFont("c:\windows\fonts\CODE39.ttf", BaseFont.IDENTITY_H, True), 12) 'バーコード
            Dim fnt1 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 10)
            Dim fnt2 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 24, iTextSharp.text.Font.BOLD)
            Dim fnt3 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 12, iTextSharp.text.Font.UNDERLINE)
            Dim fnt4 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 16, iTextSharp.text.Font.UNDERLINE)
            Dim fnt5 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 10, iTextSharp.text.Font.UNDERLINE)
            Dim fnt6 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 8)
            Dim fnt7 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 16)

            'FileStreamを生成 
            fileStream = New FileStream(savePDFPass & pdfFileName, FileMode.Create)

            'Documentを生成 
            doc = New Document(PageSize.A4, 5, 5, 5, 5)

            'PdfWriter生成 
            pdfWriter = PdfWriter.GetInstance(doc, fileStream)

            'Documentのオープン 
            doc.Open()

            Dim pcb As PdfContentByte = pdfWriter.DirectContent

            'PDF表示行列設定
            Const pdfRow As Integer = 10

            'テーブル作成
            Dim table As PdfPTable
            table = New PdfPTable(pdfRow)
            table.TotalWidth = 100%

            '列の設定（10列）
            Dim widths As Single()
            widths = New Single() {1.0F, 1.0F, 1.0F, 0.5F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 0.5F}
            table.SetWidths(widths)

            '二つ目のPDF表示行列設定
            Const pdfRow2 As Integer = 8

            '二つ目のテーブル作成
            Dim table2 As PdfPTable
            table2 = New PdfPTable(pdfRow2)
            table2.TotalWidth = 100%

            '二つ目の列の設定（８列）
            Dim widths2 As Single()
            widths2 = New Single() {1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F, 1.0F}
            table2.SetWidths(widths2)

            'セル作成
            Dim cell As PdfPCell

            '件名
            doc.Add(New Paragraph("   Tax Invoice", fnt2))

            'Quick Garageのアイコンをセット
            Dim p As New Paragraph()
            p.Add(New Chunk(image1, 0, 0))
            p.Alignment = Element.ALIGN_RIGHT
            p.Add(" ")
            doc.Add(p)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '受付日
            cell = New PdfPCell(New Paragraph("Reception Date：" & otherData.Repair_Received_Date, fnt))
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'Po番号
            cell = New PdfPCell(New Paragraph("PO : " & otherData.po_no, fnt))
            'cell.FixedHeight = 26.0F
            cell.Colspan = 6
            table.AddCell(cell)

            cell = New PdfPCell(New Paragraph("*" & Right(otherData.po_no, Len(otherData.po_no) - 2) & "*", fntBa2))
            'cell.FixedHeight = 26.0F
            cell.Colspan = 4
            table.AddCell(cell)

            'グレーセル
            cell = New PdfPCell(New Paragraph("store information", fnt))
            cell.GrayFill = 0.8F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'ship name:
            cell = New PdfPCell(New Paragraph("name:", fnt))
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            cell = New PdfPCell(New Paragraph(otherData.ship_Name, fnt))
            cell.Colspan = 4
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'GSTIN:
            cell = New PdfPCell(New Paragraph("GSTIN:", fnt))
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            cell = New PdfPCell(New Paragraph(otherData.GSTIN, fnt))
            cell.Colspan = 4
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'e-mail:
            cell = New PdfPCell(New Paragraph("e-mail:", fnt))
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            cell = New PdfPCell(New Paragraph(otherData.Ship_Mail, fnt))
            cell.Colspan = 4
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'tel:
            cell = New PdfPCell(New Paragraph("tel:", fnt))
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            cell = New PdfPCell(New Paragraph(otherData.Ship_Tel, fnt))
            cell.Colspan = 4
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'address:
            cell = New PdfPCell(New Paragraph("address:", fnt))
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            cell = New PdfPCell(New Paragraph(otherData.Ship_Addr1, fnt))
            cell.Colspan = 9
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'グレーセル
            cell = New PdfPCell(New Paragraph("Declaration and work report", fnt))
            cell.GrayFill = 0.8F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'お客様深刻状況と改善状況の説明
            cell = New PdfPCell(New Paragraph(otherData.comment2, fnt))
            cell.FixedHeight = 60.0F
            cell.Colspan = 10
            table.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'お客様情報
            cell = New PdfPCell(New Paragraph("Customer Information", fnt))
            cell.FixedHeight = 16.0F
            cell.GrayFill = 0.8F
            cell.Colspan = 3
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '保証有無
            cell = New PdfPCell(New Paragraph("Warranty status : " & otherData.warranty, fnt))
            cell.HorizontalAlignment = Element.ALIGN_RIGHT
            cell.FixedHeight = 16.0F
            cell.GrayFill = 0.8F
            cell.Colspan = 7
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'product type
            cell = New PdfPCell(New Paragraph("product type : " & otherData.Product_Type, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 5
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'お支払方法
            cell = New PdfPCell(New Paragraph("Payment : " & otherData.denomi, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 4
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'name
            cell = New PdfPCell(New Paragraph("name : " & Left(otherData.Consumer_Name, 30), fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 5
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'tel
            cell = New PdfPCell(New Paragraph("telephone : " & otherData.Consumer_Telephone, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 4
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'mail address
            cell = New PdfPCell(New Paragraph("mail address : " & otherData.Customer_mail_address, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 9
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'serial No
            cell = New PdfPCell(New Paragraph("serial No : " & otherData.Serial_No, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 9
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'Maker
            cell = New PdfPCell(New Paragraph("Maker : " & otherData.Maker, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 9
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'Model
            cell = New PdfPCell(New Paragraph("Model : " & otherData.Model, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 9
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'address1
            cell = New PdfPCell(New Paragraph("address1 : " & otherData.Consumer_Addr1, fnt))
            cell.FixedHeight = 32.0F
            cell.Colspan = 9
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 32.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            'address2
            cell = New PdfPCell(New Paragraph("address2 : " & otherData.Consumer_Addr2, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 9
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            '余白用
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            ''address
            'cell = New PdfPCell(New Paragraph("address : " & otherData.Consumer_Addr1, fnt))
            'cell.FixedHeight = 34.0F
            'cell.Colspan = 10
            'table.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 10
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table.AddCell(cell)

            doc.Add(table)

            'ブルーセル：Service costs
            cell = New PdfPCell(New Paragraph("Service costs", fnt))
            cell.BackgroundColor = New BaseColor(167, 219, 162)
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            '項目
            cell = New PdfPCell(New Paragraph("item", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '名称
            cell = New PdfPCell(New Paragraph("name", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table2.AddCell(cell)

            '単価
            cell = New PdfPCell(New Paragraph("unit price", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '数量
            cell = New PdfPCell(New Paragraph("qty", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '金額
            cell = New PdfPCell(New Paragraph("Amount of money", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            '作業費用
            cell = New PdfPCell(New Paragraph("Labor", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '作業費名称
            cell = New PdfPCell(New Paragraph(otherData.LaborName, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table2.AddCell(cell)

            '単価
            cell = New PdfPCell(New Paragraph(otherData.gUnitPriceLabor, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '数量
            cell = New PdfPCell(New Paragraph(otherData.Labor_Qty, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '金額
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.Labor_Amount, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            '郵送費用
            cell = New PdfPCell(New Paragraph("Postage", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '名称
            cell = New PdfPCell(New Paragraph(otherData.ShipMentName, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table2.AddCell(cell)

            '単価
            cell = New PdfPCell(New Paragraph(otherData.gUnitPriceShipMent, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '数量
            cell = New PdfPCell(New Paragraph(otherData.ShipMent_Qty, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '金額
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.ShipMent_Price, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'その他費用
            cell = New PdfPCell(New Paragraph("Other", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '名称
            cell = New PdfPCell(New Paragraph(otherData.OtherName, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table2.AddCell(cell)

            '単価
            cell = New PdfPCell(New Paragraph(otherData.gUnitPriceOther, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '数量
            cell = New PdfPCell(New Paragraph(otherData.Other_Qty, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '金額
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.Other_Price, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            '④TAX
            cell = New PdfPCell(New Paragraph("TAX", fnt))
            cell.FixedHeight = 16.0F
            cell.GrayFill = 0.8F
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            cell.Colspan = 2
            cell.Rowspan = 2
            table2.AddCell(cell)

            'CGST
            cell = New PdfPCell(New Paragraph("CGST", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'SGST
            cell = New PdfPCell(New Paragraph("SGST", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'IGST
            cell = New PdfPCell(New Paragraph("IGST", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'CGST
            '計算用
            Dim tmp_CGST As Decimal
            Dim tmp_Other_Freight_CGST As Decimal

            tmp_CGST = otherData.CGST
            tmp_Other_Freight_CGST = otherData.Other_Freight_CGST

            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph((tmp_CGST + tmp_Other_Freight_CGST).ToString, fnt))
            End If

            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'SGST
            '計算用
            Dim tmp_SGST As Decimal
            Dim tmp_Other_Freight_SGST As Decimal

            tmp_SGST = otherData.SGST
            tmp_Other_Freight_SGST = otherData.Other_Freight_SGST

            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph((tmp_SGST + tmp_Other_Freight_SGST).ToString, fnt))
            End If

            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'IGST
            '計算用
            Dim tmp_IGST As Decimal
            Dim tmp_Other_Freight_IGST As Decimal

            tmp_IGST = otherData.IGST
            tmp_Other_Freight_IGST = otherData.Other_Freight_IGST

            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph((tmp_IGST + tmp_Other_Freight_IGST).ToString, fnt))
            End If

            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            '合計
            '計算用
            Dim tmp_Labor_Amount As Decimal = otherData.Labor_Amount
            Dim tmp_Other_Freight_Amount As Decimal = otherData.Other_Freight_Amount

            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("amount : 0.00", fnt3))
            Else
                cell = New PdfPCell(New Paragraph("amount : " & ((tmp_CGST + tmp_Other_Freight_CGST) + (tmp_SGST + tmp_Other_Freight_SGST) + (tmp_IGST + tmp_Other_Freight_IGST) + (tmp_Labor_Amount + tmp_Other_Freight_Amount)).ToString, fnt3))
            End If
            cell.HorizontalAlignment = Element.ALIGN_RIGHT
            cell.FixedHeight = 16.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            'ピンクセル：Parts costs
            cell = New PdfPCell(New Paragraph("Parts costs", fnt))
            cell.BackgroundColor = New BaseColor(239, 193, 196)
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            '項目
            cell = New PdfPCell(New Paragraph("item", fnt))
            cell.Colspan = 1
            table2.AddCell(cell)

            '名称
            cell = New PdfPCell(New Paragraph("name ", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table2.AddCell(cell)

            '単価
            cell = New PdfPCell(New Paragraph("unit price", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '数量
            cell = New PdfPCell(New Paragraph("qty", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '金額
            cell = New PdfPCell(New Paragraph("Amount of money", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'parts 1
            cell = New PdfPCell(New Paragraph("parts 1", fnt))
            cell.Colspan = 1
            table2.AddCell(cell)

            '名称
            cell = New PdfPCell(New Paragraph(Left(otherData.Parts1Name, 38), fnt6))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table2.AddCell(cell)

            '単価
            cell = New PdfPCell(New Paragraph(otherData.gUnitPriceParts1, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '数量
            cell = New PdfPCell(New Paragraph(otherData.Qty_1, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '金額
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.GUnit_Price_1, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'parts 2
            cell = New PdfPCell(New Paragraph("parts 2", fnt))
            cell.Colspan = 1
            table2.AddCell(cell)

            '部品名称
            cell = New PdfPCell(New Paragraph(Left(otherData.Parts2Name, 38), fnt6))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table2.AddCell(cell)

            '単価
            cell = New PdfPCell(New Paragraph(otherData.gUnitPriceParts2, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '数量
            cell = New PdfPCell(New Paragraph(otherData.Qty_2, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '金額
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.GUnit_Price_2, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'parts 3
            cell = New PdfPCell(New Paragraph("parts 3", fnt))
            cell.Colspan = 1
            table2.AddCell(cell)

            '部品名称
            cell = New PdfPCell(New Paragraph(Left(otherData.Parts3Name, 38), fnt6))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table2.AddCell(cell)

            '単価
            cell = New PdfPCell(New Paragraph(otherData.gUnitPriceParts3, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '数量
            cell = New PdfPCell(New Paragraph(otherData.Qty_3, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '金額
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.GUnit_Price_3, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'parts 4
            cell = New PdfPCell(New Paragraph("parts 4", fnt))
            cell.Colspan = 1
            table2.AddCell(cell)

            '部品名称
            cell = New PdfPCell(New Paragraph(Left(otherData.Parts4Name, 38), fnt6))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table2.AddCell(cell)

            '単価
            cell = New PdfPCell(New Paragraph(otherData.gUnitPriceParts4, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '数量
            cell = New PdfPCell(New Paragraph(otherData.Qty_4, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '金額
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.GUnit_Price_4, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'parts 5
            cell = New PdfPCell(New Paragraph("parts 5", fnt))
            cell.Colspan = 1
            table2.AddCell(cell)

            '部品名称
            cell = New PdfPCell(New Paragraph(Left(otherData.Parts5Name, 38), fnt6))
            cell.FixedHeight = 16.0F
            cell.Colspan = 3
            table2.AddCell(cell)

            '単価
            cell = New PdfPCell(New Paragraph(otherData.gUnitPriceParts5, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '数量
            cell = New PdfPCell(New Paragraph(otherData.Qty_5, fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 1
            table2.AddCell(cell)

            '金額
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.GUnit_Price_5, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'TAX
            cell = New PdfPCell(New Paragraph("TAX", fnt))
            cell.FixedHeight = 16.0F
            cell.GrayFill = 0.8F
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            cell.Colspan = 2
            cell.Rowspan = 2
            table2.AddCell(cell)

            'CGST
            cell = New PdfPCell(New Paragraph("CGST", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'SGST
            cell = New PdfPCell(New Paragraph("SGST", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'IGST
            cell = New PdfPCell(New Paragraph("IGST", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'CGST
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.Parts_CGST, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'SGST
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.Parts_SGST, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            'IGST
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("0.00", fnt))
            Else
                cell = New PdfPCell(New Paragraph(otherData.Parts_IGST, fnt))
            End If
            cell.FixedHeight = 16.0F
            cell.Colspan = 2
            table2.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 4.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            '合計
            Dim tmp_Parts_CGST As Decimal = otherData.Parts_CGST
            Dim tmp_Parts_SGST As Decimal = otherData.Parts_SGST
            Dim tmp_Parts_IGST As Decimal = otherData.Parts_IGST
            Dim tmp_Parts_Amount As Decimal = otherData.Parts_Amount

            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("amount : 0.00", fnt3))
            Else
                cell = New PdfPCell(New Paragraph("amount : " & (tmp_Parts_CGST + tmp_Parts_SGST + tmp_Parts_IGST + tmp_Parts_Amount).ToString, fnt3))
            End If
            cell.HorizontalAlignment = Element.ALIGN_RIGHT
            cell.FixedHeight = 16.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 6.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            'toatal
            cell = New PdfPCell(New Paragraph("total amount(Service costs+Parts costs+TAX)", fnt))
            cell.BackgroundColor = New BaseColor(186, 183, 224)
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            cell = New PdfPCell(New Paragraph("Repair completed date: " & otherData.Completed_Date, fnt))
            cell.HorizontalAlignment = Element.ALIGN_RIGHT
            cell.FixedHeight = 16.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            cell = New PdfPCell(New Paragraph("　　I will accept the above contents.", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            cell = New PdfPCell(New Paragraph("　　Repair warranty is 90 days.", fnt))
            cell.FixedHeight = 16.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            '最終合計金額
            If otherData.warranty = "in warranty" Then
                cell = New PdfPCell(New Paragraph("amount : 0.00INR", fnt4))
            Else
                cell = New PdfPCell(New Paragraph("amount : " & ((tmp_Parts_CGST + tmp_Parts_SGST + tmp_Parts_IGST + tmp_Parts_Amount) + (tmp_CGST + tmp_Other_Freight_CGST) + (tmp_SGST + tmp_Other_Freight_SGST) + (tmp_IGST + tmp_Other_Freight_IGST) + (tmp_Labor_Amount + tmp_Other_Freight_Amount)).ToString & "INR", fnt4))
            End If
            cell.HorizontalAlignment = Element.ALIGN_RIGHT
            cell.FixedHeight = 23.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            '余白用(下の行と重なるのを防ぐ)
            cell = New PdfPCell(New Paragraph("", fnt))
            cell.FixedHeight = 6.0F
            cell.Colspan = 8
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            'サイン
            cell = New PdfPCell(New Paragraph("signature : ", fnt7))
            cell.HorizontalAlignment = Element.ALIGN_RIGHT
            cell.Colspan = 5
            cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            'サイン
            cell = New PdfPCell(New Paragraph("", fnt7))
            cell.Colspan = 3
            'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
            cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
            table2.AddCell(cell)

            doc.Add(table2)

            'クローズ 
            doc.Close()

        Catch ex As Exception
            errFlg = 1
        Finally
            If fileStream IsNot Nothing Then
                fileStream.Close()
            End If
        End Try

    End Sub

    '****************************************************
    '処理：CSVファイルのヘッダ情報を確認
    '引数：colsHead()   　読み込んだCSVファイルのヘッダ情報　
    '　　　csvKind  　　　CSVファイルの種類名称　GSPN:GSPNData　入力情報：inputData その他売り上げ情報：otherData　
    '返却：FALSE header⇒NG　 TRRUE　header⇒OK 　
    '****************************************************
    Public Function chkHead(ByVal colsHead() As String, ByVal csvKind As String) As Boolean

        If csvKind = "GSPN" Then

            If colsHead(0) <> "ASC Code" Then
                Return False
            End If

            If colsHead(1) <> "Branch Code" Then
                Return False
            End If

            If colsHead(2) <> "ASC Claim No" Then
                Return False
            End If

            If colsHead(3) <> "Samsung Claim No" Then
                Return False
            End If

            If colsHead(4) <> "Service Type" Then
                Return False
            End If

            If colsHead(5) <> "Consumer Name" Then
                Return False
            End If

            If colsHead(6) <> "Consumer Addr1" Then
                Return False
            End If

            If colsHead(7) <> "Consumer Addr2" Then
                Return False
            End If

            If colsHead(8) <> "Consumer Telephone" Then
                Return False
            End If

            If colsHead(9) <> "Consumer Fax" Then
                Return False
            End If

            If colsHead(10) <> "Postal Code" Then
                Return False
            End If

            If colsHead(11) <> "Model" Then
                Return False
            End If

            If colsHead(12) <> "Serial No" Then
                Return False
            End If

            If colsHead(13) <> "IMEI No" Then
                Return False
            End If

            If colsHead(14) <> "Defect Type" Then
                Return False
            End If

            If colsHead(15) <> "Condition" Then
                Return False
            End If

            If colsHead(16) <> "Symptom" Then
                Return False
            End If

            If colsHead(17) <> "Defect Code" Then
                Return False
            End If

            If colsHead(18) <> "Repair Code" Then
                Return False
            End If

            If colsHead(19) <> "Defect Desc" Then
                Return False
            End If

            If colsHead(20) <> "Repair Description" Then
                Return False
            End If

            If colsHead(21) <> "Purchase Date" Then
                Return False
            End If

            If colsHead(22) <> "Repair Received Date" Then
                Return False
            End If

            If colsHead(23) <> "Completed Date" Then
                Return False
            End If

            If colsHead(24) <> "Delivery Date" Then
                Return False
            End If

            If colsHead(25) <> "Production Date" Then
                Return False
            End If

            If colsHead(26) <> "Labor Amount" Then
                Return False
            End If

            If colsHead(27) <> "Parts Amount" Then
                Return False
            End If

            If colsHead(28) <> "Freight" Then
                Return False
            End If

            If colsHead(29) <> "Other" Then
                Return False
            End If

            If colsHead(30) <> "Parts SGST" Then
                Return False
            End If

            If colsHead(31) <> "Parts UTGST" Then
                Return False
            End If

            If colsHead(32) <> "Parts CGST" Then
                Return False
            End If

            If colsHead(33) <> "Parts IGST" Then
                Return False
            End If

            If colsHead(34) <> "Parts Cess" Then
                Return False
            End If

            If colsHead(35) <> "SGST" Then
                Return False
            End If

            If colsHead(36) <> "UTGST" Then
                Return False
            End If

            If colsHead(37) <> "CGST" Then
                Return False
            End If

            If colsHead(38) <> "IGST" Then
                Return False
            End If

            If colsHead(39) <> "Cess" Then
                Return False
            End If

            If colsHead(40) <> "Total Invoice Amount" Then
                Return False
            End If

            If colsHead(41) <> "Remark" Then
                Return False
            End If

            If colsHead(42) <> "Tr No" Then
                Return False
            End If

            If colsHead(43) <> "Tr Type" Then
                Return False
            End If

            If colsHead(44) <> "Status" Then
                Return False
            End If

            If colsHead(45) <> "Engineer" Then
                Return False
            End If

            If colsHead(46) <> "Collection Point" Then
                Return False
            End If

            If colsHead(47) <> "Collection Point Name" Then
                Return False
            End If

            If colsHead(48) <> "Location-1" Then
                Return False
            End If

            If colsHead(49) <> "Part-1" Then
                Return False
            End If

            If colsHead(50) <> "Qty-1" Then
                Return False
            End If

            If colsHead(51) <> "Unit Price-1" Then
                Return False
            End If

            If colsHead(52) <> "Doc Num-1" Then
                Return False
            End If

            If colsHead(53) <> "Matrial Serial-1" Then
                Return False
            End If

            If colsHead(54) <> "Location-2" Then
                Return False
            End If

            If colsHead(55) <> "Part-2" Then
                Return False
            End If

            If colsHead(56) <> "Qty-2" Then
                Return False
            End If

            If colsHead(57) <> "Unit Price-2" Then
                Return False
            End If

            If colsHead(58) <> "Doc Num-2" Then
                Return False
            End If

            If colsHead(59) <> "Matrial Serial-2" Then
                Return False
            End If

            If colsHead(60) <> "Location-3" Then
                Return False
            End If

            If colsHead(61) <> "Part-3" Then
                Return False
            End If

            If colsHead(62) <> "Qty-3" Then
                Return False
            End If

            If colsHead(63) <> "Unit Price-3" Then
                Return False
            End If

            If colsHead(64) <> "Doc Num-3" Then
                Return False
            End If

            If colsHead(65) <> "Matrial Serial-3" Then
                Return False
            End If

            If colsHead(66) <> "Location-4" Then
                Return False
            End If

            If colsHead(67) <> "Part-4" Then
                Return False
            End If

            If colsHead(68) <> "Qty-4" Then
                Return False
            End If

            If colsHead(69) <> "Unit Price-4" Then
                Return False
            End If

            If colsHead(70) <> "Doc Num-4" Then
                Return False
            End If

            If colsHead(71) <> "Matrial Serial-4" Then
                Return False
            End If

            If colsHead(72) <> "Location-5" Then
                Return False
            End If

            If colsHead(73) <> "Part-5" Then
                Return False
            End If

            If colsHead(74) <> "Qty-5" Then
                Return False
            End If

            If colsHead(75) <> "Unit Price-5" Then
                Return False
            End If

            If colsHead(76) <> "Doc Num-5" Then
                Return False
            End If

            If colsHead(77) <> "Matrial Serial-5" Then
                Return False
            End If

            If colsHead(78) <> "Location-6" Then
                Return False
            End If

            If colsHead(79) <> "Part-6" Then
                Return False
            End If

            If colsHead(80) <> "Qty-6" Then
                Return False
            End If

            If colsHead(81) <> "Unit Price-6" Then
                Return False
            End If

            If colsHead(82) <> "Doc Num-6" Then
                Return False
            End If

            If colsHead(83) <> "Matrial Serial-6" Then
                Return False
            End If

            If colsHead(84) <> "Location-7" Then
                Return False
            End If

            If colsHead(85) <> "Part-7" Then
                Return False
            End If

            If colsHead(86) <> "Qty-7" Then
                Return False
            End If

            If colsHead(87) <> "Unit Price-7" Then
                Return False
            End If

            If colsHead(88) <> "Doc Num-7" Then
                Return False
            End If

            If colsHead(89) <> "Matrial Serial-7" Then
                Return False
            End If

            If colsHead(90) <> "Location-8" Then
                Return False
            End If

            If colsHead(91) <> "Part-8" Then
                Return False
            End If

            If colsHead(92) <> "Qty-8" Then
                Return False
            End If

            If colsHead(93) <> "Unit Price-8" Then
                Return False
            End If

            If colsHead(94) <> "Doc Num-8" Then
                Return False
            End If

            If colsHead(95) <> "Matrial Serial-8" Then
                Return False
            End If

            If colsHead(96) <> "Location-9" Then
                Return False
            End If

            If colsHead(97) <> "Part-9" Then
                Return False
            End If

            If colsHead(98) <> "Qty-9" Then
                Return False
            End If

            If colsHead(99) <> "Unit Price-9" Then
                Return False
            End If

            If colsHead(100) <> "Doc Num-9" Then
                Return False
            End If

            If colsHead(101) <> "Matrial Serial-9" Then
                Return False
            End If

            If colsHead(102) <> "Location-10" Then
                Return False
            End If

            If colsHead(103) <> "Part-10" Then
                Return False
            End If

            If colsHead(104) <> "Qty-10" Then
                Return False
            End If

            If colsHead(105) <> "Unit Price-10" Then
                Return False
            End If

            If colsHead(106) <> "Doc Num-10" Then
                Return False
            End If

            If colsHead(107) <> "Matrial Serial-10" Then
                Return False
            End If

            If colsHead(108) <> "Location-11" Then
                Return False
            End If

            If colsHead(109) <> "Part-11" Then
                Return False
            End If

            If colsHead(110) <> "Qty-11" Then
                Return False
            End If

            If colsHead(111) <> "Unit Price-11" Then
                Return False
            End If

            If colsHead(112) <> "Doc Num-11" Then
                Return False
            End If

            If colsHead(113) <> "Matrial Serial-11" Then
                Return False
            End If

            If colsHead(114) <> "Location-12" Then
                Return False
            End If

            If colsHead(115) <> "Part-12" Then
                Return False
            End If

            If colsHead(116) <> "Qty-12" Then
                Return False
            End If

            If colsHead(117) <> "Unit Price-12" Then
                Return False
            End If

            If colsHead(118) <> "Doc Num-12" Then
                Return False
            End If

            If colsHead(119) <> "Matrial Serial-12" Then
                Return False
            End If

            If colsHead(120) <> "Location-13" Then
                Return False
            End If

            If colsHead(121) <> "Part-13" Then
                Return False
            End If

            If colsHead(122) <> "Qty-13" Then
                Return False
            End If

            If colsHead(123) <> "Unit Price-13" Then
                Return False
            End If

            If colsHead(124) <> "Doc Num-13" Then
                Return False
            End If

            If colsHead(125) <> "Matrial Serial-13" Then
                Return False
            End If

            If colsHead(126) <> "Location-14" Then
                Return False
            End If

            If colsHead(127) <> "Part-14" Then
                Return False
            End If

            If colsHead(128) <> "Qty-14" Then
                Return False
            End If

            If colsHead(129) <> "Unit Price-14" Then
                Return False
            End If

            If colsHead(130) <> "Doc Num-14" Then
                Return False
            End If

            If colsHead(131) <> "Matrial Serial-14" Then
                Return False
            End If

            If colsHead(132) <> "Location-15" Then
                Return False
            End If

            If colsHead(133) <> "Part-15" Then
                Return False
            End If

            If colsHead(134) <> "Qty-15" Then
                Return False
            End If

            If colsHead(135) <> "Unit Price-15" Then
                Return False
            End If

            If colsHead(136) <> "Doc Num-15" Then
                Return False
            End If

            If colsHead(137) <> "Matrial Serial-15" Then
                Return False
            End If

        ElseIf csvKind = "inputData" Then

            If colsHead(0) <> "rec_datetime" Then
                Return False
            End If

            If colsHead(1) <> "rec_yuser" Then
                Return False
            End If

            If colsHead(2) <> "rpt_counter" Then
                Return False
            End If

            If colsHead(3) <> "rpt_repair" Then
                Return False
            End If

            If colsHead(4) <> "close_datetime" Then
                Return False
            End If

            If colsHead(5) <> "denomi" Then
                Return False
            End If

            If colsHead(6) <> "amount" Then
                Return False
            End If

            If colsHead(7) <> "asc_c_num" Then
                Return False
            End If

            If colsHead(8) <> "sam_c_num" Then
                Return False
            End If

            If colsHead(9) <> "comment" Then
                Return False
            End If

        ElseIf csvKind = "otherData" Then

            If colsHead(0) <> "rec_datetime" Then
                Return False
            End If

            If colsHead(1) <> "rec_yuser" Then
                Return False
            End If

            If colsHead(2) <> "rpt_counter" Then
                Return False
            End If

            If colsHead(3) <> "rpt_repair" Then
                Return False
            End If

            If colsHead(4) <> "close_datetime" Then
                Return False
            End If

            If colsHead(5) <> "denomi" Then
                Return False
            End If

            If colsHead(6) <> "amount" Then
                Return False
            End If

            If colsHead(7) <> "comment" Then
                Return False
            End If

            If colsHead(8) <> "Consumer_Name" Then
                Return False
            End If

            If colsHead(9) <> "Consumer_Addr1" Then
                Return False
            End If

            If colsHead(10) <> "Consumer_Addr2" Then
                Return False
            End If

            If colsHead(11) <> "Consumer_Telephone" Then
                Return False
            End If

            If colsHead(12) <> "Consumer_Fax" Then
                Return False
            End If

            If colsHead(13) <> "Postal_Code" Then
                Return False
            End If

            If colsHead(14) <> "Model" Then
                Return False
            End If

            If colsHead(15) <> "Serial_No" Then
                Return False
            End If

            If colsHead(16) <> "IMEI_No" Then
                Return False
            End If

            If colsHead(17) <> "Defect_Type" Then
                Return False
            End If

            If colsHead(18) <> "Repair_Description" Then
                Return False
            End If

            If colsHead(19) <> "Repair_Received_Date" Then
                Return False
            End If

            If colsHead(20) <> "Completed_Date" Then
                Return False
            End If

            If colsHead(21) <> "Delivery_Date" Then
                Return False
            End If

            If colsHead(22) <> "Labor_Amount" Then
                Return False
            End If

            If colsHead(23) <> "Parts_Amount" Then
                Return False
            End If

            If colsHead(24) <> "Parts_SGST" Then
                Return False
            End If

            If colsHead(25) <> "Parts_CGST" Then
                Return False
            End If

            If colsHead(26) <> "Total_Invoice_Amount" Then
                Return False
            End If

        End If

        Return True

    End Function

    '****************************************************
    '処理　　：〒ボタン押下処理の確認確認
    '処理概要：郵便番号チェック　州名称との不整合はNGを返す
    '引数　　：postCord  〒番号　
    '　　　　　stateName 〒番号に紐づく州名称
    '          errMsg　　エラー内容をセット  
    '返却　　：FALSE ⇒NG　 TRRUE　⇒OK 　
    '****************************************************
    Public Function chkPostCord(ByVal postCord As String, ByVal stateName As String, ByRef errMsg As String) As Boolean

        Dim dsM_Postal As New DataSet
        Dim errFlg As Integer
        Dim sqlStr As String = ""

        If stateName = "select state" Or stateName = "" Then
            errMsg = "Please select the state <br />Or, specify the code and press the button. <br />※Leave the code empty and all the messages will be displayed with the  button pressed."
            Exit Function
        End If

        '郵便番号と州名称の不整合を確認
        If postCord <> "" Then

            sqlStr = "SELECT * FROM dbo.M_Postal WHERE zip_code = '" & postCord & "';"
            dsM_Postal = DBCommon.Get_DS(sqlStr, errFlg)

            If errFlg = 1 Then
                errMsg = "Failed to get information on M_Postal"
                Exit Function
            End If

            If dsM_Postal Is Nothing Then
                errMsg = "The postal code is not registered"
                Exit Function
            Else

                Dim dr As DataRow = dsM_Postal.Tables(0).Rows(0)

                If dr("state") IsNot DBNull.Value Then
                    If dr("state") = stateName Then
                        Return True
                    Else
                        Return False
                    End If
                End If

            End If

        Else
            '州名称のみはOK
            Return True
        End If

    End Function
    '****************************************************
    '処理　　：更新処理可否チェック
    '処理概要：入力項目の各データと登録済のそのデータが一致するか確認
    '          一致していれば、更新不要の為TRUEを返す
    '引数　　：otherData  入力データ　
    '        ：dsWork     T_repair1のデータセット  
    '返却　　：FALSE ⇒DB登録　 TRRUE　⇒更新不要 　
    '****************************************************
    Public Function updateChk(ByVal otherData As OTHER_DATA, ByVal dsWork As DataSet, ByRef errMsg As String) As Boolean

        Try

            If dsWork IsNot Nothing Then

                If dsWork.Tables(0).Rows.Count = 1 Then

                    Dim dr As DataRow = dsWork.Tables(0).Rows(0)

                    '■受付情報
                    If dr("comment") IsNot DBNull.Value Then
                        If otherData.comment <> dr("comment") Then
                            Return False
                        End If
                    Else
                        If otherData.comment <> "" Then
                            Return False
                        End If
                    End If

                    If dr("comment2") IsNot DBNull.Value Then
                        If otherData.comment2 <> dr("comment2") Then
                            Return False
                        End If
                    Else
                        If otherData.comment2 <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Consumer_Name") IsNot DBNull.Value Then
                        If otherData.Consumer_Name <> dr("Consumer_Name") Then
                            Return False
                        End If
                    Else
                        If otherData.Consumer_Name <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Consumer_Addr1") IsNot DBNull.Value Then
                        If otherData.Consumer_Addr1 <> dr("Consumer_Addr1") Then
                            Return False
                        End If
                    Else
                        If otherData.Consumer_Addr1 <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Consumer_Addr2") IsNot DBNull.Value Then
                        If otherData.Consumer_Addr2 <> dr("Consumer_Addr2") Then
                            Return False
                        End If
                    Else
                        If otherData.Consumer_Addr2 <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Consumer_MailAddress") IsNot DBNull.Value Then
                        If otherData.Customer_mail_address <> dr("Consumer_MailAddress") Then
                            Return False
                        End If
                    Else
                        If otherData.Customer_mail_address <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Consumer_Telephone") IsNot DBNull.Value Then
                        If otherData.Consumer_Telephone <> dr("Consumer_Telephone") Then
                            Return False
                        End If
                    Else
                        If otherData.Consumer_Telephone <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Consumer_Fax") IsNot DBNull.Value Then
                        If otherData.Consumer_Fax <> dr("Consumer_Fax") Then
                            Return False
                        End If
                    Else
                        If otherData.Consumer_Fax <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Postal_Code") IsNot DBNull.Value Then
                        If otherData.Postal_Code <> dr("Postal_Code") Then
                            Return False
                        End If
                    Else
                        If otherData.Postal_Code <> "" Then
                            Return False
                        End If
                    End If

                    If dr("State_Name") IsNot DBNull.Value Then
                        If otherData.State_Name <> dr("State_Name") Then
                            Return False
                        End If
                    Else
                        If otherData.State_Name <> "" Then
                            Return False
                        End If
                    End If


                    If dr("Model") IsNot DBNull.Value Then
                        If otherData.Model <> dr("Model") Then
                            Return False
                        End If
                    Else
                        If otherData.Model <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Serial_No") IsNot DBNull.Value Then
                        If otherData.Serial_No <> dr("Serial_No") Then
                            Return False
                        End If
                    Else
                        If otherData.Serial_No <> "" Then
                            Return False
                        End If
                    End If

                    If dr("IMEI_No") IsNot DBNull.Value Then
                        If otherData.IMEI_No <> dr("IMEI_No") Then
                            Return False
                        End If
                    Else
                        If otherData.IMEI_No <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Repair_Description") IsNot DBNull.Value Then
                        If otherData.Repair_Description <> dr("Repair_Description") Then
                            Return False
                        End If
                    Else
                        If otherData.Repair_Description <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Repair_Received_Date") IsNot DBNull.Value Then
                        If otherData.Repair_Received_Date <> dr("Repair_Received_Date") Then
                            Return False
                        End If
                    Else
                        If otherData.Repair_Received_Date <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Maker") IsNot DBNull.Value Then
                        If otherData.Maker <> dr("Maker") Then
                            Return False
                        End If
                    Else
                        If otherData.Maker <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Product_Type") IsNot DBNull.Value Then
                        If otherData.Product_Type <> dr("Product_Type") Then
                            Return False
                        End If
                    Else
                        If otherData.Product_Type <> "" Then
                            Return False
                        End If
                    End If

                    If dr("warranty") IsNot DBNull.Value Then
                        If otherData.warranty <> dr("warranty") Then
                            Return False
                        End If
                    Else
                        If otherData.warranty <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Consumer_MailAddress") IsNot DBNull.Value Then
                        If otherData.Customer_mail_address <> dr("Consumer_MailAddress") Then
                            Return False
                        End If
                    Else
                        If otherData.Customer_mail_address <> "" Then
                            Return False
                        End If
                    End If

                    '■見積完了情報
                    If dr("Completed_Date") IsNot DBNull.Value Then
                        Dim CompletedDate As DateTime
                        CompletedDate = dr("Completed_Date")
                        If otherData.Completed_Date <> CompletedDate.ToShortDateString Then
                            Return False
                        End If
                    Else
                        If otherData.Completed_Date <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Delivery_Date") IsNot DBNull.Value Then
                        Dim DeliveryDate As DateTime
                        DeliveryDate = dr("Delivery_Date")
                        If otherData.Delivery_Date <> DeliveryDate.ToShortDateString Then
                            Return False
                        End If
                    Else
                        If otherData.Delivery_Date <> "" Then
                            Return False
                        End If
                    End If

                    If dr("rec_datetime") IsNot DBNull.Value Then
                        Dim recDatetime As DateTime
                        recDatetime = dr("rec_datetime")
                        If otherData.rec_datetime <> recDatetime.ToShortDateString Then
                            Return False
                        End If
                    Else
                        If otherData.rec_datetime <> "" Then
                            Return False
                        End If
                    End If

                    If dr("denomi") IsNot DBNull.Value Then
                        If otherData.denomi <> dr("denomi") Then
                            Return False
                        End If
                    Else
                        If otherData.denomi <> "" Then
                            Return False
                        End If
                    End If

                    If dr("rec_yuser") IsNot DBNull.Value Then
                        If otherData.rec_yuser <> dr("rec_yuser") Then
                            Return False
                        End If
                    Else
                        If otherData.rec_yuser <> "" Then
                            Return False
                        End If
                    End If

                    '■部品情報
                    If dr("Part_1") IsNot DBNull.Value Then
                        If otherData.Part_1 <> dr("Part_1") Then
                            Return False
                        End If
                    Else
                        If otherData.Part_1 <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Qty_1") IsNot DBNull.Value Then
                        If otherData.Qty_1 <> dr("Qty_1") Then
                            Return False
                        End If
                    Else
                        If otherData.Qty_1 <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Unit_Price_1") IsNot DBNull.Value Then
                        If otherData.GUnit_Price_1 <> setINR((dr("Unit_Price_1")).ToString) Then
                            Return False
                        End If
                    Else
                        If otherData.GUnit_Price_1 <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Part_2") IsNot DBNull.Value Then
                        If otherData.Part_2 <> dr("Part_2") Then
                            Return False
                        End If
                    Else
                        If otherData.Part_2 <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Qty_2") IsNot DBNull.Value Then
                        If otherData.Qty_2 <> dr("Qty_2") Then
                            Return False
                        End If
                    Else
                        If otherData.Qty_2 <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Unit_Price_2") IsNot DBNull.Value Then
                        If otherData.GUnit_Price_2 <> setINR((dr("Unit_Price_2")).ToString) Then
                            Return False
                        End If
                    Else
                        If otherData.GUnit_Price_2 <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Part_3") IsNot DBNull.Value Then
                        If otherData.Part_3 <> dr("Part_3") Then
                            Return False
                        End If
                    Else
                        If otherData.Part_3 <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Qty_3") IsNot DBNull.Value Then
                        If otherData.Qty_3 <> dr("Qty_3") Then
                            Return False
                        End If
                    Else
                        If otherData.Qty_3 <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Unit_Price_3") IsNot DBNull.Value Then
                        If otherData.GUnit_Price_3 <> setINR((dr("Unit_Price_3")).ToString) Then
                            Return False
                        End If
                    Else
                        If otherData.GUnit_Price_3 <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Part_4") IsNot DBNull.Value Then
                        If otherData.Part_4 <> dr("Part_4") Then
                            Return False
                        End If
                    Else
                        If otherData.Part_4 <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Qty_4") IsNot DBNull.Value Then
                        If otherData.Qty_4 <> dr("Qty_4") Then
                            Return False
                        End If
                    Else
                        If otherData.Qty_4 <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Unit_Price_4") IsNot DBNull.Value Then
                        If otherData.GUnit_Price_4 <> setINR((dr("Unit_Price_4")).ToString) Then
                            Return False
                        End If
                    Else
                        If otherData.GUnit_Price_4 <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Part_5") IsNot DBNull.Value Then
                        If otherData.Part_5 <> dr("Part_5") Then
                            Return False
                        End If
                    Else
                        If otherData.Part_5 <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Qty_5") IsNot DBNull.Value Then
                        If otherData.Qty_5 <> dr("Qty_5") Then
                            Return False
                        End If
                    Else
                        If otherData.Qty_5 <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Unit_Price_5") IsNot DBNull.Value Then
                        If otherData.GUnit_Price_5 <> setINR((dr("Unit_Price_5")).ToString) Then
                            Return False
                        End If
                    Else
                        If otherData.GUnit_Price_5 <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Labor_No") IsNot DBNull.Value Then
                        If otherData.Labor_No <> dr("Labor_No") Then
                            Return False
                        End If
                    Else
                        If otherData.Labor_No <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Labor_Qty") IsNot DBNull.Value Then
                        If otherData.Labor_Qty <> dr("Labor_Qty") Then
                            Return False
                        End If
                    Else
                        If otherData.Labor_Qty <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Labor_Amount") IsNot DBNull.Value Then
                        If otherData.Labor_Amount <> dr("Labor_Amount") Then
                            Return False
                        End If
                    Else
                        If otherData.Labor_Amount <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Parts_Amount") IsNot DBNull.Value Then
                        If otherData.Parts_Amount <> dr("Parts_Amount") Then
                            Return False
                        End If
                    Else
                        If otherData.Parts_Amount <> "" Then
                            Return False
                        End If
                    End If

                    If dr("SGST") IsNot DBNull.Value Then
                        If otherData.SGST <> dr("SGST") Then
                            Return False
                        End If
                    Else
                        If otherData.SGST <> "" Then
                            Return False
                        End If
                    End If

                    If dr("IGST") IsNot DBNull.Value Then
                        If otherData.IGST <> dr("IGST") Then
                            Return False
                        End If
                    Else
                        If otherData.IGST <> "" Then
                            Return False
                        End If
                    End If

                    If dr("CGST") IsNot DBNull.Value Then
                        If otherData.CGST <> dr("CGST") Then
                            Return False
                        End If
                    Else
                        If otherData.CGST <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Parts_SGST") IsNot DBNull.Value Then
                        If otherData.Parts_SGST <> dr("Parts_SGST") Then
                            Return False
                        End If
                    Else
                        If otherData.Parts_SGST <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Parts_IGST") IsNot DBNull.Value Then
                        If otherData.Parts_IGST <> dr("Parts_IGST") Then
                            Return False
                        End If
                    Else
                        If otherData.Parts_IGST <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Parts_CGST") IsNot DBNull.Value Then
                        If otherData.Parts_CGST <> dr("Parts_CGST") Then
                            Return False
                        End If
                    Else
                        If otherData.Parts_CGST <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Freight") IsNot DBNull.Value Then
                        If otherData.ShipMent_No <> dr("Freight") Then
                            Return False
                        End If
                    Else
                        If otherData.ShipMent_No <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Freight_Qty") IsNot DBNull.Value Then
                        If otherData.ShipMent_Qty <> dr("Freight_Qty") Then
                            Return False
                        End If
                    Else
                        If otherData.ShipMent_Qty <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Freight_Price") IsNot DBNull.Value Then
                        If otherData.ShipMent_Price <> dr("Freight_Price") Then
                            Return False
                        End If
                    Else
                        If otherData.ShipMent_Price <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Other") IsNot DBNull.Value Then
                        If otherData.Other_No <> dr("Other") Then
                            Return False
                        End If
                    Else
                        If otherData.Other_No <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Other_Qty") IsNot DBNull.Value Then
                        If otherData.Other_Qty <> dr("Other_Qty") Then
                            Return False
                        End If
                    Else
                        If otherData.Other_Qty <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Other_Price") IsNot DBNull.Value Then
                        If otherData.Other_Price <> dr("Other_Price") Then
                            Return False
                        End If
                    Else
                        If otherData.Other_Price <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Other_Freight_Amount") IsNot DBNull.Value Then
                        If otherData.Other_Freight_Amount <> dr("Other_Freight_Amount") Then
                            Return False
                        End If
                    Else
                        If otherData.Other_Freight_Amount <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Other_Freight_SGST") IsNot DBNull.Value Then
                        If otherData.Other_Freight_SGST <> dr("Other_Freight_SGST") Then
                            Return False
                        End If
                    Else
                        If otherData.Other_Freight_SGST <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Other_Freight_CGST") IsNot DBNull.Value Then
                        If otherData.Other_Freight_CGST <> dr("Other_Freight_CGST") Then
                            Return False
                        End If
                    Else
                        If otherData.Other_Freight_CGST <> "" Then
                            Return False
                        End If
                    End If

                    If dr("Other_Freight_IGST") IsNot DBNull.Value Then
                        If otherData.Other_Freight_IGST <> dr("Other_Freight_IGST") Then
                            Return False
                        End If
                    Else
                        If otherData.Other_Freight_IGST <> "" Then
                            Return False
                        End If
                    End If

                End If

            End If

            Return True

        Catch ex As Exception
            errMsg = "Updatability check processing to T_repair 1 failed"
        End Try

    End Function
    '****************************************************
    '処理　　：chkSyoriOpenClose
    '処理概要：レジ点検終了確認
    '引数　　：syoriStatus  　
    '        ：shipCode
    '        ：SyoriFlg　　　　戻り値　レジ点検済はTRUEをセット（確認者のチェック完了で終了）
    '        ：reserveData　　 戻り値　T_Reserveの情報
    '                          date_time 処理時間をセット
    '                          username  処理対応者をセット　
    '                          diff　　　Open 預かり金との差額をセット　Open以外　売り上げ金額とレジチェック金額との差額をセット  
    '                          reserve   預かり金をセット（ship_baseの金額と同じ）
    '                          M_ Coin_  金種情報をセット 
    '        ：errMsg　　　　　戻り値
    '****************************************************
    Public Sub chkSyoriOpenClose(ByVal syoriStatus As String, ByVal shipCode As String, ByRef SyoriFlg As Boolean, ByRef reserveData As T_Reserve, ByRef errMsg As String)

        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia

        Dim strSQL = "SELECT TOP 1 * FROM dbo.T_Reserve WHERE DELFG = 0 "
        strSQL &= "AND ship_code = '" & shipCode & "' AND status = '" & syoriStatus & "' AND LEFT(CONVERT(VARCHAR, datetime, 111), 10) = '" & dtNow.ToShortDateString & "' "
        strSQL &= "ORDER BY datetime DESC;"

        Dim errFlg As Integer

        Dim DT_T_Reserve As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

        If errFlg = 1 Then
            If syoriStatus = "open" Then
                errMsg = "Failed to acquire information on the opening confirmation"
            ElseIf syoriStatus = "inspection1" Then
                errMsg = "Today's INSPECTION 1st time processing failed to acquire information on processing completion confirmation"
            ElseIf syoriStatus = "inspection2" Then
                errMsg = "Today's INSPECTION 2nd processing failed to acquire the processing completion confirmation information."
            ElseIf syoriStatus = "inspection3" Then
                errMsg = "Today's INSPECTION Third time processing failed to acquire information on processing completion confirmation."
            ElseIf syoriStatus = "close" Then
                errMsg = "Failed to acquire information on closing processing end confirmation."
            End If
            Exit Sub
        End If

        If DT_T_Reserve IsNot Nothing Then

            If DT_T_Reserve.Rows(0)("datetime") IsNot DBNull.Value Then
                reserveData.datetime = DT_T_Reserve.Rows(0)("datetime")
            End If

            If DT_T_Reserve.Rows(0)("youser_name") IsNot DBNull.Value Then
                reserveData.youser_name = DT_T_Reserve.Rows(0)("youser_name")
            End If

            If DT_T_Reserve.Rows(0)("diff") IsNot DBNull.Value Then
                reserveData.diff = DT_T_Reserve.Rows(0)("diff")
            End If

            If DT_T_Reserve.Rows(0)("reserve") IsNot DBNull.Value Then
                reserveData.reserve = DT_T_Reserve.Rows(0)("reserve")
            End If

            If DT_T_Reserve.Rows(0)("conf_user") IsNot DBNull.Value Then
                reserveData.conf_user = DT_T_Reserve.Rows(0)("conf_user")
            End If

            If DT_T_Reserve.Rows(0)("conf_datetime") IsNot DBNull.Value Then
                reserveData.conf_datetime = DT_T_Reserve.Rows(0)("conf_datetime")
            End If

            If DT_T_Reserve.Rows(0)("M_2000") IsNot DBNull.Value Then
                reserveData.M_2000 = DT_T_Reserve.Rows(0)("M_2000")
            End If

            If DT_T_Reserve.Rows(0)("M_500") IsNot DBNull.Value Then
                reserveData.M_500 = DT_T_Reserve.Rows(0)("M_500")
            End If

            If DT_T_Reserve.Rows(0)("M_200") IsNot DBNull.Value Then
                reserveData.M_200 = DT_T_Reserve.Rows(0)("M_200")
            End If

            If DT_T_Reserve.Rows(0)("M_100") IsNot DBNull.Value Then
                reserveData.M_100 = DT_T_Reserve.Rows(0)("M_100")
            End If

            If DT_T_Reserve.Rows(0)("M_50") IsNot DBNull.Value Then
                reserveData.M_50 = DT_T_Reserve.Rows(0)("M_50")
            End If

            If DT_T_Reserve.Rows(0)("M_20") IsNot DBNull.Value Then
                reserveData.M_20 = DT_T_Reserve.Rows(0)("M_20")
            End If

            If DT_T_Reserve.Rows(0)("M_10") IsNot DBNull.Value Then
                reserveData.M_10 = DT_T_Reserve.Rows(0)("M_10")
            End If

            If DT_T_Reserve.Rows(0)("Coin_10") IsNot DBNull.Value Then
                reserveData.Coin_10 = DT_T_Reserve.Rows(0)("Coin_10")
            End If

            If DT_T_Reserve.Rows(0)("Coin_5") IsNot DBNull.Value Then
                reserveData.Coin_5 = DT_T_Reserve.Rows(0)("Coin_5")
            End If

            If DT_T_Reserve.Rows(0)("Coin_2") IsNot DBNull.Value Then
                reserveData.Coin_2 = DT_T_Reserve.Rows(0)("Coin_2")
            End If

            If DT_T_Reserve.Rows(0)("Coin_1") IsNot DBNull.Value Then
                reserveData.Coin_1 = DT_T_Reserve.Rows(0)("Coin_1")
            End If

            If DT_T_Reserve.Rows(0)("total") IsNot DBNull.Value Then
                reserveData.total = DT_T_Reserve.Rows(0)("total")
            End If

            If reserveData.conf_user <> "" Then
                SyoriFlg = True
            End If

        End If

    End Sub
    '****************************************************
    '処理　　：chkSyoriOpenClose
    '処理概要：レジ点検の開始・終了（締め）を登録
    '引数　　：syoriStatus  　open/close
    '        ：userid        
    '        ：shipCode         
    '        ：errFlg　　　　　戻り値
    '****************************************************
    Public Sub chkSyoriOpenClose2(ByVal syoriStatus As String, ByVal userid As String, ByVal shipCode As String, ByRef errFlg As Integer)

        '***登録処理***
        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try
            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia

            '■M_ship_base
            Dim select_sql1 As String = ""
            select_sql1 = "SELECT * FROM dbo.M_ship_base WHERE DELFG = 0 "
            select_sql1 &= "AND ship_code = '" & shipCode & "' "

            Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
            Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
            Dim Builder1 As New SqlCommandBuilder(Adapter1)
            Dim ds1 As New DataSet
            Dim dr1 As DataRow

            Adapter1.Fill(ds1)

            If ds1.Tables(0).Rows.Count = 1 Then

                dr1 = ds1.Tables(0).Rows(0)
                dr1("UPDDT") = dtNow
                dr1("UPDCD") = userid

                If syoriStatus = "open" Then
                    dr1("open_time") = "True"
                ElseIf syoriStatus = "close" Then
                    dr1("open_time") = "False"
                End If

                '更新
                Adapter1.Update(ds1)

            End If

            trn.Commit()

        Catch ex As Exception
            trn.Rollback()
            errFlg = 1
        Finally
            'DB接続クローズ
            If con.State <> ConnectionState.Closed Then
                con.Close()
            End If
        End Try

    End Sub

    '****************************************************
    '処理　　：setConfirmData
    '処理概要：Open処理で確認者の完了登録処理
    '引数　　：syoriStatus     
    '    　　：confUser        確認者
    '        ：confIp　　　　　ログインユーザのグローバルIPアドレス　　
    '        ：userid      　　ログインユーザ
    '        ：shipCode        拠点コード 
    '        ：errFlg　　　　　戻り値
    '****************************************************
    Public Sub setConfirmData(ByVal syoriStatus As String, ByVal confUser As String, ByVal confIp As String, ByVal userid As String, ByVal shipCode As String, ByRef confirmTime As DateTime, ByRef errFlg As Integer)

        '***登録処理***
        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try
            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia

            '■
            Dim select_sql1 As String = ""
            select_sql1 = "SELECT top 1 * FROM dbo.T_Reserve WHERE DELFG = 0 "
            select_sql1 &= "AND ship_code = '" & shipCode & "' AND status = '" & syoriStatus & "' "
            select_sql1 &= "AND CRTCD = '" & userid & "' AND (conf_user = '' OR conf_user IS NULL) "
            select_sql1 &= "ORDER BY datetime DESC;"

            Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
            Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
            Dim Builder1 As New SqlCommandBuilder(Adapter1)
            Dim ds1 As New DataSet
            Dim dr1 As DataRow

            Adapter1.Fill(ds1)

            If ds1.Tables(0).Rows.Count = 1 Then

                dr1 = ds1.Tables(0).Rows(0)
                dr1("UPDDT") = dtNow
                dr1("UPDCD") = userid
                dr1("conf_user") = confUser
                dr1("conf_datetime") = dtNow
                confirmTime = dr1("conf_datetime")
                dr1("conf_ip") = confIp

                '更新
                Adapter1.Update(ds1)

            End If

            trn.Commit()

        Catch ex As Exception
            trn.Rollback()
            errFlg = 1
        Finally
            'DB接続クローズ
            If con.State <> ConnectionState.Closed Then
                con.Close()
            End If
        End Try

    End Sub

    '****************************************************
    '処理名　：set_ConsumerInfo
    '処理概要：入力画面で、##で指定された顧客名より、顧客情報を取得する
    '引数　　：otherData   戻り値  登録済の顧客情報をセット
    '          shipCode
    '　　　　　errFlg      戻り値　0:正常　1:異常
    '****************************************************
    Public Sub set_ConsumerInfo(ByRef otherData As Class_money.OTHER_DATA, ByVal shipCode As String, ByRef errFlg As Integer)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try

            'Listに表示する削除済データを取得
            Dim select_sql As String = ""
            select_sql &= "SELECT top 1 * "
            select_sql &= "FROM dbo.T_repair1 WHERE DELFG = 0 "
            select_sql &= "AND Branch_Code = '" & shipCode & "' "
            select_sql &= "AND Consumer_Name = '" & otherData.Consumer_Name & "' "
            select_sql &= "ORDER BY CRTDT DESC;"

            Dim sqlSelect As New SqlCommand(select_sql, con, trn)
            Dim Adapter As New SqlDataAdapter(sqlSelect)
            Dim ds As New DataSet

            Adapter.Fill(ds)

            If ds.Tables(0).Rows.Count = 1 Then

                Dim dr As DataRow = ds.Tables(0).Rows(0)

                If dr("Postal_Code") IsNot DBNull.Value Then
                    otherData.Postal_Code = dr("Postal_Code")
                End If

                If dr("State_Name") IsNot DBNull.Value Then
                    otherData.State_Name = dr("State_Name")
                End If

                If dr("Consumer_Addr1") IsNot DBNull.Value Then
                    otherData.Consumer_Addr1 = dr("Consumer_Addr1")
                End If

                If dr("Consumer_Addr2") IsNot DBNull.Value Then
                    otherData.Consumer_Addr2 = dr("Consumer_Addr2")
                End If

                If dr("Consumer_MailAddress") IsNot DBNull.Value Then
                    otherData.Customer_mail_address = dr("Consumer_MailAddress")
                End If

                If dr("Consumer_Telephone") IsNot DBNull.Value Then
                    otherData.Consumer_Telephone = dr("Consumer_Telephone")
                End If

                If dr("Consumer_Fax") IsNot DBNull.Value Then
                    otherData.Consumer_Fax = dr("Consumer_Fax")
                End If

            End If

        Catch ex As Exception
            trn.Rollback()
            errFlg = 1
        Finally
            'DB接続クローズ
            If con.State <> ConnectionState.Closed Then
                con.Close()
            End If
        End Try

    End Sub

End Class
