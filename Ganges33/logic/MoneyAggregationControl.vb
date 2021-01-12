Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class MoneyAggregationControl
    Public Function SelectMoneyAggregationCheck(ByVal queryParams As MoneyAggregationModel) As String
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "COUNT(*) AS AggCount "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "dbo.cash_track "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        sqlStr = sqlStr & "AND FALSE=0 "

        If Not String.IsNullOrEmpty(queryParams.InvoiceDate) Then
            sqlStr = sqlStr & "AND (CONVERT(VARCHAR,invoice_date, 111)) = @InvoiceDate "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@InvoiceDate", queryParams.InvoiceDate))
        End If
        If Not String.IsNullOrEmpty(queryParams.Location) Then
            sqlStr = sqlStr & "AND location = @Location "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@location", queryParams.Location))
        End If
        If Not String.IsNullOrEmpty(queryParams.PaymentKind) Then
            'sqlStr = sqlStr & "AND ((payment_kind = @PaymentKind) or (payment_kind is Null)) "
            sqlStr = sqlStr & "AND (payment_kind = @PaymentKind) "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PaymentKind", queryParams.PaymentKind))
        End If
        ''If Not String.IsNullOrEmpty(queryParams.PaymentKind) Then
        ''    sqlStr = sqlStr & "AND ((payment_kind = @PaymentKind) or (payment_kind is Null)) "
        ''    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PaymentKind", queryParams.PaymentKind))
        ''End If
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return dt.Rows(0)("AggCount").ToString()
    End Function

    Public Function SelectMoneyAggregation(ByVal queryParams As MoneyAggregationModel) As List(Of MoneyAggregationModel)
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "payment_kind, "
        sqlStr = sqlStr & "Warranty, "
        sqlStr = sqlStr & "payment, "
        sqlStr = sqlStr & "total_amount, "
        sqlStr = sqlStr & "claim, "
        sqlStr = sqlStr & "change, "
        sqlStr = sqlStr & "deposit, "
        sqlStr = sqlStr & "claim_card, "
        sqlStr = sqlStr & "discount, "
        sqlStr = sqlStr & "full_discount, "
        sqlStr = sqlStr & "incomplete, "
        sqlStr = sqlStr & "corp_flg, "
        sqlStr = sqlStr & "corp_collect, "
        sqlStr = sqlStr & "invoice_date "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "dbo.cash_track "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        sqlStr = sqlStr & "AND FALSE=0 "

        If Not String.IsNullOrEmpty(queryParams.InvoiceDate) Then
            sqlStr = sqlStr & "AND (CONVERT(VARCHAR,invoice_date, 111)) = @InvoiceDate "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@InvoiceDate", queryParams.InvoiceDate))
        End If
        If Not String.IsNullOrEmpty(queryParams.Location) Then
            sqlStr = sqlStr & "AND location = @Location "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@location", queryParams.Location))
        End If
        If Not String.IsNullOrEmpty(queryParams.PaymentKind) Then
            'sqlStr = sqlStr & "AND ((payment_kind = @PaymentKind) or (payment_kind is Null)) "
            sqlStr = sqlStr & "AND (payment_kind = @PaymentKind) "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PaymentKind", queryParams.PaymentKind))
        End If
        Dim dt As DataTable = New DataTable()
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        'Return dt.Rows(0)("AggCount").ToString()
        Dim MoneyAggregationModel_ As List(Of MoneyAggregationModel) = New List(Of MoneyAggregationModel)()
        Dim _MoneyAggregationModel As MoneyAggregationModel = New MoneyAggregationModel()

        Dim PaymentKind As String = ""
        Dim Discount As Decimal = 0.00
        Dim Claim As Decimal = 0.00
        Dim ClaimCardAmt As Decimal = 0.00

        If dt Is Nothing Then Return MoneyAggregationModel_

        For Each row As DataRow In dt.Rows

            'For PaymentKind
            'Default 0 - From Db Null
            '1 Cash
            '2 Card
            PaymentKind = "0"
            Discount = 0.00
            Claim = 0.00
            ClaimCardAmt = 0.00

            If Not String.IsNullOrEmpty(row("payment_kind").ToString().Trim()) Then
                PaymentKind = row("payment_kind")
            End If



            'Discount Amount
            If Not String.IsNullOrEmpty(row("discount").ToString().Trim()) Then
                If PaymentKind <> "2" Then 'when split cash and card, no discount in card
                    If (row("discount") > 0) Then
                        _MoneyAggregationModel.DiscountAmt = _MoneyAggregationModel.DiscountAmt + row("discount")
                        _MoneyAggregationModel.DiscountCnt = _MoneyAggregationModel.DiscountCnt + 1
                        Discount = row("discount")
                    End If
                End If
            End If
                'Full Discount
                If Not String.IsNullOrEmpty(row("full_discount").ToString().Trim()) Then
                If (row("full_discount") > 0) Then
                    _MoneyAggregationModel.FullDiscountAmt = _MoneyAggregationModel.FullDiscountAmt + row("full_discount")
                    _MoneyAggregationModel.FullDiscountCnt = _MoneyAggregationModel.FullDiscountCnt + 1
                End If
            End If
            'Warranty Type (IW/OOW/OTHER) / Payment (Cash/Card) 
            If Not String.IsNullOrEmpty(row("Warranty").ToString().Trim()) Then
                Select Case row("Warranty").ToString().Trim().ToUpper()
                    Case "IW"
                        If Not String.IsNullOrEmpty(row("total_amount").ToString().Trim()) Then
                            _MoneyAggregationModel.InWardAmt = _MoneyAggregationModel.InWardAmt + row("total_amount")
                        End If
                        If Not String.IsNullOrEmpty(row("claim").ToString().Trim()) Then
                            _MoneyAggregationModel.InWardClaimAmt = _MoneyAggregationModel.InWardClaimAmt + row("claim")
                        End If
                        _MoneyAggregationModel.InWardCnt = _MoneyAggregationModel.InWardCnt + 1
                    Case "OOW"
                        If Not String.IsNullOrEmpty(row("payment").ToString().Trim()) Then
                            Select Case row("payment").ToString().Trim().ToUpper()
                                Case "CASH"
                                    '***********************************************
                                    '***********************************************
                                    '***********************************************
                                    '***********************************************
                                    '***********************************************
                                    '***********************************************
                                    '***********************************************
                                    'Need to check incomplete == 1
                                    If Not String.IsNullOrEmpty(row("incomplete").ToString().Trim()) Then
                                        If (row("incomplete") = 1) Then  'Corporate Sales


                                            'Other than Corporase Sales
                                        Else
                                            If Not String.IsNullOrEmpty(row("discount").ToString().Trim()) Then
                                                If PaymentKind <> "2" Then 'when split cash and card, no discount in card
                                                    If (row("discount") > 0) Then
                                                        _MoneyAggregationModel.DiscountAmtCash = _MoneyAggregationModel.DiscountAmtCash + row("discount")
                                                    End If
                                                End If
                                            End If

                                            If Not String.IsNullOrEmpty(row("claim").ToString().Trim()) Then
                                                If (row("claim") > 0) Then
                                                    _MoneyAggregationModel.OutWardCashClaimAmt = _MoneyAggregationModel.OutWardCashClaimAmt + row("claim")
                                                    Claim = row("claim")
                                                End If
                                            End If


                                            If Not String.IsNullOrEmpty(row("total_amount").ToString().Trim()) Then
                                                If PaymentKind = "1" Then
                                                    _MoneyAggregationModel.OutWardCashAmt = _MoneyAggregationModel.OutWardCashAmt + (Claim + Discount)
                                                    _MoneyAggregationModel.OutWardCashCnt = _MoneyAggregationModel.OutWardCashCnt + 1

                                                Else
                                                    If (row("total_amount") > 0) Then
                                                        _MoneyAggregationModel.OutWardCashAmt = _MoneyAggregationModel.OutWardCashAmt + row("total_amount")
                                                        _MoneyAggregationModel.OutWardCashCnt = _MoneyAggregationModel.OutWardCashCnt + 1
                                                    End If
                                                End If
                                            End If

                                        End If
                                    End If






                                Case "CREDIT"


                                    If Not String.IsNullOrEmpty(row("claim_card").ToString().Trim()) Then
                                        If (row("claim_card") > 0) Then
                                            _MoneyAggregationModel.OutWardCardClaimAmt = _MoneyAggregationModel.OutWardCardClaimAmt + row("claim_card")
                                            ClaimCardAmt = row("claim_card")
                                        End If
                                    End If

                                    If PaymentKind = "2" Then
                                        _MoneyAggregationModel.OutWardCardAmt = _MoneyAggregationModel.OutWardCardAmt + ClaimCardAmt
                                        _MoneyAggregationModel.OutWardCardCnt = _MoneyAggregationModel.OutWardCardCnt + 1
                                    Else
                                        If Not String.IsNullOrEmpty(row("total_amount").ToString().Trim()) Then
                                            If (row("total_amount") > 0) Then
                                                _MoneyAggregationModel.OutWardCardAmt = _MoneyAggregationModel.OutWardCardAmt + row("total_amount")
                                                _MoneyAggregationModel.OutWardCardCnt = _MoneyAggregationModel.OutWardCardCnt + 1
                                            End If
                                        End If
                                    End If
                                    If PaymentKind <> "2" Then 'when split cash and card, no discount in card
                                        If Not String.IsNullOrEmpty(row("discount").ToString().Trim()) Then
                                            If (row("discount") > 0) Then
                                                _MoneyAggregationModel.DiscountAmtCard = _MoneyAggregationModel.DiscountAmtCard + row("discount")
                                            End If
                                        End If
                                    End If

                            End Select
                        End If
                    Case "OTHER"
                        If Not String.IsNullOrEmpty(row("payment").ToString().Trim()) Then
                            Select Case row("payment").ToString().Trim().ToUpper()
                                Case "CASH"
                                    'If Not String.IsNullOrEmpty(row("discount").ToString().Trim()) Then
                                    '    If (row("discount") > 0) Then
                                    '        _MoneyAggregationModel.DiscountAmtCash = _MoneyAggregationModel.DiscountAmtCash + row("discount")
                                    '        Discount = row("discount")
                                    '    End If
                                    'End If

                                    If Not String.IsNullOrEmpty(row("claim").ToString().Trim()) Then
                                        If (row("claim") > 0) Then
                                            _MoneyAggregationModel.OtherCashClaimAmt = _MoneyAggregationModel.OtherCashClaimAmt + row("claim")
                                            Claim = row("claim")
                                        End If
                                    End If

                                    If Not String.IsNullOrEmpty(row("total_amount").ToString().Trim()) Then
                                        If PaymentKind = "0" Then 'Null
                                            _MoneyAggregationModel.OtherCashAmt = _MoneyAggregationModel.OtherCashAmt + row("total_amount")
                                            _MoneyAggregationModel.OtherCashCnt = _MoneyAggregationModel.OtherCashCnt + 1
                                        ElseIf PaymentKind = "1" Then 'Cash  - Card Not possible
                                            _MoneyAggregationModel.OtherCashAmt = _MoneyAggregationModel.OtherCashAmt + (Claim)
                                            _MoneyAggregationModel.OtherCashCnt = _MoneyAggregationModel.OtherCashCnt + 1
                                        End If
                                    End If

                                Case "CREDIT"
                                    If Not String.IsNullOrEmpty(row("claim_card").ToString().Trim()) Then
                                        If (row("claim_card") > 0) Then
                                            _MoneyAggregationModel.OtherCardClaimAmt = _MoneyAggregationModel.OtherCardClaimAmt + row("claim_card")
                                            _MoneyAggregationModel.OtherCardAmt = _MoneyAggregationModel.OtherCardAmt + row("claim_card")
                                            ClaimCardAmt = row("claim_card")
                                            _MoneyAggregationModel.OtherCardCnt = _MoneyAggregationModel.OtherCardCnt + 1
                                        End If
                                    End If

                                    ''If Not String.IsNullOrEmpty(row("total_amount").ToString().Trim()) Then
                                    ''    If (row("total_amount") > 0) Then

                                    ''        If Not String.IsNullOrEmpty(row("total_amount").ToString().Trim()) Then
                                    ''            _MoneyAggregationModel.OtherCardAmt = _MoneyAggregationModel.OtherCardAmt + row("total_amount")
                                    ''        End If
                                    ''        If Not String.IsNullOrEmpty(row("claim").ToString().Trim()) Then
                                    ''            _MoneyAggregationModel.OtherCardClaimAmt = _MoneyAggregationModel.OtherCardClaimAmt + row("claim")
                                    ''        End If

                                    ''    End If

                                    ''End If

                                    '''''If Not String.IsNullOrEmpty(row("discount").ToString().Trim()) Then
                                    '''''    If (row("discount") > 0) Then
                                    '''''        _MoneyAggregationModel.DiscountAmtCard = _MoneyAggregationModel.DiscountAmtCard + row("discount")
                                    '''''    End If
                                    '''''End If

                            End Select
                        End If
                    Case "DEPOSIT"
                        If Not String.IsNullOrEmpty(row("payment").ToString().Trim()) Then
                            Select Case row("payment").ToString().Trim().ToUpper()
                                Case "CASH"
                                    If Not String.IsNullOrEmpty(row("claim").ToString().Trim()) Then
                                        If (row("claim") > 0) Then
                                            _MoneyAggregationModel.BankDeposit = _MoneyAggregationModel.BankDeposit + row("claim")
                                        End If
                                    End If
                            End Select
                            'Deposited to bank which collected money from the customer + opening cash

                        End If

                End Select

                'Need to check incomplete == 1
                If Not String.IsNullOrEmpty(row("incomplete").ToString().Trim()) Then
                    If (row("incomplete") = 1) Then  'Corporate Sales


                        'Other than Corporase Sales
                    Else
                        'Total Amount
                        If Not String.IsNullOrEmpty(row("total_amount").ToString().Trim()) Then
                            If PaymentKind = "0" Then
                                _MoneyAggregationModel.Sales = _MoneyAggregationModel.Sales + row("total_amount")
                            ElseIf PaymentKind = "1" Then
                                _MoneyAggregationModel.Sales = _MoneyAggregationModel.Sales + row("total_amount")
                            ElseIf PaymentKind = "2" Then
                                '' _MoneyAggregationModel.Sales = _MoneyAggregationModel.Sales + row("total_amount")
                            End If
                        End If
                    End If
                End If









            End If

        Next

        'Dim sum = Convert.ToInt32(dt.Compute("SUM(total_amount)", "payment='oow'"))

        MoneyAggregationModel_.Add(_MoneyAggregationModel)

        Return MoneyAggregationModel_

    End Function

    Public Function SelectOpeningBalance(ByVal queryParams As MoneyAggregationModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "TOP 1 M_2000,M_1000,M_500,M_200,M_100,M_50,M_20,M_10,Coin_10,Coin_5,Coin_2,Coin_1,total,datetime "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "T_Reserve "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        sqlStr = sqlStr & "AND STATUS='open' "
        If Not String.IsNullOrEmpty(queryParams.Location) Then
            sqlStr = sqlStr & "AND ship_code = @Location "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@location", queryParams.Location))
        End If
        If Not String.IsNullOrEmpty(queryParams.InvoiceDate) Then
            sqlStr = sqlStr & "AND (CONVERT(VARCHAR,datetime, 111)) = @SearchDate "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SearchDate", queryParams.SearchDate))
        End If
        sqlStr = sqlStr & "ORDER BY datetime DESC "
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return dt
    End Function

    Public Function SelectCustomerAdvance(ByVal queryParams As MoneyAggregationModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "SUM(cash) AS AdvanceAmt, "
        sqlStr = sqlStr & "COUNT(cash) AS COUNT "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "custody "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        sqlStr = sqlStr & "AND takeout = 1 "
        If Not String.IsNullOrEmpty(queryParams.Location) Then
            sqlStr = sqlStr & "AND ship_code = @Location "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@location", queryParams.Location))
        End If
        If Not String.IsNullOrEmpty(queryParams.InvoiceDate) Then
            sqlStr = sqlStr & "AND (CONVERT(VARCHAR,CRTDT, 111)) = @SearchDate "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SearchDate", queryParams.SearchDate))
        End If

        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return dt
    End Function

    Public Function SelectClosingCheck(ByVal queryParams As MoneyAggregationModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "TOP 1 datetime, "
        sqlStr = sqlStr & "status "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "T_Reserve "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        sqlStr = sqlStr & "AND STATUS='open' "
        If Not String.IsNullOrEmpty(queryParams.Location) Then
            sqlStr = sqlStr & "AND ship_code = @Location "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@location", queryParams.Location))
        End If
        If Not String.IsNullOrEmpty(queryParams.InvoiceDate) Then
            sqlStr = sqlStr & "AND (CONVERT(VARCHAR,datetime, 111)) = @SearchDate "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SearchDate", queryParams.SearchDate))
        End If
        sqlStr = sqlStr & "ORDER BY datetime DESC "
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return dt
    End Function

    Public Function SelectMoneyAggregationAll(ByVal queryParams As MoneyAggregationModel) As List(Of MoneyAggregationModel)
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "location, "
        sqlStr = sqlStr & "Warranty, "
        sqlStr = sqlStr & "payment, "
        sqlStr = sqlStr & "total_amount, "
        sqlStr = sqlStr & "claim, "
        sqlStr = sqlStr & "change, "
        sqlStr = sqlStr & "deposit, "
        sqlStr = sqlStr & "claim_card, "
        sqlStr = sqlStr & "discount, "
        sqlStr = sqlStr & "full_discount, "
        sqlStr = sqlStr & "incomplete, "
        sqlStr = sqlStr & "corp_flg, "
        sqlStr = sqlStr & "corp_collect, "
        sqlStr = sqlStr & "invoice_date "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "dbo.cash_track "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        sqlStr = sqlStr & "AND FALSE=0 "

        If Not String.IsNullOrEmpty(queryParams.InvoiceDate) Then
            sqlStr = sqlStr & "AND (CONVERT(VARCHAR,invoice_date, 111)) = @InvoiceDate "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@InvoiceDate", queryParams.InvoiceDate))
        End If
        If Not String.IsNullOrEmpty(queryParams.Location) Then
            sqlStr = sqlStr & "AND location = @Location "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@location", queryParams.Location))
        End If
        If Not String.IsNullOrEmpty(queryParams.PaymentKind) Then
            'sqlStr = sqlStr & "AND ((payment_kind = @PaymentKind) or (payment_kind is Null)) "
            sqlStr = sqlStr & "AND (payment_kind = @PaymentKind) "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PaymentKind", queryParams.PaymentKind))
        End If
        Dim dt As DataTable = New DataTable()
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        'Return dt.Rows(0)("AggCount").ToString()
        Dim MoneyAggregationModel_ As List(Of MoneyAggregationModel) = New List(Of MoneyAggregationModel)()
        Dim _MoneyAggregationModel As MoneyAggregationModel = New MoneyAggregationModel()
        If dt Is Nothing Then Return MoneyAggregationModel_

        For Each row As DataRow In dt.Rows
            'Total Amount
            If Not String.IsNullOrEmpty(row("total_amount").ToString().Trim()) Then
                _MoneyAggregationModel.Sales = _MoneyAggregationModel.Sales + row("total_amount")
            End If
            'Discount Amount
            If Not String.IsNullOrEmpty(row("discount").ToString().Trim()) Then
                If (row("discount") > 0) Then
                    _MoneyAggregationModel.DiscountAmt = _MoneyAggregationModel.DiscountAmt + row("discount")
                    _MoneyAggregationModel.DiscountCnt = _MoneyAggregationModel.DiscountCnt + 1
                End If
            End If
            'Full Discount
            If Not String.IsNullOrEmpty(row("full_discount").ToString().Trim()) Then
                If (row("full_discount") > 0) Then
                    _MoneyAggregationModel.FullDiscountAmt = _MoneyAggregationModel.FullDiscountAmt + row("full_discount")
                    _MoneyAggregationModel.FullDiscountCnt = _MoneyAggregationModel.FullDiscountCnt + 1
                End If
            End If
            'Warranty Type (IW/OOW/OTHER) / Payment (Cash/Card) 
            If Not String.IsNullOrEmpty(row("Warranty").ToString().Trim()) Then
                Select Case row("Warranty").ToString().Trim().ToUpper()
                    Case "IW"
                        _MoneyAggregationModel.InWardAmt = _MoneyAggregationModel.InWardAmt + row("total_amount")
                        _MoneyAggregationModel.InWardClaimAmt = _MoneyAggregationModel.InWardClaimAmt + row("claim")
                        _MoneyAggregationModel.InWardCnt = _MoneyAggregationModel.InWardCnt + 1
                    Case "OOW"
                        If Not String.IsNullOrEmpty(row("payment").ToString().Trim()) Then
                            Select Case row("payment").ToString().Trim().ToUpper()
                                Case "CASH"


                                    'Need to check incomplete == 1
                                    If Not String.IsNullOrEmpty(row("incomplete").ToString().Trim()) Then
                                        If (row("incomplete") = 1) Then  'Corporate Sales


                                            'Other than Corporase Sales
                                        Else
                                            If Not String.IsNullOrEmpty(row("total_amount").ToString().Trim()) Then
                                                If (row("total_amount") > 0) Then
                                                    _MoneyAggregationModel.OutWardCashAmt = _MoneyAggregationModel.OutWardCashAmt + row("total_amount")
                                                    _MoneyAggregationModel.OutWardCashCnt = _MoneyAggregationModel.OutWardCashCnt + 1
                                                End If
                                            End If
                                            If (row("claim") > 0) Then
                                                _MoneyAggregationModel.OutWardCashClaimAmt = _MoneyAggregationModel.OutWardCashClaimAmt + row("claim")
                                            End If
                                        End If
                                    End If
                                Case "CREDIT"
                                    If Not String.IsNullOrEmpty(row("total_amount").ToString().Trim()) Then
                                        If (row("total_amount") > 0) Then
                                            _MoneyAggregationModel.OutWardCardAmt = _MoneyAggregationModel.OutWardCardAmt + row("total_amount")
                                            _MoneyAggregationModel.OutWardCardCnt = _MoneyAggregationModel.OutWardCardCnt + 1
                                        End If
                                    End If
                                    If (row("claim_card") > 0) Then
                                        _MoneyAggregationModel.OutWardCardClaimAmt = _MoneyAggregationModel.OutWardCardClaimAmt + row("claim_card")
                                    End If

                            End Select
                        End If
                    Case "OTHER"
                        If Not String.IsNullOrEmpty(row("payment").ToString().Trim()) Then
                            Select Case row("payment").ToString().Trim().ToUpper()
                                Case "CASH"
                                    If Not String.IsNullOrEmpty(row("total_amount").ToString().Trim()) Then
                                        If (row("total_amount") > 0) Then
                                            _MoneyAggregationModel.OtherCashAmt = _MoneyAggregationModel.OtherCashAmt + row("total_amount")
                                            _MoneyAggregationModel.OtherCashCnt = _MoneyAggregationModel.OtherCashCnt + 1
                                        End If
                                    End If
                                    If (row("claim") > 0) Then
                                        _MoneyAggregationModel.OtherCashClaimAmt = _MoneyAggregationModel.OtherCashClaimAmt + row("claim")
                                    End If

                                Case "CREDIT"
                                    If Not String.IsNullOrEmpty(row("total_amount").ToString().Trim()) Then
                                        If (row("total_amount") > 0) Then
                                            _MoneyAggregationModel.OtherCardAmt = _MoneyAggregationModel.OtherCardAmt + row("total_amount")
                                            _MoneyAggregationModel.OtherCardClaimAmt = _MoneyAggregationModel.OtherCardClaimAmt + row("claim")
                                            _MoneyAggregationModel.OtherCardCnt = _MoneyAggregationModel.OtherCardCnt + 1
                                        End If
                                    End If
                                    If (row("claim_card") > 0) Then
                                        _MoneyAggregationModel.OtherCardClaimAmt = _MoneyAggregationModel.OtherCardClaimAmt + row("claim_card")
                                    End If

                            End Select
                        End If
                    Case "DEPOSIT"


                End Select
            End If
            'Actual Cash Collected from the customer without discount
            If Not String.IsNullOrEmpty(row("deposit").ToString().Trim()) Then
                If (row("deposit") > 0) Then
                    _MoneyAggregationModel.CashCollected = _MoneyAggregationModel.CashCollected + row("deposit")
                End If
            End If
        Next

        'Dim sum = Convert.ToInt32(dt.Compute("SUM(total_amount)", "payment='oow'"))

        MoneyAggregationModel_.Add(_MoneyAggregationModel)

        Return MoneyAggregationModel_

    End Function
End Class
