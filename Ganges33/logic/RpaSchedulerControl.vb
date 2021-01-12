Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class RpaSchedulerControl
    Public Function RpaSchedulerStatusUpdate(ByVal queryParams As RpaSchedulerModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim flag As Boolean = True
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "UPDATE RPA_SCHEDULER "
        sqlStr = sqlStr & "set  "

        If Not String.IsNullOrEmpty(queryParams.LastRunStatus) Then
            sqlStr = sqlStr & "LAST_RUN_STATUS = @LastRunStatus, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LastRunStatus", queryParams.LastRunStatus))
        End If
        If Not String.IsNullOrEmpty(queryParams.LastRunStatus) Then
            sqlStr = sqlStr & "LAST_RUN_DATETIME = @LastRunDateTime, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LastRunDateTime", queryParams.LastRunDateTime))
        End If
        If Not String.IsNullOrEmpty(queryParams.Status) Then
            sqlStr = sqlStr & "Status = @Status "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Status", queryParams.Status))
        End If
        If Not String.IsNullOrEmpty(queryParams.TaskName) Then
            sqlStr = sqlStr & "WHERE TASK_NAME = @TaskName "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TaskName", queryParams.TaskName))
        End If
        If Not String.IsNullOrEmpty(queryParams.SchedulerName) Then
            sqlStr = sqlStr & "AND SCHEDULER_NAME = @SchedulerName "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SchedulerName", queryParams.SchedulerName))
        End If

        flag = dbConn.ExecSQL(sqlStr)
        dbConn.CloseConnection()
        Return flag
    End Function

    Public Function RpaSchedulerUpdate(ByVal queryParams As RpaSchedulerModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim flag As Boolean = True
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "UPDATE RPA_SCHEDULER "
        sqlStr = sqlStr & "set  "

        If Not String.IsNullOrEmpty(queryParams.TaskName) Then
            sqlStr = sqlStr & "TASK_NAME = @TaskName, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TaskName", queryParams.TaskName))
        End If

        If Not String.IsNullOrEmpty(queryParams.TaskSource) Then
            sqlStr = sqlStr & "TASK_SOURCE = @TaskSource, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TaskSource", queryParams.TaskSource))
        End If

        If Not String.IsNullOrEmpty(queryParams.StartDateTime) Then
            sqlStr = sqlStr & "START_DATETIME = @StartDateTime, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@StartDateTime", queryParams.StartDateTime))
        End If

        If Not String.IsNullOrEmpty(queryParams.SchedulerName) Then
            sqlStr = sqlStr & "SCHEDULER_NAME = @SchedulerName, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SchedulerName", queryParams.SchedulerName))
        End If

        If Not String.IsNullOrEmpty(queryParams.Status) Then
            sqlStr = sqlStr & "Status = @Status "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Status", queryParams.Status))
        End If

        If Not String.IsNullOrEmpty(queryParams.RpaSchId) Then
            sqlStr = sqlStr & "WHERE RPASCHID = @RpaSchId "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RpaSchId", queryParams.RpaSchId))
        End If

        flag = dbConn.ExecSQL(sqlStr)
        dbConn.CloseConnection()
        Return flag
    End Function

    Public Function RpaSchedulerDelete(ByVal queryParams As RpaSchedulerModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim flag As Boolean = True
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "DELETE FROM RPA_SCHEDULER WHERE RPASCHID=@RpaSchId"

        If Not String.IsNullOrEmpty(queryParams.RpaSchId) Then
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RpaSchId", queryParams.RpaSchId))
        End If
        flag = dbConn.ExecSQL(sqlStr)
        dbConn.CloseConnection()
        Return flag
    End Function

    Public Function SelectRpaScheduler(ByVal queryParams As RpaSchedulerModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & " * "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "RPA_SCHEDULER WHERE (DELFG IS NULL OR DELFG=0)  "
        If Not String.IsNullOrEmpty(queryParams.SchedulerName) Then
            sqlStr = sqlStr & "AND SCHEDULER_NAME=@SchedulerName "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SchedulerName", queryParams.SchedulerName))
        End If
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable

    End Function


    Public Function AddScheduler(queryParams As RpaSchedulerModel) As Boolean
        Dim sqlStr As String = ""
        Dim flag As Boolean = True
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn
        sqlStr = "Insert into RPA_SCHEDULER ("
        sqlStr = sqlStr & "SCHEDULER_NAME, "
        sqlStr = sqlStr & "TASK_SOURCE, "
        sqlStr = sqlStr & "START_DATETIME, "
        sqlStr = sqlStr & "END_DATETIME, "
        sqlStr = sqlStr & "TASK_NAME, "
        ' sqlStr = sqlStr & "REPEAT_TIME, "
        sqlStr = sqlStr & "STATUS, "
        sqlStr = sqlStr & "BATCH_FILE "
        sqlStr = sqlStr & " ) "
        sqlStr = sqlStr & " values ( "
        sqlStr = sqlStr & "@SchedulerName, "
        sqlStr = sqlStr & "@TaskSource, "
        sqlStr = sqlStr & "@StartDateTime, "
        sqlStr = sqlStr & "@EndDateTime, "
        sqlStr = sqlStr & "@TaskName, "
        ' sqlStr = sqlStr & "@RepeatTime, "
        sqlStr = sqlStr & "@Status, "
        sqlStr = sqlStr & "@BatchFile"
        sqlStr = sqlStr & " ) "
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SchedulerName", queryParams.SchedulerName))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TaskSource", queryParams.TaskSource))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@StartDateTime", queryParams.StartDateTime))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EndDateTime", queryParams.EndDateTime))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TaskName", queryParams.TaskName))
        '  dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RepeatTime", queryParams.RepeatTime))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Status", queryParams.Status))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BatchFile", queryParams.BatchFile))

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
End Class
