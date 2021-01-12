Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class ScDsrControl
    Dim _Expflddtl As Export_File_Details = New Export_File_Details()
    Dim _ShipBaseControl As ShipBaseControl = New ShipBaseControl()
    Public Function SelectScDsr(ByVal queryParams As ScDsrModel) As DataTable

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & " ServiceOrder_No as 'Service Order No',	LastUpdt_user as 'Last Updated User',	Billinb_user as 'Billing User',	LEFT(CONVERT(VARCHAR, Billing_date, 103), 10) as 'Billing Date',	GoodsDelivered_date as 'Goods Delivered Date',	Branch_name as 'Branch Name',	Engineer,		Product_1,	Product_2,	IW_Labor,	IW_Parts,	IW_Transport,	IW_Others,	IW_Tax,	IW_total,	OW_Labor,	OW_Parts,	OW_Transport,	OW_Others,	OW_Tax	OW_total  "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SC_DSR "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "

        If Not String.IsNullOrEmpty(queryParams.ShipToBranch) Then
            sqlStr = sqlStr & "AND Branch_name = @ShipToBranch "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipToBranch", queryParams.ShipToBranch))
        End If

        If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= @DateTo "
            'sqlStr = sqlStr & "AND INVOICE_DATE >= @DateFrom and INVOICE_DATE <= @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
            'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateFrom "
            sqlStr = sqlStr & "AND Billing_date = @DateFrom "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
            sqlStr = sqlStr & "AND Billing_date = @DateTo "
            'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))

            'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateFrom "
            sqlStr = sqlStr & "GROUP BY Branch_name; "

        End If


        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function

    'Public Function StoreManagement_drsCounts(ByVal queryParams As ScDsrModel) As DataTable

    '    Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
    '    Dim dbConn As DBUtility = New DBUtility()
    '    Dim dt As DataTable = New DataTable()
    '    Dim sqlStr As String = "SELECT "
    '    sqlStr = sqlStr & " Branch_name, SUM(IW_goods_total) "
    '    sqlStr = sqlStr & "as IW_goods_total, "
    '    sqlStr = sqlStr & "SUM (ow_goods_total) "
    '    sqlStr = sqlStr & "as OW_goods_total, "
    '    sqlStr = sqlStr & "SUM (iw_goods_total) "
    '    sqlStr = sqlStr & "+ "
    '    sqlStr = sqlStr & "SUM (ow_goods_total)"
    '    sqlStr = sqlStr & "as TotalGoods "
    '    sqlStr = sqlStr & "FROM "
    '    sqlStr = sqlStr & "SC_DSR_info "
    '    sqlStr = sqlStr & "WHERE "
    '    sqlStr = sqlStr & "Branch_name "
    '    sqlStr = sqlStr & "IN "


    '    If Not String.IsNullOrEmpty(queryParams.BranchName) Then
    '        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Branch_name ", queryParams.BranchName))
    '        sqlStr = sqlStr & "(" & queryParams.BranchName & ") "
    '    End If
    '    If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
    '        sqlStr = sqlStr & "AND LEFT (CONVERT(VARCHAR, Billing_date, 111), 10)>= @DateFrom and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= @DateTo GROUP BY Branch_name;"

    '        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
    '        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
    '    ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
    '        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
    '    ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
    '        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))

    '    End If
    '    Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
    '    dbConn.CloseConnection()
    '    Return _DataTable

    'End Function

    Public Function StoreManagement_drsCounts(ByVal queryParams As ScDsrModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim _DataTable As DataTable = New DataTable()
        Dim sqlStr As String
        Dim listquery As New List(Of String)
        Dim SplitBrances As String() = queryParams.BranchName.Split(New Char() {","c})
        For Each branch As String In SplitBrances

            Dim dbConn1 As DBUtility = New DBUtility()
            Dim branchname As String = branch.Replace("'", "")
            sqlStr = "SELECT "
            sqlStr = sqlStr & " IsShipCodeChanged from M_ship_base "
            If Not String.IsNullOrEmpty(branchname) Then
                sqlStr = sqlStr & " where ship_name= @ship_name"

                dbConn1.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ship_name", branchname))
            End If

            Dim _dtGetIsShipCodeChanged As DataTable = New DataTable()
            _dtGetIsShipCodeChanged = dbConn1.GetDataSet(sqlStr)
            dbConn1.sqlCmd.Parameters.Clear()
            dbConn1.CloseConnection()

            sqlStr = ""


            If _dtGetIsShipCodeChanged.Rows(0)("IsShipCodeChanged") = "0" Then
                Dim dbConn2 As DBUtility = New DBUtility()
                sqlStr = "select  Format( dateadd(day,-1, cast(datefrom as date)),'yyyy/MM/dd') as datefrom from M_ship_base_code_change_trn where ship_name_new= @ship_name_new "
                If Not String.IsNullOrEmpty(branchname) Then
                    dbConn2.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ship_name_new ", branchname))
                End If
                Dim _dtDateFrom As DataTable = New DataTable()
                _dtDateFrom = dbConn2.GetDataSet(sqlStr)
                dbConn2.sqlCmd.Parameters.Clear()
                dbConn2.CloseConnection()

                sqlStr = ""

                If (_dtDateFrom.Rows.Count > 0) Then
                    Dim GetPreviousDate As String = _dtDateFrom.Rows(0)("datefrom")
                    If queryParams.DateFrom <= GetPreviousDate And queryParams.DateTo <= GetPreviousDate Then
                        'no need to combine
                        sqlStr = sqlStr & "select  Branch_name, SUM(IW_goods_total) "
                        sqlStr = sqlStr & "as IW_goods_total, "
                        sqlStr = sqlStr & "SUM (ow_goods_total) "
                        sqlStr = sqlStr & "as OW_goods_total, "
                        sqlStr = sqlStr & "SUM (iw_goods_total) "
                        sqlStr = sqlStr & "+ "
                        sqlStr = sqlStr & "SUM (ow_goods_total)"
                        sqlStr = sqlStr & "as TotalGoods "
                        sqlStr = sqlStr & "FROM "
                        sqlStr = sqlStr & "SC_DSR_info "
                        sqlStr = sqlStr & "WHERE "
                        sqlStr = sqlStr & "Branch_name = @Branch_name "
                        Dim dbConn3 As DBUtility = New DBUtility()
                        If Not String.IsNullOrEmpty(branchname) Then
                            dbConn3.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Branch_name", branchname))
                        End If
                        If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
                            sqlStr = sqlStr & "AND LEFT (CONVERT(VARCHAR, Billing_date, 111), 10)>= @DateFrom and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= @DateTo GROUP BY Branch_name ;"
                            dbConn3.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                            dbConn3.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
                        ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
                            dbConn3.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                        ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
                            dbConn3.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
                        End If
                        Dim _dt1 As DataTable = New DataTable()
                        _dt1 = dbConn3.GetDataSet(sqlStr)
                        dbConn3.CloseConnection()
                        _DataTable.Merge(_dt1)

                    ElseIf queryParams.DateFrom > GetPreviousDate And queryParams.DateTo > GetPreviousDate Then
                        'bharathirajaa
                        'logic for skipping t
                        Dim GetPreviousMonth As Integer

                        GetPreviousMonth = Convert.ToDateTime(queryParams.DateFrom).Month




                    ElseIf queryParams.DateTo >= GetPreviousDate And queryParams.DateFrom <= GetPreviousDate Then
                        If GetPreviousDate = queryParams.DateTo Then
                            'not combined
                            sqlStr = sqlStr & "select  Branch_name, SUM(IW_goods_total) "
                            sqlStr = sqlStr & "as IW_goods_total, "
                            sqlStr = sqlStr & "SUM (ow_goods_total) "
                            sqlStr = sqlStr & "as OW_goods_total, "
                            sqlStr = sqlStr & "SUM (iw_goods_total) "
                            sqlStr = sqlStr & "+ "
                            sqlStr = sqlStr & "SUM (ow_goods_total)"
                            sqlStr = sqlStr & "as TotalGoods "
                            sqlStr = sqlStr & "FROM "
                            sqlStr = sqlStr & "SC_DSR_info "
                            sqlStr = sqlStr & "WHERE "
                            sqlStr = sqlStr & "Branch_name = @Branch_name "
                            Dim dbConn3 As DBUtility = New DBUtility()
                            If Not String.IsNullOrEmpty(branchname) Then
                                dbConn3.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Branch_name", branchname))
                            End If
                            If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
                                sqlStr = sqlStr & "AND LEFT (CONVERT(VARCHAR, Billing_date, 111), 10)>= @DateFrom and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= @DateTo GROUP BY Branch_name ;"
                                dbConn3.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                                dbConn3.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
                            ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
                                dbConn3.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                            ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
                                dbConn3.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
                            End If
                            Dim _dt1 As DataTable = New DataTable()
                            _dt1 = dbConn3.GetDataSet(sqlStr)
                            dbConn3.CloseConnection()
                            _DataTable.Merge(_dt1)
                            sqlStr = ""

                        Else
                            'from to middate

                            sqlStr = "SELECT "
                            sqlStr = sqlStr & " Branch_name, SUM(IW_goods_total) "
                            sqlStr = sqlStr & "as IW_goods_total, "
                            sqlStr = sqlStr & "SUM (ow_goods_total) "
                            sqlStr = sqlStr & "as OW_goods_total, "
                            sqlStr = sqlStr & "SUM (iw_goods_total) "
                            sqlStr = sqlStr & "+ "
                            sqlStr = sqlStr & "SUM (ow_goods_total)"
                            sqlStr = sqlStr & "as TotalGoods "
                            sqlStr = sqlStr & "FROM "
                            sqlStr = sqlStr & "SC_DSR_info "
                            sqlStr = sqlStr & "WHERE "
                            sqlStr = sqlStr & "Branch_name = @Branch_name  "

                            Dim dbConn4 As DBUtility = New DBUtility()
                            If Not String.IsNullOrEmpty(queryParams.BranchName) Then
                                dbConn4.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Branch_name ", branchname))
                            End If
                            If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(GetPreviousDate)))) Then
                                sqlStr = sqlStr & "AND LEFT (CONVERT(VARCHAR, Billing_date, 111), 10)>= @DateFrom and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= @DateTo GROUP BY Branch_name ;"
                                dbConn4.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                                dbConn4.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", GetPreviousDate))
                            ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
                                dbConn4.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                            ElseIf Not String.IsNullOrEmpty(GetPreviousDate) Then
                                dbConn4.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", GetPreviousDate))
                            End If

                            Dim _dt2 As DataTable = New DataTable()
                            _dt2 = dbConn4.GetDataSet(sqlStr)
                            dbConn4.CloseConnection()
                            _DataTable.Merge(_dt2)
                            sqlStr = ""

                        End If
                    End If

                Else 'as usual
                    sqlStr = "SELECT "
                    sqlStr = sqlStr & " Branch_name, SUM(IW_goods_total) "
                    sqlStr = sqlStr & "as IW_goods_total, "
                    sqlStr = sqlStr & "SUM (ow_goods_total) "
                    sqlStr = sqlStr & "as OW_goods_total, "
                    sqlStr = sqlStr & "SUM (iw_goods_total) "
                    sqlStr = sqlStr & "+ "
                    sqlStr = sqlStr & "SUM (ow_goods_total)"
                    sqlStr = sqlStr & "as TotalGoods "
                    sqlStr = sqlStr & "FROM "
                    sqlStr = sqlStr & "SC_DSR_info "
                    sqlStr = sqlStr & "WHERE "
                    sqlStr = sqlStr & "Branch_name = @Branch_name  "

                    Dim dbConn4 As DBUtility = New DBUtility()
                    If Not String.IsNullOrEmpty(queryParams.BranchName) Then
                        dbConn4.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Branch_name ", branchname))
                    End If
                    If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
                        sqlStr = sqlStr & "AND LEFT (CONVERT(VARCHAR, Billing_date, 111), 10)>= @DateFrom and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= @DateTo GROUP BY Branch_name ;"
                        dbConn4.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                        dbConn4.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
                    ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
                        dbConn4.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                    ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
                        dbConn4.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
                    End If

                    Dim _dt2 As DataTable = New DataTable()
                    _dt2 = dbConn4.GetDataSet(sqlStr)
                    dbConn4.CloseConnection()
                    _DataTable.Merge(_dt2)

                    sqlStr = ""

                End If

            Else
                sqlStr = ""

                Dim dbConn2 As DBUtility = New DBUtility()
                sqlStr = "select  ship_name,ship_code,ship_name_new,ship_code_new,Format( dateadd(day,-1, cast(datefrom as date)),'yyyy/MM/dd') as datefrom,Format(datefrom ,'yyyy/MM/dd') as EffectiveDate from M_ship_base_code_change_trn where ship_name= @ship_name "
                If Not String.IsNullOrEmpty(branchname) Then
                    dbConn2.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ship_name ", branchname))
                End If
                Dim _dtDateFrom As DataTable = New DataTable()
                _dtDateFrom = dbConn2.GetDataSet(sqlStr)
                dbConn2.sqlCmd.Parameters.Clear()
                dbConn2.CloseConnection()

                sqlStr = ""


                If (_dtDateFrom.Rows.Count > 0) Then
                    Dim GetPreviousDate As String = _dtDateFrom.Rows(0)("datefrom")
                    Dim EffectiveDate As String = _dtDateFrom.Rows(0)("EffectiveDate")
                    Dim ShipNameNew As String = _dtDateFrom.Rows(0)("ship_name_new")


                    If queryParams.DateFrom > GetPreviousDate And queryParams.DateTo > GetPreviousDate Then
                        Dim GetPreviousMonth As Integer
                        Dim ToDateMonth As Integer
                        GetPreviousMonth = Convert.ToDateTime(GetPreviousDate).Month
                        ToDateMonth = Convert.ToDateTime(queryParams.DateTo).Month
                        If ToDateMonth > GetPreviousMonth Then
                            'combined data
                            sqlStr = ""
                            sqlStr = "SELECT SUM(IW_goods_total) AS IW_goods_total ,SUM(OW_goods_total) AS OW_goods_total,SUM(TotalGoods) AS TotalGoods FROM ( "
                            sqlStr = sqlStr & "select Branch_name, SUM(IW_goods_total) "
                            sqlStr = sqlStr & "as IW_goods_total, "
                            sqlStr = sqlStr & "SUM (ow_goods_total) "
                            sqlStr = sqlStr & "as OW_goods_total, "
                            sqlStr = sqlStr & "SUM (iw_goods_total) "
                            sqlStr = sqlStr & "+ "
                            sqlStr = sqlStr & "SUM (ow_goods_total)"
                            sqlStr = sqlStr & "as TotalGoods "
                            sqlStr = sqlStr & "FROM "
                            sqlStr = sqlStr & "SC_DSR_info "
                            sqlStr = sqlStr & "WHERE "
                            sqlStr = sqlStr & "Branch_name = @Branch_name  "


                            Dim dbConn6 As DBUtility = New DBUtility()
                            If Not String.IsNullOrEmpty(ShipNameNew) Then
                                dbConn6.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Branch_name ", ShipNameNew))
                            End If
                            If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
                                sqlStr = sqlStr & "AND LEFT (CONVERT(VARCHAR, Billing_date, 111), 10)>= @DateFrom and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= @DateTo GROUP BY Branch_name  UNION "

                                dbConn6.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                                dbConn6.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
                            ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
                                dbConn6.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                            ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
                                dbConn6.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
                            End If

                            sqlStr = sqlStr & " SELECT "
                            sqlStr = sqlStr & " Branch_name, SUM(IW_goods_total) "
                            sqlStr = sqlStr & "as IW_goods_total, "
                            sqlStr = sqlStr & "SUM (ow_goods_total) "
                            sqlStr = sqlStr & "as OW_goods_total, "
                            sqlStr = sqlStr & "SUM (iw_goods_total) "
                            sqlStr = sqlStr & "+ "
                            sqlStr = sqlStr & "SUM (ow_goods_total)"
                            sqlStr = sqlStr & "as TotalGoods "
                            sqlStr = sqlStr & "FROM "
                            sqlStr = sqlStr & "SC_DSR_info "
                            sqlStr = sqlStr & "WHERE "
                            sqlStr = sqlStr & "Branch_name = @Branch_name1  "

                            If Not String.IsNullOrEmpty(branchname) Then
                                dbConn6.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Branch_name1 ", branchname))
                            End If
                            If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
                                sqlStr = sqlStr & "AND LEFT (CONVERT(VARCHAR, Billing_date, 111), 10)>= @DateFrom1 and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= @DateTo1 GROUP BY Branch_name )A "

                                dbConn6.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom1", queryParams.DateFrom))
                                dbConn6.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo1", queryParams.DateTo))
                            ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
                                dbConn6.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom1", queryParams.DateFrom))
                            ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
                                dbConn6.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo1", queryParams.DateTo))
                            End If

                            Dim _DataTable2 As DataTable = dbConn6.GetDataSet(sqlStr)
                            dbConn6.sqlCmd.Parameters.Clear()
                            dbConn6.CloseConnection()

                            Dim column As DataColumn = New DataColumn("Branch_name", GetType(String))
                            _DataTable2.Columns.Add(column)
                            column.SetOrdinal(0)
                            _DataTable2.Rows(0)(0) = branchname
                            _DataTable.Merge(_DataTable2)
                        Else

                        End If




                    ElseIf queryParams.DateTo >= GetPreviousDate And queryParams.DateFrom <= GetPreviousDate Then
                        If GetPreviousDate = queryParams.DateTo Then
                            'not combined
                            sqlStr = "SELECT "
                            sqlStr = sqlStr & " Branch_name, SUM(IW_goods_total) "
                            sqlStr = sqlStr & "as IW_goods_total, "
                            sqlStr = sqlStr & "SUM (ow_goods_total) "
                            sqlStr = sqlStr & "as OW_goods_total, "
                            sqlStr = sqlStr & "SUM (iw_goods_total) "
                            sqlStr = sqlStr & "+ "
                            sqlStr = sqlStr & "SUM (ow_goods_total)"
                            sqlStr = sqlStr & "as TotalGoods "
                            sqlStr = sqlStr & "FROM "
                            sqlStr = sqlStr & "SC_DSR_info "
                            sqlStr = sqlStr & "WHERE "
                            sqlStr = sqlStr & "Branch_name = @Branch_name  "

                            Dim dbConn4 As DBUtility = New DBUtility()
                            If Not String.IsNullOrEmpty(queryParams.BranchName) Then
                                dbConn4.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Branch_name ", branchname))
                            End If
                            If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
                                sqlStr = sqlStr & "AND LEFT (CONVERT(VARCHAR, Billing_date, 111), 10)>= @DateFrom and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= @DateTo GROUP BY Branch_name ;"
                                dbConn4.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                                dbConn4.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
                            ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
                                dbConn4.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                            ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
                                dbConn4.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
                            End If

                            Dim _dt2 As DataTable = New DataTable()
                            _dt2 = dbConn4.GetDataSet(sqlStr)
                            dbConn4.CloseConnection()
                            _DataTable.Merge(_dt2)

                            sqlStr = ""
                        Else
                            'combined
                            sqlStr = ""
                            sqlStr = "SELECT SUM(IW_goods_total) AS IW_goods_total ,SUM(OW_goods_total) AS OW_goods_total,SUM(TotalGoods) AS TotalGoods FROM ( "
                            sqlStr = sqlStr & "select Branch_name, SUM(IW_goods_total) "
                            sqlStr = sqlStr & "as IW_goods_total, "
                            sqlStr = sqlStr & "SUM (ow_goods_total) "
                            sqlStr = sqlStr & "as OW_goods_total, "
                            sqlStr = sqlStr & "SUM (iw_goods_total) "
                            sqlStr = sqlStr & "+ "
                            sqlStr = sqlStr & "SUM (ow_goods_total)"
                            sqlStr = sqlStr & "as TotalGoods "
                            sqlStr = sqlStr & "FROM "
                            sqlStr = sqlStr & "SC_DSR_info "
                            sqlStr = sqlStr & "WHERE "
                            sqlStr = sqlStr & "Branch_name = @Branch_name  "


                            Dim dbConn6 As DBUtility = New DBUtility()
                            If Not String.IsNullOrEmpty(ShipNameNew) Then
                                dbConn6.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Branch_name ", ShipNameNew))
                            End If
                            If (Not (String.IsNullOrEmpty(EffectiveDate)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
                                sqlStr = sqlStr & "AND LEFT (CONVERT(VARCHAR, Billing_date, 111), 10)>= @DateFrom and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= @DateTo GROUP BY Branch_name  UNION "

                                dbConn6.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", EffectiveDate))
                                dbConn6.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
                            ElseIf Not String.IsNullOrEmpty(EffectiveDate) Then
                                dbConn6.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", EffectiveDate))
                            ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
                                dbConn6.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
                            End If

                            sqlStr = sqlStr & " SELECT "
                            sqlStr = sqlStr & " Branch_name, SUM(IW_goods_total) "
                            sqlStr = sqlStr & "as IW_goods_total, "
                            sqlStr = sqlStr & "SUM (ow_goods_total) "
                            sqlStr = sqlStr & "as OW_goods_total, "
                            sqlStr = sqlStr & "SUM (iw_goods_total) "
                            sqlStr = sqlStr & "+ "
                            sqlStr = sqlStr & "SUM (ow_goods_total)"
                            sqlStr = sqlStr & "as TotalGoods "
                            sqlStr = sqlStr & "FROM "
                            sqlStr = sqlStr & "SC_DSR_info "
                            sqlStr = sqlStr & "WHERE "
                            sqlStr = sqlStr & "Branch_name = @Branch_name1  "

                            If Not String.IsNullOrEmpty(branchname) Then
                                dbConn6.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Branch_name1 ", branchname))
                            End If
                            If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
                                sqlStr = sqlStr & "AND LEFT (CONVERT(VARCHAR, Billing_date, 111), 10)>= @DateFrom1 and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= @DateTo1 GROUP BY Branch_name )A "

                                dbConn6.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom1", queryParams.DateFrom))
                                dbConn6.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo1", queryParams.DateTo))
                            ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
                                dbConn6.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom1", queryParams.DateFrom))
                            ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
                                dbConn6.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo1", queryParams.DateTo))
                            End If

                            Dim _DataTable2 As DataTable = dbConn6.GetDataSet(sqlStr)
                            dbConn6.sqlCmd.Parameters.Clear()
                            dbConn6.CloseConnection()

                            Dim column As DataColumn = New DataColumn("Branch_name", GetType(String))
                            _DataTable2.Columns.Add(column)
                            column.SetOrdinal(0)
                            _DataTable2.Rows(0)(0) = branchname
                            _DataTable.Merge(_DataTable2)

                        End If
                    Else
                        'no need to combine
                        sqlStr = "SELECT "
                        sqlStr = sqlStr & " Branch_name, SUM(IW_goods_total) "
                        sqlStr = sqlStr & "as IW_goods_total, "
                        sqlStr = sqlStr & "SUM (ow_goods_total) "
                        sqlStr = sqlStr & "as OW_goods_total, "
                        sqlStr = sqlStr & "SUM (iw_goods_total) "
                        sqlStr = sqlStr & "+ "
                        sqlStr = sqlStr & "SUM (ow_goods_total)"
                        sqlStr = sqlStr & "as TotalGoods "
                        sqlStr = sqlStr & "FROM "
                        sqlStr = sqlStr & "SC_DSR_info "
                        sqlStr = sqlStr & "WHERE "
                        sqlStr = sqlStr & "Branch_name = @Branch_name  "

                        Dim dbConn4 As DBUtility = New DBUtility()
                        If Not String.IsNullOrEmpty(queryParams.BranchName) Then
                            dbConn4.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Branch_name ", branchname))
                        End If
                        If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
                            sqlStr = sqlStr & "AND LEFT (CONVERT(VARCHAR, Billing_date, 111), 10)>= @DateFrom and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= @DateTo GROUP BY Branch_name ;"
                            dbConn4.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                            dbConn4.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
                        ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
                            dbConn4.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                        ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
                            dbConn4.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
                        End If

                        Dim _dt2 As DataTable = New DataTable()
                        _dt2 = dbConn4.GetDataSet(sqlStr)
                        dbConn4.CloseConnection()
                        _DataTable.Merge(_dt2)

                        sqlStr = ""
                    End If
                End If

            End If

        Next
        If _DataTable.Rows.Count = 0 Then

            For Each branch As String In SplitBrances
                Dim R As DataRow = _DataTable.NewRow
                R("Branch_name") = branch.Replace("'", "")
                R("IW_goods_total") = 0
                R("OW_goods_total") = 0
                R("TotalGoods") = 0
                _DataTable.Rows.Add(R)
            Next
        End If
        Return _DataTable
    End Function
    Public Function StoreManagement_RevWoTax(ByVal queryParams As ScDsrModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim _DataTable As DataTable = New DataTable()
        _DataTable.Columns.Add("Branch_name")
        _DataTable.Columns.Add("In_Ward")
        _DataTable.Columns.Add("Out_Ward")
        _DataTable.Columns.Add("Labor")
        _DataTable.Columns.Add("Parts")
        _DataTable.Columns.Add("RevWoTax")

        Dim sqlStr As String
        Dim listquery As New List(Of String)
        Dim SplitBrances As String() = queryParams.BranchName.Split(New Char() {","c})
        Dim strSQL2 As String = ""
        Dim strSQL3 As String = ""
        Dim strSQL4 As String = ""
        Dim strShipCode As String = ""
        Dim dsActivity_report As New DataSet
        Dim dsActivityReportTmp As New DataSet
        Dim dsSC_DSR_info As New DataSet
        Dim comcontrol As New CommonControl
        Dim errMsg As String
        Dim errFlg As Integer
        Dim Labor As Decimal
        Dim Parts As Decimal
        Dim In_Ward As Decimal
        Dim Out_Ward As Decimal
        Dim tmpDay As DateTime

        For Each branch As String In SplitBrances

            Dim R As DataRow = _DataTable.NewRow
            branch = branch.Replace("'", "")
            R("Branch_name") = branch
            Labor = 0.00
            Parts = 0.00
            In_Ward = 0.00
            Out_Ward = 0.00

            Dim shipdtl As DataRow() = _ShipBaseControl.SelectShipBaseDetails().Select("ship_name = '" & branch & "'")
            Dim shipdtlParent As DataRow() = _ShipBaseControl.SelectShipBaseDetails().Select("ship_name = '" & branch & "' AND IsChildShip = 1 AND DELFG = 0")
            Dim shipdtlChild As DataRow() = _ShipBaseControl.SelectShipBaseDetails().Select("Parent_Ship_Name = '" & branch & "' AND IsChildShip = 1 AND DELFG = 0")

            Dim shipName As String = "'" & branch & "'"

            If (shipdtl.Count() > 0) Then
                For Each row As DataRow In shipdtl
                    strShipCode = row.Item("ship_code").ToString()
                    ' DataTable.ImportRow(row)
                Next
            End If

            'Dim shipname As String
            If (shipdtlParent.Count() > 0) Then
                For Each row As DataRow In shipdtlParent
                    shipName &= ",'" & row.Item("Parent_Ship_Name").ToString() & "'"
                    ' DataTable.ImportRow(row)
                Next
            End If
            If (shipdtlChild.Count() > 0) Then
                For Each row As DataRow In shipdtlChild
                    shipName &= ",'" & row.Item("ship_name").ToString() & "'"
                    'DataTable.ImportRow(row)
                Next

            End If


            'strSQL2 &= "SELECT Distinct (CONVERT(DATETIME, month + '/' + day)) as day FROM dbo.Activity_report WHERE DELFG =0 AND location = '" & strShipCode & "' "

            'If queryParams.DateTo <> "" Then
            '    If queryParams.DateFrom <> "" Then
            '        strSQL2 &= "AND (CONVERT(DATETIME, month + '/' + day)) <= CONVERT(DATETIME, '" & queryParams.DateTo & "') "
            '        strSQL2 &= "AND (CONVERT(DATETIME, month + '/' + day)) >= CONVERT(DATETIME, '" & queryParams.DateFrom & "') "
            '    Else
            '        strSQL2 &= "AND (CONVERT(DATETIME, month + '/' + day)) <= CONVERT(DATETIME, '" & queryParams.DateTo & "') "
            '    End If
            'Else
            '    If queryParams.DateFrom <> "" Then
            '        strSQL2 &= "AND (CONVERT(DATETIME, month + '/' + day)) >= CONVERT(DATETIME, '" & queryParams.DateFrom & "') "
            '    End If
            'End If

            'strSQL2 &= "ORDER BY day;"

            'dsActivity_report = DBCommon.Get_DS(strSQL2, errFlg)

            'Dim shipname As String
            'If (shipdtlParent.Count() > 0) Then
            '    For Each row As DataRow In shipdtlParent
            '        shipName &= ",'" & row.Item("Parent_Ship_Name").ToString() & "'"
            '        ' DataTable.ImportRow(row)
            '    Next
            'End If
            'If (shipdtlChild.Count() > 0) Then
            '    For Each row As DataRow In shipdtlChild
            '        shipName &= ",'" & row.Item("ship_name").ToString() & "'"
            '        'DataTable.ImportRow(row)
            '    Next

            'End If
            'Dim dr As DataRow
            'If dsActivity_report IsNot Nothing Then
            '    For i = 0 To dsActivity_report.Tables(0).Rows.Count - 1

            '        dr = dsActivity_report.Tables(0).Rows(i)
            '        If dr("day") IsNot DBNull.Value Then
            '            tmpDay = dr("day")
            '            tmpDay = tmpDay.ToShortDateString
            '            strSQL4 = "SELECT sum(Invoice_update.LABOR) as Labor,sum(Invoice_update.Parts) as Parts FROM  Invoice_update  "
            '            strSQL4 &= " where  "

            '            'VJ - 2020-04-16 SSC parent child Comment

            '            'If (DropListLocation.SelectedItem.Text = "SSC1") Or (DropListLocation.SelectedItem.Text = "SSC2") Then
            '            '    strSQL4 &= "  upload_Branch in ( 'SSC1','SSC2') and "
            '            'Else
            '            '    strSQL4 &= "  upload_Branch = '" & DropListLocation.SelectedItem.Text.Trim() & "' and "
            '            'End If
            '            strSQL4 &= "  upload_Branch in (" & shipName & ") and "
            '            strSQL4 &= "  Your_Ref_No in (  select  distinct ASC_Claim_No from Wty_Excel where Wty_Excel.DELFG!=1  AND  Wty_Excel.Branch_Code='" & strShipCode & "') and "
            '            strSQL4 &= "  Your_Ref_No in (  select  distinct ServiceOrder_No from SC_DSR where SC_DSR.DELFG!=1  AND  ( (DAY('" & tmpDay & "') = 1 and SC_DSR.Billing_date between  LEFT(CONVERT(VARCHAR, DATEADD(D,-3,'" & tmpDay & "'), 111), 10) and LEFT(CONVERT(VARCHAR, '" & tmpDay & "', 111), 10) ) or (DAY('" & tmpDay & "') != 1 and SC_DSR.Billing_date between  LEFT(CONVERT(VARCHAR, '" & tmpDay & "', 111), 10) and LEFT(CONVERT(VARCHAR, '" & tmpDay & "', 111), 10) )))"



            '            dsActivityReportTmp = DBCommon.Get_DS(strSQL4, errFlg)
            '            If errFlg <> 1 Then 'If other than Error
            '                If dsActivityReportTmp IsNot Nothing Then
            '                    If dsActivityReportTmp.Tables(0).Rows.Count <> 0 Then
            '                        Dim dr1 As DataRow
            '                        For k = 0 To dsActivityReportTmp.Tables(0).Rows.Count - 1
            '                            dr1 = dsActivityReportTmp.Tables(0).Rows(k)

            '                            If dr1("Labor") IsNot DBNull.Value Then
            '                                Labor += comcontrol.Money_Round(dr1("Labor"), 2)
            '                            End If
            '                            If dr1("Parts") IsNot DBNull.Value Then
            '                                Parts += comcontrol.Money_Round(dr1("Parts"), 2)
            '                            End If
            '                        Next
            '                    End If
            '                End If
            '            Else ' If Error is occured, then default assign zero
            '            End If

            '        End If

            '    Next
            'End If
            'VJ 20201005 Added condition in Invoice update table for Labor and Parts
            strSQL4 = "SELECT "
            strSQL4 &= "sum(Invoice_update.LABOR) As Labor,sum(Invoice_update.Parts) As Parts "
            strSQL4 &= " From Invoice_update "
            strSQL4 &= " Where upload_Branch in (" & shipName & ") AND Number = 'OWC' "
            strSQL4 &= " And Your_Ref_No in "
            strSQL4 &= " ( "
            strSQL4 &= " Select distinct ASC_Claim_No from Wty_Excel where Wty_Excel.DELFG!=1 "
            strSQL4 &= " And  Wty_Excel.Branch_Code='" & strShipCode & "') "
            strSQL4 &= " And   Your_Ref_No in ( "
            strSQL4 &= " Select distinct ServiceOrder_No "
            strSQL4 &= " From SC_DSR DSR "
            strSQL4 &= " Join DBO.M_ship_base SB ON DSR.Branch_name = SB.ship_name  "
            strSQL4 &= " Join Activity_report AR ON AR.location = SB.ship_code "
            strSQL4 &= " Where DSR.DELFG! = 1 And AR.DELFG! = 1 "
            strSQL4 &= " And (CONVERT(DATETIME, AR.month + '/' + AR.day)) = DSR.Billing_date "
            strSQL4 &= " And (CONVERT(DATETIME, AR.month + '/' + AR.day)) <= CONVERT(DATETIME, '" & queryParams.DateTo & "') AND (CONVERT(DATETIME, AR.month + '/' + AR.day)) >= CONVERT(DATETIME, '" & queryParams.DateFrom & "') "
            strSQL4 &= "And  ( "
            strSQL4 &= " (DAY(DSR.Billing_date) = 1 "
            strSQL4 &= " And DSR.Billing_date between  "
            'LEFT(CONVERT(VARCHAR, DATEADD(D,-3,DSR.Billing_date), 111), 10) 
            'And LEFT(CONVERT(VARCHAR, DSR.Billing_date, 111), 10) ) 
            strSQL4 &= " DateAdd(D, -3, DSR.Billing_date) And DSR.Billing_date) "
            strSQL4 &= " Or "
            strSQL4 &= " (DAY(DSR.Billing_date) != 1 And DSR.Billing_date "
            strSQL4 &= " between  DSR.Billing_date And DSR.Billing_date)) "
            'LEFT(CONVERT(VARCHAR, DSR.Billing_date, 111), 10) 
            'And LEFT(CONVERT(VARCHAR, DSR.Billing_date, 111), 10) ))
            strSQL4 &= " ) "

            dsActivityReportTmp = DBCommon.Get_DS(strSQL4, errFlg)
            If errFlg <> 1 Then 'If other than Error
                If dsActivityReportTmp IsNot Nothing Then
                    If dsActivityReportTmp.Tables(0).Rows.Count <> 0 Then
                        Dim dr1 As DataRow
                        For k = 0 To dsActivityReportTmp.Tables(0).Rows.Count - 1
                            dr1 = dsActivityReportTmp.Tables(0).Rows(k)

                            If dr1("Labor") IsNot DBNull.Value Then
                                Labor += comcontrol.Money_Round(dr1("Labor"), 2)
                            End If
                            If dr1("Parts") IsNot DBNull.Value Then
                                Parts += comcontrol.Money_Round(dr1("Parts"), 2)
                            End If
                        Next
                    End If
                End If
            Else ' If Error is occured, then default assign zero
            End If
            R("Labor") = Labor
            R("Parts") = Parts

            strSQL3 = " Select isnull(sum(IW_Labor_total+IW_Transport_total+IW_Others_total),0.0) 'In Warranty (Amount)',"
            strSQL3 &= " isnull(sum(OW_Labor_total+OW_Parts_total+OW_Transport_total+OW_Others_total),0.00) 'Out Warranty (Amount)' "
            strSQL3 &= " from dbo.SC_DSR_info DSR "
            strSQL3 &= " JOIN DBO.M_ship_base SB ON DSR.Branch_name = SB.ship_name "
            strSQL3 &= " Join Activity_report AR ON AR.location = SB.ship_code "
            strSQL3 &= " WHERE DSR.DELFG = 0 AND AR.DELFG = 0 And Branch_name = '" & branch & "' "
            strSQL3 &= " AND (CONVERT(DATETIME, AR.month + '/' + AR.day)) = DSR.Billing_date "

            If queryParams.DateTo <> "" Then
                If queryParams.DateFrom <> "" Then
                    strSQL3 &= "AND (CONVERT(DATETIME, AR.month + '/' + AR.day)) <= CONVERT(DATETIME, '" & queryParams.DateTo & "') "
                    strSQL3 &= "AND (CONVERT(DATETIME, AR.month + '/' + AR.day)) >= CONVERT(DATETIME, '" & queryParams.DateFrom & "') "
                Else
                    strSQL3 &= "AND (CONVERT(DATETIME, AR.month + '/' + AR.day)) <= CONVERT(DATETIME, '" & queryParams.DateTo & "') "
                End If
            Else
                If queryParams.DateFrom <> "" Then
                    strSQL3 &= "AND (CONVERT(DATETIME, AR.month + '/' + AR.day)) >= CONVERT(DATETIME, '" & queryParams.DateFrom & "') "
                End If
            End If

            dsSC_DSR_info = DBCommon.Get_DS(strSQL3, errFlg)
            Dim drdsr As DataRow
            If dsSC_DSR_info IsNot Nothing Then
                If dsSC_DSR_info.Tables(0).Rows.Count <> 0 Then
                    For i = 0 To dsSC_DSR_info.Tables(0).Rows.Count - 1
                        drdsr = dsSC_DSR_info.Tables(0).Rows(i)

                        If drdsr("In Warranty (Amount)") IsNot DBNull.Value Then
                            In_Ward = comcontrol.Money_Round(drdsr("In Warranty (Amount)"), 2)
                        End If
                        If drdsr("Out Warranty (Amount)") IsNot DBNull.Value Then
                            Out_Ward = comcontrol.Money_Round(drdsr("Out Warranty (Amount)"), 2)
                        End If
                    Next


                End If
            End If
            R("In_Ward") = In_Ward
            R("Out_Ward") = Out_Ward
            R("RevWoTax") = Convert.ToDecimal(In_Ward) + Convert.ToDecimal(Out_Ward) + Convert.ToDecimal(Labor) + Convert.ToDecimal(Parts)
            _DataTable.Rows.Add(R)
        Next
        Return _DataTable
    End Function
End Class
