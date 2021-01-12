Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class CashTrackControl

    Public Function UpdateAutoCashTrack(ByVal csvData()() As String, queryParams As CashTrackModel) As Boolean
        'Row 0 - Header 1
        'Row 1 - Header 2
        '0 ServiceOrderNo
        '1 LastUpdatedUser
        '2 BillingUser
        '3 BillingDate
        '4 GoodsDeliveredDate
        '5 BranchName
        '6 Engineer
        '7 EngineerName
        '8 Product
        '9 ProductType
        '10 InLabor
        '11 InParts
        '12 InTransport
        '13 InOthers
        '14 InTax
        '15 InTotal
        '16 OutLabor
        '17 OutParts
        '18 OutTransport
        '19 OutOthers
        '20 OutTax
        '21 OutTotal
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)


        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))

        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn
        Dim flag As Boolean = True
        Dim flagAll As Boolean = True

        Dim sqlStr As String = ""
        Dim dtCashTrackExist As DataTable
        Dim inComplete As Int16
        For i = 0 To csvData.Length - 1

            If i > 1 Then '0 and 1 Header
                '1st check Service Order Number exist in the table 
                ' dtCashTrackExist = SelectCashTrackExists(csvData(i)(0))
                sqlStr = "SELECT TOP 1 claim_no as ServiceOrderNo,incomplete FROM cash_track "
                sqlStr = sqlStr & " WHERE DELFG = 0 AND LOCATION='" & queryParams.Location & "' and claim_no='" & csvData(i)(0) & "'"
                dtCashTrackExist = dbConn.GetDataSet(sqlStr)
                If (dtCashTrackExist Is Nothing) Or (dtCashTrackExist.Rows.Count = 0) Then   ' (tblRecMONEY_STATUS1 Is Nothing) Or (tblRecMONEY_STATUS1.Rows.Count = 0) 
                    inComplete = -99
                Else
                    inComplete = dtCashTrackExist.Rows(0)("incomplete")
                    'Exist then check if the records updated already 
                    '0 - Need to update because incomplete
                    '1 - Already updated 
                    If inComplete = 0 Then 'Delete the record - Change the DELETE FLAG
                        sqlStr = "UPDATE cash_track SET DELFG=1  "
                        sqlStr = sqlStr & "WHERE DELFG=0 AND "
                        sqlStr = sqlStr & "claim_no = @claim_no  "
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@claim_no", csvData(i)(0))) 'ServiceOrderNo
                        flag = dbConn.ExecSQL(sqlStr)
                        dbConn.sqlCmd.Parameters.Clear()
                        'If Error occurs then will store the flag as false
                        If Not flag Then
                            flagAll = False
                            Exit For
                        End If
                    End If
                End If
                'If dtCashTrackCnt Is Nothing Then
                'New Record
                If (inComplete = 0) Or (inComplete = -99) Then
                    sqlStr = "Insert into cash_track ("
                    sqlStr = sqlStr & "CRTDT, "
                    sqlStr = sqlStr & "CRTCD, "
                    '      sqlStr = sqlStr & "UPDDT, "
                    sqlStr = sqlStr & "UPDCD, "
                    sqlStr = sqlStr & "UPDPG, "
                    sqlStr = sqlStr & "DELFG, "
                    sqlStr = sqlStr & "claim_no, "
                    sqlStr = sqlStr & "invoice_date, "
                    '     sqlStr = sqlStr & "Invoice_No, "
                    ' sqlStr = sqlStr & "customer_name, "
                    sqlStr = sqlStr & "Warranty, "
                    '    sqlStr = sqlStr & "payment, "
                    '    sqlStr = sqlStr & "payment_kind, "
                    sqlStr = sqlStr & "total_amount, "
                    sqlStr = sqlStr & "input_user, "
                    sqlStr = sqlStr & "location, "
                    '        sqlStr = sqlStr & "card_number, "
                    '      sqlStr = sqlStr & "card_type, "
                    '        sqlStr = sqlStr & "deposit, "
                    '      sqlStr = sqlStr & "change, "
                    '    sqlStr = sqlStr & "count_no, "
                    '     sqlStr = sqlStr & "message, "
                    sqlStr = sqlStr & "FALSE, "
                    '       sqlStr = sqlStr & "claim, "
                    '       sqlStr = sqlStr & "claim_card, "
                    ' '       sqlStr = sqlStr & "full_discount, "
                    '        sqlStr = sqlStr & "discount, "
                    '         sqlStr = sqlStr & "discount_after_amt, "
                    '      sqlStr = sqlStr & "discount_after_amt_credit, "
                    sqlStr = sqlStr & "incomplete, "
                    sqlStr = sqlStr & "count_no "
                    sqlStr = sqlStr & " ) "
                    sqlStr = sqlStr & " values ( "
                    sqlStr = sqlStr & "@CRTDT, "
                    sqlStr = sqlStr & "@CRTCD, "
                    '    sqlStr = sqlStr & "@UPDDT, "
                    sqlStr = sqlStr & "@UPDCD, "
                    sqlStr = sqlStr & "@UPDPG, "
                    sqlStr = sqlStr & "@DELFG, "
                    sqlStr = sqlStr & "@claim_no, "
                    sqlStr = sqlStr & "@invoice_date, "
                    '     sqlStr = sqlStr & "@Invoice_No, "
                    '   sqlStr = sqlStr & "@customer_name, "
                    sqlStr = sqlStr & "@Warranty, "
                    '  sqlStr = sqlStr & "@payment, "
                    '   sqlStr = sqlStr & "@payment_kind, "
                    sqlStr = sqlStr & "@total_amount, "
                    sqlStr = sqlStr & "@input_user, "
                    sqlStr = sqlStr & "@location, "
                    '       sqlStr = sqlStr & "@card_number, "
                    '       sqlStr = sqlStr & "@card_type, "
                    '       sqlStr = sqlStr & "@deposit, "
                    '      sqlStr = sqlStr & "@change, "
                    '       sqlStr = sqlStr & "@count_no, "
                    '       sqlStr = sqlStr & "@message, "
                    sqlStr = sqlStr & "0, "
                    '       sqlStr = sqlStr & "@claim, "
                    '       sqlStr = sqlStr & "@claim_card, "
                    '        sqlStr = sqlStr & "@full_discount, "
                    '       sqlStr = sqlStr & "@discount, "
                    '       sqlStr = sqlStr & "@discount_after_amt, "
                    '      sqlStr = sqlStr & "@discount_after_amt_credit, "
                    sqlStr = sqlStr & "@incomplete, "
                    sqlStr = sqlStr & " (select max(count_no)+1 from cash_track)) "
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTDT", dtNow))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTCD", queryParams.UserId))
                    ' dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDDT", queryParams.Location))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", csvData(i)(2))) ' BillingUser
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDPG", queryParams.FileName))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELFG", 0))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@claim_no", csvData(i)(0))) 'ServiceOrderNo
                    'Convert Date
                    Dim strDate As String = csvData(i)(3)
                    If queryParams.DateType = "DD" Then
                        strDate = strDate.Replace(".", "/")
                        strDate = DateTime.ParseExact(strDate, "dd/MM/yyyy", Nothing).ToString("yyyy/MM/dd")
                    Else
                        strDate = strDate.Replace(".", "/")
                        strDate = DateTime.ParseExact(strDate, "MM/dd/yyyy", Nothing).ToString("yyyy/MM/dd")
                    End If

                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@invoice_date", strDate)) 'GoodsDeliveredDate
                    ' dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Invoice_No", queryParams.Location))
                    ' dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@customer_name", queryParams.Location))
                    If Len(csvData(i)(10)) > 1 Then
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Warranty", "IW"))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@incomplete", 0))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@total_amount", csvData(i)(15))) ' OutTotal
                    Else
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Warranty", "OOW"))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@incomplete", -1))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@total_amount", csvData(i)(21))) ' OutTotal
                    End If

                    ' dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@payment", queryParams.Location))
                    ' dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@payment_kind", queryParams.Location))

                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@input_user", csvData(i)(7))) 'EngineerName
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@location", queryParams.Location))
                    '       dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@card_number", queryParams.Location))
                    '      dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@card_type", queryParams.Location))
                    '       dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@deposit", queryParams.Location))
                    '       dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@change", queryParams.Location))
                    '        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@count_no", queryParams.Location))
                    '        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@message", queryParams.Location))
                    '        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FALSE", queryParams.Location))
                    '         dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@claim", queryParams.Location))
                    '      dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@claim_card", queryParams.Location))
                    '       dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@full_discount", queryParams.Location))
                    '       dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@discount", queryParams.Location))
                    '       dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@discount_after_amt", queryParams.Location))
                    '       dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@discount_after_amt_credit", queryParams.Location))
                    flag = dbConn.ExecSQL(sqlStr)
                    dbConn.sqlCmd.Parameters.Clear()
                    'If Error occurs then will store the flag as false
                    If Not flag Then
                        flagAll = False
                    End If
                End If
            End If 'Other than header - End
        Next


        If flagAll Then
            flag = True
            dbConn.sqlTrn.Commit()
        Else
            flag = False
            dbConn.sqlTrn.Rollback()
        End If
        dbConn.CloseConnection()

        Return flag

        ''''''''Dim _ShipBaseModel As ShipBaseModel = New ShipBaseModel()
        ''''''''Dim dbConn1 As DBUtility = New DBUtility()
        ''''''''Dim dt1 As DataTable = New DataTable()
        ''''''''Dim codeMaster As List(Of CodeMasterModel) = New List(Of CodeMasterModel)()
        ''''''''Dim sqlStr1 As String = "SELECT CRTDT,CRTCD,UPDDT,UPDCD,UPDPG,DELFG,claim_no,invoice_date,Invoice_No,customer_name,Warranty,payment,payment_kind,total_amount,input_user,location,card_number,card_type,deposit,change,count_no,message,FALSE,claim,claim_card,full_discount,discount,discount_after_amt,discount_after_amt_credit,incomplete FROM cash_track "
        ''''''''sqlStr1 = sqlStr1 & " WHERE DELFG = 0 AND incomplete=0 AND LOCATION='" & queryParams.Location & "'"
        ''''''''Dim _DataTable As DataTable = dbConn1.GetDataSet(sqlStr1)
        ''''''''dbConn1.CloseConnection()
        ''''''''Return _DataTable
    End Function

    Public Function SelectCashTrackExists(ServiceOrderNo As String) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT TOP 1 claim_no as ServiceOrderNo,incomplete FROM cash_track "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND LOCATION='" & ServiceOrderNo & "'"
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable


    End Function

    Public Function SelectCashTrackIncomplete(queryParams As CashTrackModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT CRTDT,CRTCD,UPDDT,UPDCD,UPDPG,DELFG,claim_no,FORMAT(invoice_date,'yyyy/MM/dd') as invoice_date,Invoice_No,customer_name,Warranty,payment,payment_kind,cast(total_amount as numeric(18,2)) as total_amount ,input_user,location,card_number,card_type,deposit,change,count_no,message,FALSE,claim,claim_card,full_discount,discount,discount_after_amt,discount_after_amt_credit,incomplete FROM cash_track "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND incomplete=-1 AND LOCATION='" & queryParams.Location & "'"
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable


    End Function

    Public Function UpdateCashTrackIncomplete(ByVal lstCashTrackModel As List(Of CashTrackModel)) As Boolean
        Dim dbConn As DBUtility = New DBUtility()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        Dim rowIndex As Integer = 0
        For Each target As CashTrackModel In lstCashTrackModel

            'Full Cash - only one transaction
            'Full Card -  only one transaction
            'Half Cash & Hald Card - two transaction

            rowIndex = rowIndex + 1
            dbConn.sqlCmd.Transaction = dbConn.sqlTrn
            Dim sqlStr As String = "" '

            If target.MoneyType = 1 Then 'Cash

                sqlStr = "UPDATE cash_track 
						SET
						   payment = 'Cash'
                         ,  claim_card = 0
                           ,  deposit = @deposit1{0}
                           ,  claim = @claim1{0}
						  , discount = @discount{0}
                          ,  discount_after_amt = @discount_after_amt{0}
                         ,  discount_after_amt_credit = @discount_after_amt_credit{0}
                          , payment_kind =null
                          ,    incomplete = @incomplete{0}
						WHERE
						  claim_no = @claim_no{0} 
						  and location = @location{0} and  incomplete = '-1'
							"
            ElseIf target.MoneyType = 2 Then 'Card
                sqlStr = "UPDATE cash_track 
						SET
						   payment = 'Credit'
                         ,  claim_card =  @claim_card{0}
                           ,  deposit = @deposit1{0}
                           ,  claim = @claim1{0}
						  , discount = @discount{0}
                          ,  discount_after_amt = @discount_after_amt{0}
                         ,  discount_after_amt_credit = @discount_after_amt_credit{0}
                          , payment_kind = null
                          ,    incomplete = '0'
						WHERE
						  claim_no = @claim_no{0} 
						  and location = @location{0}  and  incomplete = '-1'
							"
            ElseIf target.MoneyType = 3 Then 'Cash & Card
                'payment_kind = 1 for Cash Transaction
                sqlStr = "UPDATE cash_track 
						SET
						   payment = 'Cash'
                          ,  claim_card = 0
                           ,  deposit = @deposit1{0}
                           ,  claim = @claim1{0}
						  , discount = @discount{0}
                          ,  discount_after_amt = @discount_after_amt{0}
                         ,  discount_after_amt_credit =0
                          , payment_kind = 1 
 						WHERE
						  claim_no = @claim_no{0} 
						  and location = @location{0}  and  incomplete = '-1'
							"
            End If
            sqlStr = String.Format(sqlStr, rowIndex)
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter(String.Format("@deposit1{0}", rowIndex), target.Deposit))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter(String.Format("@claim1{0}", rowIndex), target.Claim))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter(String.Format("@claim_card{0}", rowIndex), target.ClaimCard))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter(String.Format("@discount_after_amt{0}", rowIndex), target.DiscountAfterAmount))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter(String.Format("@discount_after_amt_credit{0}", rowIndex), target.DiscountAfterCredit))

            ' dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter(String.Format("@deposit2{0}", rowIndex), target.CashAmt))
            ' dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter(String.Format("@claim2{0}", rowIndex), target.CashAmt))
            'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter(String.Format("@claim_card2{0}", rowIndex), target.CardAmt))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter(String.Format("@discount{0}", rowIndex), target.Discount))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter(String.Format("@claim_no{0}", rowIndex), target.ServiceOrderNo))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter(String.Format("@location{0}", rowIndex), target.Location))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter(String.Format("@incomplete{0}", rowIndex), target.InComplete))

            Dim updateFlg As Boolean = dbConn.ExecSQL(sqlStr)

            If Not updateFlg Then
                dbConn.sqlTrn.Rollback()
                Return False
            End If
            'For Insert then Update for Card
            If target.MoneyType = 3 Then
                sqlStr = "insert into cash_track (CRTDT,CRTCD,UPDCD,UPDPG,DELFG,claim_no,invoice_date,Warranty,payment,payment_kind,total_amount,input_user,location,card_number,card_type,deposit,count_no,false,claim,claim_card,full_discount,discount,discount_after_amt,discount_after_amt_credit,incomplete)"
                sqlStr = sqlStr & "(select CRTDT,CRTCD,UPDCD,UPDPG,DELFG,claim_no,invoice_date,Warranty,'Credit','2',total_amount,input_user,location,card_number,card_type,0,(select max(count_no)+1 from cash_track),false,0," & target.ClaimCard & ",full_discount,discount,0," & target.DiscountAfterCredit & ",'0' from cash_track where claim_no='" & target.ServiceOrderNo & "' and DELFG=0 and  incomplete = '-1')"
                updateFlg = dbConn.ExecSQL(sqlStr)
                If Not updateFlg Then
                    dbConn.sqlTrn.Rollback()
                    Return False
                End If

                sqlStr = "update  cash_track set   incomplete = '0' where claim_no='" & target.ServiceOrderNo & "'   and location = '" & target.Location & "'  and  incomplete = '-1' and  payment = 'Cash'"
                updateFlg = dbConn.ExecSQL(sqlStr)
                If Not updateFlg Then
                    dbConn.sqlTrn.Rollback()
                    Return False
                End If
            End If

        Next
        dbConn.sqlTrn.Commit()
        dbConn.CloseConnection()
        Return True
    End Function
End Class
