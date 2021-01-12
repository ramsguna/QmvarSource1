Imports System.IO
Imports System.Text
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class RpaTaskMstControl
    Public Function SelectRpaTask() As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & " TASKID, TASK_NAME, FILE_NAME, PATH "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "RPA_TASK_MST WHERE DELFG=0 ORDER BY TASK_NAME"

        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable

    End Function
End Class
