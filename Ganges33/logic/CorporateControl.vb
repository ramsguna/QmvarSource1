Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class CorporateControl
    Public Function AddCorporate(queryParams As CorporateModel) As Boolean
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim sqlStr As String = ""
        Dim flag As Boolean = True

        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn
        sqlStr = "Insert into M_CORP ("
        sqlStr = sqlStr & "CRTDT, "
        sqlStr = sqlStr & "CRTCD, "
        sqlStr = sqlStr & "UPDPG, "
        sqlStr = sqlStr & "DELFG, "
        sqlStr = sqlStr & "create_user, "
        sqlStr = sqlStr & "corp_name, "
        sqlStr = sqlStr & "corp_per_name, "
        sqlStr = sqlStr & "corp_add_1, "
        sqlStr = sqlStr & "corp_add_2, "
        sqlStr = sqlStr & "corp_tel, "
        sqlStr = sqlStr & "corp_fax, "
        sqlStr = sqlStr & "corp_zip, "
        sqlStr = sqlStr & "response_ship, "
        sqlStr = sqlStr & "user_PO, "
        sqlStr = sqlStr & "close_date, "
        sqlStr = sqlStr & "payment_date, "
        sqlStr = sqlStr & "corp_email, "
        sqlStr = sqlStr & "corp_bank, "
        sqlStr = sqlStr & "casa_account, "
        sqlStr = sqlStr & "casa_number, "
        sqlStr = sqlStr & "casa_type, "
        sqlStr = sqlStr & "update_user, "
        sqlStr = sqlStr & "corp_number "
        sqlStr = sqlStr & " ) "
        sqlStr = sqlStr & " values ( "
        sqlStr = sqlStr & "@CRTDT, "
        sqlStr = sqlStr & "@CRTCD, "
        '    sqlStr = sqlStr & "@UPDDT, "
        'sqlStr = sqlStr & "@UPDCD, "
        sqlStr = sqlStr & "@UPDPG, "
        sqlStr = sqlStr & "@DELFG, "
        sqlStr = sqlStr & "@CreateUser, "
        sqlStr = sqlStr & "@CorpName, "
        sqlStr = sqlStr & "@CorpPerName, "
        sqlStr = sqlStr & "@CorpAdd1, "
        sqlStr = sqlStr & "@CorpAdd2, "
        sqlStr = sqlStr & "@CorpTel, "
        sqlStr = sqlStr & "@CorpFax, "
        sqlStr = sqlStr & "@CorpZip, "
        sqlStr = sqlStr & "@ResponseShipCode, "
        sqlStr = sqlStr & "@UserPO, "
        sqlStr = sqlStr & "@CloseDate, "
        sqlStr = sqlStr & "@PaymentDate, "
        sqlStr = sqlStr & "@CorpEmail, "
        sqlStr = sqlStr & "@CorpBank, "
        sqlStr = sqlStr & "@CasaAccount, "
        sqlStr = sqlStr & "@CasaNumber, "
        sqlStr = sqlStr & "@CasaType, "
        sqlStr = sqlStr & "@UpdateUser, "
        sqlStr = sqlStr & "@CorpNumber"
        sqlStr = sqlStr & " ) "

        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTDT", dtNow))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTCD", queryParams.UserId))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDPG", queryParams.UPDPG))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELFG", 0))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CreateUser", queryParams.CreateUser))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpName", queryParams.CorpName))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpPerName", queryParams.CorpPerName))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpAdd1", queryParams.CorpAdd1))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpAdd2", queryParams.CorpAdd2))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpTel", queryParams.CorpTel))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpFax", queryParams.CorpFax))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpZip", queryParams.CorpZip))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ResponseShipCode", queryParams.ResponsShipCode))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UserPO", queryParams.UserPO))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CloseDate", queryParams.CloseDate))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PaymentDate", queryParams.PaymentDate))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpEmail", queryParams.CorpEmail))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpBank", queryParams.CorpBank))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CasaAccount", queryParams.CasaAccount))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CasaNumber", queryParams.CasaNumber))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CasaType", queryParams.CasaType))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UpdateUser", queryParams.UpdateUser))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpNumber", queryParams.CorpNumber))

        flag = dbConn.ExecSQL(sqlStr)
        dbConn.sqlCmd.Parameters.Clear()

        If flag Then
            dbConn.sqlTrn.Commit()
        Else
            dbConn.sqlTrn.Rollback()
        End If
        dbConn.CloseConnection()

        Return flag
    End Function

    Public Function EditCorporate(queryParams As CorporateModel) As Boolean
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim sqlStr As String = ""
        Dim flag As Boolean = True

        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn

        sqlStr = "UPDATE M_CORP "
        sqlStr = sqlStr & "SET "
        sqlStr = sqlStr & "CRTDT= @CRTDT,"
        sqlStr = sqlStr & "CRTCD=@CRTCD, "
        sqlStr = sqlStr & "UPDPG=@UPDPG, "
        sqlStr = sqlStr & "DELFG=@DELFG, "
        sqlStr = sqlStr & "create_user=@CreateUser, "
        sqlStr = sqlStr & "corp_name=@CorpName, "
        sqlStr = sqlStr & "corp_per_name=@CorpPerName, "
        sqlStr = sqlStr & "corp_add_1=@CorpAdd1, "
        sqlStr = sqlStr & "corp_add_2=@CorpAdd2, "
        sqlStr = sqlStr & "corp_tel=@CorpTel, "
        sqlStr = sqlStr & "corp_fax=@CorpFax, "
        sqlStr = sqlStr & "corp_zip=@CorpZip, "
        sqlStr = sqlStr & "response_ship=@ResponseShipCode, "
        sqlStr = sqlStr & "user_PO=@UserPO, "
        sqlStr = sqlStr & "close_date=@CloseDate, "
        sqlStr = sqlStr & "payment_date=@PaymentDate, "
        sqlStr = sqlStr & "corp_email=@CorpEmail, "
        sqlStr = sqlStr & "corp_bank=@CorpBank, "
        sqlStr = sqlStr & "casa_account=@CasaAccount, "
        sqlStr = sqlStr & "casa_number=@CasaNumber, "
        sqlStr = sqlStr & "casa_type=@CasaType, "
        sqlStr = sqlStr & "update_user=@UpdateUser "
        sqlStr = sqlStr & "where corp_number=@CorpNumber "


        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTDT", dtNow))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTCD", queryParams.UserId))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDPG", queryParams.UPDPG))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELFG", 0))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CreateUser", queryParams.CreateUser))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpName", queryParams.CorpName))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpPerName", queryParams.CorpPerName))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpAdd1", queryParams.CorpAdd1))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpAdd2", queryParams.CorpAdd2))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpTel", queryParams.CorpTel))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpFax", queryParams.CorpFax))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpZip", queryParams.CorpZip))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ResponseShipCode", queryParams.ResponsShipCode))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UserPO", queryParams.UserPO))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CloseDate", queryParams.CloseDate))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PaymentDate", queryParams.PaymentDate))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpEmail", queryParams.CorpEmail))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpBank", queryParams.CorpBank))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CasaAccount", queryParams.CasaAccount))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CasaNumber", queryParams.CasaNumber))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CasaType", queryParams.CasaType))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UpdateUser", queryParams.UpdateUser))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpNumber", queryParams.CorpNumber))

        flag = dbConn.ExecSQL(sqlStr)
        dbConn.sqlCmd.Parameters.Clear()

        If flag Then
            dbConn.sqlTrn.Commit()
        Else
            dbConn.sqlTrn.Rollback()
        End If
        dbConn.CloseConnection()

        Return flag
    End Function


    Public Function SelectCorporateNumber(queryParams As CorporateModel) As String
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dtStatus As StatusModel = New StatusModel()
        'Sample Query
        'SELECT LEFT(corp_number, CHARINDEX('-', corp_number) - 1) [before_delim], Right(corp_number, Len(corp_number) - CHARINDEX('-', corp_number)) [after_delim] From [M_CORP] Where response_ship ='0002203913'

        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        'sqlStr = sqlStr & "max(RIGHT(corp_number, LEN(corp_number) - CHARINDEX('-', corp_number))) [CORP_NUMBER] "
        sqlStr = sqlStr & "max(cast(RIGHT(corp_number, LEN(corp_number) - CHARINDEX('-', corp_number)) as int)) [CORP_NUMBER] "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "M_CORP "
        '      sqlStr = sqlStr & "WHERE "
        'sqlStr = sqlStr & "DELFG=0 "
        '       If Not String.IsNullOrEmpty(queryParams.CRTCD) Then
        '       sqlStr = sqlStr & " response_ship = @BranchCode "
        '       dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BranchCode", queryParams.BranchCode))
        '        End If
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return dt.Rows(0)("CORP_NUMBER").ToString()
    End Function

    Public Function SelectCorpNumberCheck(ByVal queryParams As CorporateModel) As String
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "COUNT(*) AS CorpNumExistCnt "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "M_CORP "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        If Not String.IsNullOrEmpty(queryParams.CorpNumber) Then
            sqlStr = sqlStr & "AND corp_number = @CorpNumber "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpNumber", queryParams.CorpNumber))
        End If
        If Not String.IsNullOrEmpty(queryParams.CorpName) Then
            sqlStr = sqlStr & "AND corp_name = @CorpName "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpName", queryParams.CorpName))
        End If
        If Not String.IsNullOrEmpty(queryParams.CorpPerName) Then
            sqlStr = sqlStr & "AND corp_per_name = @CorpPerName "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpPerName", queryParams.CorpPerName))
        End If
        If Not String.IsNullOrEmpty(queryParams.CorpZip) Then
            sqlStr = sqlStr & "AND corp_zip = @CorpZip "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpZip", queryParams.CorpZip))
        End If
        If Not String.IsNullOrEmpty(queryParams.CorpEmail) Then
            sqlStr = sqlStr & "AND corp_email = @CorpEmail "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpEmail", queryParams.CorpEmail))
        End If
        If Not String.IsNullOrEmpty(queryParams.CorpTel) Then
            sqlStr = sqlStr & "AND corp_tel = @CorpTel "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpTel", queryParams.CorpTel))
        End If
        If (Not (String.IsNullOrEmpty(queryParams.RegistrationDateFrom)) And (Not (String.IsNullOrEmpty(queryParams.RegistrationDateTo)))) Then
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, crtdt, 111), 10) >= @RegistrationDateFrom and LEFT(CONVERT(VARCHAR, crtdt, 111), 10) <= @RegistrationDateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RegistrationDateFrom", queryParams.RegistrationDateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RegistrationDateTo", queryParams.RegistrationDateTo))
        ElseIf Not String.IsNullOrEmpty(queryParams.RegistrationDateFrom) Then
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, crtdt, 111), 10) = @RegistrationDateFrom "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RegistrationDateFrom", queryParams.RegistrationDateFrom))
        ElseIf Not String.IsNullOrEmpty(queryParams.RegistrationDateTo) Then
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, crtdt, 111), 10) = @RegistrationDateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RegistrationDateTo", queryParams.RegistrationDateTo))
        End If

        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return dt.Rows(0)("CorpNumExistCnt").ToString()
    End Function

    Public Function SelectCorpDetails(ByVal queryParams As CorporateModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "CRTDT, "
        sqlStr = sqlStr & "CRTCD, "
        sqlStr = sqlStr & "UPDDT, "
        sqlStr = sqlStr & "UPDCD, "
        sqlStr = sqlStr & "UPDPG, "
        sqlStr = sqlStr & "DELFG, "
        sqlStr = sqlStr & "create_user, "
        sqlStr = sqlStr & "corp_name, "
        sqlStr = sqlStr & "corp_per_name, "
        sqlStr = sqlStr & "corp_add_1, "
        sqlStr = sqlStr & "corp_add_2, "
        sqlStr = sqlStr & "corp_tel, "
        sqlStr = sqlStr & "corp_fax, "
        sqlStr = sqlStr & "corp_zip, "
        sqlStr = sqlStr & "response_ship, "
        sqlStr = sqlStr & "user_PO, "
        sqlStr = sqlStr & "close_date, "
        sqlStr = sqlStr & "payment_date, "
        sqlStr = sqlStr & "corp_email, "
        sqlStr = sqlStr & "corp_bank, "
        sqlStr = sqlStr & "casa_account, "
        sqlStr = sqlStr & "casa_number, "
        sqlStr = sqlStr & "casa_type, "
        sqlStr = sqlStr & "update_user, "
        sqlStr = sqlStr & "corp_number "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "M_CORP "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        If Not String.IsNullOrEmpty(queryParams.CorpNumber) Then
            sqlStr = sqlStr & "AND corp_number = @CorpNumber "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpNumber", queryParams.CorpNumber))
        End If
        If Not String.IsNullOrEmpty(queryParams.CorpName) Then
            sqlStr = sqlStr & "AND corp_name = @CorpName "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpName", queryParams.CorpName))
        End If
        If Not String.IsNullOrEmpty(queryParams.CorpPerName) Then
            sqlStr = sqlStr & "AND corp_per_name = @CorpPerName "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpPerName", queryParams.CorpPerName))
        End If
        If Not String.IsNullOrEmpty(queryParams.CorpZip) Then
            sqlStr = sqlStr & "AND corp_zip = @CorpZip "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpZip", queryParams.CorpZip))
        End If
        If Not String.IsNullOrEmpty(queryParams.CorpEmail) Then
            sqlStr = sqlStr & "AND corp_email = @CorpEmail "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpEmail", queryParams.CorpEmail))
        End If
        If Not String.IsNullOrEmpty(queryParams.CorpTel) Then
            sqlStr = sqlStr & "AND corp_tel = @CorpTel "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpTel", queryParams.CorpTel))
        End If
        If (Not (String.IsNullOrEmpty(queryParams.RegistrationDateFrom)) And (Not (String.IsNullOrEmpty(queryParams.RegistrationDateTo)))) Then
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, crtdt, 111), 10) >= @RegistrationDateFrom and LEFT(CONVERT(VARCHAR, crtdt, 111), 10) <= @RegistrationDateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RegistrationDateFrom", queryParams.RegistrationDateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RegistrationDateTo", queryParams.RegistrationDateTo))
        ElseIf Not String.IsNullOrEmpty(queryParams.RegistrationDateFrom) Then
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, crtdt, 111), 10) = @RegistrationDateFrom "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RegistrationDateFrom", queryParams.RegistrationDateFrom))
        ElseIf Not String.IsNullOrEmpty(queryParams.RegistrationDateTo) Then
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, crtdt, 111), 10) = @RegistrationDateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RegistrationDateTo", queryParams.RegistrationDateTo))
        End If
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function
End Class
