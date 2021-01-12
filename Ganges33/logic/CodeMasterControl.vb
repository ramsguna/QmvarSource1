Imports System.Data
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Namespace Ganges33.logic
    ''' <summary>
    ''' Get branch name and branch code
    ''' </summary>
    Public Class CodeMasterControl
        Public Function SelectBranchMaster(ByVal Optional QryFlag As Int16 = 0, ByVal Optional queryParam As String = "0", ByVal Optional orderBy As String = "ASC") As List(Of CodeMasterModel)
            Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
            Dim dbConn As DBUtility = New DBUtility()
            Dim dt As DataTable = New DataTable()
            Dim codeMaster As List(Of CodeMasterModel) = New List(Of CodeMasterModel)()
            Dim sqlStr As String = "SELECT ship_name, ship_code FROM M_ship_base "
            sqlStr = sqlStr & "WHERE DELFG = 0 "
            If QryFlag = 1 Then
                sqlStr = sqlStr & "AND  ship_code IN ( " & queryParam & ") order by ship_code " & orderBy
            ElseIf QryFlag = 2 Then
                sqlStr = sqlStr & " order by ship_code " & orderBy
            End If
            '   Could not add parameter, the datas are pulling from database. Can use directly with query
            '   dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@branch", queryParam))
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
        Public Function SelectBranchMasterSony(ByVal Optional QryFlag As Int16 = 0, ByVal Optional queryParam As String = "0", ByVal Optional orderBy As String = "ASC") As List(Of CodeMasterModel)
            Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
            Dim dbConn As DBUtility = New DBUtility()
            Dim dt As DataTable = New DataTable()
            Dim codeMaster As List(Of CodeMasterModel) = New List(Of CodeMasterModel)()
            Dim sqlStr As String = "SELECT ship_name, ship_code FROM M_ship_base "
            sqlStr = sqlStr & "WHERE ship_name like 'SID%' AND DELFG = 0 "
            If QryFlag = 1 Then
                sqlStr = sqlStr & "AND  ship_code IN ( " & queryParam & ") order by ship_code " & orderBy
            ElseIf QryFlag = 2 Then
                sqlStr = sqlStr & " order by ship_code " & orderBy
            End If
            '   Could not add parameter, the datas are pulling from database. Can use directly with query
            '   dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@branch", queryParam))
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
        Public Function SelectBranchMasterSamsung(ByVal Optional QryFlag As Int16 = 0, ByVal Optional queryParam As String = "0", ByVal Optional orderBy As String = "ASC") As List(Of CodeMasterModel)
            Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
            Dim dbConn As DBUtility = New DBUtility()
            Dim dt As DataTable = New DataTable()
            Dim codeMaster As List(Of CodeMasterModel) = New List(Of CodeMasterModel)()
            Dim sqlStr As String = "SELECT ship_name, ship_code FROM M_ship_base "
            sqlStr = sqlStr & "WHERE ship_name like 'SSC%' AND DELFG = 0 "
            If QryFlag = 1 Then
                sqlStr = sqlStr & "AND  ship_code IN ( " & queryParam & ") order by ship_code " & orderBy
            ElseIf QryFlag = 2 Then
                sqlStr = sqlStr & " order by ship_code " & orderBy
            End If
            '   Could not add parameter, the datas are pulling from database. Can use directly with query
            '   dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@branch", queryParam))
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

        Public Function SelectBranchSSC(ByVal Optional QryFlag As Int16 = 0, ByVal Optional queryParam As String = "0", ByVal Optional orderBy As String = "ASC") As List(Of CodeMasterModel)
            Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
            Dim dbConn As DBUtility = New DBUtility()
            Dim dt As DataTable = New DataTable()
            Dim codeMaster As List(Of CodeMasterModel) = New List(Of CodeMasterModel)()
            Dim sqlStr As String = "SELECT ship_name, ship_code FROM M_ship_base "
            sqlStr = sqlStr & "WHERE DELFG = 0 "
            If QryFlag = 1 Then
                sqlStr = sqlStr & "AND  ship_code IN ( " & queryParam & ") order by ship_code " & orderBy
            ElseIf QryFlag = 2 Then
                sqlStr = sqlStr & " order by ship_code " & orderBy
            End If
            '   Could not add parameter, the datas are pulling from database. Can use directly with query
            '   dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@branch", queryParam))
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
        Public Function SelectBranchSSCCreditInfo(ByVal Optional QryFlag As Int16 = 0, ByVal Optional queryParam As String = "0", ByVal Optional orderBy As String = "ASC") As List(Of CodeMasterModel)
            Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
            Dim dbConn As DBUtility = New DBUtility()
            Dim dt As DataTable = New DataTable()
            Dim codeMaster As List(Of CodeMasterModel) = New List(Of CodeMasterModel)()
            Dim sqlStr As String = "SELECT ship_name, ship_code FROM M_ship_base "
            sqlStr = sqlStr & "WHERE DELFG = 0 AND IsChildShip = 0 "
            If QryFlag = 1 Then
                sqlStr = sqlStr & "AND  ship_code IN ( " & queryParam & ") order by ship_code " & orderBy
            ElseIf QryFlag = 2 Then
                sqlStr = sqlStr & " order by ship_code " & orderBy
            End If
            '   Could not add parameter, the datas are pulling from database. Can use directly with query
            '   dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@branch", queryParam))
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
        Public Function SelectCorpMaster(ByVal Optional QryFlag As Int16 = 0, ByVal Optional queryParam As String = "0", ByVal Optional orderBy As String = "ASC") As List(Of CodeMasterModel)
            Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
            Dim dbConn As DBUtility = New DBUtility()
            Dim dt As DataTable = New DataTable()
            Dim codeMaster As List(Of CodeMasterModel) = New List(Of CodeMasterModel)()
            Dim sqlStr As String = "SELECT corp_name,corp_number from M_CORP "
            sqlStr = sqlStr & "WHERE DELFG = 0 "
            If QryFlag = 1 Then
                sqlStr = sqlStr & "AND  corp_number IN ( " & queryParam & ") order by corp_number " & orderBy
            ElseIf QryFlag = 2 Then
                sqlStr = sqlStr & " order by corp_number " & orderBy
            End If
            '   Could not add parameter, the datas are pulling from database. Can use directly with query
            '   dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@branch", queryParam))
            dt = dbConn.GetDataSet(sqlStr)
            dbConn.CloseConnection()
            For Each row As DataRow In dt.Rows
                Dim cm As CodeMasterModel = New CodeMasterModel()
                cm.CodeDispValue = row("corp_name").ToString()
                cm.CodeValue = row("corp_number").ToString()
                codeMaster.Add(cm)
            Next
            Return codeMaster
        End Function


        Public Function RpaSchedulerType() As List(Of CodeMasterModel)
            Dim selectRpaSchedulerType As List(Of CodeMasterModel) = New List(Of CodeMasterModel)()
            Dim cm1 As CodeMasterModel = New CodeMasterModel()
            cm1.CodeDispValue = "Daily"
            cm1.CodeType = "1"
            selectRpaSchedulerType.Add(cm1)
            Dim cm2 As CodeMasterModel = New CodeMasterModel()
            cm2.CodeDispValue = "Weekly"
            cm2.CodeType = "2"
            selectRpaSchedulerType.Add(cm2)
            Dim cm3 As CodeMasterModel = New CodeMasterModel()
            cm3.CodeDispValue = "10 Days"
            cm3.CodeType = "3"
            selectRpaSchedulerType.Add(cm3)
            Dim cm4 As CodeMasterModel = New CodeMasterModel()
            cm4.CodeDispValue = "Monthly"
            cm4.CodeType = "4"
            selectRpaSchedulerType.Add(cm4)
            Return selectRpaSchedulerType
        End Function
    End Class
End Namespace
