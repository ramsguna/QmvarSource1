Imports iFont = iTextSharp.text.Font
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO
Imports System.Text
Imports System.Data.SqlClient
Imports System.Globalization

Public Class Class_analysis

    'サーバ側でファイルを一時保存するパス
    Public savePass As String = "C:\Analysis\CSV\"

    'CSV出力場所
    Public CsvFilePass As String = "C:\\Analysis\\CSV\\"

    'インポートファイルの項目数
    Public DailyStatementCol As Integer = 22
    Public WtyExcelDownCol As Integer = 138
    Public WtyListbyReportNoCol As Integer = 14
    Public GoodRecivedCol As Integer = 10
    Public BillingInfoCol As Integer = 18
    Dim _ShipBaseControl As ShipBaseControl = New ShipBaseControl()

    'DailyStatement_Repartデータの項目設定
    Public Structure DAILYSTATEMENTREPART
        Dim IW_Labor As Decimal  '③
        Dim IW_Parts As Decimal　'④　
        Dim IW_Transport As Decimal
        Dim IW_Others As Decimal
        Dim IW_Tax As Decimal　　'⑤　
        Dim IW_total As Decimal　'⑥
        Dim OW_Labor As Decimal　'⑦
        Dim OW_Parts As Decimal  '⑧   
        Dim OW_Transport As Decimal
        Dim OW_Others As Decimal
        Dim OW_Tax As Decimal　　'⑨
        Dim OW_total As Decimal　'⑩
        Dim IW_goods_total As Integer　'①
        Dim OW_goods_total As Integer　'②
        Dim IW_Count As Integer
        Dim OW_Count As Integer
        Dim Billing_date As String
        Dim GoodsDelivered_date As String
        'Comment on 20190916
        Dim Labor As Decimal
        Dim Parts As Decimal
    End Structure
    'GSPNシステムのhtmlデータ
    Public Structure PAID_CHEK
        Dim Billing_Doc As String
        Dim Comment As String
        Dim Document As String
        Dim Account_Doc As String
        Dim Document_Due_Date As String
        Dim Amount As String
    End Structure
    'Activity Report
    Public Structure ACTIVITY_REPORT
        Dim Customer_Visit As Integer
        Dim Call_Registerd As Integer
        Dim Repair_Completed As Integer
        Dim Goods_Delivered As Integer
        Dim Pending_Calls As Integer
        Dim Cancelled_Calls As Integer
        Dim day As String  'mon-dd' 例）jun-01
        Dim day2 As String 'YYYY/MM/DD'
        Dim youbi As String
        Dim month As String 'YYYY/MM'
        Dim ActYear As String 'YYYY /* Added Mon Year VJ 20100106*/
        Dim ActMonth As String  'MM /* Added Mon Year VJ 20100106*/
        Dim note As String
        Dim location As String

        'Comment on 20200106
        Dim Labor As Decimal
        Dim Parts As Decimal
        Dim Billing_date As String

    End Structure
    'cash_track
    Public Structure CASH_TRACK
        Dim claim_no As String
        Dim invoice_date As DateTime
        Dim invoice_no As String
        Dim customer_name As String
        Dim Warranty As String
        Dim payment As String
        Dim payment_kind As String
        Dim total_amount As Decimal
        Dim input_user As String
        Dim location As String
        Dim card_number As String
        Dim card_type As String
        Dim deposit As Decimal
        Dim change As Decimal
        Dim message As String
        Dim count_no As Integer
        Dim delFlg As String
        Dim claim As Decimal
        Dim claimCredit As Decimal
        Dim full_discount As Integer
        Dim discount As Decimal
        Dim discount_after_amt As Decimal
        Dim discount_after_amt_credit As Decimal
    End Structure
    'Custody
    Public Structure CUSTODY
        Dim keep_no As String
        Dim customer_name As String
        Dim customer_tel As String
        Dim samsung_claim_no As String
        Dim product_name As String
        Dim cash As Decimal
        Dim ship_code As String
    End Structure
    'WTY_EXCEL
    Public Structure WTY_EXCEL
        Dim OW_Labor As String
        Dim OW_Parts As String
        Dim OW_total As String
        Dim OW_Tax As String
        Dim ASC_Code As String
        Dim Branch_Code As String
        Dim ASC_Claim_No As String
        Dim Samsung_Claim_No As String
        Dim Service_Type As String
        Dim Consumer_Name As String
        Dim Consumer_Addr1 As String
        Dim Consumer_Addr2 As String
        Dim Consumer_Telephone As String
        Dim Consumer_Fax As String
        Dim Postal_Code As String
        Dim Model As String
        Dim Serial_No As String
        Dim IMEI_No As String
        Dim Defect_Type As String
        Dim Condition As String
        Dim Symptom As String
        Dim Defect_Code As String
        Dim Repair_Code As String
        Dim Defect_Desc As String
        Dim Repair_Description As String
        Dim Purchase_Date As String
        Dim Repair_Received_Date As String
        Dim Completed_Date As String
        Dim Delivery_Date As String
        Dim Production_Date As String
        Dim Labor_Amount As Decimal
        Dim Parts_Amount As Decimal
        Dim Tax As String
        Dim Freight As String
        Dim Other As String
        Dim Parts_SGST As Decimal
        Dim Parts_UTGST As Decimal
        Dim Parts_CGST As Decimal
        Dim Parts_IGST As Decimal
        Dim Parts_Cess As Decimal
        Dim SGST As Decimal
        Dim UTGST As Decimal
        Dim CGST As Decimal
        Dim IGST As Decimal
        Dim Cess As Decimal
        Dim Total_Invoice_Amount As String
        Dim Remark As String
        Dim Tr_No As String
        Dim Tr_Type As String
        Dim Status As String
        Dim Engineer As String
        Dim Collection_Point As String
        Dim Collection_Point_Name As String
        Dim Location_1 As String
        Dim Part_1 As String
        Dim Qty_1 As String
        Dim Unit_Price_1 As String
        Dim Doc_Num_1 As String
        Dim Matrial_Serial_1 As String
        Dim Location_2 As String
        Dim Part_2 As String
        Dim Qty_2 As String
        Dim Unit_Price_2 As String
        Dim Doc_Num_2 As String
        Dim Matrial_Serial_2 As String
        Dim Location_3 As String
        Dim Part_3 As String
        Dim Qty_3 As String
        Dim Unit_Price_3 As String
        Dim Doc_Num_3 As String
        Dim Matrial_Serial_3 As String
        Dim Location_4 As String
        Dim Part_4 As String
        Dim Qty_4 As String
        Dim Unit_Price_4 As String
        Dim Doc_Num_4 As String
        Dim Matrial_Serial_4 As String
        Dim Location_5 As String
        Dim Part_5 As String
        Dim Qty_5 As String
        Dim Unit_Price_5 As String
        Dim Doc_Num_5 As String
        Dim Matrial_Serial_5 As String
        Dim Location_6 As String
        Dim Part_6 As String
        Dim Qty_6 As String
        Dim Unit_Price_6 As String
        Dim Doc_Num_6 As String
        Dim Matrial_Serial_6 As String
        Dim Location_7 As String
        Dim Part_7 As String
        Dim Qty_7 As String
        Dim Unit_Price_7 As String
        Dim Doc_Num_7 As String
        Dim Matrial_Serial_7 As String
        Dim Location_8 As String
        Dim Part_8 As String
        Dim Qty_8 As String
        Dim Unit_Price_8 As String
        Dim Doc_Num_8 As String
        Dim Matrial_Serial_8 As String
        Dim Location_9 As String
        Dim Part_9 As String
        Dim Qty_9 As String
        Dim Unit_Price_9 As String
        Dim Doc_Num_9 As String
        Dim Matrial_Serial_9 As String
        Dim Location_10 As String
        Dim Part_10 As String
        Dim Qty_10 As String
        Dim Unit_Price_10 As String
        Dim Doc_Num_10 As String
        Dim Matrial_Serial_10 As String
        Dim Location_11 As String
        Dim Part_11 As String
        Dim Qty_11 As String
        Dim Unit_Price_11 As String
        Dim Doc_Num_11 As String
        Dim Matrial_Serial_11 As String
        Dim Location_12 As String
        Dim Part_12 As String
        Dim Qty_12 As String
        Dim Unit_Price_12 As String
        Dim Doc_Num_12 As String
        Dim Matrial_Serial_12 As String
        Dim Location_13 As String
        Dim Part_13 As String
        Dim Qty_13 As String
        Dim Unit_Price_13 As String
        Dim Doc_Num_13 As String
        Dim Matrial_Serial_13 As String
        Dim Location_14 As String
        Dim Part_14 As String
        Dim Qty_14 As String
        Dim Unit_Price_14 As String
        Dim Doc_Num_14 As String
        Dim Matrial_Serial_14 As String
        Dim Location_15 As String
        Dim Part_15 As String
        Dim Qty_15 As String
        Dim Unit_Price_15 As String
        Dim Doc_Num_15 As String
        Dim Matrial_Serial_15 As String
        '税金種類毎の合計
        Dim SGST_Payable As Decimal
        Dim CGST_Payable As Decimal
        Dim IGST_Payable As Decimal
        Dim Sum_Tax_Amount As Decimal
        Dim Sum_Total_Invoice_Amount As Decimal
        'InvoiceNo_Final
        Dim InvoiceNo_Final As String
        'parts種類の個数　例）part1,part2使用なら2
        Dim partsCount As Integer
        'parts情報の構造体(part1～part15の情報をセット)
        Dim partsInfo() As PARTSINFO
        'Invoice_updateの情報⇓
        Dim Invoice As Decimal
        Dim Labor As Decimal
        Dim Parts As Decimal
        Dim Parts_invoice_No As String
        Dim Labor_Invoice_No As String
        'payment情報（cash_trackテーブルより取得してセット）
        Dim payment As String
        'Comment on 20190809
        'Added


    End Structure
    Public Structure PARTSINFO
        Dim PartsName As String
        Dim Qty As String
        Dim UnitPrice As String
    End Structure
    Public Structure INVOICE
        Dim Parts_invoice_No As String
        Dim Labor_Invoice_No As String
        Dim Invoice_date As DateTime
        Dim number As String
        Dim samsung_Ref_No As String
        Dim Your_Ref_No As String
        Dim Model As String
        Dim Serial As String
        Dim Product As String
        Dim Serivice As String
        Dim Defect_Code As String
        Dim Currency As String
        Dim Invoice As String
        Dim Labor As String
        Dim Parts As String
        Dim Felight As String
        Dim Other As String
        Dim Tax As String
    End Structure
    Public Structure BILLINGINFODETAIL
        'good_recived
        Dim delivery_date As String    'Invoice Date
        Dim GR_Date As String
        Dim Invoice_No As String       'delivery_No
        Dim local_invoice_No As String
        Dim Items As String
        Dim GR_Status As String
        'Billing_Info
        Dim Branch_Code As String
        Dim Ship_Branch As String
        Dim Invoice_No2 As String
        Dim Delivery_No As String
        Dim Amount As Decimal
        Dim SGST_UTGST As Decimal
        Dim CGST As Decimal
        Dim IGST As Decimal
        Dim Cess As Decimal
        Dim Tax As Decimal
        Dim Total As Decimal

        '出力時に、Invoice_No毎に下記項目の合計値をセット
        Dim SumAmount As Decimal
        Dim SumSGST_UTGST As Decimal
        Dim SumCGST As Decimal
        Dim SumIGST As Decimal
        Dim SumCess As Decimal
        Dim SumTax As Decimal
        Dim SumTotal As Decimal
    End Structure
    Public Structure DELETE_LOG
        Dim del_user As String
        Dim del_datetime As String
        Dim del_location As String
        Dim del_data As String
    End Structure
    '****************************************************
    '処理概要：CSVデータを配列にセット
    '引数：filePass CSVファイル名(pass付き)
    '      colLen 戻り値  項目数をセット
    '      errFlg 戻り値　0:正常　1:異常
    '      csvChk 戻り値  ヘッダ情報チェック結果をセット NG:False OK:True
    '      kindCsv        CSVファイルの種類を指定  
    '返却：配列にセットしたCSVデータ
    '****************************************************
    Public Function getCsvData(ByVal filePass As String, ByRef colLen As Integer, ByRef errFlg As Integer, ByRef csvChk As String, ByVal kindCsv As Integer) As String()()

        Try
            'StreamReader の新しいインスタンスを生成する
            Dim cReader As New System.IO.StreamReader(filePass, System.Text.Encoding.Default)
            Dim textLines As New List(Of String())
            Dim strArr()() As String
            Dim rowCnt As Integer

            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            Dim colsHead2() As String 'ヘッダ２行目

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
                ElseIf rowCnt = 2 Then
                    colsHead2 = Split(stBuffer, ",")
                End If

            End While

            colLen = cols.Length

            cReader.Close()

            'ヘッダ情報確認
            'DailyStatement_Repart
            Select Case kindCsv

                Case 1

                    If colLen = DailyStatementCol Then
                        If chkHead(colsHead, colsHead2, kindCsv) = False Then
                            csvChk = "False"
                        End If
                    Else
                        csvChk = "False"
                    End If

                Case 2

                    If colLen = WtyExcelDownCol Then
                        If chkHead(colsHead, colsHead2, kindCsv) = False Then
                            csvChk = "False"
                        End If
                    Else
                        csvChk = "False"
                    End If

                Case 3, 4

                    If colLen = WtyListbyReportNoCol Then
                        If chkHead(colsHead, colsHead2, kindCsv) = False Then
                            csvChk = "False"
                        End If
                    Else
                        csvChk = "False"
                    End If

                Case 5

                    If colLen = GoodRecivedCol Then
                        If chkHead(colsHead, colsHead2, kindCsv) = False Then
                            csvChk = "False"
                        End If
                    Else
                        csvChk = "False"
                    End If

                Case 6

                    If colLen = BillingInfoCol Then
                        If chkHead(colsHead, colsHead2, kindCsv) = False Then
                            csvChk = "False"
                        End If
                    Else
                        csvChk = "False"
                    End If

            End Select

            'CSVデータセット
            strArr = textLines.ToArray

            Return strArr

        Catch ex As Exception
            Return Nothing
            errFlg = 1
        End Try

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
    Public Function getCsvData2(ByVal filePass As String, ByRef colLen As Integer, ByRef errFlg As Integer, ByRef csvChk As String, ByVal kindCsv As Integer) As String()()

        Try
            'StreamReader の新しいインスタンスを生成する
            Dim cReader As New System.IO.StreamReader(filePass, System.Text.Encoding.Default)
            Dim textLines As New List(Of String())
            Dim strArr()() As String
            Dim rowCnt As Integer
            Dim delimit As String = "<td class=""td_ac"">"
            Dim delimitRow As String = "<tr>"
            Dim rowCntHtml As Integer
            Dim paidChekData() As PAID_CHEK

            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            Dim colsHead2() As String 'ヘッダ２行目

            While (cReader.Peek() >= 0)

                'ファイルを1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                cols = Split(stBuffer, delimit, -1, CompareMethod.Text)

                If cols.Length <> 1 Then

                    ReDim paidChekData(((cols.Length - 1) / 6) - 1)

                    For i = 0 To cols.Length - 1

                        If i <> 0 Then

                            '不要なタグを削除
                            cols(i) = Replace(cols(i), "</td>", "")

                            cols(i) = Replace(cols(i), "</tr><tr>", "")

                        End If

                    Next i

                    Dim Cnt As Integer = 1

                    For i = 0 To paidChekData.Length - 1

                        paidChekData(i).Billing_Doc = cols(Cnt)
                        paidChekData(i).Comment = cols(Cnt + 1)
                        paidChekData(i).Document = cols(Cnt + 2)
                        paidChekData(i).Account_Doc = cols(Cnt + 3)
                        paidChekData(i).Document_Due_Date = cols(Cnt + 4)
                        paidChekData(i).Amount = cols(Cnt + 5)

                        Cnt = Cnt + 6

                    Next i

                End If

            End While

            colLen = cols.Length

            cReader.Close()

            ''ヘッダ情報確認
            ''DailyStatement_Repart
            'Select Case kindCsv

            '    Case 1

            '        If colLen = DailyStatementCol Then
            '            If chkHead(colsHead, colsHead2, kindCsv) = False Then
            '                csvChk = "False"
            '            End If
            '        Else
            '            csvChk = "False"
            '        End If

            '    Case 2

            '        If colLen = WtyExcelDownCol Then
            '            If chkHead(colsHead, colsHead2, kindCsv) = False Then
            '                csvChk = "False"
            '            End If
            '        Else
            '            csvChk = "False"
            '        End If

            '    Case 3, 4

            '        If colLen = WtyListbyReportNoCol Then
            '            If chkHead(colsHead, colsHead2, kindCsv) = False Then
            '                csvChk = "False"
            '            End If
            '        Else
            '            csvChk = "False"
            '        End If

            '    Case 5

            '        If colLen = GoodRecivedCol Then
            '            If chkHead(colsHead, colsHead2, kindCsv) = False Then
            '                csvChk = "False"
            '            End If
            '        Else
            '            csvChk = "False"
            '        End If

            '    Case 6

            '        If colLen = BillingInfoCol Then
            '            If chkHead(colsHead, colsHead2, kindCsv) = False Then
            '                csvChk = "False"
            '            End If
            '        Else
            '            csvChk = "False"
            ''        End If

            'End Select

            'CSVデータセット
            strArr = textLines.ToArray

            Return strArr

        Catch ex As Exception
            Return Nothing
            errFlg = 1
        End Try

    End Function
    '****************************************************
    '処理：DailyStatement_Repartデータの登録
    '引数：csvData CSVデータ
    '　　　userid
    '　　　userName
    '      uploadShipname
    '      errFlg         戻り値　0:正常　1:異常
    '      dailyKind      MM:mm/dd/yyyy DD:dd/mm/yyyy
    '      billingDateAll 登録された請求日をセット ⇒　history欄へ記載用
    '      FileName       アップロードファイル名（※リカバリ処理で登録ファイルを確認するために使用していた。。⇒　不要であれば、削除しても良い。）
    '****************************************************
    Public Sub setCsvData(ByVal csvData()() As String, ByVal userid As String, ByVal userName As String, ByVal uploadShipname As String, ByVal setShipCode As String, ByRef errFlg As Integer, ByVal dailyKind As String, ByRef billingDateAll() As String, ByVal FileName As String)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia
        Dim totalDailyStatementRepart() As DAILYSTATEMENTREPART                  'BillingDate毎集計用
        Dim dailyStatementRepartData(csvData.Length - 4) As DAILYSTATEMENTREPART '（total行以外） BillingDate取得用
        '※要素数：CSV全行から、件名、total行を除く
        '2020/05/27 Delete total record VJ Start
        Dim origFileDate As String
        Dim OrigFileName As String

        Dim PositionStart As Integer = 0


        PositionStart = InStr(1, FileName, "_")
        PositionStart = PositionStart + 2

        origFileDate = FileName.Substring(PositionStart, 8)
        origFileDate = DateTime.ParseExact(origFileDate, "yyyyMMdd",
                                CultureInfo.InvariantCulture).ToString("yyyy/MM/dd")
        Dim FirstCharacter As Integer
        FirstCharacter = FileName.ToUpper().IndexOf(".CSV")
        OrigFileName = FileName.Substring(0, FirstCharacter + 4)
        '2020/05/27 Delete total record VJ End
        Dim CntRow As Integer

        Try
            '***SC_DSRに登録***
            For i = 0 To csvData.Length - 1
                'Temp & Delete 20190924
                If csvData(i)(0) = "4284983754" Then
                    Dim str As String = ""
                    str = "1" + 1
                End If

                If i <> 0 AndAlso i <> 1 Then

                    Dim newData As Integer = -1 '新規追加登録確認用
                    Dim add As Integer = -1     '追加登録確認用

                    If dailyKind = "DD" Then

                        'GoodsDeliveredDate  （ddmmyyyy ⇒ yyyymmddへ変換）
                        If csvData(i)(4).Length = 10 Then
                            Dim tmpMon2 As String = csvData(i)(4).Substring(3, 2)
                            Dim tmpDay2 As String = csvData(i)(4).Substring(0, 2)
                            Dim tmpYear2 As String = csvData(i)(4).Substring(6, 4)
                            csvData(i)(4) = tmpYear2 & "/" & tmpMon2 & "/" & tmpDay2
                        End If

                        'BillingDate （ddmmyyyy ⇒ yyyymmddへ変換）
                        If csvData(i)(3).Length = 10 Then
                            Dim tmpMon As String = csvData(i)(3).Substring(3, 2)
                            Dim tmpDay As String = csvData(i)(3).Substring(0, 2)
                            Dim tmpYear As String = csvData(i)(3).Substring(6, 4)
                            csvData(i)(3) = tmpYear & "/" & tmpMon & "/" & tmpDay
                        End If

                    Else

                        'GoodsDeliveredDate（変換せず）
                        If csvData(i)(4).Length = 10 Then
                            Dim tmpMon2 As String = csvData(i)(4).Substring(0, 2)
                            Dim tmpDay2 As String = csvData(i)(4).Substring(3, 2)
                            Dim tmpYear2 As String = csvData(i)(4).Substring(6, 4)
                            csvData(i)(4) = tmpYear2 & "/" & tmpMon2 & "/" & tmpDay2
                        End If

                        'BillingDate（変換せず）
                        If csvData(i)(3).Length = 10 Then
                            Dim tmpMon As String = csvData(i)(3).Substring(0, 2)
                            Dim tmpDay As String = csvData(i)(3).Substring(3, 2)
                            Dim tmpYear As String = csvData(i)(3).Substring(6, 4)
                            csvData(i)(3) = tmpYear & "/" & tmpMon & "/" & tmpDay
                        End If

                    End If

                    '■SC_DSR登録
                    Dim delete_sql1 As String = ""
                    Dim select_sql1 As String = ""
                    '2020/05/27 Delete total record VJ
                    If csvData(i)(0).ToUpper() = "Total".ToUpper() Then


                        Dim conndel As New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)


                        'Dim sqlDelete As New SqlCommand(select_sql1, con, trn)
                        'sqlDelete.Connection = con


                        delete_sql1 = "Delete from dbo.SC_DSR WHERE DELFG = 0"
                        delete_sql1 &= "AND ServiceOrder_No = '" & csvData(i)(0) & "' "
                        delete_sql1 &= " AND Upload_FileName like '" & OrigFileName & "%' "
                        ' delete_sql1 &= " And Left(Convert(VARCHAR, Billing_date, 111), 10) = '" & origFileDate & "' "
                        ' delete_sql1 &= "AND GoodsDelivered_date = '" & csvData(i)(4) & "' "
                        delete_sql1 &= "AND Branch_name = '" & uploadShipname & "' "
                        Dim sqldelcmd As New SqlCommand(delete_sql1, con, trn)
                        sqldelcmd.CommandType = CommandType.Text
                        'sqldelcmd.CommandText = delete_sql1

                        'conndel.Open()
                        sqldelcmd.ExecuteNonQuery()
                        ' conndel.Close()
                        'sqlDelete.Connection.Open()

                        'sqlDelete.ExecuteNonQuery()
                        'sqlDelete.Connection.Close()

                        'Dim DelAdapter As New SqlDataAdapter()
                        'DelAdapter.DeleteCommand = con.CreateCommand()
                        'DelAdapter.DeleteCommand.CommandText = select_sql1
                        'DelAdapter.DeleteCommand.ExecuteNonQuery()

                    End If

                    '2020/05/27 Delete total record VJ
                    select_sql1 = "SELECT * FROM dbo.SC_DSR WHERE DELFG = 0 "
                    select_sql1 &= "AND ServiceOrder_No = '" & csvData(i)(0) & "' "
                    If csvData(i)(0).ToUpper() = "Total".ToUpper() Then
                        select_sql1 &= "AND LEFT(CONVERT(VARCHAR, Billing_date,111), 10) = '" & origFileDate & "' "
                    Else
                        select_sql1 &= "AND LEFT(CONVERT(VARCHAR, Billing_date,111), 10) = '" & csvData(i)(3) & "' "
                    End If


                    select_sql1 &= "AND GoodsDelivered_date = '" & csvData(i)(4) & "' "
                    select_sql1 &= "AND Branch_name = '" & uploadShipname & "' "

                    Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
                    Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
                    Dim Builder1 As New SqlCommandBuilder(Adapter1)
                    Dim ds1 As New DataSet
                    Dim dr1 As DataRow
                    Adapter1.Fill(ds1)

                    If ds1.Tables(0).Rows.Count = 1 Then
                        'インポート済の為上書き登録
                        dr1 = ds1.Tables(0).Rows(0)
                        add = 1
                    Else
                        '新規追加登録
                        dr1 = ds1.Tables(0).NewRow
                        newData = 1
                    End If

                    '◆total行有り無しに関わらず、共通設定◆
                    If newData = 1 Then
                        dr1("CRTDT") = dtNow
                        dr1("CRTCD") = userid
                    End If

                    If add = 1 Then
                        dr1("UPDDT") = dtNow
                        dr1("UPDCD") = userid
                    End If

                    dr1("UPDPG") = "Class_analysis.vb"
                    dr1("DELFG") = 0
                    dr1("Branch_name") = uploadShipname

                    dr1("Upload_FileName") = FileName

                    dr1("upload_date") = dtNow

                    dr1("GoodsDelivered_date") = csvData(i)(4)

                    If csvData(i)(0) = "Total" Then

                        '◆total有り行の設定◆
                        dr1("t_flg") = False
                        dr1("ServiceOrder_No") = "Total"
                        dr1("Billing_date") = origFileDate

                        If csvData(i)(10) = "" Then
                            dr1("IW_Labor") = 0
                        Else
                            dr1("IW_Labor") = csvData(i)(10)
                        End If

                        If csvData(i)(11) = "" Then
                            dr1("IW_Parts") = 0
                        Else
                            dr1("IW_Parts") = csvData(i)(11)
                        End If

                        If csvData(i)(12) = "" Then
                            dr1("IW_Transport") = 0
                        Else
                            dr1("IW_Transport") = csvData(i)(12)
                        End If

                        If csvData(i)(13) = "" Then
                            dr1("IW_Others") = 0
                        Else
                            dr1("IW_Others") = csvData(i)(13)
                        End If

                        If csvData(i)(14) = "" Then
                            dr1("IW_Tax") = 0
                        Else
                            dr1("IW_Tax") = csvData(i)(14)
                        End If

                        If csvData(i)(15) = "" Then
                            dr1("IW_total") = 0
                        Else
                            dr1("IW_total") = csvData(i)(15)
                        End If

                        If csvData(i)(16) = "" Then
                            dr1("OW_Labor") = 0
                        Else
                            dr1("OW_Labor") = csvData(i)(16)
                        End If

                        If csvData(i)(17) = "" Then
                            dr1("OW_Parts") = 0
                        Else
                            dr1("OW_Parts") = csvData(i)(17)
                        End If

                        If csvData(i)(18) = "" Then
                            dr1("OW_Transport") = 0
                        Else
                            dr1("OW_Transport") = csvData(i)(18)
                        End If

                        If csvData(i)(19) = "" Then
                            dr1("OW_Others") = 0
                        Else
                            dr1("OW_Others") = csvData(i)(19)
                        End If

                        If csvData(i)(20) = "" Then
                            dr1("OW_Tax") = 0
                        Else
                            dr1("OW_Tax") = csvData(i)(20)
                        End If

                        If csvData(i)(21) = "" Then
                            dr1("OW_total") = 0
                        Else
                            dr1("OW_total") = csvData(i)(21)
                        End If

                    Else

                        '◆total行無しの設定◆
                        Dim BillingDate As DateTime
                        If DateTime.TryParse(csvData(i)(3), BillingDate) Then
                            BillingDate = csvData(i)(3)
                            dr1("Billing_date") = BillingDate
                            dailyStatementRepartData(CntRow).Billing_date = BillingDate
                        End If

                        dr1("t_flg") = True
                        dr1("ServiceOrder_No") = csvData(i)(0)
                        dr1("LastUpdt_user") = csvData(i)(1)
                        dr1("Billinb_user") = csvData(i)(2)
                        dr1("Engineer") = csvData(i)(7)
                        dr1("Product_1") = csvData(i)(8)
                        dr1("Product_2") = csvData(i)(9)

                        If csvData(i)(10) = "" Then
                            dr1("IW_Labor") = 0
                        Else
                            dr1("IW_Labor") = csvData(i)(10)
                        End If

                        If csvData(i)(11) = "" Then
                            dr1("IW_Parts") = 0
                        Else
                            dr1("IW_Parts") = csvData(i)(11)
                        End If

                        If csvData(i)(12) = "" Then
                            dr1("IW_Transport") = 0
                        Else
                            dr1("IW_Transport") = csvData(i)(12)
                        End If

                        If csvData(i)(13) = "" Then
                            dr1("IW_Others") = 0
                        Else
                            dr1("IW_Others") = csvData(i)(13)
                        End If

                        If csvData(i)(14) = "" Then
                            dr1("IW_Tax") = 0
                        Else
                            dr1("IW_Tax") = csvData(i)(14)
                        End If

                        If csvData(i)(15) = "" Then
                            dr1("IW_total") = 0
                        Else
                            dr1("IW_total") = csvData(i)(15)
                        End If

                        If csvData(i)(16) = "" Then
                            dr1("OW_Labor") = 0
                        Else
                            dr1("OW_Labor") = csvData(i)(16)
                        End If

                        If csvData(i)(17) = "" Then
                            dr1("OW_Parts") = 0
                        Else
                            dr1("OW_Parts") = csvData(i)(17)
                        End If

                        If csvData(i)(18) = "" Then
                            dr1("OW_Transport") = 0
                        Else
                            dr1("OW_Transport") = csvData(i)(18)
                        End If

                        If csvData(i)(19) = "" Then
                            dr1("OW_Others") = 0
                        Else
                            dr1("OW_Others") = csvData(i)(19)
                        End If

                        If csvData(i)(20) = "" Then
                            dr1("OW_Tax") = 0
                        Else
                            dr1("OW_Tax") = csvData(i)(20)
                        End If

                        If csvData(i)(21) = "" Then
                            dr1("OW_total") = 0
                        Else
                            dr1("OW_total") = csvData(i)(21)
                        End If

                        CntRow = CntRow + 1

                    End If

                    '新規DRをDataTableに追加
                    If newData = 1 Then
                        ds1.Tables(0).Rows.Add(dr1)
                    End If

                    Adapter1.Update(ds1)

                End If

            Next i

            '■コミット
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

        If errFlg = 1 Then
            Exit Sub
        End If

        '***billingDateをユニークで取得***
        Dim cnt, cnt2, m As Integer

        For i = 0 To dailyStatementRepartData.Length - 1

            cnt2 = 0

            If i = 0 Then
                ReDim Preserve billingDateAll(cnt)
                billingDateAll(cnt) = dailyStatementRepartData(i).Billing_date
                cnt = cnt + 1
            Else

                'billingDateの重複確認
                If billingDateAll IsNot Nothing Then
                    For m = 0 To UBound(billingDateAll)
                        If dailyStatementRepartData(i).Billing_date = billingDateAll(m) Then
                            cnt2 = cnt2 + 1
                            Exit For
                        End If
                    Next m
                End If

                '重複なければ、billingDateAllにbillingDateを設定
                If cnt2 = 0 Then
                    ReDim Preserve billingDateAll(cnt)
                    billingDateAll(cnt) = dailyStatementRepartData(i).Billing_date
                    cnt = cnt + 1
                End If

            End If

        Next i

        ReDim totalDailyStatementRepart(billingDateAll.Length - 1)

        '**登録済のSC_DSRよりBilling_dat毎の集計情報を取得)***
        'billingDateのKeySQLの設定
        Dim billingDateKey As String = ""

        For i = 0 To billingDateAll.Length - 1
            billingDateKey &= "'" & billingDateAll(i) & "',"
        Next i

        billingDateKey = Left(billingDateKey, billingDateKey.Length - 1)

        'コネクションを取得
        Dim con2 As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con2.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn2 As SqlTransaction = con2.BeginTransaction(IsolationLevel.ReadCommitted)

        Try
            'SUM
            Dim select_sql3 As String = ""
            select_sql3 &= "SELECT Billing_date, SUM(IW_Labor) AS SUM_IW_Labor, SUM(IW_Parts) AS SUM_IW_Parts, SUM(IW_Tax) AS SUM_IW_Tax, SUM(IW_total) AS SUM_IW_total, SUM(OW_Labor) AS SUM_OW_Labor, SUM(OW_Parts) AS SUM_OW_Parts, SUM(OW_Tax) AS SUM_OW_Tax, SUM(OW_total) AS SUM_OW_total "
            ''comment on 20190924
            ''Added 
            select_sql3 &= ", SUM(OW_Transport) as SUM_OW_Transport,SUM(OW_Others) as SUM_OW_Others,SUM(IW_Transport) as SUM_IW_Transport,SUM(IW_Others) as SUM_IW_Others "
            select_sql3 &= "FROM dbo.SC_DSR "
            select_sql3 &= "WHERE DELFG = 0 AND Branch_name = '" & uploadShipname & "' "
            select_sql3 &= "AND GoodsDelivered_date <> '0000/00/00' "
            select_sql3 &= "AND ServiceOrder_No <> 'Total' "
            select_sql3 &= "AND Billing_date IN (" & billingDateKey & ") "
            select_sql3 &= "GROUP BY Billing_date ORDER BY Billing_date;"

            Dim sqlSelect3 As New SqlCommand(select_sql3, con2, trn2)
            Dim Adapter3 As New SqlDataAdapter(sqlSelect3)
            Dim ds3 As New DataSet
            Dim dr3 As DataRow
            Adapter3.Fill(ds3)

            If ds3.Tables(0).Rows.Count <> 0 Then

                For i = 0 To ds3.Tables(0).Rows.Count - 1

                    dr3 = ds3.Tables(0).Rows(i)

                    '**日付**
                    If dr3("Billing_date") IsNot DBNull.Value Then
                        totalDailyStatementRepart(i).Billing_date = dr3("Billing_date")
                    End If

                    '**total**
                    'IW
                    If dr3("SUM_IW_Labor") IsNot DBNull.Value Then
                        totalDailyStatementRepart(i).IW_Labor = dr3("SUM_IW_Labor")
                    End If

                    If dr3("SUM_IW_Parts") IsNot DBNull.Value Then
                        totalDailyStatementRepart(i).IW_Parts = dr3("SUM_IW_Parts")
                    End If

                    If dr3("SUM_IW_Tax") IsNot DBNull.Value Then
                        totalDailyStatementRepart(i).IW_Tax = dr3("SUM_IW_Tax")
                    End If

                    If dr3("SUM_IW_total") IsNot DBNull.Value Then
                        totalDailyStatementRepart(i).IW_total = dr3("SUM_IW_total")
                    End If

                    'OW
                    If dr3("SUM_OW_Labor") IsNot DBNull.Value Then
                        totalDailyStatementRepart(i).OW_Labor = dr3("SUM_OW_Labor")
                    End If

                    If dr3("SUM_OW_Parts") IsNot DBNull.Value Then
                        totalDailyStatementRepart(i).OW_Parts = dr3("SUM_OW_Parts")
                    End If

                    If dr3("SUM_OW_Tax") IsNot DBNull.Value Then
                        totalDailyStatementRepart(i).OW_Tax = dr3("SUM_OW_Tax")
                    End If

                    If dr3("SUM_OW_total") IsNot DBNull.Value Then
                        totalDailyStatementRepart(i).OW_total = dr3("SUM_OW_total")
                    End If
                    select_sql3 &= ", SUM(OW_Transport) as SUM_OW_Transport,SUM(OW_Others) as SUM_OW_Others,SUM(IW_Transport) as SUM_IW_Transport,SUM(IW_Others) as SUM_IW_Others"

                    'Added on 20190924
                    If dr3("SUM_OW_Transport") IsNot DBNull.Value Then
                        totalDailyStatementRepart(i).OW_Transport = dr3("SUM_OW_Transport")
                    End If
                    If dr3("SUM_OW_Others") IsNot DBNull.Value Then
                        totalDailyStatementRepart(i).OW_Others = dr3("SUM_OW_Others")
                    End If
                    If dr3("SUM_IW_Transport") IsNot DBNull.Value Then
                        totalDailyStatementRepart(i).IW_Transport = dr3("SUM_IW_Transport")
                    End If
                    If dr3("SUM_IW_Others") IsNot DBNull.Value Then
                        totalDailyStatementRepart(i).IW_Others = dr3("SUM_IW_Others")
                    End If

                Next i

            End If

            'COUNT
            Dim select_sql4 As String = ""
            select_sql4 &= "SELECT Billing_date, "
            select_sql4 &= "COUNT(case when IW_Labor <> 0 then IW_Labor else NULL end ) AS CNT_IW_Labor, "
            select_sql4 &= "COUNT(case when OW_Labor <> 0 then OW_Labor else NULL end ) AS CNT_OW_Labor, "
            select_sql4 &= "COUNT(case when IW_total <> 0 AND OW_total = 0 then IW_total else NULL end ) AS CNT_IW_total, "
            select_sql4 &= "COUNT(case when (OW_total <> 0 AND IW_total = 0) OR ( OW_total = 0 AND IW_total = 0 ) OR (IW_total <> 0 AND OW_total <> 0 ) then OW_total else NULL end ) AS CNT_OW_total "
            select_sql4 &= "FROM dbo.SC_DSR "
            select_sql4 &= "WHERE DELFG = 0 AND Branch_name = '" & uploadShipname & "' "
            select_sql4 &= "AND GoodsDelivered_date <> '0000/00/00' "
            select_sql4 &= "AND ServiceOrder_No <> 'Total' "
            select_sql4 &= "AND Billing_date IN (" & billingDateKey & ") "
            select_sql4 &= "GROUP BY Billing_date ORDER BY Billing_date;"

            Dim sqlSelect4 As New SqlCommand(select_sql4, con2, trn2)
            Dim Adapter4 As New SqlDataAdapter(sqlSelect4)
            Dim ds4 As New DataSet
            Dim dr4 As DataRow
            Adapter4.Fill(ds4)

            If ds4.Tables(0).Rows.Count <> 0 Then

                For i = 0 To ds4.Tables(0).Rows.Count - 1

                    dr4 = ds4.Tables(0).Rows(i)
                    'Added on 20200706
                    'Billing date is not exit 
                    If dr4("Billing_date") IsNot DBNull.Value Then
                        totalDailyStatementRepart(i).Billing_date = dr4("Billing_date")
                    End If

                    If dr4("CNT_IW_Labor") IsNot DBNull.Value Then
                        totalDailyStatementRepart(i).IW_Count = dr4("CNT_IW_Labor")
                    End If

                    If dr4("CNT_IW_Labor") IsNot DBNull.Value Then
                        totalDailyStatementRepart(i).IW_Count = dr4("CNT_IW_Labor")
                    End If

                    If dr4("CNT_OW_Labor") IsNot DBNull.Value Then
                        totalDailyStatementRepart(i).OW_Count = dr4("CNT_OW_Labor")
                    End If

                    If dr4("CNT_IW_total") IsNot DBNull.Value Then
                        totalDailyStatementRepart(i).IW_goods_total = dr4("CNT_IW_total")
                    End If

                    If dr4("CNT_OW_total") IsNot DBNull.Value Then
                        totalDailyStatementRepart(i).OW_goods_total = dr4("CNT_OW_total")
                    End If

                Next i

            End If

            '***SC_DSR_infoへ請求日毎の合計情報を登録***
            '■SC_DSR_info取得
            For i = 0 To totalDailyStatementRepart.Length - 1

                Dim newData2 As Integer = -1 '新規追加登録確認用
                Dim add2 As Integer = -1     '追加登録確認用

                'VJ 2020/07/06 Added Billing date not null condition Begin
                If totalDailyStatementRepart(i).Billing_date <> Nothing Then
                    Dim select_sql2 As String = ""
                    select_sql2 = "SELECT * FROM dbo.SC_DSR_info WHERE DELFG = 0 "
                    select_sql2 &= "AND Branch_name = '" & uploadShipname & "' "
                    select_sql2 &= "AND LEFT(CONVERT(VARCHAR, Billing_date,111), 10) = '" & totalDailyStatementRepart(i).Billing_date & "' "

                    Dim sqlSelect2 As New SqlCommand(select_sql2, con2, trn2)
                    Dim Adapter2 As New SqlDataAdapter(sqlSelect2)
                    Dim Builder2 As New SqlCommandBuilder(Adapter2)
                    Dim ds2 As New DataSet
                    Dim dr2 As DataRow
                    Adapter2.Fill(ds2)

                    If ds2.Tables(0).Rows.Count = 1 Then
                        'インポート済の為上書き登録
                        dr2 = ds2.Tables(0).Rows(0)
                        add2 = 1
                    Else
                        '新規追加登録
                        dr2 = ds2.Tables(0).NewRow
                        newData2 = 1
                    End If

                    If newData2 = 1 Then
                        dr2("CRTDT") = dtNow
                        dr2("CRTCD") = userid
                    End If

                    If add2 = 1 Then
                        dr2("UPDDT") = dtNow
                        dr2("UPDCD") = userid
                    End If

                    dr2("UPDPG") = "Class_analysis.vb"
                    dr2("DELFG") = 0
                    dr2("Branch_name") = uploadShipname

                    Dim BillingDate As DateTime
                    If DateTime.TryParse(totalDailyStatementRepart(i).Billing_date, BillingDate) Then
                        BillingDate = totalDailyStatementRepart(i).Billing_date
                        dr2("Billing_date") = BillingDate
                    End If

                    dr2("upload_user") = userName
                    dr2("upload_date") = dtNow
                    dr2("IW_Count") = totalDailyStatementRepart(i).IW_Count
                    dr2("OW_Count") = totalDailyStatementRepart(i).OW_Count
                    dr2("IW_Labor_total") = totalDailyStatementRepart(i).IW_Labor
                    dr2("IW_Parts_total") = totalDailyStatementRepart(i).IW_Parts
                    'Comment on 20190924
                    '''dr2("IW_Transport_total") = 0
                    '''dr2("IW_Others_total") = 0
                    dr2("IW_Transport_total") = totalDailyStatementRepart(i).IW_Transport
                    dr2("IW_Others_total") = totalDailyStatementRepart(i).IW_Others
                    dr2("IW_Tax_total") = totalDailyStatementRepart(i).IW_Tax
                    dr2("IW_Total_total") = totalDailyStatementRepart(i).IW_total
                    dr2("OW_Labor_total") = totalDailyStatementRepart(i).OW_Labor
                    dr2("OW_Parts_total") = totalDailyStatementRepart(i).OW_Parts
                    'Comment on 20190924
                    '''dr2("OW_Transport_total") = 0
                    '''dr2("OW_Others_total") = 0
                    dr2("OW_Transport_total") = totalDailyStatementRepart(i).OW_Transport
                    dr2("OW_Others_total") = totalDailyStatementRepart(i).OW_Others
                    dr2("OW_Tax_total") = totalDailyStatementRepart(i).OW_Tax
                    dr2("OW_Total_total") = totalDailyStatementRepart(i).OW_total
                    dr2("IW_goods_total") = totalDailyStatementRepart(i).IW_goods_total
                    dr2("OW_goods_total") = totalDailyStatementRepart(i).OW_goods_total

                    dr2("Upload_FileName") = FileName

                    '新規DRをDataTableに追加
                    If newData2 = 1 Then
                        ds2.Tables(0).Rows.Add(dr2)
                    End If

                    Adapter2.Update(ds2)
                End If
                'VJ 2020/07/06 Added Billing date not null condition End

            Next i

            '■コミット
            trn2.Commit()

        Catch ex As Exception
            trn2.Rollback()
            errFlg = 1
        Finally
            'DB接続クローズ
            If con2.State <> ConnectionState.Closed Then
                con2.Close()
            End If
        End Try

    End Sub

    '****************************************************
    '処理：WtyExcelDownデータの登録
    '引数：csvData CSVデータ
    '      userid
    '      userName
    '　　　errFlg            戻り値　0:正常　1:異常
    '      tourokuFlg        1 登録あり
    '                        ※ファイル中身に記載の拠点コードと、リストで指定されたアップロード拠点が一致しているデータのみ登録可。
    '      uploadShipname    アップロード拠点名称
    '                        ※所属拠点とは限らない。リストで指定された拠点。
    '      setShipCode
    '      FileName          アップロードファイル名（※リカバリ処理で登録ファイルを確認するため）
    '****************************************************
    Public Sub setCsvDataWtyExcelDown(ByVal csvData()() As String, ByVal userid As String, ByVal userName As String, ByRef errFlg As Integer, ByRef tourokuFlg As Integer, ByVal uploadShipname As String, ByVal setShipCode As String, ByVal FileName As String)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)
        Dim dt As DateTime

        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia

        'VJ - 2020-04-16 SSC parent child Add
        Dim ParentShipCode As String = String.Empty

        Dim shipdtlParent As DataRow() = _ShipBaseControl.SelectParentShipBaseDetails(setShipCode).Select()


        If (shipdtlParent.Count() > 0) Then
            For Each row As DataRow In shipdtlParent
                ParentShipCode = row.Item("ship_code").ToString()
            Next
        End If


        'VJ - 2020-04-16 SSC parent child Add
        Try

            Dim newData As Integer  '新規追加登録確認用 
            Dim add As Integer      '追加登録確認用　

            For i = 0 To csvData.Length - 1

                If i <> 0 Then
                    'Comment  on 20190826 
                    'SSC1  & SSC2 are same asc code but different branch code,  so bypass and allowed for SSC1 used OR condition
                    'Original  If (csvData(i)(1) = setShipCode)  then
                    'VJ - 2020-04-16 SSC parent child Add
                    'If (csvData(i)(1) = setShipCode) Or (csvData(i)(1) = "0002203913") Then
                    If (csvData(i)(1) = setShipCode) Or (csvData(i)(1) = ParentShipCode And ParentShipCode <> "") Then
                        newData = -1
                        add = -1
                        Dim select_sql1 As String = ""
                        select_sql1 = "SELECT * FROM dbo.Wty_Excel WHERE DELFG = 0 "
                        select_sql1 &= "AND Branch_Code = '" & csvData(i)(1) & "' "
                        select_sql1 &= "AND ASC_Claim_No = '" & csvData(i)(2) & "' "
                        select_sql1 &= "AND Samsung_Claim_No = '" & csvData(i)(3) & "' "
                        select_sql1 &= "AND Delivery_Date = '" & csvData(i)(24) & "' "
                        select_sql1 &= "AND upload_Branch = '" & uploadShipname & "' "
                        'Comment on 20191202
                        'select_sql1 &= "AND Branch_Code = '" & setShipCode & "' "

                        Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
                        Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
                        Dim Builder1 As New SqlCommandBuilder(Adapter1)
                        Dim ds1 As New DataSet
                        Dim dr1 As DataRow
                        Adapter1.Fill(ds1)


                        tourokuFlg = 1

                        If ds1.Tables(0).Rows.Count = 1 Then
                            'インポート済の為上書き登録
                            dr1 = ds1.Tables(0).Rows(0)
                            add = 1
                        Else
                            '新規追加登録
                            dr1 = ds1.Tables(0).NewRow
                            newData = 1
                        End If

                        If newData = 1 Then
                            dr1("CRTDT") = dtNow
                            dr1("CRTCD") = userid
                        End If

                        If add = 1 Then
                            dr1("UPDDT") = dtNow
                            dr1("UPDCD") = userid
                        End If

                        dr1("UPDPG") = "Class_analysis.vb"
                        dr1("DELFG") = 0
                        dr1("upload_date") = dtNow
                        dr1("upload_user") = userName
                        dr1("upload_Branch") = uploadShipname
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

                        '?をスペースに変換
                        csvData(i)(20) = Replace(csvData(i)(20), "?", " ")
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
                                dr1("Delivery_Date") = ""
                            ElseIf csvData(i)(24) = 0 Then
                                dr1("Delivery_Date") = ""
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

                        If csvData(i)(28) = "" Then
                            dr1("Freight") = 0
                        Else
                            dr1("Freight") = csvData(i)(28)
                        End If

                        If csvData(i)(29) = "" Then
                            dr1("Other") = 0
                        Else
                            dr1("Other") = csvData(i)(29)
                        End If

                        If csvData(i)(30) = "" Then
                            dr1("Parts_SGST") = 0
                        Else
                            dr1("Parts_SGST") = csvData(i)(30)
                        End If

                        If csvData(i)(31) = "" Then
                            dr1("Parts_UTGST") = 0
                        Else
                            dr1("Parts_UTGST") = csvData(i)(31)
                        End If

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

                        If csvData(i)(34) = "" Then
                            dr1("Parts_Cess") = 0
                        Else
                            dr1("Parts_Cess") = csvData(i)(34)
                        End If

                        If csvData(i)(35) = "" Then
                            dr1("SGST") = 0
                        Else
                            dr1("SGST") = csvData(i)(35)
                        End If

                        If csvData(i)(36) = "" Then
                            dr1("UTGST") = 0
                        Else
                            dr1("UTGST") = csvData(i)(36)
                        End If

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

                        If csvData(i)(39) = "" Then
                            dr1("Cess") = 0
                        Else
                            dr1("Cess") = csvData(i)(39)
                        End If

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

                        dr1("Upload_FileName") = FileName

                        '新規DRをDataTableに追加
                        If newData = 1 Then
                            ds1.Tables(0).Rows.Add(dr1)
                        End If

                        Adapter1.Update(ds1)

                    End If

                End If

            Next i

            '■コミット
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
    '処理：WtyListbyReportNoデータの登録
    '引数：csvData CSVデータ
    '      userid
    '      userName
    '　　　errFlg            戻り値　0:正常　1:異常
    '      uploadShipname    アップロード拠点名称
    '                        ※所属拠点とは限らない。
    '      invoiceData       画面からの入力データ
    '      FileName          アップロードファイル名（※リカバリ処理で登録ファイルを確認するため）
    '****************************************************
    Public Sub setCsvInvoiceUpdate(ByVal csvData()() As String, ByVal userid As String, ByVal userName As String, ByRef errFlg As Integer, ByVal uploadShipname As String, ByVal invoiceData As INVOICE, ByVal FileName As String)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)
        Dim dt As DateTime

        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia
        Dim CanUpdate As Boolean = True

        Try

            For i = 0 To csvData.Length - 1
                'Always true 
                CanUpdate = True
                ''invoice is 0.00, then no need to upload it 
                If invoiceData.number = "OWC" Then
                    If csvData(i)(8) = "0.00" Then
                        CanUpdate = False
                    End If
                End If

                If CanUpdate Then
                    If i <> 0 Then
                        Dim newData As Integer = -1 '新規追加登録確認用 
                        Dim add As Integer = -1     '追加登録確認用
                        Dim select_sql1 As String = ""
                        select_sql1 = "SELECT * FROM dbo.Invoice_update WHERE DELFG = 0 "
                        select_sql1 &= "AND upload_Branch = '" & uploadShipname & "' "
                        select_sql1 &= "AND samsung_Ref_No = '" & csvData(i)(0) & "' "
                        Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
                        Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
                        Dim Builder1 As New SqlCommandBuilder(Adapter1)
                        Dim ds1 As New DataSet
                        Dim dr1 As DataRow
                        Adapter1.Fill(ds1)

                        If ds1.Tables(0).Rows.Count = 1 Then
                            'インポート済の為上書き登録
                            dr1 = ds1.Tables(0).Rows(0)
                            add = 1
                        Else
                            '新規追加登録
                            dr1 = ds1.Tables(0).NewRow
                            newData = 1
                        End If

                        If newData = 1 Then
                            dr1("CRTDT") = dtNow
                            dr1("CRTCD") = userid
                        End If

                        If add = 1 Then
                            dr1("UPDDT") = dtNow
                            dr1("UPDCD") = userid
                        End If

                        dr1("UPDPG") = "Class_analysis.vb"
                        dr1("DELFG") = 0
                        dr1("upload_date") = dtNow
                        dr1("upload_user") = userName
                        dr1("upload_Branch") = uploadShipname
                        dr1("samsung_Ref_No") = csvData(i)(0)
                        dr1("Your_Ref_No") = csvData(i)(1)
                        dr1("Model") = csvData(i)(2)
                        dr1("Serial") = csvData(i)(3)
                        dr1("Product") = csvData(i)(4)
                        dr1("Serivice") = csvData(i)(5)
                        dr1("Defect_Code") = csvData(i)(6)
                        dr1("Currency") = csvData(i)(7)

                        'Remove -negative for 4) in invoice, labor, parts
                        If invoiceData.number = "OWC" Then
                            Dim strValue As String = ""
                            If csvData(i)(8) = "" Then
                                dr1("Invoice") = 0
                            Else
                                strValue = csvData(i)(8)
                                dr1("Invoice") = strValue.Replace("-", "")
                            End If
                            If csvData(i)(9) = "" Then
                                dr1("Labor") = 0
                            Else
                                strValue = csvData(i)(9)
                                dr1("Labor") = strValue.Replace("-", "")
                            End If
                            If csvData(i)(10) = "" Then
                                dr1("Parts") = 0
                            Else
                                strValue = csvData(i)(10)
                                dr1("Parts") = strValue.Replace("-", "")
                            End If
                        Else ' For 3A & 3B updation
                            If csvData(i)(8) = "" Then
                                dr1("Invoice") = 0
                            Else
                                dr1("Invoice") = csvData(i)(8)
                            End If
                            If csvData(i)(9) = "" Then
                                dr1("Labor") = 0
                            Else
                                dr1("Labor") = csvData(i)(9)
                            End If
                            If csvData(i)(10) = "" Then
                                dr1("Parts") = 0
                            Else
                                dr1("Parts") = csvData(i)(10)
                            End If
                        End If


                        If csvData(i)(11) = "" Then
                            dr1("Felight") = 0
                        Else
                            dr1("Felight") = csvData(i)(11)
                        End If

                        If csvData(i)(12) = "" Then
                            dr1("Other") = 0
                        Else
                            dr1("Other") = csvData(i)(12)
                        End If

                        If csvData(i)(13) = "" Then
                            dr1("Tax") = 0
                        Else
                            dr1("Tax") = csvData(i)(13)
                        End If

                        dr1("Parts_invoice_No") = invoiceData.Parts_invoice_No
                        dr1("Labor_Invoice_No") = invoiceData.Labor_Invoice_No
                        dr1("Invoice_date") = invoiceData.Invoice_date
                        dr1("number") = invoiceData.number
                        dr1("Upload_FileName") = FileName

                        '新規DRをDataTableに追加
                        If newData = 1 Then
                            ds1.Tables(0).Rows.Add(dr1)
                        End If

                        Adapter1.Update(ds1)

                    End If
                End If

            Next i

            '■コミット
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
    '処理：setCsvGoodRecivedデータの登録
    '引数：csvData CSVデータ
    '      userid
    '      userName
    '　　　errFlg            戻り値　0:正常　1:異常
    '      uploadShipname    アップロード拠点名称
    '                        ※所属拠点と違うケースあり。あくまでもアップロードする拠点
    '      dailyKind         MM:mm/dd/yyyy DD:dd/mm/yyyy
    '      deliveryDateAll 　登録された配送日をセット ⇒　history欄へ記載用　
    '      FileName          アップロードファイル名（※リカバリ処理で登録ファイルを確認するため）
    '****************************************************
    Public Sub setCsvGoodRecived(ByVal csvData()() As String, ByVal userid As String, ByVal userName As String, ByRef errFlg As Integer, ByVal uploadShipname As String, ByVal dailyKind As String, ByRef deliveryDateAll() As String, ByVal FileName As String)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)
        Dim dt As DateTime

        Dim clsSetCommomn As New Class_common
        Dim dtNow As DateTime = clsSetCommomn.dtIndia

        Try

            For i = 0 To csvData.Length - 1

                If i <> 0 AndAlso i <> 1 Then

                    Dim newData As Integer = -1 '新規追加登録確認用
                    Dim add As Integer = -1     '追加登録確認用
                    Dim select_sql1 As String = ""
                    select_sql1 = "SELECT * FROM dbo.good_recived WHERE DELFG = 0 "
                    select_sql1 &= "AND upload_Branch = '" & uploadShipname & "' "
                    select_sql1 &= "AND delivery_No =  '" & csvData(i)(1) & "' "
                    select_sql1 &= "AND local_invoice_No =  '" & csvData(i)(3) & "' "
                    Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
                    Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
                    Dim Builder1 As New SqlCommandBuilder(Adapter1)
                    Dim ds1 As New DataSet
                    Dim dr1 As DataRow
                    Adapter1.Fill(ds1)

                    If ds1.Tables(0).Rows.Count = 1 Then
                        'インポート済の為上書き登録
                        dr1 = ds1.Tables(0).Rows(0)
                        add = 1
                    Else
                        '新規追加登録
                        dr1 = ds1.Tables(0).NewRow
                        newData = 1
                    End If

                    If newData = 1 Then
                        dr1("CRTDT") = dtNow
                        dr1("CRTCD") = userid
                    End If

                    If add = 1 Then
                        dr1("UPDDT") = dtNow
                        dr1("UPDCD") = userid
                    End If

                    dr1("UPDPG") = "Class_analysis.vb"
                    dr1("DELFG") = 0
                    dr1("upload_date") = dtNow
                    dr1("upload_user") = userName
                    dr1("upload_Branch") = uploadShipname

                    dr1("delivery_No") = csvData(i)(1)

                    '日付型がddから始まる　dd/mm/yyyy
                    If dailyKind = "DD" Then

                        'delivery_date （ddmmyyyy ⇒ yyyymmddへ変換
                        If csvData(i)(2).Length = 10 Then
                            Dim tmpMon2 As String = csvData(i)(2).Substring(3, 2)
                            Dim tmpDay2 As String = csvData(i)(2).Substring(0, 2)
                            Dim tmpYear2 As String = csvData(i)(2).Substring(6, 4)
                            csvData(i)(2) = tmpYear2 & "/" & tmpMon2 & "/" & tmpDay2
                        End If

                        'GR_Date （ddmmyyyy ⇒ yyyymmddへ変換）
                        If csvData(i)(7).Length = 10 Then
                            Dim tmpMon2 As String = csvData(i)(7).Substring(3, 2)
                            Dim tmpDay2 As String = csvData(i)(7).Substring(0, 2)
                            Dim tmpYear2 As String = csvData(i)(7).Substring(6, 4)
                            csvData(i)(7) = tmpYear2 & "/" & tmpMon2 & "/" & tmpDay2
                        End If

                    End If

                    Dim deliveryDate As DateTime

                    If DateTime.TryParse(csvData(i)(2), deliveryDate) Then
                        dr1("delivery_date") = csvData(i)(2)
                    Else
                        dr1("delivery_date") = DBNull.Value
                    End If

                    Dim GRDate As DateTime

                    If DateTime.TryParse(csvData(i)(7), GRDate) Then
                        dr1("GR_Date") = csvData(i)(7)
                    Else
                        dr1("GR_Date") = DBNull.Value
                    End If

                    dr1("local_invoice_No") = csvData(i)(3)
                    dr1("Items") = csvData(i)(4)
                    dr1("Total_Qty") = csvData(i)(5)

                    If csvData(i)(6) = "" Then
                        dr1("Total_Amount") = "0.00"
                    Else
                        dr1("Total_Amount") = csvData(i)(6)
                    End If

                    dr1("Create_By") = csvData(i)(8)
                    dr1("GR_Status") = csvData(i)(9)

                    dr1("Upload_FileName") = FileName

                    '新規DRをDataTableに追加
                    If newData = 1 Then
                        ds1.Tables(0).Rows.Add(dr1)
                    End If

                    Adapter1.Update(ds1)

                End If

            Next i

            '■コミット
            trn.Commit()

            '***deliveryDateをユニークで取得***
            Dim cnt, cnt2, m As Integer

            For i = 0 To csvData.Length - 1

                If i <> 0 AndAlso i <> 1 Then

                    cnt2 = 0

                    If i = 0 Then
                        ReDim Preserve deliveryDateAll(cnt)
                        deliveryDateAll(cnt) = csvData(i)(2)
                        cnt = cnt + 1
                    Else

                        'deliveryDateの重複確認
                        If deliveryDateAll IsNot Nothing Then
                            For m = 0 To UBound(deliveryDateAll)
                                If csvData(i)(2) = deliveryDateAll(m) Then
                                    cnt2 = cnt2 + 1
                                    Exit For
                                End If
                            Next m
                        End If

                        '重複なければ、deliveryDateAllにdeliveryDateを設定
                        If cnt2 = 0 Then
                            ReDim Preserve deliveryDateAll(cnt)
                            deliveryDateAll(cnt) = csvData(i)(2)
                            cnt = cnt + 1
                        End If

                    End If

                End If

            Next i

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
    '処理：setCsvBillingInfoデータの登録
    '引数：csvData CSVデータ
    '      userid
    '      userName
    '　　　errFlg            戻り値　0:正常　1:異常
    '      uploadShipname    アップロード拠点名称
    '                        ※所属拠点と同じと限らない。あくまでもアップロードする拠点
    '      FileName          アップロードファイル名（※リカバリ処理で登録ファイルを確認するため）
    '****************************************************
    Public Sub setCsvBillingInfo(ByVal csvData()() As String, ByVal userid As String, ByVal userName As String, ByRef errFlg As Integer, ByVal uploadShipname As String, ByVal FileName As String)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)
        Dim dt As DateTime

        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia

        Try

            Dim newData As Integer  '新規追加登録確認用
            Dim add As Integer      '追加登録確認用

            For i = 0 To csvData.Length - 1

                If i <> 0 Then

                    newData = -1
                    add = -1
                    Dim select_sql1 As String = ""
                    select_sql1 = "SELECT * FROM dbo.Billing_Info WHERE DELFG = 0 "
                    select_sql1 &= "AND upload_Branch = '" & uploadShipname & "' "
                    select_sql1 &= "AND Invoice_No =  '" & csvData(i)(3) & "' "
                    select_sql1 &= "AND Item_No =  '" & csvData(i)(4) & "' "

                    Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
                    Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
                    Dim Builder1 As New SqlCommandBuilder(Adapter1)
                    Dim ds1 As New DataSet
                    Dim dr1 As DataRow
                    Adapter1.Fill(ds1)

                    If ds1.Tables(0).Rows.Count = 1 Then
                        'インポート済の為上書き登録
                        dr1 = ds1.Tables(0).Rows(0)
                        add = 1
                    Else
                        '新規追加登録
                        dr1 = ds1.Tables(0).NewRow
                        newData = 1
                    End If

                    If newData = 1 Then
                        dr1("CRTDT") = dtNow
                        dr1("CRTCD") = userid
                    End If

                    If add = 1 Then
                        dr1("UPDDT") = dtNow
                        dr1("UPDCD") = userid
                    End If

                    dr1("UPDPG") = "Class_analysis.vb"
                    dr1("DELFG") = 0
                    dr1("upload_date") = dtNow
                    dr1("upload_user") = userName
                    dr1("upload_Branch") = uploadShipname
                    dr1("Branch_Code") = csvData(i)(0)
                    dr1("Ship_Branch") = csvData(i)(1)

                    Dim BillingDateStr As String = csvData(i)(2)

                    If csvData(i)(2).Length = 8 Then

                        Dim tmpMon As String = BillingDateStr.Substring(4, 2)
                        Dim tmpDay As String = BillingDateStr.Substring(6, 2)
                        Dim tmpYear As String = BillingDateStr.Substring(0, 4)

                        csvData(i)(2) = tmpYear & "/" & tmpMon & "/" & tmpDay

                        Dim BillingDate As DateTime

                        If DateTime.TryParse(csvData(i)(2), BillingDate) Then
                            dr1("Billing_Date") = csvData(i)(2)
                        End If

                    End If

                    dr1("Invoice_No") = csvData(i)(3)
                    dr1("Item_No") = csvData(i)(4)
                    dr1("Delivery_No") = csvData(i)(5)
                    dr1("PO_No") = csvData(i)(6)
                    dr1("PO_Type_Code") = csvData(i)(7)
                    dr1("Part_No") = csvData(i)(8)
                    dr1("Billing_Qty") = csvData(i)(9)

                    If csvData(i)(10) = "" Then
                        dr1("Amount") = "0.00"
                    Else
                        dr1("Amount") = csvData(i)(10)
                    End If

                    If csvData(i)(11) = "" Then
                        dr1("SGST_UTGST") = "0.00"
                    Else
                        dr1("SGST_UTGST") = csvData(i)(11)
                    End If

                    If csvData(i)(12) = "" Then
                        dr1("CGST") = "0.00"
                    Else
                        dr1("CGST") = csvData(i)(12)
                    End If

                    If csvData(i)(13) = "" Then
                        dr1("IGST") = "0.00"
                    Else
                        dr1("IGST") = csvData(i)(13)
                    End If

                    If csvData(i)(14) = "" Then
                        dr1("Cess") = "0.00"
                    Else
                        dr1("Cess") = csvData(i)(14)
                    End If

                    If csvData(i)(15) = "" Then
                        dr1("Tax") = "0.00"
                    Else
                        dr1("Tax") = csvData(i)(15)
                    End If

                    dr1("Core_Flag") = csvData(i)(16)

                    If csvData(i)(17) = "" Then
                        dr1("Total") = "0.00"
                    Else
                        dr1("Total") = csvData(i)(17)
                    End If

                    dr1("Upload_FileName") = FileName

                    '新規DRをDataTableに追加
                    If newData = 1 Then
                        ds1.Tables(0).Rows.Add(dr1)
                    End If

                    Adapter1.Update(ds1)

                End If

            Next i

            '■コミット
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
    '処理名　：setWtyExcelDown
    '処理概要：出力する情報をデータセットと構造体にセット
    '　　　　　dailyReportの部品情報をWtyExcelテーブルに追加登録　
    '引数　　：dsWtyExcel　
    '          wtyData
    '      　　userid
    '      　　userName
    '      　　shipCode
    '      　　exportShipName
    '　　　　　errFlg      戻り値　0:正常　1:異常
    '      　　setMon      対象月
    '      　　dateFrom    対象期間　FROM　※月指定がされていないとき
    '      　　dateTo　　　対象期間　TO
    '****************************************************
    Public Sub setWtyExcelDown(ByRef dsWtyExcel As DataSet, ByRef wtyData() As WTY_EXCEL, ByVal userid As String, ByVal userName As String, ByVal shipCode As String, ByVal exportShipName As String, ByRef errFlg As Integer, ByVal setMon As String, ByVal dateFrom As String, ByVal dateTo As String)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try
            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia

            '■更新対象を取得
            Dim select_sql As String = ""
            Dim ds As New DataSet

            select_sql &= "SELECT B.OW_Labor, B.OW_Parts, B.OW_total, B.OW_Tax, A.* "
            select_sql &= "FROM dbo.Wty_Excel A "
            select_sql &= ",SC_DSR B "
            select_sql &= "WHERE A.DELFG = 0 "
            select_sql &= "AND A.ASC_Claim_No = B.ServiceOrder_No "
            select_sql &= "AND A.Status = '80' "
            select_sql &= "AND B.Branch_name = '" & exportShipName & "' "
            select_sql &= "AND A.Branch_Code = '" & shipCode & "' "
            select_sql &= "AND A.Delivery_Date = B.Billing_date "

            If setMon = "00" Then
                If dateTo <> "" Then
                    If dateFrom <> "" Then
                        select_sql &= "And LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),10) <= '" & dateTo & "' "
                        select_sql &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),10) >= '" & dateFrom & "';"
                    Else
                        select_sql &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),10) <= '" & dateTo & "';"
                    End If
                Else
                    If dateFrom <> "" Then
                        select_sql &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),10) >= '" & dateFrom & "';"
                    End If
                End If

            Else
                select_sql &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),7) = '" & dtNow.ToLongDateString.Substring(0, 4) & "/" & setMon & "'; "
            End If

            Dim sqlSelect As New SqlCommand(select_sql, con, trn)
            Dim Adapter As New SqlDataAdapter(sqlSelect)

            Adapter.Fill(ds)

            If ds.Tables(0).Rows.Count <> 0 Then

                ReDim wtyData(ds.Tables(0).Rows.Count - 1)

                For i = 0 To ds.Tables(0).Rows.Count - 1

                    Dim dr As DataRow = ds.Tables(0).Rows(i)

                    If dr("OW_Labor") IsNot DBNull.Value Then
                        wtyData(i).OW_Labor = dr("OW_Labor")
                    Else
                        wtyData(i).OW_Labor = "0.00"
                    End If

                    If dr("OW_Parts") IsNot DBNull.Value Then
                        wtyData(i).OW_Parts = dr("OW_Parts")
                    Else
                        wtyData(i).OW_Parts = "0.00"
                    End If

                    If dr("OW_total") IsNot DBNull.Value Then
                        wtyData(i).OW_total = dr("OW_total")
                    Else
                        wtyData(i).OW_total = "0.00"
                    End If

                    If dr("OW_Tax") IsNot DBNull.Value Then
                        wtyData(i).OW_Tax = dr("OW_Tax")
                    Else
                        wtyData(i).OW_Tax = "0.00"
                    End If

                    If dr("ASC_Claim_No") IsNot DBNull.Value Then
                        wtyData(i).ASC_Claim_No = dr("ASC_Claim_No")
                    End If

                    If dr("Samsung_Claim_No") IsNot DBNull.Value Then
                        wtyData(i).Samsung_Claim_No = dr("Samsung_Claim_No")
                    End If

                Next i

                '■更新
                For i = 0 To wtyData.Length - 1

                    Dim select_sql2 As String = ""
                    select_sql2 &= "SELECT A.* "
                    select_sql2 &= "FROM dbo.Wty_Excel A "
                    select_sql2 &= "WHERE A.DELFG = 0 "
                    select_sql2 &= "AND A.Status = '80' "
                    select_sql2 &= "AND A.Branch_Code = '" & shipCode & "' "
                    select_sql2 &= "AND A.ASC_Claim_No = '" & wtyData(i).ASC_Claim_No & "' "
                    select_sql2 &= "AND A.Samsung_Claim_No = '" & wtyData(i).Samsung_Claim_No & "' "

                    If setMon = "00" Then
                        If dateTo <> "" Then
                            If dateFrom <> "" Then
                                select_sql2 &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),10) <= '" & dateTo & "' "
                                select_sql2 &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),10) >= '" & dateFrom & "';"
                            Else
                                select_sql2 &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),10) <= '" & dateTo & "';"
                            End If
                        Else
                            If dateFrom <> "" Then
                                select_sql2 &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),10) >= '" & dateFrom & "';"
                            End If
                        End If

                    Else
                        select_sql2 &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),7) = '" & dtNow.ToLongDateString.Substring(0, 4) & "/" & setMon & "'; "
                    End If

                    Dim sqlSelect2 As New SqlCommand(select_sql2, con, trn)
                    Dim Adapter2 As New SqlDataAdapter(sqlSelect2)
                    Dim Builder2 As New SqlCommandBuilder(Adapter2)
                    Dim ds2 As New DataSet

                    Adapter2.Fill(ds2)

                    If ds2.Tables(0).Rows.Count = 1 Then

                        Dim dr As DataRow = ds2.Tables(0).Rows(0)

                        dr("Labor_Amount_I") = wtyData(i).OW_Labor
                        dr("Parts_Amount_I") = wtyData(i).OW_Parts
                        dr("Total_Invoice_Amount_I") = wtyData(i).OW_total
                        'dr("Tax") = wtyData(i).OW_Tax
                        dr("UPDDT") = dtNow
                        dr("UPDCD") = userid
                        dr("upload_date") = dtNow
                        dr("upload_user") = userName

                        '更新
                        Adapter2.Update(ds2)

                    End If

                Next i

                '■コミット
                trn.Commit()

                '■更新後の情報を取得
                Dim select_sql3 As String = ""
                'Comment on 20190809
                'Modified
                '''''''''''Option1: Orginal
                'select_sql3 &= "SELECT B.OW_Tax, A.* "
                ''''''''''Option2: Feature
                ''''''''''select_sql3 &= "SELECT C.Parts_invoice_No, C.Labor_Invoice_No,B.OW_Tax, A.* "
                ''''''''''select_sql3 &= "AND C.samsung_Ref_No = B.ServiceOrder_No "
                ''''''''''Option3: Runttion
                select_sql3 &= "SELECT (SELECT TOP 1 Parts_invoice_No FROM Invoice_update WHERE samsung_Ref_No=A.ASC_Claim_No) AS 'Parts_invoice_No', "
                select_sql3 &= " (SELECT TOP 1 Labor_Invoice_No FROM Invoice_update WHERE samsung_Ref_No=A.ASC_Claim_No) AS 'Labor_Invoice_No', "
                select_sql3 &= " B.OW_Tax, A.* "
                select_sql3 &= "FROM dbo.Wty_Excel A "
                select_sql3 &= ",SC_DSR B "
                select_sql3 &= "WHERE A.DELFG = 0 "
                select_sql3 &= "AND A.ASC_Claim_No = B.ServiceOrder_No "
                select_sql3 &= "AND A.Delivery_Date = B.Billing_date "
                select_sql3 &= "AND A.Status = '80' "
                select_sql3 &= "AND B.Branch_name = '" & exportShipName & "' "
                select_sql3 &= "AND A.Branch_Code = '" & shipCode & "' "

                If setMon = "00" Then
                    If dateTo <> "" Then
                        If dateFrom <> "" Then
                            select_sql3 &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),10) <= '" & dateTo & "' "
                            select_sql3 &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),10) >= '" & dateFrom & "';"
                        Else
                            select_sql3 &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),10) <= '" & dateTo & "';"
                        End If
                    Else
                        If dateFrom <> "" Then
                            select_sql3 &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),10) >= '" & dateFrom & "';"
                        End If
                    End If

                Else
                    select_sql3 &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),7) = '" & dtNow.ToLongDateString.Substring(0, 4) & "/" & setMon & "'; "
                End If

                Dim sqlSelect3 As New SqlCommand(select_sql3, con, trn)
                Dim Adapter3 As New SqlDataAdapter(sqlSelect3)

                Adapter3.Fill(dsWtyExcel)

                If dsWtyExcel.Tables(0).Rows.Count = 0 Then
                    dsWtyExcel = Nothing
                Else

                    '出力対象を構造体に設定
                    ReDim wtyData(dsWtyExcel.Tables(0).Rows.Count - 1)

                    For i = 0 To dsWtyExcel.Tables(0).Rows.Count - 1

                        Dim dr As DataRow = dsWtyExcel.Tables(0).Rows(i)

                        If dr("ASC_Code") IsNot DBNull.Value Then
                            wtyData(i).ASC_Code = dr("ASC_Code")
                        End If

                        If dr("Branch_Code") IsNot DBNull.Value Then
                            wtyData(i).Branch_Code = dr("Branch_Code")
                        End If

                        If dr("ASC_Claim_No") IsNot DBNull.Value Then
                            wtyData(i).ASC_Claim_No = dr("ASC_Claim_No")
                        End If

                        If dr("Samsung_Claim_No") IsNot DBNull.Value Then
                            wtyData(i).Samsung_Claim_No = dr("Samsung_Claim_No")
                        End If

                        If dr("Service_Type") IsNot DBNull.Value Then
                            wtyData(i).Service_Type = dr("Service_Type")
                        End If

                        If dr("Consumer_Name") IsNot DBNull.Value Then
                            wtyData(i).Consumer_Name = dr("Consumer_Name")
                        End If

                        If dr("Consumer_Addr1") IsNot DBNull.Value Then
                            wtyData(i).Consumer_Addr1 = dr("Consumer_Addr1")
                        End If

                        If dr("Consumer_Addr2") IsNot DBNull.Value Then
                            wtyData(i).Consumer_Addr2 = dr("Consumer_Addr2")
                        End If

                        If dr("Consumer_Telephone") IsNot DBNull.Value Then
                            wtyData(i).Consumer_Telephone = dr("Consumer_Telephone")
                        End If

                        If dr("Consumer_Fax") IsNot DBNull.Value Then
                            wtyData(i).Consumer_Fax = dr("Consumer_Fax")
                        End If

                        If dr("Postal_Code") IsNot DBNull.Value Then
                            wtyData(i).Postal_Code = dr("Postal_Code")
                        End If

                        If dr("Model") IsNot DBNull.Value Then
                            wtyData(i).Model = dr("Model")
                        End If

                        If dr("Serial_No") IsNot DBNull.Value Then
                            wtyData(i).Serial_No = dr("Serial_No")
                        End If

                        If dr("IMEI_No") IsNot DBNull.Value Then
                            wtyData(i).IMEI_No = dr("IMEI_No")
                        End If

                        If dr("Defect_Type") IsNot DBNull.Value Then
                            wtyData(i).Defect_Type = dr("Defect_Type")
                        End If

                        If dr("Condition") IsNot DBNull.Value Then
                            wtyData(i).Condition = dr("Condition")
                        End If

                        If dr("Symptom") IsNot DBNull.Value Then
                            wtyData(i).Symptom = dr("Symptom")
                        End If

                        If dr("Defect_Code") IsNot DBNull.Value Then
                            wtyData(i).Defect_Code = dr("Defect_Code")
                        End If

                        If dr("Repair_Code") IsNot DBNull.Value Then
                            wtyData(i).Repair_Code = dr("Repair_Code")
                        End If

                        If dr("Defect_Desc") IsNot DBNull.Value Then
                            wtyData(i).Defect_Desc = dr("Defect_Desc")
                        End If

                        If dr("Repair_Description") IsNot DBNull.Value Then
                            wtyData(i).Repair_Description = dr("Repair_Description")
                        End If

                        If dr("Purchase_Date") IsNot DBNull.Value Then
                            wtyData(i).Purchase_Date = dr("Purchase_Date")
                        End If

                        If dr("Repair_Received_Date") IsNot DBNull.Value Then
                            wtyData(i).Repair_Received_Date = dr("Repair_Received_Date")
                        End If

                        If dr("Completed_Date") IsNot DBNull.Value Then
                            wtyData(i).Completed_Date = dr("Completed_Date")
                        End If

                        If dr("Delivery_Date") IsNot DBNull.Value Then
                            wtyData(i).Delivery_Date = dr("Delivery_Date")
                        End If

                        If dr("Production_Date") IsNot DBNull.Value Then
                            wtyData(i).Production_Date = dr("Production_Date")
                        End If

                        If dr("Labor_Amount_I") IsNot DBNull.Value Then
                            wtyData(i).Labor_Amount = dr("Labor_Amount_I")
                        End If

                        If dr("Parts_Amount_I") IsNot DBNull.Value Then
                            wtyData(i).Parts_Amount = dr("Parts_Amount_I")
                        End If

                        If dr("OW_Tax") IsNot DBNull.Value Then
                            wtyData(i).Tax = dr("OW_Tax")
                        End If
                        '''''''''''''''''''''''''''''''
                        'Comment on 20190809
                        'Modified 
                        '''''''''''''''''''''''''''''
                        '''
                        If dr("Parts_invoice_No") IsNot DBNull.Value Then
                            wtyData(i).Parts_invoice_No = dr("Parts_invoice_No")
                        End If
                        If dr("Labor_Invoice_No") IsNot DBNull.Value Then
                            wtyData(i).Labor_Invoice_No = dr("Labor_Invoice_No")
                        End If

                        If dr("Freight") IsNot DBNull.Value Then
                            wtyData(i).Freight = dr("Freight")
                        End If
                        If dr("Other") IsNot DBNull.Value Then
                            wtyData(i).Other = dr("Other")
                        End If
                        If dr("Parts_SGST") IsNot DBNull.Value Then
                            wtyData(i).Parts_SGST = dr("Parts_SGST")
                        End If
                        If dr("Parts_UTGST") IsNot DBNull.Value Then
                            wtyData(i).Parts_UTGST = dr("Parts_UTGST")
                        End If
                        If dr("Parts_CGST") IsNot DBNull.Value Then
                            wtyData(i).Parts_CGST = dr("Parts_CGST")
                        End If
                        If dr("Parts_IGST") IsNot DBNull.Value Then
                            wtyData(i).Parts_IGST = dr("Parts_IGST")
                        End If
                        If dr("Parts_Cess") IsNot DBNull.Value Then
                            wtyData(i).Parts_Cess = dr("Parts_Cess")
                        End If
                        If dr("SGST") IsNot DBNull.Value Then
                            wtyData(i).SGST = dr("SGST")
                        End If
                        If dr("UTGST") IsNot DBNull.Value Then
                            wtyData(i).UTGST = dr("UTGST")
                        End If
                        If dr("CGST") IsNot DBNull.Value Then
                            wtyData(i).CGST = dr("CGST")
                        End If
                        If dr("IGST") IsNot DBNull.Value Then
                            wtyData(i).IGST = dr("IGST")
                        End If
                        If dr("Cess") IsNot DBNull.Value Then
                            wtyData(i).Cess = dr("Cess")
                        End If
                        ''''''''''''''''''''''''End
                        If dr("Total_Invoice_Amount_I") IsNot DBNull.Value Then
                            wtyData(i).Total_Invoice_Amount = dr("Total_Invoice_Amount_I")
                        End If

                        If dr("Remark") IsNot DBNull.Value Then
                            wtyData(i).Remark = dr("Remark")
                        End If

                        If dr("Tr_No") IsNot DBNull.Value Then
                            wtyData(i).Tr_No = dr("Tr_No")
                        End If

                        If dr("Tr_Type") IsNot DBNull.Value Then
                            wtyData(i).Tr_Type = dr("Tr_Type")
                        End If

                        If dr("Status") IsNot DBNull.Value Then
                            wtyData(i).Status = dr("Status")
                        End If

                        If dr("Engineer") IsNot DBNull.Value Then
                            wtyData(i).Engineer = dr("Engineer")
                        End If

                        If dr("Collection_Point") IsNot DBNull.Value Then
                            wtyData(i).Collection_Point = dr("Collection_Point")
                        End If

                        If dr("Collection_Point_Name") IsNot DBNull.Value Then
                            wtyData(i).Collection_Point_Name = dr("Collection_Point_Name")
                        End If

                        If dr("Location_1") IsNot DBNull.Value Then
                            wtyData(i).Location_1 = dr("Location_1")
                        End If

                        If dr("Part_1") IsNot DBNull.Value Then
                            wtyData(i).Part_1 = dr("Part_1")
                        End If

                        If dr("Qty_1") IsNot DBNull.Value Then
                            wtyData(i).Qty_1 = dr("Qty_1")
                        End If

                        If dr("Unit_Price_1") IsNot DBNull.Value Then
                            wtyData(i).Unit_Price_1 = dr("Unit_Price_1")
                        End If

                        If dr("Doc_Num_1") IsNot DBNull.Value Then
                            wtyData(i).Doc_Num_1 = dr("Doc_Num_1")
                        End If

                        If dr("Matrial_Serial_1") IsNot DBNull.Value Then
                            wtyData(i).Matrial_Serial_1 = dr("Matrial_Serial_1")
                        End If

                        If dr("Location_2") IsNot DBNull.Value Then
                            wtyData(i).Location_2 = dr("Location_2")
                        End If

                        If dr("Part_2") IsNot DBNull.Value Then
                            wtyData(i).Part_2 = dr("Part_2")
                        End If

                        If dr("Qty_2") IsNot DBNull.Value Then
                            wtyData(i).Qty_2 = dr("Qty_2")
                        End If

                        If dr("Unit_Price_2") IsNot DBNull.Value Then
                            wtyData(i).Unit_Price_2 = dr("Unit_Price_2")
                        End If

                        If dr("Doc_Num_2") IsNot DBNull.Value Then
                            wtyData(i).Doc_Num_2 = dr("Doc_Num_2")
                        End If

                        If dr("Matrial_Serial_2") IsNot DBNull.Value Then
                            wtyData(i).Matrial_Serial_2 = dr("Matrial_Serial_2")
                        End If

                        If dr("Location_3") IsNot DBNull.Value Then
                            wtyData(i).Location_3 = dr("Location_3")
                        End If

                        If dr("Part_3") IsNot DBNull.Value Then
                            wtyData(i).Part_3 = dr("Part_3")
                        End If

                        If dr("Qty_3") IsNot DBNull.Value Then
                            wtyData(i).Qty_3 = dr("Qty_3")
                        End If

                        If dr("Unit_Price_3") IsNot DBNull.Value Then
                            wtyData(i).Unit_Price_3 = dr("Unit_Price_3")
                        End If

                        If dr("Doc_Num_3") IsNot DBNull.Value Then
                            wtyData(i).Doc_Num_3 = dr("Doc_Num_3")
                        End If

                        If dr("Matrial_Serial_3") IsNot DBNull.Value Then
                            wtyData(i).Matrial_Serial_3 = dr("Matrial_Serial_3")
                        End If

                        If dr("Location_4") IsNot DBNull.Value Then
                            wtyData(i).Location_4 = dr("Location_4")
                        End If

                        If dr("Part_4") IsNot DBNull.Value Then
                            wtyData(i).Part_4 = dr("Part_4")
                        End If

                        If dr("Qty_4") IsNot DBNull.Value Then
                            wtyData(i).Qty_4 = dr("Qty_4")
                        End If

                        If dr("Unit_Price_4") IsNot DBNull.Value Then
                            wtyData(i).Unit_Price_4 = dr("Unit_Price_4")
                        End If

                        If dr("Doc_Num_4") IsNot DBNull.Value Then
                            wtyData(i).Doc_Num_4 = dr("Doc_Num_4")
                        End If

                        If dr("Matrial_Serial_4") IsNot DBNull.Value Then
                            wtyData(i).Matrial_Serial_4 = dr("Matrial_Serial_4")
                        End If

                        If dr("Location_5") IsNot DBNull.Value Then
                            wtyData(i).Location_5 = dr("Location_5")
                        End If

                        If dr("Part_5") IsNot DBNull.Value Then
                            wtyData(i).Part_5 = dr("Part_5")
                        End If

                        If dr("Qty_5") IsNot DBNull.Value Then
                            wtyData(i).Qty_5 = dr("Qty_5")
                        End If

                        If dr("Unit_Price_5") IsNot DBNull.Value Then
                            wtyData(i).Unit_Price_5 = dr("Unit_Price_5")
                        End If

                        If dr("Doc_Num_5") IsNot DBNull.Value Then
                            wtyData(i).Doc_Num_5 = dr("Doc_Num_5")
                        End If

                        If dr("Matrial_Serial_5") IsNot DBNull.Value Then
                            wtyData(i).Matrial_Serial_5 = dr("Matrial_Serial_5")
                        End If

                        If dr("Location_6") IsNot DBNull.Value Then
                            wtyData(i).Location_6 = dr("Location_6")
                        End If

                        If dr("Part_6") IsNot DBNull.Value Then
                            wtyData(i).Part_6 = dr("Part_6")
                        End If

                        If dr("Qty_6") IsNot DBNull.Value Then
                            wtyData(i).Qty_6 = dr("Qty_6")
                        End If

                        If dr("Unit_Price_6") IsNot DBNull.Value Then
                            wtyData(i).Unit_Price_6 = dr("Unit_Price_6")
                        End If

                        If dr("Doc_Num_6") IsNot DBNull.Value Then
                            wtyData(i).Doc_Num_6 = dr("Doc_Num_6")
                        End If

                        If dr("Matrial_Serial_6") IsNot DBNull.Value Then
                            wtyData(i).Matrial_Serial_6 = dr("Matrial_Serial_6")
                        End If

                        If dr("Location_7") IsNot DBNull.Value Then
                            wtyData(i).Location_7 = dr("Location_7")
                        End If

                        If dr("Part_7") IsNot DBNull.Value Then
                            wtyData(i).Part_7 = dr("Part_7")
                        End If

                        If dr("Qty_7") IsNot DBNull.Value Then
                            wtyData(i).Qty_7 = dr("Qty_7")
                        End If

                        If dr("Unit_Price_7") IsNot DBNull.Value Then
                            wtyData(i).Unit_Price_7 = dr("Unit_Price_7")
                        End If

                        If dr("Doc_Num_7") IsNot DBNull.Value Then
                            wtyData(i).Doc_Num_7 = dr("Doc_Num_7")
                        End If

                        If dr("Matrial_Serial_7") IsNot DBNull.Value Then
                            wtyData(i).Matrial_Serial_7 = dr("Matrial_Serial_7")
                        End If

                        If dr("Location_8") IsNot DBNull.Value Then
                            wtyData(i).Location_8 = dr("Location_8")
                        End If

                        If dr("Part_8") IsNot DBNull.Value Then
                            wtyData(i).Part_8 = dr("Part_8")
                        End If

                        If dr("Qty_8") IsNot DBNull.Value Then
                            wtyData(i).Qty_8 = dr("Qty_8")
                        End If

                        If dr("Unit_Price_8") IsNot DBNull.Value Then
                            wtyData(i).Unit_Price_8 = dr("Unit_Price_8")
                        End If

                        If dr("Doc_Num_8") IsNot DBNull.Value Then
                            wtyData(i).Doc_Num_8 = dr("Doc_Num_8")
                        End If

                        If dr("Matrial_Serial_8") IsNot DBNull.Value Then
                            wtyData(i).Matrial_Serial_8 = dr("Matrial_Serial_8")
                        End If

                        If dr("Location_9") IsNot DBNull.Value Then
                            wtyData(i).Location_9 = dr("Location_9")
                        End If

                        If dr("Part_9") IsNot DBNull.Value Then
                            wtyData(i).Part_9 = dr("Part_9")
                        End If

                        If dr("Qty_9") IsNot DBNull.Value Then
                            wtyData(i).Qty_9 = dr("Qty_9")
                        End If

                        If dr("Unit_Price_9") IsNot DBNull.Value Then
                            wtyData(i).Unit_Price_9 = dr("Unit_Price_9")
                        End If

                        If dr("Doc_Num_9") IsNot DBNull.Value Then
                            wtyData(i).Doc_Num_9 = dr("Doc_Num_9")
                        End If

                        If dr("Matrial_Serial_9") IsNot DBNull.Value Then
                            wtyData(i).Matrial_Serial_9 = dr("Matrial_Serial_9")
                        End If

                        If dr("Location_10") IsNot DBNull.Value Then
                            wtyData(i).Location_10 = dr("Location_10")
                        End If

                        If dr("Part_10") IsNot DBNull.Value Then
                            wtyData(i).Part_10 = dr("Part_10")
                        End If

                        If dr("Qty_10") IsNot DBNull.Value Then
                            wtyData(i).Qty_10 = dr("Qty_10")
                        End If

                        If dr("Unit_Price_10") IsNot DBNull.Value Then
                            wtyData(i).Unit_Price_10 = dr("Unit_Price_10")
                        End If

                        If dr("Doc_Num_10") IsNot DBNull.Value Then
                            wtyData(i).Doc_Num_10 = dr("Doc_Num_10")
                        End If

                        If dr("Matrial_Serial_10") IsNot DBNull.Value Then
                            wtyData(i).Matrial_Serial_10 = dr("Matrial_Serial_10")
                        End If

                        If dr("Location_11") IsNot DBNull.Value Then
                            wtyData(i).Location_11 = dr("Location_11")
                        End If

                        If dr("Part_11") IsNot DBNull.Value Then
                            wtyData(i).Part_11 = dr("Part_11")
                        End If

                        If dr("Qty_11") IsNot DBNull.Value Then
                            wtyData(i).Qty_11 = dr("Qty_11")
                        End If

                        If dr("Unit_Price_11") IsNot DBNull.Value Then
                            wtyData(i).Unit_Price_11 = dr("Unit_Price_11")
                        End If

                        If dr("Doc_Num_11") IsNot DBNull.Value Then
                            wtyData(i).Doc_Num_11 = dr("Doc_Num_11")
                        End If

                        If dr("Matrial_Serial_11") IsNot DBNull.Value Then
                            wtyData(i).Matrial_Serial_11 = dr("Matrial_Serial_11")
                        End If

                        If dr("Location_12") IsNot DBNull.Value Then
                            wtyData(i).Location_12 = dr("Location_12")
                        End If

                        If dr("Part_12") IsNot DBNull.Value Then
                            wtyData(i).Part_12 = dr("Part_12")
                        End If

                        If dr("Qty_12") IsNot DBNull.Value Then
                            wtyData(i).Qty_12 = dr("Qty_12")
                        End If

                        If dr("Unit_Price_12") IsNot DBNull.Value Then
                            wtyData(i).Unit_Price_12 = dr("Unit_Price_12")
                        End If

                        If dr("Doc_Num_12") IsNot DBNull.Value Then
                            wtyData(i).Doc_Num_12 = dr("Doc_Num_12")
                        End If

                        If dr("Matrial_Serial_12") IsNot DBNull.Value Then
                            wtyData(i).Matrial_Serial_12 = dr("Matrial_Serial_12")
                        End If

                        If dr("Location_13") IsNot DBNull.Value Then
                            wtyData(i).Location_13 = dr("Location_13")
                        End If

                        If dr("Part_13") IsNot DBNull.Value Then
                            wtyData(i).Part_13 = dr("Part_13")
                        End If

                        If dr("Qty_13") IsNot DBNull.Value Then
                            wtyData(i).Qty_13 = dr("Qty_13")
                        End If

                        If dr("Unit_Price_13") IsNot DBNull.Value Then
                            wtyData(i).Unit_Price_13 = dr("Unit_Price_13")
                        End If

                        If dr("Doc_Num_13") IsNot DBNull.Value Then
                            wtyData(i).Doc_Num_13 = dr("Doc_Num_13")
                        End If

                        If dr("Matrial_Serial_13") IsNot DBNull.Value Then
                            wtyData(i).Matrial_Serial_13 = dr("Matrial_Serial_13")
                        End If

                        If dr("Location_14") IsNot DBNull.Value Then
                            wtyData(i).Location_14 = dr("Location_14")
                        End If

                        If dr("Part_14") IsNot DBNull.Value Then
                            wtyData(i).Part_14 = dr("Part_14")
                        End If

                        If dr("Qty_14") IsNot DBNull.Value Then
                            wtyData(i).Qty_14 = dr("Qty_14")
                        End If

                        If dr("Unit_Price_14") IsNot DBNull.Value Then
                            wtyData(i).Unit_Price_14 = dr("Unit_Price_14")
                        End If

                        If dr("Doc_Num_14") IsNot DBNull.Value Then
                            wtyData(i).Doc_Num_14 = dr("Doc_Num_14")
                        End If

                        If dr("Matrial_Serial_14") IsNot DBNull.Value Then
                            wtyData(i).Matrial_Serial_14 = dr("Matrial_Serial_14")
                        End If

                        If dr("Location_15") IsNot DBNull.Value Then
                            wtyData(i).Location_15 = dr("Location_15")
                        End If

                        If dr("Part_15") IsNot DBNull.Value Then
                            wtyData(i).Part_15 = dr("Part_15")
                        End If

                        If dr("Qty_15") IsNot DBNull.Value Then
                            wtyData(i).Qty_15 = dr("Qty_15")
                        End If

                        If dr("Unit_Price_15") IsNot DBNull.Value Then
                            wtyData(i).Unit_Price_15 = dr("Unit_Price_15")
                        End If

                        If dr("Doc_Num_15") IsNot DBNull.Value Then
                            wtyData(i).Doc_Num_15 = dr("Doc_Num_15")
                        End If

                        If dr("Matrial_Serial_15") IsNot DBNull.Value Then
                            wtyData(i).Matrial_Serial_15 = dr("Matrial_Serial_15")
                        End If

                    Next i

                End If

            Else
                dsWtyExcel = Nothing
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

    '****************************************************
    '処理：Sales_Invoice2samsung
    '引数：dsInvoiceUpdate　出力対象をセット
    '      exportShipName
    '　　　errFlg           戻り値　0:正常　1:異常
    '      setMon           対象請求月
    '****************************************************
    Public Sub setInvoice(ByRef invoiceData() As INVOICE, ByVal exportShipName As String, ByRef errFlg As Integer, ByVal setMon As String, ByVal number As String)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try
            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia

            '■出力対象を取得
            Dim dsInvoiceUpdate As New DataSet
            Dim select_sql As String = ""
            select_sql &= "SELECT A.* "
            select_sql &= "FROM dbo.Invoice_update A "
            select_sql &= "WHERE A.DELFG = 0 "
            select_sql &= "AND LEFT(CONVERT(VARCHAR, A.Invoice_date,111), 7) = '" & dtNow.ToShortDateString.Substring(0, 4) & "/" & setMon & "' "
            select_sql &= "AND A.upload_Branch = '" & exportShipName & "' "
            select_sql &= "AND A.number ='" & number & "';"

            Dim sqlSelect As New SqlCommand(select_sql, con, trn)
            Dim Adapter As New SqlDataAdapter(sqlSelect)

            Adapter.Fill(dsInvoiceUpdate)

            '出力対象を構造体に設定
            If dsInvoiceUpdate.Tables(0).Rows.Count <> 0 Then

                ReDim invoiceData(dsInvoiceUpdate.Tables(0).Rows.Count - 1)

                For i = 0 To dsInvoiceUpdate.Tables(0).Rows.Count - 1

                    Dim dr As DataRow = dsInvoiceUpdate.Tables(0).Rows(i)

                    If dr("samsung_Ref_No") IsNot DBNull.Value Then
                        invoiceData(i).samsung_Ref_No = dr("samsung_Ref_No")
                    End If

                    If dr("Your_Ref_No") IsNot DBNull.Value Then
                        invoiceData(i).Your_Ref_No = dr("Your_Ref_No")
                    End If

                    If dr("Model") IsNot DBNull.Value Then
                        invoiceData(i).Model = dr("Model")
                    End If

                    If dr("Serial") IsNot DBNull.Value Then
                        invoiceData(i).Serial = dr("Serial")
                    End If

                    If dr("Product") IsNot DBNull.Value Then
                        invoiceData(i).Product = dr("Product")
                    End If

                    If dr("Serivice") IsNot DBNull.Value Then
                        invoiceData(i).Serivice = dr("Serivice")
                    End If

                    If dr("Defect_Code") IsNot DBNull.Value Then
                        invoiceData(i).Defect_Code = dr("Defect_Code")
                    End If

                    If dr("Currency") IsNot DBNull.Value Then
                        invoiceData(i).Currency = dr("Currency")
                    End If

                    If dr("Invoice") IsNot DBNull.Value Then
                        invoiceData(i).Invoice = dr("Invoice")
                    End If

                    If dr("Labor") IsNot DBNull.Value Then
                        invoiceData(i).Labor = dr("Labor")
                    End If

                    If dr("Parts") IsNot DBNull.Value Then
                        invoiceData(i).Parts = dr("Parts")
                    End If

                    If dr("Felight") IsNot DBNull.Value Then
                        invoiceData(i).Felight = dr("Felight")
                    End If

                    If dr("Other") IsNot DBNull.Value Then
                        invoiceData(i).Other = dr("Other")
                    End If

                    'If dr("Tax") IsNot DBNull.Value Then
                    '    invoiceData(i).Tax = dr("Tax")
                    'End If

                    If dr("Parts_invoice_No") IsNot DBNull.Value Then
                        invoiceData(i).Parts_invoice_No = dr("Parts_invoice_No")
                    End If

                    If dr("Labor_Invoice_No") IsNot DBNull.Value Then
                        invoiceData(i).Labor_Invoice_No = dr("Labor_Invoice_No")
                    End If

                    If dr("Invoice_date") IsNot DBNull.Value Then
                        invoiceData(i).Invoice_date = dr("Invoice_date")
                    End If

                Next i

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

    '****************************************************
    '処理：setPurchaseRegister
    '引数：dpurchaseData2　 出力対象をセット
    '      exportShipName
    '　　　errFlg           戻り値　0:正常　1:異常
    '      setMon           対象請求月
    '****************************************************
    Public Sub setPurchaseRegister(ByRef purchaseData2() As BILLINGINFODETAIL, ByVal exportShipName As String, ByVal shipCode As String, ByRef errFlg As Integer, ByVal setMon As String, ByVal dateFrom As String, ByVal dateTo As String)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try

            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia

            '■出力対象を取得
            'good_recivedテーブルよりユニークデータ取得
            Dim i, j As Integer

            Dim select_sql As String = ""
            select_sql &= "SELECT A.* , B.* "
            select_sql &= "FROM dbo.good_recived A "
            select_sql &= ",Billing_Info B "
            select_sql &= "WHERE A.DELFG = 0 "
            select_sql &= "AND A.delivery_No = B.Invoice_No "
            select_sql &= "AND A.upload_Branch = '" & exportShipName & "' "
            select_sql &= "AND B.Branch_Code = '" & shipCode & "' "

            If setMon = "00" Then
                If dateTo <> "" Then
                    If dateFrom <> "" Then
                        select_sql &= "AND LEFT(CONVERT(VARCHAR, A.delivery_date,111), 10) <= '" & dateTo & "' "
                        select_sql &= "AND LEFT(CONVERT(VARCHAR, A.delivery_date,111), 10) >= '" & dateFrom & "' "
                        select_sql &= "ORDER BY A.delivery_No;"
                    Else
                        select_sql &= "AND LEFT(CONVERT(VARCHAR, A.delivery_date,111), 10) <= '" & dateTo & "' "
                        select_sql &= "ORDER BY A.delivery_No;"
                    End If
                Else
                    If dateFrom <> "" Then
                        select_sql &= "AND LEFT(CONVERT(VARCHAR, A.delivery_date,111), 10) >= '" & dateFrom & "' "
                        select_sql &= "ORDER BY A.delivery_No;"
                    End If
                End If
            Else
                select_sql &= "AND LEFT(CONVERT(VARCHAR, A.delivery_date,111), 7) = '" & dtNow.ToShortDateString.Substring(0, 4) & "/" & setMon & "' "
                select_sql &= "ORDER BY A.delivery_No;"
            End If

            Dim sqlSelect As New SqlCommand(select_sql, con, trn)
            Dim Adapter As New SqlDataAdapter(sqlSelect)
            Dim dsgoodRecived As New DataSet

            Adapter.Fill(dsgoodRecived)

            '出力対象を構造体に設定
            If dsgoodRecived.Tables(0).Rows.Count <> 0 Then

                Dim purchaseData() As Class_analysis.BILLINGINFODETAIL  'tmp用

                ReDim purchaseData(dsgoodRecived.Tables(0).Rows.Count - 1)

                For i = 0 To dsgoodRecived.Tables(0).Rows.Count - 1

                    Dim dr As DataRow = dsgoodRecived.Tables(0).Rows(i)

                    If dr("delivery_date") IsNot DBNull.Value Then
                        purchaseData(i).delivery_date = dr("delivery_date")
                    End If

                    If dr("GR_Date") IsNot DBNull.Value Then
                        purchaseData(i).GR_Date = dr("GR_Date")
                    End If

                    If dr("delivery_No") IsNot DBNull.Value Then
                        purchaseData(i).Invoice_No = dr("delivery_No")
                    End If

                    If dr("local_invoice_No") IsNot DBNull.Value Then
                        purchaseData(i).local_invoice_No = dr("local_invoice_No")
                    End If

                    If dr("Items") IsNot DBNull.Value Then
                        purchaseData(i).Items = dr("Items")
                    End If

                    If dr("GR_Status") IsNot DBNull.Value Then
                        purchaseData(i).GR_Status = dr("GR_Status")
                    End If

                    If dr("Branch_Code") IsNot DBNull.Value Then
                        purchaseData(i).Branch_Code = dr("Branch_Code")
                    End If

                    If dr("Ship_Branch") IsNot DBNull.Value Then
                        purchaseData(i).Ship_Branch = dr("Ship_Branch")
                    End If

                    If dr("Delivery_No") IsNot DBNull.Value Then
                        purchaseData(i).Delivery_No = dr("Delivery_No")
                    End If

                    If dr("Amount") IsNot DBNull.Value Then
                        purchaseData(i).Amount = dr("Amount")
                    End If

                    If dr("SGST_UTGST") IsNot DBNull.Value Then
                        purchaseData(i).SGST_UTGST = dr("SGST_UTGST")
                    End If

                    If dr("CGST") IsNot DBNull.Value Then
                        purchaseData(i).CGST = dr("CGST")
                    End If

                    If dr("IGST") IsNot DBNull.Value Then
                        purchaseData(i).IGST = dr("IGST")
                    End If

                    If dr("Cess") IsNot DBNull.Value Then
                        purchaseData(i).Cess = dr("Cess")
                    End If

                    If dr("Tax") IsNot DBNull.Value Then
                        purchaseData(i).Tax = dr("Tax")
                    End If

                    If dr("Total") IsNot DBNull.Value Then
                        purchaseData(i).Total = dr("Total")
                    End If

                    If dr("Total") IsNot DBNull.Value Then
                        purchaseData(i).Invoice_No2 = dr("Invoice_No")
                    End If

                Next i

                Dim jStart As Integer
                Dim Cnt As Integer
                Dim sumAmount As Decimal
                Dim sumSGST_UTGST As Decimal
                Dim sumCGST As Decimal
                Dim sumIGST As Decimal
                Dim sumCess As Decimal
                Dim sumTax As Decimal
                Dim sumTotal As Decimal

                For i = 0 To purchaseData.Length - 1

                    'item数分処理
                    For j = jStart To (jStart + purchaseData(i).Items) - 1

                        If purchaseData(j).Invoice_No = purchaseData(j).Invoice_No2 Then
                            sumAmount = sumAmount + purchaseData(j).Amount
                            sumSGST_UTGST = sumSGST_UTGST + purchaseData(j).SGST_UTGST
                            sumCGST = sumCGST + purchaseData(j).CGST
                            sumIGST = sumIGST + purchaseData(j).IGST
                            sumCess = sumCess + purchaseData(j).Cess
                            sumTax = sumTax + purchaseData(j).Tax
                            sumTotal = sumTotal + purchaseData(j).Total
                        End If

                    Next j

                    ReDim Preserve purchaseData2(Cnt)

                    purchaseData2(Cnt).Branch_Code = purchaseData(i).Branch_Code
                    purchaseData2(Cnt).Ship_Branch = purchaseData(i).Ship_Branch
                    purchaseData2(Cnt).delivery_date = purchaseData(i).delivery_date
                    purchaseData2(Cnt).GR_Date = purchaseData(i).GR_Date
                    purchaseData2(Cnt).Invoice_No = purchaseData(i).Invoice_No
                    purchaseData2(Cnt).local_invoice_No = purchaseData(i).local_invoice_No
                    purchaseData2(Cnt).Delivery_No = purchaseData(i).Delivery_No
                    purchaseData2(Cnt).Items = purchaseData(i).Items

                    purchaseData2(Cnt).SumAmount = sumAmount
                    purchaseData2(Cnt).SumSGST_UTGST = sumSGST_UTGST
                    purchaseData2(Cnt).SumCGST = sumCGST
                    purchaseData2(Cnt).SumIGST = sumIGST
                    purchaseData2(Cnt).SumCess = sumCess
                    purchaseData2(Cnt).SumTax = sumTax
                    purchaseData2(Cnt).SumTotal = sumTotal

                    purchaseData2(Cnt).GR_Status = purchaseData(i).GR_Status

                    Cnt = Cnt + 1

                    i = (j - 1)
                    jStart = j

                    sumAmount = 0
                    sumSGST_UTGST = 0
                    sumCGST = 0
                    sumIGST = 0
                    sumCess = 0
                    sumTax = 0
                    sumTotal = 0

                Next i

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

    '****************************************************
    '処理：setSalesRegister_IW_OTHER
    '引数：wtyDataIW()　　出力対象をセット
    '      exportShipName
    '　　　errFlg          戻り値　0:正常　1:異常
    '      setMon          対象請求月
    '      dateFrom　　　　対象期間　日付指定
    '      dateTo　　　　　対象期間　日付指定　　　　　　　　
    '      kind            登録種類　IW/OTHER 
    '****************************************************
    Public Sub setSalesRegister_IW_OTHER(ByRef wtyDataIW() As WTY_EXCEL, ByVal exportShipName As String, ByVal shipCode As String, ByVal userid As String, ByVal userName As String, ByRef errFlg As Integer, ByVal setMon As String, ByVal dateFrom As String, ByVal dateTo As String, ByVal kind As String)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try

            '■出力対象を取得

            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia

            Dim dsWtyExcel As New DataSet
            Dim select_sql As String = ""

            If kind = "IW" Then

                select_sql &= "SELECT B.Invoice, B.Labor, B.Parts, B.Parts_Invoice_No, B.Labor_Invoice_No, A.* "
                select_sql &= "FROM dbo.Wty_Excel A "
                select_sql &= ",Invoice_update B "
                select_sql &= "WHERE A.DELFG = 0 "
                select_sql &= "AND A.Samsung_Claim_No = B.samsung_Ref_No "
                select_sql &= "And A.upload_Branch = B.upload_Branch "
                select_sql &= "AND A.upload_Branch = '" & exportShipName & "' "
                select_sql &= "AND A.Status IN ('10','20') "

            ElseIf kind = "OTHER" Then

                select_sql &= "SELECT B.Invoice, B.Labor, B.Parts, B.Parts_Invoice_No, B.Labor_Invoice_No, A.* "
                select_sql &= "FROM dbo.Wty_Excel A "
                select_sql &= ",Invoice_update B "
                select_sql &= "WHERE A.DELFG = 0 "
                select_sql &= "AND A.Samsung_Claim_No = B.samsung_Ref_No "
                select_sql &= "And A.upload_Branch = B.upload_Branch "
                select_sql &= "AND A.upload_Branch = '" & exportShipName & "' "
                select_sql &= "AND A.Status = '80' "
                select_sql &= "AND (B.Invoice <> '0.00' and  B.Invoice is not NULL) "

            End If

            If setMon = "00" Then
                If dateTo <> "" Then
                    If dateFrom <> "" Then
                        select_sql &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111), 10) <= '" & dateTo & "' "
                        select_sql &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111), 10) >= '" & dateFrom & "' "
                        select_sql &= "ORDER BY A.Samsung_Claim_No;"
                    Else
                        select_sql &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111), 10) <= '" & dateTo & "' "
                        select_sql &= "ORDER BY A.Samsung_Claim_No;"
                    End If
                Else
                    If dateFrom <> "" Then
                        select_sql &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111), 10) >= '" & dateFrom & "' "
                        select_sql &= "ORDER BY A.Samsung_Claim_No;"
                    End If
                End If
            Else
                select_sql &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111), 7) = '" & dtNow.ToShortDateString.Substring(0, 4) & "/" & setMon & "' "
                select_sql &= "ORDER BY A.Samsung_Claim_No;"
            End If

            Dim sqlSelect As New SqlCommand(select_sql, con, trn)
            Dim Adapter As New SqlDataAdapter(sqlSelect)

            Adapter.Fill(dsWtyExcel)

            If dsWtyExcel.Tables(0).Rows.Count <> 0 Then

                Dim i As Integer

                ReDim wtyDataIW(dsWtyExcel.Tables(0).Rows.Count - 1)

                For i = 0 To dsWtyExcel.Tables(0).Rows.Count - 1

                    Dim dr As DataRow = dsWtyExcel.Tables(0).Rows(i)

                    '出力情報を構造体にセット
                    If dr("ASC_Code") IsNot DBNull.Value Then
                        wtyDataIW(i).ASC_Code = dr("ASC_Code")
                    End If

                    If dr("Branch_Code") IsNot DBNull.Value Then
                        wtyDataIW(i).Branch_Code = dr("Branch_Code")
                    End If

                    If dr("ASC_Claim_No") IsNot DBNull.Value Then
                        wtyDataIW(i).ASC_Claim_No = dr("ASC_Claim_No")
                    End If

                    If dr("Samsung_Claim_No") IsNot DBNull.Value Then
                        wtyDataIW(i).Samsung_Claim_No = dr("Samsung_Claim_No")
                    End If

                    If dr("Service_Type") IsNot DBNull.Value Then
                        wtyDataIW(i).Service_Type = dr("Service_Type")
                    End If

                    If dr("Consumer_Name") IsNot DBNull.Value Then
                        wtyDataIW(i).Consumer_Name = dr("Consumer_Name")
                    End If

                    If dr("Consumer_Addr1") IsNot DBNull.Value Then
                        wtyDataIW(i).Consumer_Addr1 = dr("Consumer_Addr1")
                    End If

                    If dr("Consumer_Addr2") IsNot DBNull.Value Then
                        wtyDataIW(i).Consumer_Addr2 = dr("Consumer_Addr2")
                    End If

                    If dr("Consumer_Telephone") IsNot DBNull.Value Then
                        wtyDataIW(i).Consumer_Telephone = dr("Consumer_Telephone")
                    End If

                    If dr("Consumer_Fax") IsNot DBNull.Value Then
                        wtyDataIW(i).Consumer_Fax = dr("Consumer_Fax")
                    End If

                    If dr("Postal_Code") IsNot DBNull.Value Then
                        wtyDataIW(i).Postal_Code = dr("Postal_Code")
                    End If

                    If dr("Model") IsNot DBNull.Value Then
                        wtyDataIW(i).Model = dr("Model")
                    End If

                    If dr("Serial_No") IsNot DBNull.Value Then
                        wtyDataIW(i).Serial_No = dr("Serial_No")
                    End If

                    If dr("IMEI_No") IsNot DBNull.Value Then
                        wtyDataIW(i).IMEI_No = dr("IMEI_No")
                    End If

                    If dr("Defect_Type") IsNot DBNull.Value Then
                        wtyDataIW(i).Defect_Type = dr("Defect_Type")
                    End If

                    If dr("Condition") IsNot DBNull.Value Then
                        wtyDataIW(i).Condition = dr("Condition")
                    End If

                    If dr("Symptom") IsNot DBNull.Value Then
                        wtyDataIW(i).Symptom = dr("Symptom")
                    End If

                    If dr("Defect_Code") IsNot DBNull.Value Then
                        wtyDataIW(i).Defect_Code = dr("Defect_Code")
                    End If

                    If dr("Repair_Code") IsNot DBNull.Value Then
                        wtyDataIW(i).Repair_Code = dr("Repair_Code")
                    End If

                    If dr("Defect_Desc") IsNot DBNull.Value Then
                        wtyDataIW(i).Defect_Desc = dr("Defect_Desc")
                    End If

                    If dr("Repair_Description") IsNot DBNull.Value Then
                        wtyDataIW(i).Repair_Description = dr("Repair_Description")
                    End If

                    If dr("Purchase_Date") IsNot DBNull.Value Then
                        wtyDataIW(i).Purchase_Date = dr("Purchase_Date")
                    End If

                    If dr("Repair_Received_Date") IsNot DBNull.Value Then
                        wtyDataIW(i).Repair_Received_Date = dr("Repair_Received_Date")
                    End If

                    If dr("Completed_Date") IsNot DBNull.Value Then
                        wtyDataIW(i).Completed_Date = dr("Completed_Date")
                    End If

                    If dr("Delivery_Date") IsNot DBNull.Value Then
                        wtyDataIW(i).Delivery_Date = dr("Delivery_Date")
                    End If

                    If dr("Production_Date") IsNot DBNull.Value Then
                        wtyDataIW(i).Production_Date = dr("Production_Date")
                    End If

                    If dr("Labor_Amount") IsNot DBNull.Value Then
                        wtyDataIW(i).Labor_Amount = dr("Labor_Amount")
                    End If

                    If dr("Parts_Amount") IsNot DBNull.Value Then
                        wtyDataIW(i).Parts_Amount = dr("Parts_Amount")
                    End If

                    If dr("Freight") IsNot DBNull.Value Then
                        wtyDataIW(i).Freight = dr("Freight")
                    End If

                    If dr("Other") IsNot DBNull.Value Then
                        wtyDataIW(i).Other = dr("Other")
                    End If

                    If dr("Parts_SGST") IsNot DBNull.Value Then
                        wtyDataIW(i).Parts_SGST = dr("Parts_SGST")
                    End If

                    If dr("Parts_UTGST") IsNot DBNull.Value Then
                        wtyDataIW(i).Parts_UTGST = dr("Parts_UTGST")
                    End If

                    If dr("Parts_CGST") IsNot DBNull.Value Then
                        wtyDataIW(i).Parts_CGST = dr("Parts_CGST")
                    End If

                    If dr("Parts_IGST") IsNot DBNull.Value Then
                        wtyDataIW(i).Parts_IGST = dr("Parts_IGST")
                    End If

                    If dr("Parts_Cess") IsNot DBNull.Value Then
                        wtyDataIW(i).Parts_Cess = dr("Parts_Cess")
                    End If

                    If dr("SGST") IsNot DBNull.Value Then
                        wtyDataIW(i).SGST = dr("SGST")
                    End If

                    If dr("UTGST") IsNot DBNull.Value Then
                        wtyDataIW(i).UTGST = dr("UTGST")
                    End If

                    If dr("CGST") IsNot DBNull.Value Then
                        wtyDataIW(i).CGST = dr("CGST")
                    End If

                    If dr("IGST") IsNot DBNull.Value Then
                        wtyDataIW(i).IGST = dr("IGST")
                    End If

                    If dr("Cess") IsNot DBNull.Value Then
                        wtyDataIW(i).Cess = dr("Cess")
                    End If

                    'If dr("Tax") IsNot DBNull.Value Then
                    '    wtyDataIW(i).Tax = dr("Tax")
                    'End If

                    If dr("Total_Invoice_Amount") IsNot DBNull.Value Then
                        wtyDataIW(i).Total_Invoice_Amount = dr("Total_Invoice_Amount")
                    End If

                    If dr("Remark") IsNot DBNull.Value Then
                        wtyDataIW(i).Remark = dr("Remark")
                    End If

                    If dr("Tr_No") IsNot DBNull.Value Then
                        wtyDataIW(i).Tr_No = dr("Tr_No")
                    End If

                    If dr("Tr_Type") IsNot DBNull.Value Then
                        wtyDataIW(i).Tr_Type = dr("Tr_Type")
                    End If

                    If dr("Status") IsNot DBNull.Value Then
                        wtyDataIW(i).Status = dr("Status")
                    End If

                    If dr("Engineer") IsNot DBNull.Value Then
                        wtyDataIW(i).Engineer = dr("Engineer")
                    End If

                    If dr("Collection_Point") IsNot DBNull.Value Then
                        wtyDataIW(i).Collection_Point = dr("Collection_Point")
                    End If

                    If dr("Collection_Point_Name") IsNot DBNull.Value Then
                        wtyDataIW(i).Collection_Point_Name = dr("Collection_Point_Name")
                    End If

                    If dr("Location_1") IsNot DBNull.Value Then
                        wtyDataIW(i).Location_1 = dr("Location_1")
                    End If

                    If dr("Part_1") IsNot DBNull.Value Then
                        wtyDataIW(i).Part_1 = dr("Part_1")
                    End If

                    If dr("Qty_1") IsNot DBNull.Value Then
                        wtyDataIW(i).Qty_1 = dr("Qty_1")
                    End If

                    If dr("Unit_Price_1") IsNot DBNull.Value Then
                        wtyDataIW(i).Unit_Price_1 = dr("Unit_Price_1")
                    End If

                    If dr("Doc_Num_1") IsNot DBNull.Value Then
                        wtyDataIW(i).Doc_Num_1 = dr("Doc_Num_1")
                    End If

                    If dr("Matrial_Serial_1") IsNot DBNull.Value Then
                        wtyDataIW(i).Matrial_Serial_1 = dr("Matrial_Serial_1")
                    End If

                    If dr("Location_2") IsNot DBNull.Value Then
                        wtyDataIW(i).Location_2 = dr("Location_2")
                    End If

                    If dr("Part_2") IsNot DBNull.Value Then
                        wtyDataIW(i).Part_2 = dr("Part_2")
                    End If

                    If dr("Qty_2") IsNot DBNull.Value Then
                        wtyDataIW(i).Qty_2 = dr("Qty_2")
                    End If

                    If dr("Unit_Price_2") IsNot DBNull.Value Then
                        wtyDataIW(i).Unit_Price_2 = dr("Unit_Price_2")
                    End If

                    If dr("Doc_Num_2") IsNot DBNull.Value Then
                        wtyDataIW(i).Doc_Num_2 = dr("Doc_Num_2")
                    End If

                    If dr("Matrial_Serial_2") IsNot DBNull.Value Then
                        wtyDataIW(i).Matrial_Serial_2 = dr("Matrial_Serial_2")
                    End If

                    If dr("Location_3") IsNot DBNull.Value Then
                        wtyDataIW(i).Location_3 = dr("Location_3")
                    End If

                    If dr("Part_3") IsNot DBNull.Value Then
                        wtyDataIW(i).Part_3 = dr("Part_3")
                    End If

                    If dr("Qty_3") IsNot DBNull.Value Then
                        wtyDataIW(i).Qty_3 = dr("Qty_3")
                    End If

                    If dr("Unit_Price_3") IsNot DBNull.Value Then
                        wtyDataIW(i).Unit_Price_3 = dr("Unit_Price_3")
                    End If

                    If dr("Doc_Num_3") IsNot DBNull.Value Then
                        wtyDataIW(i).Doc_Num_3 = dr("Doc_Num_3")
                    End If

                    If dr("Matrial_Serial_3") IsNot DBNull.Value Then
                        wtyDataIW(i).Matrial_Serial_3 = dr("Matrial_Serial_3")
                    End If

                    If dr("Location_4") IsNot DBNull.Value Then
                        wtyDataIW(i).Location_4 = dr("Location_4")
                    End If

                    If dr("Part_4") IsNot DBNull.Value Then
                        wtyDataIW(i).Part_4 = dr("Part_4")
                    End If

                    If dr("Qty_4") IsNot DBNull.Value Then
                        wtyDataIW(i).Qty_4 = dr("Qty_4")
                    End If

                    If dr("Unit_Price_4") IsNot DBNull.Value Then
                        wtyDataIW(i).Unit_Price_4 = dr("Unit_Price_4")
                    End If

                    If dr("Doc_Num_4") IsNot DBNull.Value Then
                        wtyDataIW(i).Doc_Num_4 = dr("Doc_Num_4")
                    End If

                    If dr("Matrial_Serial_4") IsNot DBNull.Value Then
                        wtyDataIW(i).Matrial_Serial_4 = dr("Matrial_Serial_4")
                    End If

                    If dr("Location_5") IsNot DBNull.Value Then
                        wtyDataIW(i).Location_5 = dr("Location_5")
                    End If

                    If dr("Part_5") IsNot DBNull.Value Then
                        wtyDataIW(i).Part_5 = dr("Part_5")
                    End If

                    If dr("Qty_5") IsNot DBNull.Value Then
                        wtyDataIW(i).Qty_5 = dr("Qty_5")
                    End If

                    If dr("Unit_Price_5") IsNot DBNull.Value Then
                        wtyDataIW(i).Unit_Price_5 = dr("Unit_Price_5")
                    End If

                    If dr("Doc_Num_5") IsNot DBNull.Value Then
                        wtyDataIW(i).Doc_Num_5 = dr("Doc_Num_5")
                    End If

                    If dr("Matrial_Serial_5") IsNot DBNull.Value Then
                        wtyDataIW(i).Matrial_Serial_5 = dr("Matrial_Serial_5")
                    End If

                    If dr("Location_6") IsNot DBNull.Value Then
                        wtyDataIW(i).Location_6 = dr("Location_6")
                    End If

                    If dr("Part_6") IsNot DBNull.Value Then
                        wtyDataIW(i).Part_6 = dr("Part_6")
                    End If

                    If dr("Qty_6") IsNot DBNull.Value Then
                        wtyDataIW(i).Qty_6 = dr("Qty_6")
                    End If

                    If dr("Unit_Price_6") IsNot DBNull.Value Then
                        wtyDataIW(i).Unit_Price_6 = dr("Unit_Price_6")
                    End If

                    If dr("Doc_Num_6") IsNot DBNull.Value Then
                        wtyDataIW(i).Doc_Num_6 = dr("Doc_Num_6")
                    End If

                    If dr("Matrial_Serial_6") IsNot DBNull.Value Then
                        wtyDataIW(i).Matrial_Serial_6 = dr("Matrial_Serial_6")
                    End If

                    If dr("Location_7") IsNot DBNull.Value Then
                        wtyDataIW(i).Location_7 = dr("Location_7")
                    End If

                    If dr("Part_7") IsNot DBNull.Value Then
                        wtyDataIW(i).Part_7 = dr("Part_7")
                    End If

                    If dr("Qty_7") IsNot DBNull.Value Then
                        wtyDataIW(i).Qty_7 = dr("Qty_7")
                    End If

                    If dr("Unit_Price_7") IsNot DBNull.Value Then
                        wtyDataIW(i).Unit_Price_7 = dr("Unit_Price_7")
                    End If

                    If dr("Doc_Num_7") IsNot DBNull.Value Then
                        wtyDataIW(i).Doc_Num_7 = dr("Doc_Num_7")
                    End If

                    If dr("Matrial_Serial_7") IsNot DBNull.Value Then
                        wtyDataIW(i).Matrial_Serial_7 = dr("Matrial_Serial_7")
                    End If

                    If dr("Location_8") IsNot DBNull.Value Then
                        wtyDataIW(i).Location_8 = dr("Location_8")
                    End If

                    If dr("Part_8") IsNot DBNull.Value Then
                        wtyDataIW(i).Part_8 = dr("Part_8")
                    End If

                    If dr("Qty_8") IsNot DBNull.Value Then
                        wtyDataIW(i).Qty_8 = dr("Qty_8")
                    End If

                    If dr("Unit_Price_8") IsNot DBNull.Value Then
                        wtyDataIW(i).Unit_Price_8 = dr("Unit_Price_8")
                    End If

                    If dr("Doc_Num_8") IsNot DBNull.Value Then
                        wtyDataIW(i).Doc_Num_8 = dr("Doc_Num_8")
                    End If

                    If dr("Matrial_Serial_8") IsNot DBNull.Value Then
                        wtyDataIW(i).Matrial_Serial_8 = dr("Matrial_Serial_8")
                    End If

                    If dr("Location_9") IsNot DBNull.Value Then
                        wtyDataIW(i).Location_9 = dr("Location_9")
                    End If

                    If dr("Part_9") IsNot DBNull.Value Then
                        wtyDataIW(i).Part_9 = dr("Part_9")
                    End If

                    If dr("Qty_9") IsNot DBNull.Value Then
                        wtyDataIW(i).Qty_9 = dr("Qty_9")
                    End If

                    If dr("Unit_Price_9") IsNot DBNull.Value Then
                        wtyDataIW(i).Unit_Price_9 = dr("Unit_Price_9")
                    End If

                    If dr("Doc_Num_9") IsNot DBNull.Value Then
                        wtyDataIW(i).Doc_Num_9 = dr("Doc_Num_9")
                    End If

                    If dr("Matrial_Serial_9") IsNot DBNull.Value Then
                        wtyDataIW(i).Matrial_Serial_9 = dr("Matrial_Serial_9")
                    End If

                    If dr("Location_10") IsNot DBNull.Value Then
                        wtyDataIW(i).Location_10 = dr("Location_10")
                    End If

                    If dr("Part_10") IsNot DBNull.Value Then
                        wtyDataIW(i).Part_10 = dr("Part_10")
                    End If

                    If dr("Qty_10") IsNot DBNull.Value Then
                        wtyDataIW(i).Qty_10 = dr("Qty_10")
                    End If

                    If dr("Unit_Price_10") IsNot DBNull.Value Then
                        wtyDataIW(i).Unit_Price_10 = dr("Unit_Price_10")
                    End If

                    If dr("Doc_Num_10") IsNot DBNull.Value Then
                        wtyDataIW(i).Doc_Num_10 = dr("Doc_Num_10")
                    End If

                    If dr("Matrial_Serial_10") IsNot DBNull.Value Then
                        wtyDataIW(i).Matrial_Serial_10 = dr("Matrial_Serial_10")
                    End If

                    If dr("Location_11") IsNot DBNull.Value Then
                        wtyDataIW(i).Location_11 = dr("Location_11")
                    End If

                    If dr("Part_11") IsNot DBNull.Value Then
                        wtyDataIW(i).Part_11 = dr("Part_11")
                    End If

                    If dr("Qty_11") IsNot DBNull.Value Then
                        wtyDataIW(i).Qty_11 = dr("Qty_11")
                    End If

                    If dr("Unit_Price_11") IsNot DBNull.Value Then
                        wtyDataIW(i).Unit_Price_11 = dr("Unit_Price_11")
                    End If

                    If dr("Doc_Num_11") IsNot DBNull.Value Then
                        wtyDataIW(i).Doc_Num_11 = dr("Doc_Num_11")
                    End If

                    If dr("Matrial_Serial_11") IsNot DBNull.Value Then
                        wtyDataIW(i).Matrial_Serial_11 = dr("Matrial_Serial_11")
                    End If

                    If dr("Location_12") IsNot DBNull.Value Then
                        wtyDataIW(i).Location_12 = dr("Location_12")
                    End If

                    If dr("Part_12") IsNot DBNull.Value Then
                        wtyDataIW(i).Part_12 = dr("Part_12")
                    End If

                    If dr("Qty_12") IsNot DBNull.Value Then
                        wtyDataIW(i).Qty_12 = dr("Qty_12")
                    End If

                    If dr("Unit_Price_12") IsNot DBNull.Value Then
                        wtyDataIW(i).Unit_Price_12 = dr("Unit_Price_12")
                    End If

                    If dr("Doc_Num_12") IsNot DBNull.Value Then
                        wtyDataIW(i).Doc_Num_12 = dr("Doc_Num_12")
                    End If

                    If dr("Matrial_Serial_12") IsNot DBNull.Value Then
                        wtyDataIW(i).Matrial_Serial_12 = dr("Matrial_Serial_12")
                    End If

                    If dr("Location_13") IsNot DBNull.Value Then
                        wtyDataIW(i).Location_13 = dr("Location_13")
                    End If

                    If dr("Part_13") IsNot DBNull.Value Then
                        wtyDataIW(i).Part_13 = dr("Part_13")
                    End If

                    If dr("Qty_13") IsNot DBNull.Value Then
                        wtyDataIW(i).Qty_13 = dr("Qty_13")
                    End If

                    If dr("Unit_Price_13") IsNot DBNull.Value Then
                        wtyDataIW(i).Unit_Price_13 = dr("Unit_Price_13")
                    End If

                    If dr("Doc_Num_13") IsNot DBNull.Value Then
                        wtyDataIW(i).Doc_Num_13 = dr("Doc_Num_13")
                    End If

                    If dr("Matrial_Serial_13") IsNot DBNull.Value Then
                        wtyDataIW(i).Matrial_Serial_13 = dr("Matrial_Serial_13")
                    End If

                    If dr("Location_14") IsNot DBNull.Value Then
                        wtyDataIW(i).Location_14 = dr("Location_14")
                    End If

                    If dr("Part_14") IsNot DBNull.Value Then
                        wtyDataIW(i).Part_14 = dr("Part_14")
                    End If

                    If dr("Qty_14") IsNot DBNull.Value Then
                        wtyDataIW(i).Qty_14 = dr("Qty_14")
                    End If

                    If dr("Unit_Price_14") IsNot DBNull.Value Then
                        wtyDataIW(i).Unit_Price_14 = dr("Unit_Price_14")
                    End If

                    If dr("Doc_Num_14") IsNot DBNull.Value Then
                        wtyDataIW(i).Doc_Num_14 = dr("Doc_Num_14")
                    End If

                    If dr("Matrial_Serial_14") IsNot DBNull.Value Then
                        wtyDataIW(i).Matrial_Serial_14 = dr("Matrial_Serial_14")
                    End If

                    If dr("Location_15") IsNot DBNull.Value Then
                        wtyDataIW(i).Location_15 = dr("Location_15")
                    End If

                    If dr("Part_15") IsNot DBNull.Value Then
                        wtyDataIW(i).Part_15 = dr("Part_15")
                    End If

                    If dr("Qty_15") IsNot DBNull.Value Then
                        wtyDataIW(i).Qty_15 = dr("Qty_15")
                    End If

                    If dr("Unit_Price_15") IsNot DBNull.Value Then
                        wtyDataIW(i).Unit_Price_15 = dr("Unit_Price_15")
                    End If

                    If dr("Doc_Num_15") IsNot DBNull.Value Then
                        wtyDataIW(i).Doc_Num_15 = dr("Doc_Num_15")
                    End If

                    If dr("Matrial_Serial_15") IsNot DBNull.Value Then
                        wtyDataIW(i).Matrial_Serial_15 = dr("Matrial_Serial_15")
                    End If

                    '⇓Invoice_updateより取得した情報を出力構造体にセット⇓
                    If dr("Invoice") IsNot DBNull.Value Then

                        If kind = "IW" Then

                            wtyDataIW(i).Invoice = dr("Invoice")

                        ElseIf kind = "OTHER" Then

                            Dim tmp As Decimal = dr("Invoice")
                            tmp = tmp * -1
                            wtyDataIW(i).Invoice = tmp

                        End If

                    End If

                    If dr("Labor") IsNot DBNull.Value Then

                        If kind = "IW" Then

                            wtyDataIW(i).Labor = dr("Labor")

                        ElseIf kind = "OTHER" Then

                            Dim tmp As Decimal = dr("Labor")
                            tmp = tmp * -1
                            wtyDataIW(i).Labor = tmp

                        End If

                    End If

                    If dr("Parts") IsNot DBNull.Value Then

                        If kind = "IW" Then
                            wtyDataIW(i).Parts = dr("Parts")

                        ElseIf kind = "OTHER" Then

                            Dim tmp As Decimal = dr("Parts")
                            tmp = tmp * -1
                            wtyDataIW(i).Parts = tmp

                        End If

                    End If

                    If dr("Parts_invoice_No") IsNot DBNull.Value Then
                        wtyDataIW(i).Parts_invoice_No = dr("Parts_invoice_No")
                    End If

                    If dr("Labor_Invoice_No") IsNot DBNull.Value Then
                        wtyDataIW(i).Labor_Invoice_No = dr("Labor_Invoice_No")
                    End If

                Next i

                '■更新
                For i = 0 To wtyDataIW.Length - 1

                    Dim select_sql2 As String = ""

                    If kind = "IW" Then

                        select_sql2 &= "SELECT A.* "
                        select_sql2 &= "FROM dbo.Wty_Excel A "
                        select_sql2 &= "WHERE A.DELFG = 0 "
                        select_sql2 &= "AND A.upload_Branch = '" & exportShipName & "' "
                        select_sql2 &= "AND A.Status IN ('10','20') "
                        select_sql2 &= "AND A.Samsung_Claim_No = '" & wtyDataIW(i).Samsung_Claim_No & "' "

                    ElseIf kind = "OTHER" Then

                        select_sql2 &= "SELECT A.* "
                        select_sql2 &= "FROM dbo.Wty_Excel A "
                        select_sql2 &= "WHERE A.DELFG = 0 "
                        select_sql2 &= "AND A.upload_Branch = '" & exportShipName & "' "
                        select_sql2 &= "AND A.Status = '80' "
                        select_sql2 &= "AND A.Samsung_Claim_No = '" & wtyDataIW(i).Samsung_Claim_No & "' "

                    End If

                    Dim sqlSelect2 As New SqlCommand(select_sql2, con, trn)
                    Dim Adapter2 As New SqlDataAdapter(sqlSelect2)
                    Dim Builder2 As New SqlCommandBuilder(Adapter2)
                    Dim ds2 As New DataSet

                    Adapter2.Fill(ds2)

                    If ds2.Tables(0).Rows.Count = 1 Then

                        Dim dr As DataRow = ds2.Tables(0).Rows(0)

                        dr("Labor_Amount_I") = wtyDataIW(i).Labor
                        dr("Parts_Amount_I") = wtyDataIW(i).Parts
                        dr("Total_Invoice_Amount_I") = wtyDataIW(i).Invoice
                        dr("UPDDT") = dtNow
                        dr("UPDCD") = userid
                        dr("upload_date") = dtNow
                        dr("upload_user") = userName

                        '更新
                        Adapter2.Update(ds2)

                    End If

                Next i

            End If

            '■コミット
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
    '処理：setSAW_Discount_Details
    '引数：wtyDataIW()　　出力対象をセット
    '      exportShipName
    '　　　errFlg          戻り値　0:正常　1:異常
    '      setMon          対象請求月
    '      dateFrom　　　　対象期間　日付指定
    '      dateTo　　　　　対象期間　日付指定　　　　　　　　
    '      kind            登録種類　IW/OTHER 
    '****************************************************
    Public Sub setSAW_Discount_Details(ByRef wtyDataOther() As WTY_EXCEL, ByVal exportShipName As String, ByVal shipCode As String, ByVal userid As String, ByVal userName As String, ByRef errFlg As Integer, ByVal setMon As String, ByVal dateFrom As String, ByVal dateTo As String)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try

            '■出力対象を取得

            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia

            Dim dsWtyExcel As New DataSet
            Dim select_sql As String = ""

            select_sql &= "SELECT B.Invoice, B.Labor, B.Parts, B.Parts_Invoice_No, B.Labor_Invoice_No, A.* "
            select_sql &= "FROM dbo.Wty_Excel A "
            select_sql &= ",Invoice_update B "
            select_sql &= "WHERE A.DELFG = 0 "
            select_sql &= "AND A.Samsung_Claim_No = B.samsung_Ref_No "
            select_sql &= "And A.upload_Branch = B.upload_Branch "
            select_sql &= "AND A.upload_Branch = '" & exportShipName & "' "
            select_sql &= "AND A.Status = '80' "
            select_sql &= "AND (B.Invoice <> '0.00' and  B.Invoice is not NULL) "


            If setMon = "00" Then
                If dateTo <> "" Then
                    If dateFrom <> "" Then
                        select_sql &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111), 10) <= '" & dateTo & "' "
                        select_sql &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111), 10) >= '" & dateFrom & "' "
                        select_sql &= "ORDER BY A.Samsung_Claim_No;"
                    Else
                        select_sql &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111), 10) <= '" & dateTo & "' "
                        select_sql &= "ORDER BY A.Samsung_Claim_No;"
                    End If
                Else
                    If dateFrom <> "" Then
                        select_sql &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111), 10) >= '" & dateFrom & "' "
                        select_sql &= "ORDER BY A.Samsung_Claim_No;"
                    End If
                End If
            Else
                select_sql &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111), 7) = '" & dtNow.ToShortDateString.Substring(0, 4) & "/" & setMon & "' "
                select_sql &= "ORDER BY A.Samsung_Claim_No;"
            End If

            Dim sqlSelect As New SqlCommand(select_sql, con, trn)
            Dim Adapter As New SqlDataAdapter(sqlSelect)

            Adapter.Fill(dsWtyExcel)

            If dsWtyExcel.Tables(0).Rows.Count <> 0 Then

                Dim i As Integer

                ReDim wtyDataOther(dsWtyExcel.Tables(0).Rows.Count - 1)

                For i = 0 To dsWtyExcel.Tables(0).Rows.Count - 1

                    Dim dr As DataRow = dsWtyExcel.Tables(0).Rows(i)

                    '出力情報を構造体にセット
                    If dr("ASC_Code") IsNot DBNull.Value Then
                        wtyDataOther(i).ASC_Code = dr("ASC_Code")
                    End If

                    If dr("Branch_Code") IsNot DBNull.Value Then
                        wtyDataOther(i).Branch_Code = dr("Branch_Code")
                    End If

                    If dr("ASC_Claim_No") IsNot DBNull.Value Then
                        wtyDataOther(i).ASC_Claim_No = dr("ASC_Claim_No")
                    End If

                    If dr("Samsung_Claim_No") IsNot DBNull.Value Then
                        wtyDataOther(i).Samsung_Claim_No = dr("Samsung_Claim_No")
                    End If

                    If dr("Model") IsNot DBNull.Value Then
                        wtyDataOther(i).Model = dr("Model")
                    End If

                    '⇓Invoice_updateより取得した情報を出力構造体にセット⇓
                    If dr("Parts_invoice_No") IsNot DBNull.Value Then
                        wtyDataOther(i).Parts_invoice_No = dr("Parts_invoice_No")
                    End If

                    If dr("Labor_Invoice_No") IsNot DBNull.Value Then
                        wtyDataOther(i).Labor_Invoice_No = dr("Labor_Invoice_No")
                    End If

                Next i

            End If

            '■コミット
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
    '処理：CSVファイルのヘッダ情報を確認
    '引数：colsHead()   　読み込んだCSVファイルのヘッダ情報 1行目
    '      colsHead2()    読み込んだCSVファイルのヘッダ情報 2行目　
    '　　　csvKind  　　　CSVファイルの種類名称　
    '返却：FALSE header⇒NG　 TRRUE　header⇒OK 　
    '****************************************************
    Public Function chkHead(ByVal colsHead() As String, ByVal colsHead2() As String, ByVal csvKind As Integer) As Boolean

        'DailyStatement_Repart
        If csvKind = 1 Then

            If colsHead(0) <> "Service Order No" Then
                Return False
            End If

            If colsHead(1) <> "Last Updated User" Then
                Return False
            End If

            If colsHead(2) <> "Billing User" Then
                Return False
            End If

            If colsHead(3) <> "Billing Date" Then
                Return False
            End If

            If colsHead(4) <> "Goods Delivered Date" Then
                Return False
            End If

            If colsHead(5) <> "Branch Name" Then
                Return False
            End If

            If colsHead(6) <> "Engineer" Then
                Return False
            End If

            If colsHead(8) <> "Product" Then
                Return False
            End If

            If colsHead(10) <> "In Warranty(Amount)" Then
                Return False
            End If

            If colsHead(16) <> "Out of Warranty(Amount)" Then
                Return False
            End If

            If colsHead2(10) <> "Labor" Then
                Return False
            End If

            If colsHead2(11) <> "Parts" Then
                Return False
            End If

            If colsHead2(12) <> "Transport" Then
                Return False
            End If

            If colsHead2(13) <> "Others" Then
                Return False
            End If

            If colsHead2(14) <> "Tax" Then
                Return False
            End If

            If colsHead2(15) <> "Total" Then
                Return False
            End If

            If colsHead2(16) <> "Labor" Then
                Return False
            End If

            If colsHead2(17) <> "Parts" Then
                Return False
            End If

            If colsHead2(18) <> "Transport" Then
                Return False
            End If

            If colsHead2(19) <> "Others" Then
                Return False
            End If

            If colsHead2(20) <> "Tax" Then
                Return False
            End If

            If colsHead2(21) <> "Total" Then
                Return False
            End If

        ElseIf csvKind = 2 Then




            If Trim(colsHead(0)) <> "ASC Code" Then
                Return False
            End If

            If Trim(colsHead(1)) <> "Branch Code" Then
                Return False
            End If

            If Trim(colsHead(2)) <> "ASC Claim No" Then
                Return False
            End If

            If Trim(colsHead(3)) <> "Samsung Claim No" Then
                Return False
            End If

            If Trim(colsHead(4)) <> "Service Type" Then
                Return False
            End If

            If Trim(colsHead(5)) <> "Consumer Name" Then
                Return False
            End If

            If Trim(colsHead(6)) <> "Consumer Addr1" Then
                Return False
            End If

            If Trim(colsHead(7)) <> "Consumer Addr2" Then
                Return False
            End If

            If Trim(colsHead(8)) <> "Consumer Telephone" Then
                Return False
            End If

            If Trim(colsHead(9)) <> "Consumer Fax" Then
                Return False
            End If

            If Trim(colsHead(10)) <> "Postal Code" Then
                Return False
            End If

            If Trim(colsHead(11)) <> "Model" Then
                Return False
            End If

            If Trim(colsHead(12)) <> "Serial No" Then
                Return False
            End If

            If Trim(colsHead(13)) <> "IMEI No" Then
                Return False
            End If

            If Trim(colsHead(14)) <> "Defect Type" Then
                Return False
            End If

            If Trim(colsHead(15)) <> "Condition" Then
                Return False
            End If

            If Trim(colsHead(16)) <> "Symptom" Then
                Return False
            End If

            If Trim(colsHead(17)) <> "Defect Code" Then
                Return False
            End If

            If Trim(colsHead(18)) <> "Repair Code" Then
                Return False
            End If

            If Trim(colsHead(19)) <> "Defect Desc" Then
                Return False
            End If

            If Trim(colsHead(20)) <> "Repair Description" Then
                Return False
            End If

            If Trim(colsHead(21)) <> "Purchase Date" Then
                Return False
            End If

            If Trim(colsHead(22)) <> "Repair Received Date" Then
                Return False
            End If

            If Trim(colsHead(23)) <> "Completed Date" Then
                Return False
            End If

            If Trim(colsHead(24)) <> "Delivery Date" Then
                Return False
            End If

            If Trim(colsHead(25)) <> "Production Date" Then
                Return False
            End If

            If Trim(colsHead(26)) <> "Labor Amount" Then
                Return False
            End If

            If Trim(colsHead(27)) <> "Parts Amount" Then
                Return False
            End If

            If Trim(colsHead(28)) <> "Freight" Then
                Return False
            End If

            If Trim(colsHead(29)) <> "Other" Then
                Return False
            End If

            If Trim(colsHead(30)) <> "Parts SGST" Then
                Return False
            End If

            If Trim(colsHead(31)) <> "Parts UTGST" Then
                Return False
            End If

            If Trim(colsHead(32)) <> "Parts CGST" Then
                Return False
            End If

            If Trim(colsHead(33)) <> "Parts IGST" Then
                Return False
            End If

            If Trim(colsHead(34)) <> "Parts Cess" Then
                Return False
            End If

            If Trim(colsHead(35)) <> "SGST" Then
                Return False
            End If

            If Trim(colsHead(36)) <> "UTGST" Then
                Return False
            End If

            If Trim(colsHead(37)) <> "CGST" Then
                Return False
            End If

            If Trim(colsHead(38)) <> "IGST" Then
                Return False
            End If

            If Trim(colsHead(39)) <> "Cess" Then
                Return False
            End If

            If Trim(colsHead(40)) <> "Total Invoice Amount" Then
                Return False
            End If

            If Trim(colsHead(41)) <> "Remark" Then
                Return False
            End If

            If Trim(colsHead(42)) <> "Tr No" Then
                Return False
            End If

            If Trim(colsHead(43)) <> "Tr Type" Then
                Return False
            End If

            If Trim(colsHead(44)) <> "Status" Then
                Return False
            End If

            If Trim(colsHead(45)) <> "Engineer" Then
                Return False
            End If

            If Trim(colsHead(46)) <> "Collection Point" Then
                Return False
            End If

            If Trim(colsHead(47)) <> "Collection Point Name" Then
                Return False
            End If

            If Trim(colsHead(48)) <> "Location-1" Then
                Return False
            End If

            If Trim(colsHead(49)) <> "Part-1" Then
                Return False
            End If

            If Trim(colsHead(50)) <> "Qty-1" Then
                Return False
            End If

            If Trim(colsHead(51)) <> "Unit Price-1" Then
                Return False
            End If

            If Trim(colsHead(52)) <> "Doc Num-1" Then
                Return False
            End If

            If Trim(colsHead(53)) <> "Matrial Serial-1" Then
                Return False
            End If

            If Trim(colsHead(54)) <> "Location-2" Then
                Return False
            End If

            If Trim(colsHead(55)) <> "Part-2" Then
                Return False
            End If

            If Trim(colsHead(56)) <> "Qty-2" Then
                Return False
            End If

            If Trim(colsHead(57)) <> "Unit Price-2" Then
                Return False
            End If

            If Trim(colsHead(58)) <> "Doc Num-2" Then
                Return False
            End If

            If Trim(colsHead(59)) <> "Matrial Serial-2" Then
                Return False
            End If

            If Trim(colsHead(60)) <> "Location-3" Then
                Return False
            End If

            If Trim(colsHead(61)) <> "Part-3" Then
                Return False
            End If

            If Trim(colsHead(62)) <> "Qty-3" Then
                Return False
            End If

            If Trim(colsHead(63)) <> "Unit Price-3" Then
                Return False
            End If

            If Trim(colsHead(64)) <> "Doc Num-3" Then
                Return False
            End If

            If Trim(colsHead(65)) <> "Matrial Serial-3" Then
                Return False
            End If

            If Trim(colsHead(66)) <> "Location-4" Then
                Return False
            End If

            If Trim(colsHead(67)) <> "Part-4" Then
                Return False
            End If

            If Trim(colsHead(68)) <> "Qty-4" Then
                Return False
            End If

            If Trim(colsHead(69)) <> "Unit Price-4" Then
                Return False
            End If

            If Trim(colsHead(70)) <> "Doc Num-4" Then
                Return False
            End If

            If Trim(colsHead(71)) <> "Matrial Serial-4" Then
                Return False
            End If

            If Trim(colsHead(72)) <> "Location-5" Then
                Return False
            End If

            If Trim(colsHead(73)) <> "Part-5" Then
                Return False
            End If

            If Trim(colsHead(74)) <> "Qty-5" Then
                Return False
            End If

            If Trim(colsHead(75)) <> "Unit Price-5" Then
                Return False
            End If

            If Trim(colsHead(76)) <> "Doc Num-5" Then
                Return False
            End If

            If Trim(colsHead(77)) <> "Matrial Serial-5" Then
                Return False
            End If

            If Trim(colsHead(78)) <> "Location-6" Then
                Return False
            End If

            If Trim(colsHead(79)) <> "Part-6" Then
                Return False
            End If

            If Trim(colsHead(80)) <> "Qty-6" Then
                Return False
            End If

            If Trim(colsHead(81)) <> "Unit Price-6" Then
                Return False
            End If

            If Trim(colsHead(82)) <> "Doc Num-6" Then
                Return False
            End If

            If Trim(colsHead(83)) <> "Matrial Serial-6" Then
                Return False
            End If

            If Trim(colsHead(84)) <> "Location-7" Then
                Return False
            End If

            If Trim(colsHead(85)) <> "Part-7" Then
                Return False
            End If

            If Trim(colsHead(86)) <> "Qty-7" Then
                Return False
            End If

            If Trim(colsHead(87)) <> "Unit Price-7" Then
                Return False
            End If

            If Trim(colsHead(88)) <> "Doc Num-7" Then
                Return False
            End If

            If Trim(colsHead(89)) <> "Matrial Serial-7" Then
                Return False
            End If

            If Trim(colsHead(90)) <> "Location-8" Then
                Return False
            End If

            If Trim(colsHead(91)) <> "Part-8" Then
                Return False
            End If

            If Trim(colsHead(92)) <> "Qty-8" Then
                Return False
            End If

            If Trim(colsHead(93)) <> "Unit Price-8" Then
                Return False
            End If

            If Trim(colsHead(94)) <> "Doc Num-8" Then
                Return False
            End If

            If Trim(colsHead(95)) <> "Matrial Serial-8" Then
                Return False
            End If

            If Trim(colsHead(96)) <> "Location-9" Then
                Return False
            End If

            If Trim(colsHead(97)) <> "Part-9" Then
                Return False
            End If

            If Trim(colsHead(98)) <> "Qty-9" Then
                Return False
            End If

            If Trim(colsHead(99)) <> "Unit Price-9" Then
                Return False
            End If

            If Trim(colsHead(100)) <> "Doc Num-9" Then
                Return False
            End If

            If Trim(colsHead(101)) <> "Matrial Serial-9" Then
                Return False
            End If

            If Trim(colsHead(102)) <> "Location-10" Then
                Return False
            End If

            If Trim(colsHead(103)) <> "Part-10" Then
                Return False
            End If

            If Trim(colsHead(104)) <> "Qty-10" Then
                Return False
            End If

            If Trim(colsHead(105)) <> "Unit Price-10" Then
                Return False
            End If

            If Trim(colsHead(106)) <> "Doc Num-10" Then
                Return False
            End If

            If Trim(colsHead(107)) <> "Matrial Serial-10" Then
                Return False
            End If

            If Trim(colsHead(108)) <> "Location-11" Then
                Return False
            End If

            If Trim(colsHead(109)) <> "Part-11" Then
                Return False
            End If

            If Trim(colsHead(110)) <> "Qty-11" Then
                Return False
            End If

            If Trim(colsHead(111)) <> "Unit Price-11" Then
                Return False
            End If

            If Trim(colsHead(112)) <> "Doc Num-11" Then
                Return False
            End If

            If Trim(colsHead(113)) <> "Matrial Serial-11" Then
                Return False
            End If

            If Trim(colsHead(114)) <> "Location-12" Then
                Return False
            End If

            If Trim(colsHead(115)) <> "Part-12" Then
                Return False
            End If

            If Trim(colsHead(116)) <> "Qty-12" Then
                Return False
            End If

            If Trim(colsHead(117)) <> "Unit Price-12" Then
                Return False
            End If

            If Trim(colsHead(118)) <> "Doc Num-12" Then
                Return False
            End If

            If Trim(colsHead(119)) <> "Matrial Serial-12" Then
                Return False
            End If

            If Trim(colsHead(120)) <> "Location-13" Then
                Return False
            End If

            If Trim(colsHead(121)) <> "Part-13" Then
                Return False
            End If

            If Trim(colsHead(122)) <> "Qty-13" Then
                Return False
            End If

            If Trim(colsHead(123)) <> "Unit Price-13" Then
                Return False
            End If

            If Trim(colsHead(124)) <> "Doc Num-13" Then
                Return False
            End If

            If Trim(colsHead(125)) <> "Matrial Serial-13" Then
                Return False
            End If
            If Trim(colsHead(126)) <> "Location-14" Then
                Return False
            End If

            If Trim(colsHead(127)) <> "Part-14" Then
                Return False
            End If

            If Trim(colsHead(128)) <> "Qty-14" Then
                Return False
            End If

            If Trim(colsHead(129)) <> "Unit Price-14" Then
                Return False
            End If

            If Trim(colsHead(130)) <> "Doc Num-14" Then
                Return False
            End If

            If Trim(colsHead(131)) <> "Matrial Serial-14" Then
                Return False
            End If

            If Trim(colsHead(132)) <> "Location-15" Then
                Return False
            End If

            If Trim(colsHead(133)) <> "Part-15" Then
                Return False
            End If

            If Trim(colsHead(134)) <> "Qty-15" Then
                Return False
            End If

            If Trim(colsHead(135)) <> "Unit Price-15" Then
                Return False
            End If

            If Trim(colsHead(136)) <> "Doc Num-15" Then
                Return False
            End If

            If Trim(colsHead(137)) <> "Matrial Serial-15" Then
                Return False
            End If

        ElseIf csvKind = 3 Or csvKind = 4 Then

            If colsHead(0) <> "Samsung Ref No" Then
                Return False
            End If

            If colsHead(1) <> "Your Ref No" Then
                Return False
            End If

            If colsHead(2) <> "Model" Then
                Return False
            End If

            If colsHead(3) <> "Serial" Then
                Return False
            End If

            If colsHead(4) <> "Product" Then
                Return False
            End If

            If colsHead(5) <> "Service" Then
                Return False
            End If

            If colsHead(6) <> "Defect Code" Then
                Return False
            End If

            If colsHead(7) <> "Currency" Then
                Return False
            End If

            If colsHead(8) <> "Invoice" Then
                Return False
            End If

            If colsHead(9) <> "Labor" Then
                Return False
            End If

            If colsHead(10) <> "Parts" Then
                Return False
            End If

            If colsHead(11) <> "Freight" Then
                Return False
            End If

            If colsHead(12) <> "Other" Then
                Return False
            End If

            If colsHead(13) <> "Tax" Then
                Return False
            End If


        ElseIf csvKind = 5 Then

            If colsHead(0) <> "No" Then
                Return False
            End If

            If colsHead(1) <> "Invoice No/" Then
                Return False
            End If

            If colsHead(2) <> "Invoice Date/" Then
                Return False
            End If

            If colsHead(3) <> "Local Invoice No" Then
                Return False
            End If

            If colsHead(4) <> "Items" Then
                Return False
            End If

            If colsHead(5) <> "Total Qty" Then
                Return False
            End If

            If colsHead(6) <> "Total Amount" Then
                Return False
            End If

            If colsHead(7) <> "GR Date" Then
                Return False
            End If

            If colsHead(8) <> "Create By" Then
                Return False
            End If

            If colsHead(9) <> "G/R Status" Then
                Return False
            End If

        ElseIf csvKind = 6 Then

            If colsHead(0) <> "Ship-to-Branch-Code" Then
                Return False
            End If

            If colsHead(1) <> "Ship-to-Branch" Then
                Return False
            End If

            If colsHead(2) <> "Billing Date" Then
                Return False
            End If

            If colsHead(3) <> "Invoice No" Then
                Return False
            End If

            If colsHead(4) <> "Item No" Then
                Return False
            End If

            If colsHead(5) <> "Delivery No" Then
                Return False
            End If

            If colsHead(6) <> "P/O No" Then
                Return False
            End If

            If colsHead(7) <> "P/O Type code" Then
                Return False
            End If

            If colsHead(8) <> "Part No" Then
                Return False
            End If

            If colsHead(9) <> "Billing Qty" Then
                Return False
            End If

            If colsHead(10) <> "Amount" Then
                Return False
            End If

            If colsHead(11) <> "SGST / UTGST" Then
                Return False
            End If

            If colsHead(12) <> "CGST" Then
                Return False
            End If

            If colsHead(13) <> "IGST" Then
                Return False
            End If

            If colsHead(14) <> "Cess" Then
                Return False
            End If

            If colsHead(15) <> "Tax" Then
                Return False
            End If

            If colsHead(16) <> "Core Flag" Then
                Return False
            End If

            If colsHead(17) <> "Total" Then
                Return False
            End If

        End If

        Return True

    End Function

    '****************************************************
    '処理：Activity_Reportデータの登録
    '引数：report      画面からのActivity_Reportの入力情報
    '　　　userid
    '　　　userName
    '　　　setDay
    '　　　errFlg      戻り値　0:正常　1:異常
    '      shipCode    拠点コード
    '      tourokuFlg  戻り値　新規登録：0 上書き登録：1      
    '****************************************************
    Public Sub setActivityReport(ByVal report As ACTIVITY_REPORT, ByVal userid As String, ByVal userName As String, ByVal setDay As String, ByRef errFlg As Integer, ByVal shipCode As String, ByRef tourokuFlg As Integer)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try
            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia

            '■UPDATE Activity_report ※登録済か確認
            Dim select_sql As String = ""
            '/* Added Mon Year VJ 20100106*/
            'select_sql = "SELECT * FROM dbo.Activity_report WHERE month = '" & dtNow.ToShortDateString.Substring(0, 5) & report.month & "' AND day = '" & setDay & "' AND DELFG = 0 AND location = '" & shipCode & "';"
            select_sql = "SELECT * FROM dbo.Activity_report WHERE month = '" & report.ActYear & "/" & report.ActMonth & "' AND day = '" & setDay & "' AND DELFG = 0 AND location = '" & shipCode & "';"
            Dim sqlSelect As New SqlCommand(select_sql, con, trn)
            Dim Adapter As New SqlDataAdapter(sqlSelect)
            Dim Builder As New SqlCommandBuilder(Adapter)
            Dim ds As New DataSet
            Dim dr1 As DataRow
            Adapter.Fill(ds)

            If ds.Tables(0).Rows.Count = 1 Then

                '上書き登録
                tourokuFlg = 1
                dr1 = ds.Tables(0).Rows(0)
                dr1("UPDDT") = dtNow
                dr1("UPDCD") = userid
                dr1("UPDPG") = "Class_analysis.vb"
                dr1("DELFG") = 0
                dr1("update_user") = userName
                dr1("update_datetime") = dtNow
                'dr1("month") = dtNow.ToShortDateString.Substring(0, 5) & report.month '/* Added Mon Year VJ 20100106*/
                dr1("month") = report.ActYear & "/" & report.ActMonth '/* Added Mon Year VJ 20100106*/
                dr1("day") = setDay
                dr1("note") = report.note
                dr1("Customer_Visit") = report.Customer_Visit
                dr1("Call_Registerd") = report.Call_Registerd
                dr1("Repair_Completed") = report.Repair_Completed
                dr1("Goods_Delivered") = report.Goods_Delivered
                dr1("Pending_Calls") = report.Pending_Calls
                dr1("Cancelled_Calls") = report.Cancelled_Calls
                dr1("location") = shipCode

                '更新
                Adapter.Update(ds)
            Else
                '■INSERT　SC_DSR_infoの空テーブル取得
                Dim select_sql1 As String = ""
                select_sql1 = "SELECT * FROM dbo.Activity_report WHERE update_user IS NULL;"

                Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
                Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
                Dim Builder1 As New SqlCommandBuilder(Adapter1)
                Dim ds1 As New DataSet
                Adapter1.Fill(ds1)

                '新規DR取得
                dr1 = ds1.Tables(0).NewRow

                dr1("CRTDT") = dtNow
                dr1("CRTCD") = userid
                dr1("UPDPG") = "Class_analysis.vb"
                dr1("DELFG") = 0
                dr1("update_user") = userName
                dr1("update_datetime") = dtNow
                'dr1("month") = dtNow.ToShortDateString.Substring(0, 5) & report.month '/* Added Mon Year VJ 20100106*/
                dr1("month") = report.ActYear & "/" & report.ActMonth '/* Added Mon Year VJ 20100106*/
                dr1("day") = setDay
                dr1("note") = report.note
                dr1("Customer_Visit") = report.Customer_Visit
                dr1("Call_Registerd") = report.Call_Registerd
                dr1("Repair_Completed") = report.Repair_Completed
                dr1("Goods_Delivered") = report.Goods_Delivered
                dr1("Pending_Calls") = report.Pending_Calls
                dr1("Cancelled_Calls") = report.Cancelled_Calls
                dr1("location") = shipCode

                '新規DRをDataTableに追加
                ds1.Tables(0).Rows.Add(dr1)
                Adapter1.Update(ds1)
            End If

            '■コミット
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
    '処理：cash_trackデータの登録
    '引数：cashTrackData   画面からの請求、支払い情報
    '　　　userid
    '　　　errFlg      　　戻り値　0:正常　1:異常
    '****************************************************
    Public Sub setCashTrack(ByRef cashTrackData As CASH_TRACK, ByVal userid As String, ByRef errFlg As Integer)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try

            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia
            Dim roopCount As Integer

            'cash creditあり、２行登録
            If cashTrackData.payment_kind = "1" Then
                roopCount = 1
            End If

            '■INSERT　cash_trackの空テーブル取得
            Dim select_sql1 As String = ""
            select_sql1 = "SELECT * FROM dbo.cash_track WHERE claim_no IS NULL;"

            Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
            Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
            Dim Builder1 As New SqlCommandBuilder(Adapter1)
            Dim ds1 As New DataSet
            Adapter1.Fill(ds1)

            For i = 0 To roopCount

                '新規DR取得
                Dim dr1 As DataRow = ds1.Tables(0).NewRow

                dr1("CRTDT") = dtNow
                dr1("CRTCD") = userid
                'dr1("UPDDT") = dtNow
                'dr1("UPDCD") = userid
                dr1("UPDPG") = "systemName"
                dr1("DELFG") = 0
                dr1("FALSE") = "0"

                If roopCount = 1 Then

                    If i = 0 Then
                        'cash
                        dr1("count_no") = cashTrackData.count_no
                        dr1("claim") = cashTrackData.claim
                        dr1("discount_after_amt") = cashTrackData.discount_after_amt
                        dr1("deposit") = cashTrackData.deposit
                        dr1("change") = cashTrackData.change
                        dr1("payment_kind") = cashTrackData.payment_kind
                        dr1("payment") = "Cash"
                    Else
                        'credit
                        dr1("count_no") = cashTrackData.count_no + 1
                        dr1("claim_card") = cashTrackData.claimCredit
                        dr1("discount_after_amt_credit") = cashTrackData.discount_after_amt_credit
                        dr1("card_number") = cashTrackData.card_number
                        dr1("card_type") = cashTrackData.card_type
                        dr1("payment_kind") = "2"
                        dr1("payment") = "Credit"
                    End If

                Else

                    dr1("count_no") = cashTrackData.count_no
                    dr1("payment") = cashTrackData.payment

                    'cash
                    dr1("claim") = cashTrackData.claim
                    dr1("discount_after_amt") = cashTrackData.discount_after_amt
                    dr1("deposit") = cashTrackData.deposit
                    dr1("change") = cashTrackData.change

                    'credit
                    dr1("claim_card") = cashTrackData.claimCredit
                    dr1("discount_after_amt_credit") = cashTrackData.discount_after_amt_credit
                    dr1("card_number") = cashTrackData.card_number
                    dr1("card_type") = cashTrackData.card_type

                End If

                '共通
                dr1("claim_no") = cashTrackData.claim_no
                dr1("invoice_date") = cashTrackData.invoice_date
                dr1("customer_name") = cashTrackData.customer_name
                dr1("Warranty") = cashTrackData.Warranty
                dr1("total_amount") = cashTrackData.total_amount
                dr1("input_user") = cashTrackData.input_user
                dr1("location") = cashTrackData.location
                dr1("message") = cashTrackData.message
                dr1("full_discount") = cashTrackData.full_discount
                dr1("discount") = cashTrackData.discount

                '新規DRをDataTableに追加
                ds1.Tables(0).Rows.Add(dr1)
                Adapter1.Update(ds1)

            Next i

            '■コミット
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
    '処理：cash_trackデータの登録
    '      誤登録時等による不要なデータを削除する処理（該当のFALSE項目に1を設定）
    '引数：delKey　　　　　削除対象のID
    '      invoiceDate     請求日
    '      shipCode
    '　　　userid
    '　　　errFlg      　　戻り値　0:正常　1:異常
    '****************************************************
    Public Sub setCashTrack2(ByVal delKey As String, ByVal invoiceDate As String, ByVal shipCode As String, ByVal userid As String, ByRef errFlg As Integer)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try

            Dim dtNow As DateTime = DateTime.Now
            Dim select_sql As String = ""
            select_sql = "SELECT * FROM dbo.cash_track WHERE DELFG = 0 "
            select_sql &= "AND location = '" & shipCode & "' "
            select_sql &= "AND LEFT(CONVERT(VARCHAR, invoice_date,111),10) = '" & invoiceDate & "' "
            select_sql &= "AND count_no in ("
            select_sql &= delKey & ");"
            Dim sqlSelect As New SqlCommand(select_sql, con, trn)
            Dim Adapter As New SqlDataAdapter(sqlSelect)
            Dim Builder As New SqlCommandBuilder(Adapter)
            Dim ds As New DataSet
            Dim dr1 As DataRow
            Dim i As Integer
            Adapter.Fill(ds)

            If ds.Tables(0).Rows.Count <> 0 Then

                For i = 0 To ds.Tables(0).Rows.Count - 1

                    '上書き登録
                    dr1 = ds.Tables(0).Rows(i)
                    dr1("UPDDT") = dtNow
                    dr1("UPDCD") = userid
                    dr1("FALSE") = "1"

                    '更新
                    Adapter.Update(ds)

                Next i

            End If

            '■コミット
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
    '処理：custodyデータの登録
    '      誤登録時等による不要なデータを削除する処理（該当のFALSE項目にONに設定）
    '引数：delKey　　　　　削除対象のID(keep_no)
    '      shipCode
    '　　　userid
    '　　　errFlg      　　戻り値　0:正常　1:異常
    '      tourokuFlg      戻り値　1:takeout済
    '****************************************************
    Public Sub delSet_custody(ByVal delKey As String, ByVal shipCode As String, ByVal userid As String, ByRef errFlg As Integer, ByRef tourokuFlg As Integer)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try

            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia

            Dim select_sql As String = ""
            select_sql = "SELECT * FROM dbo.custody WHERE DELFG = 0 "
            select_sql &= "AND ship_code = '" & shipCode & "' "
            select_sql &= "AND keep_no = '" & delKey & "';"

            Dim sqlSelect As New SqlCommand(select_sql, con, trn)
            Dim Adapter As New SqlDataAdapter(sqlSelect)
            Dim Builder As New SqlCommandBuilder(Adapter)
            Dim ds As New DataSet
            Dim dr1 As DataRow
            Dim i As Integer
            Adapter.Fill(ds)

            If ds.Tables(0).Rows.Count = 1 Then

                dr1 = ds.Tables(0).Rows(0)

                If dr1("takeout") IsNot DBNull.Value Then
                    Dim chkTake As String = dr1("takeout")
                    If chkTake = "False" Then
                        tourokuFlg = 1
                    End If
                End If

                dr1("UPDDT") = dtNow
                dr1("UPDCD") = userid
                dr1("takeout") = 0

                '更新
                Adapter.Update(ds)

            End If

            '■コミット
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
    '処理：データ削除処理
    '引数：delKey      　　削除対象のアップロードファイル名
    '　　　uploadShipname　削除対象の拠点名称
    '　　　errFlg      　　戻り値　0:正常　1:異常
    '      uploadFilename  削除対象のアップロードファイルの種類
    '****************************************************
    Public Sub setRecovery(ByVal delKey As String, ByVal uploadShipname As String, ByRef errFlg As Integer, ByVal uploadFilename As String, ByVal userid As String, ByVal userName As String)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Dim tblNo As Integer = Left(uploadFilename, 1)
        Dim tblName As String
        Dim branchKey As String

        'テーブル名指定
        Select Case tblNo
            Case 1
                tblName = "SC_DSR_info"
                branchKey = "AND Branch_name = '" & uploadShipname & "' "
            Case 2
                tblName = "Wty_Excel"
                branchKey = "AND upload_Branch = '" & uploadShipname & "' "
            Case 3
                tblName = "Invoice_update"
                branchKey = "AND upload_Branch = '" & uploadShipname & "' AND number = 'C' "
            Case 4
                tblName = "Invoice_update"
                branchKey = "AND upload_Branch = '" & uploadShipname & "' AND number = 'EXC' "
            Case 5
                tblName = "good_recived"
                branchKey = "AND upload_Branch = '" & uploadShipname & "' "
            Case 6
                tblName = "Billing_Info"
                branchKey = "AND upload_Branch = '" & uploadShipname & "' "
        End Select

        Try
            '■dailyreport以外の削除のケース
            If tblNo <> 1 Then

                Dim select_sql1 As String = ""
                select_sql1 &= "SELECT * "
                select_sql1 &= "FROM dbo. " & tblName & " WHERE DELFG = 0 "
                select_sql1 &= branchKey
                select_sql1 &= "AND Upload_FileName in ("
                select_sql1 &= delKey & ")"

                Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
                Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
                Dim Builder1 As New SqlCommandBuilder(Adapter1)
                Dim ds1 As New DataSet

                Adapter1.Fill(ds1)

                '***アップロードファイル名をキーに削除処理***
                If ds1.Tables(0).Rows.Count <> 0 Then

                    For i = 0 To ds1.Tables(0).Rows.Count - 1

                        Dim dr As DataRow = ds1.Tables(0).Rows(0)

                        dr.Delete()

                        '更新
                        Adapter1.Update(ds1)

                    Next i

                End If

            Else

                '■dailyreport削除のケース
                '***①番目SC_DSR_infoテーブル側の削除処理***

                'SC_DSRより、削除したいファイルの集計情報を取得
                'totalDailyStatementRepartとtotalDailyStatementRepart2に差分があれば、そのマイナスをSC_DSR_infoにセットする

                Dim totalDailyStatementRepart() As DAILYSTATEMENTREPART    '削除対象 ※アップロードファイル名をキーに取得
                Dim totalDailyStatementRepart2() As DAILYSTATEMENTREPART   'Billing_date毎の集計 ※削除対象のBilling_dateをキーに取得

                '□totalDailyStatementRepart(削除対象)の設定
                'SUM
                Dim select_sql3 As String = ""
                select_sql3 &= "SELECT Billing_date, SUM(IW_Labor) AS SUM_IW_Labor, SUM(IW_Parts) AS SUM_IW_Parts, SUM(IW_Tax) AS SUM_IW_Tax, SUM(IW_total) AS SUM_IW_total, SUM(OW_Labor) AS SUM_OW_Labor, SUM(OW_Parts) AS SUM_OW_Parts, SUM(OW_Tax) AS SUM_OW_Tax, SUM(OW_total) AS SUM_OW_total "
                select_sql3 &= "FROM dbo.SC_DSR WHERE DELFG = 0 "
                select_sql3 &= "AND GoodsDelivered_date <> '0000/00/00' "
                select_sql3 &= branchKey
                select_sql3 &= "AND ServiceOrder_No <> 'Total' "
                select_sql3 &= "AND Upload_FileName in ("
                select_sql3 &= delKey & ")"
                select_sql3 &= "GROUP BY Billing_date"

                Dim sqlSelect3 As New SqlCommand(select_sql3, con, trn)
                Dim Adapter3 As New SqlDataAdapter(sqlSelect3)
                Dim ds3 As New DataSet
                Dim dr3 As DataRow
                Adapter3.Fill(ds3)

                If ds3.Tables(0).Rows.Count <> 0 Then

                    ReDim totalDailyStatementRepart(ds3.Tables(0).Rows.Count - 1)

                    For i = 0 To ds3.Tables(0).Rows.Count - 1

                        dr3 = ds3.Tables(0).Rows(i)

                        '**日付**
                        If dr3("Billing_date") IsNot DBNull.Value Then
                            totalDailyStatementRepart(i).Billing_date = dr3("Billing_date")
                        End If

                        '**total**
                        'IW
                        If dr3("SUM_IW_Labor") IsNot DBNull.Value Then
                            totalDailyStatementRepart(i).IW_Labor = dr3("SUM_IW_Labor")
                        End If

                        If dr3("SUM_IW_Parts") IsNot DBNull.Value Then
                            totalDailyStatementRepart(i).IW_Parts = dr3("SUM_IW_Parts")
                        End If

                        If dr3("SUM_IW_Tax") IsNot DBNull.Value Then
                            totalDailyStatementRepart(i).IW_Tax = dr3("SUM_IW_Tax")
                        End If

                        If dr3("SUM_IW_total") IsNot DBNull.Value Then
                            totalDailyStatementRepart(i).IW_total = dr3("SUM_IW_total")
                        End If

                        'OW
                        If dr3("SUM_OW_Labor") IsNot DBNull.Value Then
                            totalDailyStatementRepart(i).OW_Labor = dr3("SUM_OW_Labor")
                        End If

                        If dr3("SUM_OW_Parts") IsNot DBNull.Value Then
                            totalDailyStatementRepart(i).OW_Parts = dr3("SUM_OW_Parts")
                        End If

                        If dr3("SUM_OW_Tax") IsNot DBNull.Value Then
                            totalDailyStatementRepart(i).OW_Tax = dr3("SUM_OW_Tax")
                        End If

                        If dr3("SUM_OW_total") IsNot DBNull.Value Then
                            totalDailyStatementRepart(i).OW_total = dr3("SUM_OW_total")
                        End If

                    Next i

                End If

                'COUNT
                Dim select_sql4 As String = ""
                select_sql4 &= "SELECT Billing_date, COUNT(case when IW_Labor <> 0 then IW_Labor else NULL end ) AS CNT_IW_Labor, COUNT(case when OW_Labor <> 0 then OW_Labor else NULL end ) AS CNT_OW_Labor, COUNT(case when IW_total <> 0 then IW_total else NULL end ) AS CNT_IW_total, COUNT(case when OW_total <> 0 then OW_total else NULL end ) AS CNT_OW_total "
                select_sql4 &= "FROM dbo.SC_DSR WHERE DELFG = 0 "
                select_sql4 &= "AND GoodsDelivered_date <> '0000/00/00' "
                select_sql4 &= branchKey
                select_sql4 &= "AND ServiceOrder_No <> 'Total' "
                select_sql4 &= "AND Upload_FileName in ("
                select_sql4 &= delKey & ")"
                select_sql4 &= "GROUP BY Billing_date"

                Dim sqlSelect4 As New SqlCommand(select_sql4, con, trn)
                Dim Adapter4 As New SqlDataAdapter(sqlSelect4)
                Dim ds4 As New DataSet
                Dim dr4 As DataRow
                Adapter4.Fill(ds4)

                If ds4.Tables(0).Rows.Count <> 0 Then

                    For i = 0 To ds4.Tables(0).Rows.Count - 1

                        dr4 = ds4.Tables(0).Rows(i)

                        If dr4("CNT_IW_Labor") IsNot DBNull.Value Then
                            totalDailyStatementRepart(i).IW_Count = dr4("CNT_IW_Labor")
                        End If

                        If dr4("CNT_OW_Labor") IsNot DBNull.Value Then
                            totalDailyStatementRepart(i).OW_Count = dr4("CNT_OW_Labor")
                        End If

                        If dr4("CNT_IW_total") IsNot DBNull.Value Then
                            totalDailyStatementRepart(i).IW_goods_total = dr4("CNT_IW_total")
                        End If

                        If dr4("CNT_OW_total") IsNot DBNull.Value Then
                            totalDailyStatementRepart(i).OW_goods_total = dr4("CNT_OW_total")
                        End If

                    Next i

                End If

                '**billingDateをユニークで取得**
                Dim cnt, cnt2, m As Integer
                Dim billingDateAll() As String

                For i = 0 To totalDailyStatementRepart.Length - 1

                    cnt2 = 0

                    If i = 0 Then
                        ReDim Preserve billingDateAll(cnt)
                        billingDateAll(cnt) = totalDailyStatementRepart(i).Billing_date
                        cnt = cnt + 1
                    Else

                        'billingDateの重複確認
                        If billingDateAll IsNot Nothing Then
                            For m = 0 To UBound(billingDateAll)
                                If totalDailyStatementRepart(i).Billing_date = billingDateAll(m) Then
                                    cnt2 = cnt2 + 1
                                    Exit For
                                End If
                            Next m
                        End If

                        '重複なければ、billingDateAllにbillingDateを設定
                        If cnt2 = 0 Then
                            ReDim Preserve billingDateAll(cnt)
                            billingDateAll(cnt) = totalDailyStatementRepart(i).Billing_date
                            cnt = cnt + 1
                        End If

                    End If

                Next i

                'billingDateのKeySQLの設定
                Dim billingDateKey As String = ""

                For i = 0 To billingDateAll.Length - 1
                    billingDateKey &= "'" & billingDateAll(i) & "',"
                Next i

                billingDateKey = Left(billingDateKey, billingDateKey.Length - 1)

                '□totalDailyStatementRepart2(削除対象のBilling_dateより集計情報を取得)の設定
                'SUM
                Dim select_sql5 As String = ""
                select_sql5 &= "SELECT Billing_date, SUM(IW_Labor) AS SUM_IW_Labor, SUM(IW_Parts) AS SUM_IW_Parts, SUM(IW_Tax) AS SUM_IW_Tax, SUM(IW_total) AS SUM_IW_total, SUM(OW_Labor) AS SUM_OW_Labor, SUM(OW_Parts) AS SUM_OW_Parts, SUM(OW_Tax) AS SUM_OW_Tax, SUM(OW_total) AS SUM_OW_total "
                select_sql5 &= "FROM dbo.SC_DSR WHERE DELFG = 0 "
                select_sql5 &= "AND GoodsDelivered_date <> '0000/00/00' "
                select_sql5 &= branchKey
                select_sql5 &= "AND ServiceOrder_No <> 'Total' "
                select_sql5 &= "AND Billing_date IN (" & billingDateKey & ") "
                select_sql5 &= "GROUP BY Billing_date"

                Dim sqlSelect5 As New SqlCommand(select_sql5, con, trn)
                Dim Adapter5 As New SqlDataAdapter(sqlSelect5)
                Dim ds5 As New DataSet
                Dim dr5 As DataRow
                Adapter5.Fill(ds5)

                If ds5.Tables(0).Rows.Count <> 0 Then

                    ReDim totalDailyStatementRepart2(ds5.Tables(0).Rows.Count - 1)

                    For i = 0 To ds5.Tables(0).Rows.Count - 1

                        dr5 = ds5.Tables(0).Rows(i)

                        '**日付**
                        If dr5("Billing_date") IsNot DBNull.Value Then
                            totalDailyStatementRepart2(i).Billing_date = dr5("Billing_date")
                        End If

                        '**total**
                        'IW
                        If dr5("SUM_IW_Labor") IsNot DBNull.Value Then
                            totalDailyStatementRepart2(i).IW_Labor = dr5("SUM_IW_Labor")
                        End If

                        If dr5("SUM_IW_Parts") IsNot DBNull.Value Then
                            totalDailyStatementRepart2(i).IW_Parts = dr5("SUM_IW_Parts")
                        End If

                        If dr5("SUM_IW_Tax") IsNot DBNull.Value Then
                            totalDailyStatementRepart2(i).IW_Tax = dr5("SUM_IW_Tax")
                        End If

                        If dr5("SUM_IW_total") IsNot DBNull.Value Then
                            totalDailyStatementRepart2(i).IW_total = dr5("SUM_IW_total")
                        End If

                        'OW
                        If dr5("SUM_OW_Labor") IsNot DBNull.Value Then
                            totalDailyStatementRepart2(i).OW_Labor = dr5("SUM_OW_Labor")
                        End If

                        If dr5("SUM_OW_Parts") IsNot DBNull.Value Then
                            totalDailyStatementRepart2(i).OW_Parts = dr5("SUM_OW_Parts")
                        End If

                        If dr5("SUM_OW_Tax") IsNot DBNull.Value Then
                            totalDailyStatementRepart2(i).OW_Tax = dr5("SUM_OW_Tax")
                        End If

                        If dr5("SUM_OW_total") IsNot DBNull.Value Then
                            totalDailyStatementRepart2(i).OW_total = dr5("SUM_OW_total")
                        End If

                    Next i

                End If

                'COUNT
                Dim select_sql6 As String = ""
                select_sql6 &= "SELECT Billing_date, COUNT(case when IW_Labor <> 0 then IW_Labor else NULL end ) AS CNT_IW_Labor, COUNT(case when OW_Labor <> 0 then OW_Labor else NULL end ) AS CNT_OW_Labor, COUNT(case when IW_total <> 0 then IW_total else NULL end ) AS CNT_IW_total, COUNT(case when OW_total <> 0 then OW_total else NULL end ) AS CNT_OW_total "
                select_sql6 &= "FROM dbo.SC_DSR WHERE DELFG = 0 "
                select_sql6 &= "AND GoodsDelivered_date <> '0000/00/00' "
                select_sql6 &= branchKey
                select_sql6 &= "AND ServiceOrder_No <> 'Total' "
                select_sql6 &= "AND Billing_date IN (" & billingDateKey & ") "
                select_sql6 &= "GROUP BY Billing_date"

                Dim sqlSelect6 As New SqlCommand(select_sql6, con, trn)
                Dim Adapter6 As New SqlDataAdapter(sqlSelect6)
                Dim ds6 As New DataSet
                Dim dr6 As DataRow
                Adapter6.Fill(ds6)

                If ds6.Tables(0).Rows.Count <> 0 Then

                    For i = 0 To ds6.Tables(0).Rows.Count - 1

                        dr6 = ds6.Tables(0).Rows(i)

                        If dr6("CNT_IW_Labor") IsNot DBNull.Value Then
                            totalDailyStatementRepart2(i).IW_Count = dr6("CNT_IW_Labor")
                        End If

                        If dr6("CNT_OW_Labor") IsNot DBNull.Value Then
                            totalDailyStatementRepart2(i).OW_Count = dr6("CNT_OW_Labor")
                        End If

                        If dr6("CNT_IW_total") IsNot DBNull.Value Then
                            totalDailyStatementRepart2(i).IW_goods_total = dr6("CNT_IW_total")
                        End If

                        If dr6("CNT_OW_total") IsNot DBNull.Value Then
                            totalDailyStatementRepart2(i).OW_goods_total = dr6("CNT_OW_total")
                        End If

                    Next i

                End If

                '**削除対象のSC_DSRの集計(totalDailyStatementRepart)がSC_DSR_infoの集計と一致している　　データ削除処理**
                '**                                           一致していない　削除対象分マイナスの集計をセット**
                For i = 0 To totalDailyStatementRepart.Length - 1

                    For j = 0 To totalDailyStatementRepart2.Length - 1

                        If totalDailyStatementRepart(i).Billing_date = totalDailyStatementRepart2(j).Billing_date Then

                            '一致しているか確認
                            If chkDataDiff(totalDailyStatementRepart(i), totalDailyStatementRepart2(j)) = True Then
                                '削除
                                Call setDeleteData(uploadShipname, delKey, totalDailyStatementRepart(i), errFlg)
                                If errFlg = 1 Then
                                    Exit Sub
                                End If
                            Else
                                'マイナスをセット
                                Call reSet_SC_DSR_info(totalDailyStatementRepart(i), totalDailyStatementRepart2(j), branchKey, userid, userName, errFlg)
                                If errFlg = 1 Then
                                    Exit Sub
                                End If
                            End If

                            Exit For

                        End If

                    Next j

                Next i

                '***②番目SC_DSRテーブル側の削除処理***
                Dim select_sql2 As String = ""
                select_sql2 &= "SELECT * "
                select_sql2 &= "FROM dbo.SC_DSR WHERE DELFG = 0 "
                select_sql2 &= "AND Branch_name = '" & uploadShipname & "' AND Upload_FileName in ("
                select_sql2 &= delKey & ")"

                Dim sqlSelect2 As New SqlCommand(select_sql2, con, trn)
                Dim Adapter2 As New SqlDataAdapter(sqlSelect2)
                Dim Builder2 As New SqlCommandBuilder(Adapter2)
                Dim ds2 As New DataSet

                Adapter2.Fill(ds2)

                If ds2.Tables(0).Rows.Count <> 0 Then

                    For i = 0 To ds2.Tables(0).Rows.Count - 1

                        Dim dr As DataRow = ds2.Tables(0).Rows(0)

                        dr.Delete()

                        '更新
                        Adapter2.Update(ds2)

                    Next i

                End If

            End If

            '■コミット
            trn.Commit()

        Catch ex As Exception
            errFlg = 1
            trn.Rollback()
        Finally
            'DB接続クローズ
            If con.State <> ConnectionState.Closed Then
                con.Close()
            End If
        End Try

    End Sub

    '****************************************************
    '処理名　：chkDataDiff
    '処理概要：二つのデータセット(構造体データ)の内容を比較
    '引数　　：totalDailyStatementRepart　
    '          totalDailyStatementRepart2
    '戻り値  ：TRUE 一致 FALSE 不一致
    '****************************************************'
    Public Function chkDataDiff(ByVal totalDailyStatementRepart As DAILYSTATEMENTREPART, ByVal totalDailyStatementRepart2 As DAILYSTATEMENTREPART) As Boolean

        If totalDailyStatementRepart.IW_Labor <> totalDailyStatementRepart2.IW_Labor Then
            Return False
        End If

        If totalDailyStatementRepart.IW_Parts <> totalDailyStatementRepart2.IW_Parts Then
            Return False
        End If

        If totalDailyStatementRepart.IW_Tax <> totalDailyStatementRepart2.IW_Tax Then
            Return False
        End If

        If totalDailyStatementRepart.IW_total <> totalDailyStatementRepart2.IW_total Then
            Return False
        End If

        If totalDailyStatementRepart.OW_Labor <> totalDailyStatementRepart2.OW_Labor Then
            Return False
        End If

        If totalDailyStatementRepart.OW_Parts <> totalDailyStatementRepart2.OW_Parts Then
            Return False
        End If

        If totalDailyStatementRepart.OW_Tax <> totalDailyStatementRepart2.OW_Tax Then
            Return False
        End If

        If totalDailyStatementRepart.OW_total <> totalDailyStatementRepart2.OW_total Then
            Return False
        End If

        If totalDailyStatementRepart.IW_Count <> totalDailyStatementRepart2.IW_Count Then
            Return False
        End If

        If totalDailyStatementRepart.IW_goods_total <> totalDailyStatementRepart2.IW_goods_total Then
            Return False
        End If

        If totalDailyStatementRepart.OW_Count <> totalDailyStatementRepart2.OW_Count Then
            Return False
        End If

        If totalDailyStatementRepart.OW_goods_total <> totalDailyStatementRepart2.OW_goods_total Then
            Return False
        End If

        Return True

    End Function

    '****************************************************
    '処理名　：reSet_SC_DSR_info
    '処理概要：SC_DSR_infoより、削除対象分マイナス集計をセット
    '引数　　：totalDailyStatementRepart　 　削除対象の集計情報（SC_DSRよりアップロードファイル名をキーに取得）
    '          totalDailyStatementRepart2　　削除対象の請求日をキーに取得した集計情報
    '          branchKey
    '      　　userid
    '      　　userName
    '　　　　　errFlg       戻り値　0:正常　1:異常
    '****************************************************'
    Public Sub reSet_SC_DSR_info(ByVal totalDailyStatementRepart As DAILYSTATEMENTREPART, ByVal totalDailyStatementRepart2 As DAILYSTATEMENTREPART, ByVal branchKey As String, ByVal userid As String, ByVal username As String, ByRef errFlg As Integer)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Dim dtNow As DateTime = DateTime.Now

        Try

            Dim select_sql As String = ""
            select_sql &= "SELECT * "
            select_sql &= "FROM dbo.SC_DSR_info WHERE DELFG = 0 "
            select_sql &= branchKey
            select_sql &= "AND Billing_date = '" & totalDailyStatementRepart.Billing_date & "'"

            Dim sqlSelect As New SqlCommand(select_sql, con, trn)
            Dim Adapter As New SqlDataAdapter(sqlSelect)
            Dim Builde As New SqlCommandBuilder(Adapter)
            Dim dsSC_DSR As New DataSet

            Adapter.Fill(dsSC_DSR)

            If dsSC_DSR.Tables(0).Rows.Count = 1 Then

                Dim dr As DataRow = dsSC_DSR.Tables(0).Rows(0)

                dr("UPDDT") = Now
                dr("UPDCD") = userid
                dr("upload_date") = Now
                dr("upload_user") = username

                dr("IW_Labor_total") = totalDailyStatementRepart2.IW_Labor - totalDailyStatementRepart.IW_Labor
                dr("IW_Parts_total") = totalDailyStatementRepart2.IW_Parts - totalDailyStatementRepart.IW_Parts
                dr("IW_Tax_total") = totalDailyStatementRepart2.IW_Tax - totalDailyStatementRepart.IW_Tax
                dr("IW_Total_total") = totalDailyStatementRepart2.IW_total - totalDailyStatementRepart.IW_total

                dr("OW_Labor_total") = totalDailyStatementRepart2.OW_Labor - totalDailyStatementRepart.OW_Labor
                dr("OW_Parts_total") = totalDailyStatementRepart2.OW_Parts - totalDailyStatementRepart.OW_Parts
                dr("OW_Tax_total") = totalDailyStatementRepart2.OW_Tax - totalDailyStatementRepart.OW_Tax
                dr("OW_Total_total") = totalDailyStatementRepart2.OW_total - totalDailyStatementRepart.OW_total

                dr("IW_Count") = totalDailyStatementRepart2.IW_Count - totalDailyStatementRepart.IW_Count
                dr("IW_goods_total") = totalDailyStatementRepart2.IW_goods_total - totalDailyStatementRepart.IW_goods_total

                dr("OW_Count") = totalDailyStatementRepart2.OW_Count - totalDailyStatementRepart.OW_Count
                dr("OW_goods_total") = totalDailyStatementRepart2.OW_goods_total - totalDailyStatementRepart.OW_goods_total

                '更新
                Adapter.Update(dsSC_DSR)

            End If

            '■コミット
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
    '処理名　：setDeleteData
    '処理概要：
    '引数　　：uploadShipname   　　　　　　拠点名称　
    '          delKey        　　　　　　　 削除対象のアップロードファイル名　　
    '          totalDailyStatementRepart　　請求日
    '　　　　　errFlg       　　　　　　　　戻り値　0:正常　1:異常
    '****************************************************
    Public Sub setDeleteData(ByVal uploadShipname As String, ByVal delKey As String, ByVal totalDailyStatementRepart As DAILYSTATEMENTREPART, ByRef errFlg As Integer)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Dim dtNow As DateTime = DateTime.Now

        Try

            Dim select_sql2 As String = ""
            select_sql2 &= "SELECT * "
            select_sql2 &= "FROM dbo.SC_DSR_info WHERE DELFG = 0 "
            select_sql2 &= "AND Billing_date = '" & totalDailyStatementRepart.Billing_date & "' "
            'select_sql2 &= "AND Branch_name = '" & uploadShipname & "' AND Upload_FileName in ("
            'select_sql2 &= delKey & ")"
            select_sql2 &= "AND Branch_name = '" & uploadShipname & "' "

            Dim sqlSelect2 As New SqlCommand(select_sql2, con, trn)
            Dim Adapter2 As New SqlDataAdapter(sqlSelect2)
            Dim Builder2 As New SqlCommandBuilder(Adapter2)
            Dim ds2 As New DataSet

            Adapter2.Fill(ds2)

            If ds2.Tables(0).Rows.Count <> 0 Then

                For i = 0 To ds2.Tables(0).Rows.Count - 1

                    Dim dr As DataRow = ds2.Tables(0).Rows(0)

                    dr.Delete()

                    '更新
                    Adapter2.Update(ds2)

                Next i

            End If

            '■コミット
            trn.Commit()

        Catch ex As Exception
            errFlg = 1
            trn.Rollback()
        Finally
            'DB接続クローズ
            If con.State <> ConnectionState.Closed Then
                con.Close()
            End If
        End Try

    End Sub

    '****************************************************
    '処理名　：setFinalReport
    '処理概要：WtyExcelよりFinalReportの出力対象を取得
    '引数　　：dsWtyExcel　
    '          wtyData
    '      　　userid
    '      　　userName
    '      　　shipCode
    '      　　exportShipName
    '　　　　　errFlg       戻り値　0:正常　1:異常
    '      　　setMon       対象月
    '      　　dateFrom     対象期間　FROM　※月指定がされていないとき
    '      　　dateTo　　　 対象期間　TO
    '          InvoiceNoMax 採番
    '****************************************************
    Public Sub setFinalReport(ByRef dsWtyExcel As DataSet, ByRef wtyData() As WTY_EXCEL, ByVal userid As String, ByVal userName As String, ByVal shipCode As String, ByVal exportShipName As String, ByRef errFlg As Integer, ByVal setMon As String, ByVal dateFrom As String, ByVal dateTo As String, ByVal InvoiceNoMax As Long)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try

            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia

            Dim select_sql As String = ""
            select_sql &= "SELECT B.OW_Labor, B.OW_Parts, B.OW_total, B.OW_Tax, A.* "
            select_sql &= "FROM dbo.Wty_Excel A "
            select_sql &= ",SC_DSR B "
            select_sql &= "WHERE A.DELFG = 0 "
            select_sql &= "AND A.ASC_Claim_No = B.ServiceOrder_No "
            select_sql &= "AND A.Branch_Code = '" & shipCode & "' "
            select_sql &= "AND B.Branch_name = '" & exportShipName & "' "
            select_sql &= "AND (A.Part_1 is not NULL and  A.Part_1 <> '') "
            select_sql &= "AND A.Status = '80' "
            select_sql &= "AND A.Delivery_Date = B.Billing_date "
            select_sql &= "AND B.Branch_name  = A.upload_Branch "

            If setMon = "00" Then
                If dateTo <> "" Then
                    If dateFrom <> "" Then
                        select_sql &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),10) <= '" & dateTo & "' "
                        select_sql &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),10) >= '" & dateFrom & "';"
                    Else
                        select_sql &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),10) <= '" & dateTo & "';"
                    End If
                Else
                    If dateFrom <> "" Then
                        select_sql &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),10) >= '" & dateFrom & "';"
                    End If
                End If

            Else
                select_sql &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),7) = '" & dtNow.ToLongDateString.Substring(0, 4) & "/" & setMon & "'; "
            End If

            Dim sqlSelect As New SqlCommand(select_sql, con, trn)
            Dim Adapter As New SqlDataAdapter(sqlSelect)

            Adapter.Fill(dsWtyExcel)

            If dsWtyExcel.Tables(0).Rows.Count = 0 Then
                dsWtyExcel = Nothing
            Else

                '出力対象を構造体に設定
                ReDim wtyData(dsWtyExcel.Tables(0).Rows.Count - 1)

                For i = 0 To dsWtyExcel.Tables(0).Rows.Count - 1

                    Dim dr As DataRow = dsWtyExcel.Tables(0).Rows(i)

                    If dr("Branch_Code") IsNot DBNull.Value Then
                        wtyData(i).Branch_Code = dr("Branch_Code")
                    End If

                    If dr("ASC_Claim_No") IsNot DBNull.Value Then
                        wtyData(i).ASC_Claim_No = dr("ASC_Claim_No")
                    End If

                    If dr("Samsung_Claim_No") IsNot DBNull.Value Then
                        wtyData(i).Samsung_Claim_No = dr("Samsung_Claim_No")
                    End If

                    If dr("Delivery_Date") IsNot DBNull.Value Then
                        wtyData(i).Delivery_Date = dr("Delivery_Date")
                    End If

                    If dr("Labor_Amount") IsNot DBNull.Value Then
                        wtyData(i).Labor_Amount = dr("Labor_Amount")
                    End If

                    If dr("Parts_Amount") IsNot DBNull.Value Then
                        wtyData(i).Parts_Amount = dr("Parts_Amount")
                    End If

                    If dr("Total_Invoice_Amount") IsNot DBNull.Value Then
                        wtyData(i).Total_Invoice_Amount = dr("Total_Invoice_Amount")
                    End If

                    If dr("Parts_SGST") IsNot DBNull.Value Then
                        wtyData(i).Parts_SGST = dr("Parts_SGST")
                    End If

                    If dr("Parts_CGST") IsNot DBNull.Value Then
                        wtyData(i).Parts_CGST = dr("Parts_CGST")
                    End If

                    If dr("Parts_IGST") IsNot DBNull.Value Then
                        wtyData(i).Parts_IGST = dr("Parts_IGST")
                    End If

                    If dr("SGST") IsNot DBNull.Value Then
                        wtyData(i).SGST = dr("SGST")
                    End If

                    If dr("CGST") IsNot DBNull.Value Then
                        wtyData(i).CGST = dr("CGST")
                    End If

                    If dr("IGST") IsNot DBNull.Value Then
                        wtyData(i).IGST = dr("IGST")
                    End If

                    If dr("Location_1") IsNot DBNull.Value Then
                        wtyData(i).Location_1 = dr("Location_1")
                    End If

                    If dr("Part_1") IsNot DBNull.Value Then
                        wtyData(i).Part_1 = dr("Part_1")
                    End If

                    If dr("Qty_1") IsNot DBNull.Value Then
                        wtyData(i).Qty_1 = dr("Qty_1")
                    End If

                    If dr("Unit_Price_1") IsNot DBNull.Value Then
                        wtyData(i).Unit_Price_1 = dr("Unit_Price_1")
                    End If

                    If dr("Doc_Num_1") IsNot DBNull.Value Then
                        wtyData(i).Doc_Num_1 = dr("Doc_Num_1")
                    End If

                    If dr("Matrial_Serial_1") IsNot DBNull.Value Then
                        wtyData(i).Matrial_Serial_1 = dr("Matrial_Serial_1")
                    End If

                    If dr("Location_2") IsNot DBNull.Value Then
                        wtyData(i).Location_2 = dr("Location_2")
                    End If

                    If dr("Part_2") IsNot DBNull.Value Then
                        wtyData(i).Part_2 = dr("Part_2")
                    End If

                    If dr("Qty_2") IsNot DBNull.Value Then
                        wtyData(i).Qty_2 = dr("Qty_2")
                    End If

                    If dr("Unit_Price_2") IsNot DBNull.Value Then
                        wtyData(i).Unit_Price_2 = dr("Unit_Price_2")
                    End If

                    If dr("Doc_Num_2") IsNot DBNull.Value Then
                        wtyData(i).Doc_Num_2 = dr("Doc_Num_2")
                    End If

                    If dr("Matrial_Serial_2") IsNot DBNull.Value Then
                        wtyData(i).Matrial_Serial_2 = dr("Matrial_Serial_2")
                    End If

                    If dr("Location_3") IsNot DBNull.Value Then
                        wtyData(i).Location_3 = dr("Location_3")
                    End If

                    If dr("Part_3") IsNot DBNull.Value Then
                        wtyData(i).Part_3 = dr("Part_3")
                    End If

                    If dr("Qty_3") IsNot DBNull.Value Then
                        wtyData(i).Qty_3 = dr("Qty_3")
                    End If

                    If dr("Unit_Price_3") IsNot DBNull.Value Then
                        wtyData(i).Unit_Price_3 = dr("Unit_Price_3")
                    End If

                    If dr("Doc_Num_3") IsNot DBNull.Value Then
                        wtyData(i).Doc_Num_3 = dr("Doc_Num_3")
                    End If

                    If dr("Matrial_Serial_3") IsNot DBNull.Value Then
                        wtyData(i).Matrial_Serial_3 = dr("Matrial_Serial_3")
                    End If

                    If dr("Location_4") IsNot DBNull.Value Then
                        wtyData(i).Location_4 = dr("Location_4")
                    End If

                    If dr("Part_4") IsNot DBNull.Value Then
                        wtyData(i).Part_4 = dr("Part_4")
                    End If

                    If dr("Qty_4") IsNot DBNull.Value Then
                        wtyData(i).Qty_4 = dr("Qty_4")
                    End If

                    If dr("Unit_Price_4") IsNot DBNull.Value Then
                        wtyData(i).Unit_Price_4 = dr("Unit_Price_4")
                    End If

                    If dr("Doc_Num_4") IsNot DBNull.Value Then
                        wtyData(i).Doc_Num_4 = dr("Doc_Num_4")
                    End If

                    If dr("Matrial_Serial_4") IsNot DBNull.Value Then
                        wtyData(i).Matrial_Serial_4 = dr("Matrial_Serial_4")
                    End If

                    If dr("Location_5") IsNot DBNull.Value Then
                        wtyData(i).Location_5 = dr("Location_5")
                    End If

                    If dr("Part_5") IsNot DBNull.Value Then
                        wtyData(i).Part_5 = dr("Part_5")
                    End If

                    If dr("Qty_5") IsNot DBNull.Value Then
                        wtyData(i).Qty_5 = dr("Qty_5")
                    End If

                    If dr("Unit_Price_5") IsNot DBNull.Value Then
                        wtyData(i).Unit_Price_5 = dr("Unit_Price_5")
                    End If

                    If dr("Doc_Num_5") IsNot DBNull.Value Then
                        wtyData(i).Doc_Num_5 = dr("Doc_Num_5")
                    End If

                    If dr("Matrial_Serial_5") IsNot DBNull.Value Then
                        wtyData(i).Matrial_Serial_5 = dr("Matrial_Serial_5")
                    End If

                    If dr("Location_6") IsNot DBNull.Value Then
                        wtyData(i).Location_6 = dr("Location_6")
                    End If

                    If dr("Part_6") IsNot DBNull.Value Then
                        wtyData(i).Part_6 = dr("Part_6")
                    End If

                    If dr("Qty_6") IsNot DBNull.Value Then
                        wtyData(i).Qty_6 = dr("Qty_6")
                    End If

                    If dr("Unit_Price_6") IsNot DBNull.Value Then
                        wtyData(i).Unit_Price_6 = dr("Unit_Price_6")
                    End If

                    If dr("Doc_Num_6") IsNot DBNull.Value Then
                        wtyData(i).Doc_Num_6 = dr("Doc_Num_6")
                    End If

                    If dr("Matrial_Serial_6") IsNot DBNull.Value Then
                        wtyData(i).Matrial_Serial_6 = dr("Matrial_Serial_6")
                    End If

                    If dr("Location_7") IsNot DBNull.Value Then
                        wtyData(i).Location_7 = dr("Location_7")
                    End If

                    If dr("Part_7") IsNot DBNull.Value Then
                        wtyData(i).Part_7 = dr("Part_7")
                    End If

                    If dr("Qty_7") IsNot DBNull.Value Then
                        wtyData(i).Qty_7 = dr("Qty_7")
                    End If

                    If dr("Unit_Price_7") IsNot DBNull.Value Then
                        wtyData(i).Unit_Price_7 = dr("Unit_Price_7")
                    End If

                    If dr("Doc_Num_7") IsNot DBNull.Value Then
                        wtyData(i).Doc_Num_7 = dr("Doc_Num_7")
                    End If

                    If dr("Matrial_Serial_7") IsNot DBNull.Value Then
                        wtyData(i).Matrial_Serial_7 = dr("Matrial_Serial_7")
                    End If

                    If dr("Location_8") IsNot DBNull.Value Then
                        wtyData(i).Location_8 = dr("Location_8")
                    End If

                    If dr("Part_8") IsNot DBNull.Value Then
                        wtyData(i).Part_8 = dr("Part_8")
                    End If

                    If dr("Qty_8") IsNot DBNull.Value Then
                        wtyData(i).Qty_8 = dr("Qty_8")
                    End If

                    If dr("Unit_Price_8") IsNot DBNull.Value Then
                        wtyData(i).Unit_Price_8 = dr("Unit_Price_8")
                    End If

                    If dr("Doc_Num_8") IsNot DBNull.Value Then
                        wtyData(i).Doc_Num_8 = dr("Doc_Num_8")
                    End If

                    If dr("Matrial_Serial_8") IsNot DBNull.Value Then
                        wtyData(i).Matrial_Serial_8 = dr("Matrial_Serial_8")
                    End If

                    If dr("Location_9") IsNot DBNull.Value Then
                        wtyData(i).Location_9 = dr("Location_9")
                    End If

                    If dr("Part_9") IsNot DBNull.Value Then
                        wtyData(i).Part_9 = dr("Part_9")
                    End If

                    If dr("Qty_9") IsNot DBNull.Value Then
                        wtyData(i).Qty_9 = dr("Qty_9")
                    End If

                    If dr("Unit_Price_9") IsNot DBNull.Value Then
                        wtyData(i).Unit_Price_9 = dr("Unit_Price_9")
                    End If

                    If dr("Doc_Num_9") IsNot DBNull.Value Then
                        wtyData(i).Doc_Num_9 = dr("Doc_Num_9")
                    End If

                    If dr("Matrial_Serial_9") IsNot DBNull.Value Then
                        wtyData(i).Matrial_Serial_9 = dr("Matrial_Serial_9")
                    End If

                    If dr("Location_10") IsNot DBNull.Value Then
                        wtyData(i).Location_10 = dr("Location_10")
                    End If

                    If dr("Part_10") IsNot DBNull.Value Then
                        wtyData(i).Part_10 = dr("Part_10")
                    End If

                    If dr("Qty_10") IsNot DBNull.Value Then
                        wtyData(i).Qty_10 = dr("Qty_10")
                    End If

                    If dr("Unit_Price_10") IsNot DBNull.Value Then
                        wtyData(i).Unit_Price_10 = dr("Unit_Price_10")
                    End If

                    If dr("Doc_Num_10") IsNot DBNull.Value Then
                        wtyData(i).Doc_Num_10 = dr("Doc_Num_10")
                    End If

                    If dr("Matrial_Serial_10") IsNot DBNull.Value Then
                        wtyData(i).Matrial_Serial_10 = dr("Matrial_Serial_10")
                    End If

                    If dr("Location_11") IsNot DBNull.Value Then
                        wtyData(i).Location_11 = dr("Location_11")
                    End If

                    If dr("Part_11") IsNot DBNull.Value Then
                        wtyData(i).Part_11 = dr("Part_11")
                    End If

                    If dr("Qty_11") IsNot DBNull.Value Then
                        wtyData(i).Qty_11 = dr("Qty_11")
                    End If

                    If dr("Unit_Price_11") IsNot DBNull.Value Then
                        wtyData(i).Unit_Price_11 = dr("Unit_Price_11")
                    End If

                    If dr("Doc_Num_11") IsNot DBNull.Value Then
                        wtyData(i).Doc_Num_11 = dr("Doc_Num_11")
                    End If

                    If dr("Matrial_Serial_11") IsNot DBNull.Value Then
                        wtyData(i).Matrial_Serial_11 = dr("Matrial_Serial_11")
                    End If

                    If dr("Location_12") IsNot DBNull.Value Then
                        wtyData(i).Location_12 = dr("Location_12")
                    End If

                    If dr("Part_12") IsNot DBNull.Value Then
                        wtyData(i).Part_12 = dr("Part_12")
                    End If

                    If dr("Qty_12") IsNot DBNull.Value Then
                        wtyData(i).Qty_12 = dr("Qty_12")
                    End If

                    If dr("Unit_Price_12") IsNot DBNull.Value Then
                        wtyData(i).Unit_Price_12 = dr("Unit_Price_12")
                    End If

                    If dr("Doc_Num_12") IsNot DBNull.Value Then
                        wtyData(i).Doc_Num_12 = dr("Doc_Num_12")
                    End If

                    If dr("Matrial_Serial_12") IsNot DBNull.Value Then
                        wtyData(i).Matrial_Serial_12 = dr("Matrial_Serial_12")
                    End If

                    If dr("Location_13") IsNot DBNull.Value Then
                        wtyData(i).Location_13 = dr("Location_13")
                    End If

                    If dr("Part_13") IsNot DBNull.Value Then
                        wtyData(i).Part_13 = dr("Part_13")
                    End If

                    If dr("Qty_13") IsNot DBNull.Value Then
                        wtyData(i).Qty_13 = dr("Qty_13")
                    End If

                    If dr("Unit_Price_13") IsNot DBNull.Value Then
                        wtyData(i).Unit_Price_13 = dr("Unit_Price_13")
                    End If

                    If dr("Doc_Num_13") IsNot DBNull.Value Then
                        wtyData(i).Doc_Num_13 = dr("Doc_Num_13")
                    End If

                    If dr("Matrial_Serial_13") IsNot DBNull.Value Then
                        wtyData(i).Matrial_Serial_13 = dr("Matrial_Serial_13")
                    End If

                    If dr("Location_14") IsNot DBNull.Value Then
                        wtyData(i).Location_14 = dr("Location_14")
                    End If

                    If dr("Part_14") IsNot DBNull.Value Then
                        wtyData(i).Part_14 = dr("Part_14")
                    End If

                    If dr("Qty_14") IsNot DBNull.Value Then
                        wtyData(i).Qty_14 = dr("Qty_14")
                    End If

                    If dr("Unit_Price_14") IsNot DBNull.Value Then
                        wtyData(i).Unit_Price_14 = dr("Unit_Price_14")
                    End If

                    If dr("Doc_Num_14") IsNot DBNull.Value Then
                        wtyData(i).Doc_Num_14 = dr("Doc_Num_14")
                    End If

                    If dr("Matrial_Serial_14") IsNot DBNull.Value Then
                        wtyData(i).Matrial_Serial_14 = dr("Matrial_Serial_14")
                    End If

                    If dr("Location_15") IsNot DBNull.Value Then
                        wtyData(i).Location_15 = dr("Location_15")
                    End If

                    If dr("Part_15") IsNot DBNull.Value Then
                        wtyData(i).Part_15 = dr("Part_15")
                    End If

                    If dr("Qty_15") IsNot DBNull.Value Then
                        wtyData(i).Qty_15 = dr("Qty_15")
                    End If

                    If dr("Unit_Price_15") IsNot DBNull.Value Then
                        wtyData(i).Unit_Price_15 = dr("Unit_Price_15")
                    End If

                    If dr("Doc_Num_15") IsNot DBNull.Value Then
                        wtyData(i).Doc_Num_15 = dr("Doc_Num_15")
                    End If

                    If dr("Matrial_Serial_15") IsNot DBNull.Value Then
                        wtyData(i).Matrial_Serial_15 = dr("Matrial_Serial_15")
                    End If

                    'dailyReportより税金の合計情報を設定
                    wtyData(i).Sum_Tax_Amount = dr("OW_Tax")
                    wtyData(i).SGST_Payable = wtyData(i).Sum_Tax_Amount / 2
                    wtyData(i).CGST_Payable = wtyData(i).Sum_Tax_Amount / 2

                    'dailyReportよりtotal情報を取得
                    If dr("OW_Labor") IsNot DBNull.Value Then
                        wtyData(i).OW_Labor = dr("OW_Labor")
                    End If

                    If dr("OW_Parts") IsNot DBNull.Value Then
                        wtyData(i).OW_Parts = dr("OW_Parts")
                    End If

                    If dr("OW_total") IsNot DBNull.Value Then
                        wtyData(i).OW_total = dr("OW_total")
                    End If

                Next i

            End If

            '■Wty_Excelへ採番情報更新、payment情報取得
            For i = 0 To wtyData.Length - 1

                Dim select_sql2 As String = ""
                select_sql2 &= "SELECT A.* "
                select_sql2 &= "FROM dbo.Wty_Excel A "
                select_sql2 &= "WHERE A.DELFG = 0 "
                select_sql2 &= "AND A.Status = '80' "
                select_sql2 &= "AND A.Branch_Code = '" & shipCode & "' "
                select_sql2 &= "AND A.ASC_Claim_No = '" & wtyData(i).ASC_Claim_No & "' "
                select_sql2 &= "AND A.Samsung_Claim_No = '" & wtyData(i).Samsung_Claim_No & "' "
                select_sql2 &= "AND A.upload_Branch = '" & exportShipName & "' "

                If setMon = "00" Then
                    If dateTo <> "" Then
                        If dateFrom <> "" Then
                            select_sql2 &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),10) <= '" & dateTo & "' "
                            select_sql2 &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),10) >= '" & dateFrom & "';"
                        Else
                            select_sql2 &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),10) <= '" & dateTo & "';"
                        End If
                    Else
                        If dateFrom <> "" Then
                            select_sql2 &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),10) >= '" & dateFrom & "';"
                        End If
                    End If

                Else
                    select_sql2 &= "AND LEFT(CONVERT(VARCHAR, A.Delivery_Date,111),7) = '" & dtNow.ToLongDateString.Substring(0, 4) & "/" & setMon & "'; "
                End If

                Dim sqlSelect2 As New SqlCommand(select_sql2, con, trn)
                Dim Adapter2 As New SqlDataAdapter(sqlSelect2)
                Dim Builder2 As New SqlCommandBuilder(Adapter2)
                Dim ds2 As New DataSet

                Adapter2.Fill(ds2)

                If ds2.Tables(0).Rows.Count = 1 Then

                    Dim dr As DataRow = ds2.Tables(0).Rows(0)

                    If wtyData(i).OW_Labor = "" Then
                        dr("Labor_Amount_I") = 0
                    Else
                        dr("Labor_Amount_I") = wtyData(i).OW_Labor
                    End If

                    If wtyData(i).OW_Parts = "" Then
                        dr("Parts_Amount_I") = 0
                    Else
                        dr("Parts_Amount_I") = wtyData(i).OW_Parts
                    End If

                    If wtyData(i).OW_total = "" Then
                        dr("Total_Invoice_Amount_I") = 0
                    Else
                        dr("Total_Invoice_Amount_I") = wtyData(i).OW_total
                    End If

                    dr("tax_total") = wtyData(i).Sum_Tax_Amount

                    dr("SGST_total") = wtyData(i).SGST_Payable

                    dr("CGST_total") = wtyData(i).CGST_Payable

                    dr("UPDDT") = Now
                    dr("UPDCD") = userid
                    dr("upload_date") = Now
                    dr("upload_user") = userName

                    'Invocie Numberの採番設定
                    If dr("InvoiceNo_Final") Is DBNull.Value Then
                        dr("InvoiceNo_Final") = "GSS" & exportShipName & "-" & InvoiceNoMax.ToString("0000000")
                        InvoiceNoMax = InvoiceNoMax + 1
                        wtyData(i).InvoiceNo_Final = dr("InvoiceNo_Final")
                    Else
                        '採番済の情報を取得
                        wtyData(i).InvoiceNo_Final = dr("InvoiceNo_Final")
                    End If

                    '更新
                    Adapter2.Update(ds2)

                    'payment情報取得
                    Dim select_sql3 As String = ""
                    select_sql3 &= "SELECT * "
                    select_sql3 &= "FROM dbo.cash_track "
                    select_sql3 &= "WHERE DELFG = 0 "
                    select_sql3 &= "AND location = '" & shipCode & "' "
                    select_sql3 &= "AND claim_no = '" & wtyData(i).ASC_Claim_No & "' "

                    Dim sqlSelect3 As New SqlCommand(select_sql3, con, trn)
                    Dim Adapter3 As New SqlDataAdapter(sqlSelect3)
                    Dim ds3 As New DataSet

                    Adapter3.Fill(ds3)

                    If ds3.Tables(0).Rows.Count = 1 Then

                        Dim dr2 As DataRow = ds3.Tables(0).Rows(0)

                        If dr2("payment") IsNot DBNull.Value Then
                            wtyData(i).payment = dr2("payment")
                        End If

                    End If

                End If

            Next i

            '■コミット
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
    '処理名　：exportData
    '処理概要：cash_trackよりWarranty毎の情報を取得
    '引数　　：dscash_trackl　cash_trackのデータセットをセット　　
    '          cashTrackData　構造体にdscash_tracklより出力情報をセット
    '      　　Warranty　　　 OOW/IW/Other　　
    '      　　shipCode　　　 拠点コード　
    '      　　invoiceDate    出力対象の請求日
    '　　　　　errFlg         戻り値　0:正常　1:異常
    '****************************************************
    Public Sub exportData(ByRef dscash_track As DataSet, ByRef cashTrackData() As Class_analysis.CASH_TRACK, ByVal Warranty As String, ByVal shipCode As String, ByVal invoiceDate As String, ByRef errFlg As Integer)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Dim clsCommon As New Class_common
        Dim dtNow As DateTime = clsCommon.dtIndia

        Try

            Dim strSQL As String
            If Warranty = "ALL" Then
                strSQL = "SELECT * "
                strSQL &= "FROM dbo.cash_track WHERE DELFG = 0 AND location = '" & shipCode & "' AND LEFT(CONVERT(VARCHAR, invoice_date,111),10) = '" & invoiceDate & "' ORDER BY claim_no, count_no;"
            Else
                strSQL = "SELECT * "
                strSQL &= "FROM dbo.cash_track WHERE DELFG = 0 AND location = '" & shipCode & "' AND LEFT(CONVERT(VARCHAR, invoice_date,111),10) = '" & invoiceDate & "' AND Warranty = '" & Warranty & "' ORDER BY claim_no, count_no;"
            End If

            Dim sqlSelect As New SqlCommand(strSQL, con, trn)
            Dim Adapter As New SqlDataAdapter(sqlSelect)

            Adapter.Fill(dscash_track)

            If dscash_track.Tables(0).Rows.Count = 0 Then
                dscash_track = Nothing
            Else

                '出力対象を構造体に設定
                ReDim cashTrackData(dscash_track.Tables(0).Rows.Count - 1)

                For i = 0 To dscash_track.Tables(0).Rows.Count - 1

                    Dim dr As DataRow = dscash_track.Tables(0).Rows(i)

                    If dr("FALSE") IsNot DBNull.Value Then
                        cashTrackData(i).delFlg = dr("FALSE")
                    End If

                    If dr("claim_no") IsNot DBNull.Value Then
                        cashTrackData(i).claim_no = dr("claim_no")
                    End If

                    If dr("count_no") IsNot DBNull.Value Then
                        cashTrackData(i).count_no = dr("count_no")
                    End If

                    If dr("invoice_date") IsNot DBNull.Value Then
                        cashTrackData(i).invoice_date = dr("invoice_date")
                    End If

                    If dr("customer_name") IsNot DBNull.Value Then
                        cashTrackData(i).customer_name = dr("customer_name")
                    End If

                    If dr("Warranty") IsNot DBNull.Value Then
                        cashTrackData(i).Warranty = dr("Warranty")
                    End If

                    If dr("payment") IsNot DBNull.Value Then
                        cashTrackData(i).payment = dr("payment")
                    End If

                    If dr("payment_kind") IsNot DBNull.Value Then
                        cashTrackData(i).payment_kind = dr("payment_kind")
                    End If

                    If dr("total_amount") IsNot DBNull.Value Then
                        cashTrackData(i).total_amount = dr("total_amount")
                    End If

                    If dr("claim") IsNot DBNull.Value Then
                        cashTrackData(i).claim = dr("claim")
                    End If

                    If dr("claim_card") IsNot DBNull.Value Then
                        cashTrackData(i).claimCredit = dr("claim_card")
                    End If

                    If dr("input_user") IsNot DBNull.Value Then
                        cashTrackData(i).input_user = dr("input_user")
                    End If

                    If dr("location") IsNot DBNull.Value Then
                        cashTrackData(i).location = dr("location")
                    End If

                    If dr("card_number") IsNot DBNull.Value Then
                        cashTrackData(i).card_number = dr("card_number")
                    End If

                    If dr("card_type") IsNot DBNull.Value Then
                        cashTrackData(i).card_type = dr("card_type")
                    End If

                    If dr("deposit") IsNot DBNull.Value Then
                        cashTrackData(i).deposit = dr("deposit")
                    End If

                    If dr("change") IsNot DBNull.Value Then
                        cashTrackData(i).change = dr("change")
                    End If

                    If dr("discount") IsNot DBNull.Value Then
                        cashTrackData(i).discount = dr("discount")
                    End If

                    If dr("full_discount") IsNot DBNull.Value Then
                        cashTrackData(i).full_discount = dr("full_discount")
                    End If

                    If dr("message") IsNot DBNull.Value Then
                        cashTrackData(i).message = dr("message")
                    End If

                Next i

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

    '****************************************************
    '処理名　：exportDataActivityReport
    '処理概要：Activity_reportより出力情報を取得
    '引数　　：dsActivity_report　 Activity_reportのデータセットをセット　　
    '          activityReportData　構造体にActivity_reportより出力情報をセット
    '          activityReportDataSum
    '          setMon
    '          shipCode
    '　　　　　errFlg         戻り値　0:正常　1:異常
    '****************************************************
    Public Sub exportDataActivityReport(ByRef dsActivity_report As DataSet, ByRef activityReportData() As Class_analysis.ACTIVITY_REPORT, ByRef activityReportDataSum As Class_analysis.ACTIVITY_REPORT, ByVal setMon As String, ByVal shipCode As String, ByRef errFlg As Integer)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try

            Dim strSQL As String = "SELECT * FROM dbo.Activity_report "
            strSQL &= "WHERE DELFG = 0 AND Month = '" & setMon & "' "
            strSQL &= "AND location = '" & shipCode & "';"

            Dim sqlSelect As New SqlCommand(strSQL, con, trn)
            Dim Adapter As New SqlDataAdapter(sqlSelect)

            Adapter.Fill(dsActivity_report)

            If dsActivity_report.Tables(0).Rows.Count = 0 Then
                dsActivity_report = Nothing
            Else

                '出力対象を構造体に設定
                ReDim activityReportData(dsActivity_report.Tables(0).Rows.Count - 1)

                For i = 0 To dsActivity_report.Tables(0).Rows.Count - 1

                    Dim dr As DataRow = dsActivity_report.Tables(0).Rows(i)

                    If dr("month") IsNot DBNull.Value Then
                        activityReportData(i).month = dr("month")
                    End If

                    If dr("day") IsNot DBNull.Value Then
                        activityReportData(i).day = dr("day")
                    End If

                    If dr("note") IsNot DBNull.Value Then
                        activityReportData(i).note = dr("note")
                    End If

                    If dr("Customer_Visit") IsNot DBNull.Value Then
                        activityReportData(i).Customer_Visit = dr("Customer_Visit")
                    End If

                    If dr("Call_Registerd") IsNot DBNull.Value Then
                        activityReportData(i).Call_Registerd = dr("Call_Registerd")
                    End If

                    If dr("Repair_Completed") IsNot DBNull.Value Then
                        activityReportData(i).Repair_Completed = dr("Repair_Completed")
                    End If

                    If dr("Goods_Delivered") IsNot DBNull.Value Then
                        activityReportData(i).Goods_Delivered = dr("Goods_Delivered")
                    End If

                    If dr("Pending_Calls") IsNot DBNull.Value Then
                        activityReportData(i).Pending_Calls = dr("Pending_Calls")
                    End If

                    If dr("Cancelled_Calls") IsNot DBNull.Value Then
                        activityReportData(i).Cancelled_Calls = dr("Cancelled_Calls")
                    End If

                    If dr("location") IsNot DBNull.Value Then
                        activityReportData(i).location = dr("location")
                    End If

                Next i

            End If

            Dim strSQL2 As String = "SELECT SUM(Customer_Visit) AS Customer_Visit, SUM(Call_Registerd) AS Call_Registerd, SUM(Repair_Completed) AS Repair_Completed, SUM(Goods_Delivered) AS Goods_Delivered, SUM(Pending_Calls) AS Pending_Calls, SUM(Cancelled_Calls) AS Cancelled_Calls FROM dbo.Activity_report "
            strSQL2 &= "WHERE DELFG = 0 AND Month = '" & setMon & "' "
            strSQL2 &= "AND location = '" & shipCode & "';"

            Dim sqlSelect2 As New SqlCommand(strSQL2, con, trn)
            Dim Adapter2 As New SqlDataAdapter(sqlSelect2)
            Dim Builder2 As New SqlCommandBuilder(Adapter2)
            Dim ds As New DataSet

            Adapter2.Fill(ds)

            If ds.Tables(0).Rows.Count = 1 Then

                Dim dr As DataRow = ds.Tables(0).Rows(0)

                If dr("Customer_Visit") IsNot DBNull.Value Then
                    activityReportDataSum.Customer_Visit = dr("Customer_Visit")
                End If

                If dr("Call_Registerd") IsNot DBNull.Value Then
                    activityReportDataSum.Call_Registerd = dr("Call_Registerd")
                End If

                If dr("Repair_Completed") IsNot DBNull.Value Then
                    activityReportDataSum.Repair_Completed = dr("Repair_Completed")
                End If

                If dr("Goods_Delivered") IsNot DBNull.Value Then
                    activityReportDataSum.Goods_Delivered = dr("Goods_Delivered")
                End If

                If dr("Pending_Calls") IsNot DBNull.Value Then
                    activityReportDataSum.Pending_Calls = dr("Pending_Calls")
                End If

                If dr("Cancelled_Calls") IsNot DBNull.Value Then
                    activityReportDataSum.Cancelled_Calls = dr("Cancelled_Calls")
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

    '****************************************************
    '処理：setRefresh
    '処理概要：不要なデータの削除処理
    '引数：setMon          月指定
    '　　　dateTo　        請求日等の日付指定　　終了
    '      dateFrom　　　　請求日等の日付け指定　開始
    '      targetShipname  削除対象の拠点名
    '      targetDelData　 削除対象のデータの種類
    '      userid
    '      userName
    '      setShipCode
    '　　　errFlg      　　戻り値　0:正常　1:異常
    '      tourokuFlg      戻り値　0:削除対象なし　1:削除対象あり
    '****************************************************
    Public Sub setRefresh(ByVal setMon As String, ByVal dateTo As String, ByVal dateFrom As String, ByVal targetShipname As String, ByVal targetDelData As String, ByVal userid As String, ByVal userName As String, ByVal setShipCode As String, ByRef errFlg As Integer, ByRef tourokuFlg As Integer)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)
        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia

        'テーブル名指定
        Dim tblNo As Integer = Left(targetDelData, 1)
        Dim tblName As String
        Dim branchKey As String
        Dim targetDate As String

        Select Case tblNo
            Case 1
                tblName = "SC_DSR_info"
                branchKey = "AND Branch_name = '" & targetShipname & "' "
                targetDate = "Billing_date"
            Case 2
                tblName = "Wty_Excel"
                branchKey = "AND Branch_Code = '" & setShipCode & "' "
                targetDate = "Delivery_Date"
            Case 3
                'tblName = "Billing_Info"
                'branchKey = "AND Branch_Code = '" & setShipCode & "' "
                'targetDate = "Billing_Date"
        End Select

        Try

            '日付キー、削除内容の設定
            Dim delData As String
            Dim targetDateKey As String
            If setMon <> "00" Then
                '月指定
                targetDateKey = "AND LEFT(CONVERT(VARCHAR, " & targetDate & ", 111), 7) = '" & Left(dtNow.ToShortDateString, 5) & setMon & "' "
                delData = targetDelData & "_mon: " & setMon
            Else
                '日付指定
                If dateTo <> "" Then
                    If dateFrom <> "" Then
                        targetDateKey &= "AND LEFT(CONVERT(VARCHAR, " & targetDate & ", 111), 10) <= '" & dateTo & "' "
                        targetDateKey &= "AND LEFT(CONVERT(VARCHAR, " & targetDate & ", 111), 10) >= '" & dateFrom & "' "
                        delData = targetDelData & "_day: " & dateFrom & " ～ " & dateTo
                    End If
                End If
            End If

            'SQL設定
            Dim select_sql As String = ""
            select_sql &= "SELECT * "
            select_sql &= "FROM dbo." & tblName & " WHERE DELFG = 0 "
            select_sql &= branchKey
            select_sql &= targetDateKey

            Dim sqlSelect As New SqlCommand(select_sql, con, trn)
            Dim Adapter As New SqlDataAdapter(sqlSelect)
            Dim Builder As New SqlCommandBuilder(Adapter)
            Dim ds As New DataSet

            Adapter.Fill(ds)

            If ds.Tables(0).Rows.Count <> 0 Then

                For i = 0 To ds.Tables(0).Rows.Count - 1

                    Dim dr As DataRow = ds.Tables(0).Rows(0)

                    dr.Delete()

                    '更新
                    Adapter.Update(ds)

                Next i

                If tblNo = 1 Then

                    '二つ目のテーブルデータ内を削除
                    Dim select_sql2 As String = ""
                    select_sql2 &= "SELECT * "
                    select_sql2 &= "FROM dbo.SC_DSR WHERE DELFG = 0 "
                    select_sql2 &= branchKey
                    select_sql2 &= targetDateKey

                    Dim sqlSelect2 As New SqlCommand(select_sql2, con, trn)
                    Dim Adapter2 As New SqlDataAdapter(sqlSelect2)
                    Dim Builder2 As New SqlCommandBuilder(Adapter2)
                    Dim ds2 As New DataSet

                    Adapter2.Fill(ds2)

                    If ds2.Tables(0).Rows.Count <> 0 Then

                        For i = 0 To ds2.Tables(0).Rows.Count - 1

                            Dim dr2 As DataRow = ds2.Tables(0).Rows(0)

                            dr2.Delete()

                            '更新
                            Adapter2.Update(ds2)

                        Next i

                    End If

                End If

                '■INSERT　cash_trackの空テーブル取得
                Dim select_sql3 As String = ""
                select_sql3 = "SELECT * FROM dbo.delete_log WHERE del_user IS NULL;"

                Dim sqlSelect3 As New SqlCommand(select_sql3, con, trn)
                Dim Adapter3 As New SqlDataAdapter(sqlSelect3)
                Dim Builder3 As New SqlCommandBuilder(Adapter3)
                Dim ds3 As New DataSet
                Adapter3.Fill(ds3)

                '新規DR取得
                Dim dr3 As DataRow = ds3.Tables(0).NewRow

                dr3("CRTDT") = dtNow
                dr3("CRTCD") = userid
                'dr3("UPDDT") = dtNow
                'dr3("UPDCD") = userid
                dr3("UPDPG") = "Class_analysis.vb"
                dr3("DELFG") = 0
                dr3("del_user") = userName
                dr3("del_datetime") = dtNow
                dr3("del_location") = targetShipname
                dr3("del_data") = delData

                '新規DRをDataTableに追加
                ds3.Tables(0).Rows.Add(dr3)
                Adapter3.Update(ds3)

                '■コミット
                trn.Commit()

                tourokuFlg = 1

            End If

        Catch ex As Exception
            errFlg = 1
            trn.Rollback()
        Finally
            'DB接続クローズ
            If con.State <> ConnectionState.Closed Then
                con.Close()
            End If
        End Try

    End Sub

    '****************************************************
    '処理名　：set_deleteLog
    '処理概要：Listに表示する削除済履歴データを構造体にセット
    '引数　　：delLogData  戻り値  delete_logテーブルの情報をセット
    '          location
    '　　　　　errFlg      戻り値　0:正常　1:異常
    '****************************************************
    Public Sub set_deleteLog(ByRef delLogData() As DELETE_LOG, ByVal location As String, ByRef errFlg As Integer)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try

            'Listに表示する削除済データを取得
            Dim select_sql As String = ""
            select_sql &= "SELECT top 50 * "
            select_sql &= "FROM dbo.delete_log A "
            select_sql &= "WHERE DELFG = 0 "
            If location <> "" Then
                select_sql &= "AND del_location = '" & location & "' "
            End If
            select_sql &= "ORDER BY del_location, del_datetime DESC;"

            Dim sqlSelect As New SqlCommand(select_sql, con, trn)
            Dim Adapter As New SqlDataAdapter(sqlSelect)
            Dim ds As New DataSet

            Adapter.Fill(ds)

            If ds.Tables(0).Rows.Count <> 0 Then

                ReDim delLogData(ds.Tables(0).Rows.Count - 1)

                For i = 0 To ds.Tables(0).Rows.Count - 1

                    Dim dr As DataRow = ds.Tables(0).Rows(i)

                    If dr("del_user") IsNot DBNull.Value Then
                        delLogData(i).del_user = dr("del_user")
                    End If

                    If dr("del_datetime") IsNot DBNull.Value Then
                        Dim workDt As DateTime
                        workDt = dr("del_datetime")
                        delLogData(i).del_datetime = workDt.ToString
                    End If

                    If dr("del_location") IsNot DBNull.Value Then
                        delLogData(i).del_location = dr("del_location")
                    End If

                    If dr("del_data") IsNot DBNull.Value Then
                        delLogData(i).del_data = dr("del_data")
                    End If

                Next i

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

    '****************************************************
    '処理：Custodyデータの登録
    '引数概要： 画面からお客様の製品と金銭のお預かりの入力情報を登録
    '引数  Custody     画面からの入力情報　
    '　　　userid
    '　　　userName
    '　　　errFlg      戻り値　0:正常　1:異常
    '      shipCode    拠点コード
    '****************************************************
    Public Sub setCustody(ByVal custodyData As CUSTODY, ByVal userid As String, ByVal userName As String, ByRef errFlg As Integer, ByVal shipCode As String)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try
            Dim clsCommon As New Class_common
            Dim dtNow As DateTime = clsCommon.dtIndia

            '■INSERT Custodyの空テーブル取得
            Dim select_sql1 As String = ""
            select_sql1 = "SELECT * FROM dbo.custody WHERE keep_no IS NULL;"

            Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
            Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
            Dim Builder1 As New SqlCommandBuilder(Adapter1)
            Dim ds1 As New DataSet
            Adapter1.Fill(ds1)

            '新規DR取得
            Dim dr1 As DataRow = ds1.Tables(0).NewRow

            dr1("CRTDT") = dtNow
            dr1("CRTCD") = userid
            'dr1("UPDDT") = dtNow
            'dr1("UPDCD") = userid
            dr1("UPDPG") = "Class_analysis.vb"
            dr1("DELFG") = 0
            dr1("keep_no") = custodyData.keep_no
            dr1("customer_name") = custodyData.customer_name
            dr1("customer_tel") = custodyData.customer_tel
            dr1("samsung_claim_no") = custodyData.samsung_claim_no
            dr1("product_name") = custodyData.product_name
            dr1("cash") = custodyData.cash
            dr1("ship_code") = shipCode
            dr1("takeout") = 1

            '新規DRをDataTableに追加
            ds1.Tables(0).Rows.Add(dr1)
            Adapter1.Update(ds1)

            '■コミット
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

End Class
