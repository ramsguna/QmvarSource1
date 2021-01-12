Imports System.IO
Imports System.Text
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class RpaOnOffControl
    Public Function SelectRpaTask() As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & " SeqId as Id, TaskName, Ssc, Status as Column3, ProcessId, SCHEDULER_NAME + '/' + TaskName as Column2 "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "RpaLiveStatus WHERE DELFlG=0 AND status='Started' "

        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable

    End Function
    Public Function SelectRpaScheduler() As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & " RPASCHID as Id, TASK_NAME, Status as Column3, SCHEDULER_NAME + '/' + TASK_NAME as Column2 "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "RPA_SCHEDULER WHERE (DELFG=0 or DELFG is null) AND status='Processing' "

        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable

    End Function

    Public Function UpdateRpaTaskStatus(RpaStatus As List(Of RpaSchTskModel)) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)

        Dim sqlStr As String = ""
        Dim flag As Boolean = True
        Dim flagAll As Boolean = True

        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn

        Dim i As Integer = 0
        Dim strEncryPwd As String = ""

        For i = 0 To RpaStatus.Count - 1
            sqlStr = "Update RpaLiveStatus set  Status='Suspend' "
            sqlStr = sqlStr & "WHERE "
            ' If Not String.IsNullOrEmpty(RpaStatus.Item(i).Id) Then
            sqlStr = sqlStr & "SeqId = @Id "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Id", RpaStatus.Item(i).Id))
            ' End If
            flag = dbConn.ExecSQL(sqlStr)
            dbConn.sqlCmd.Parameters.Clear()
            'If Error occurs then will store the flag as false
            If Not flag Then
                flagAll = False
                Exit For
            End If
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
    End Function


    Public Function UpdateRpaSchedulerStatus(RpaStatus As List(Of RpaSchTskModel)) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)

        Dim sqlStr As String = ""
        Dim flag As Boolean = True
        Dim flagAll As Boolean = True

        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn

        Dim i As Integer = 0
        Dim strEncryPwd As String = ""

        For i = 0 To RpaStatus.Count - 1
            sqlStr = "Update RPA_SCHEDULER set  Status='Suspend' "
            sqlStr = sqlStr & "WHERE "
            ' If Not String.IsNullOrEmpty(RpaStatus.Item(i).Id) Then
            sqlStr = sqlStr & "RPASCHID = @Id "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Id", RpaStatus.Item(i).Id))
            ' End If
            flag = dbConn.ExecSQL(sqlStr)
            dbConn.sqlCmd.Parameters.Clear()
            'If Error occurs then will store the flag as false
            If Not flag Then
                flagAll = False
                Exit For
            End If
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
    End Function
End Class
