Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class ShipBaseControl
    ''' <summary>
    ''' Select the branch code
    ''' </summary>
    ''' <returns>return branch codes</returns>
    Public Function SelectBranchCode() As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim _ShipBaseModel As ShipBaseModel = New ShipBaseModel()
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim codeMaster As List(Of CodeMasterModel) = New List(Of CodeMasterModel)()
        Dim sqlStr As String = "SELECT ship_code FROM M_ship_base WHERE DELFG = 0 ORDER BY ship_code"
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function

    Public Function SelectShipMark(queryParams As ShipBaseModel) As String
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "ship_mark "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "M_ship_base "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        If Not String.IsNullOrEmpty(queryParams.ShipCode) Then
            sqlStr = sqlStr & "AND ship_code = @ship_code "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ship_code", queryParams.ShipCode))
        End If
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return dt.Rows(0)("ship_mark").ToString()
    End Function



    Public Function SelectBranchAll(ByVal Optional orderBy As String = "ASC") As List(Of CodeMasterModel)
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim codeMaster As List(Of CodeMasterModel) = New List(Of CodeMasterModel)()
        Dim sqlStr As String = "SELECT ship_code, ship_name FROM M_ship_base "
        sqlStr = sqlStr & "WHERE DELFG = 0 ORDER BY ship_code " & orderBy
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        For Each row As DataRow In dt.Rows
            Dim cm As CodeMasterModel = New CodeMasterModel()
            cm.CodeDispValue = row("ship_name").ToString()
            cm.CodeValue = row("ship_code").ToString()
            codeMaster.Add(cm)
        Next
        Return codeMaster
    End Function


    ''' <summary>
    ''' Find Open Date and Time, also closing time
    ''' </summary>
    ''' <returns></returns>
    Public Function SelectOpenDateTimeClosingDate(ByVal Optional queryParams As String = "") As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim _ShipBaseModel As ShipBaseModel = New ShipBaseModel()
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim codeMaster As List(Of CodeMasterModel) = New List(Of CodeMasterModel)()
        Dim sqlStr As String = "SELECT open_time,opening_date,closing_date FROM M_ship_base WHERE DELFG = 0 "
        If Not String.IsNullOrEmpty(queryParams) Then
            sqlStr = sqlStr & "AND ship_code = @ShipCode "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipCode", queryParams))
        End If
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function

    'VJ - 2020-04-16 SSC parent child 
    Public Function SelectShipBaseDetails() As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim _ShipBaseModel As ShipBaseModel = New ShipBaseModel()
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim codeMaster As List(Of CodeMasterModel) = New List(Of CodeMasterModel)()
        Dim sqlStr As String = ""
        sqlStr &= "Select ship_name,ship_info,ship_manager,ship_tel,ship_add1,"
        sqlStr &= "ship_add2,ship_add3,zip,e_mail,ship_service,open_time,close_time,"
        sqlStr &= "opening_date,closing_date,ship_code,ship_mark,item_1,item_2,"
        sqlStr &= "mess_1,mess_2,mess_3,regi_deposit,PO_no,inspection1_start,"
        sqlStr &= "inspection1_end,inspection2_start,inspection2_end,"
        sqlStr &= "inspection3_start,inspection3_end,open_start,open_end,"
        sqlStr &= "close_start,close_end,GSTIN,Parent_Ship_Name,IsChildShip,DELFG"
        sqlStr &= " From M_ship_base"
        'Dim sqlStr As String = "SELECT ship_code FROM M_ship_base WHERE DELFG = 0 ORDER BY ship_code"
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function
    'VJ - 2020-04-16 SSC parent child
    Public Function SelectParentShipBaseDetails(ByVal shipcode As String) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim _ShipBaseModel As ShipBaseModel = New ShipBaseModel()
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim codeMaster As List(Of CodeMasterModel) = New List(Of CodeMasterModel)()
        Dim sqlStr As String = ""
        sqlStr &= "select ship_name,ship_code from m_ship_base where ship_name in "
        sqlStr &= "(select Parent_Ship_Name from M_ship_base where "
        sqlStr &= " ship_code = '" & shipcode & "' and IsChildShip = 1)"
        'Dim sqlStr As String = "SELECT ship_code FROM M_ship_base WHERE DELFG = 0 ORDER BY ship_code"
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function











End Class
