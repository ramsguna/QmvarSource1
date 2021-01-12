Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class RpaStatusControl
    Public Function SelectRpaStatus(ByVal queryParams As RpaStatusModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        'sqlStr = sqlStr & "DELFG,UNQ_NO,UPLOAD_USER,UPLOAD_DATE,SHIP_TO_BRANCH_CODE,SHIP_TO_BRANCH,INVOICE_DATE,INVOICE_NO,LOCAL_INVOICE_NO,DELIVERY_NO,ITEMS,AMOUNT,SGST_UTGST,CGST,IGST,CESS,TAX,TOTAL,GR_STATUS,FILE_NAME"
        'sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE as 'Ship-to-Branch-Code',SHIP_TO_BRANCH as 'Ship-to-Branch',INVOICE_DATE as 'Invoice Date',INVOICE_NO as 'Invoice No',LOCAL_INVOICE_NO as 'Local Invoice No',DELIVERY_NO as 'Delivery No',ITEMS as 'Items',AMOUNT as 'Amount',SGST_UTGST as 'SGST / UTGST',CGST as 'CGST',IGST as 'IGST',CESS as 'Cess',TAX as 'Tax',TOTAL as 'Total',GR_STATUS as 'GR Status' "
        sqlStr = sqlStr & " * "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "RpaLiveStatus "
        sqlStr = sqlStr & "WHERE "

        If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
            sqlStr = sqlStr & "LEFT(CONVERT(VARCHAR, StartDateTime, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, StartDateTime, 111), 10) <= @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
            sqlStr = sqlStr & "LEFT(CONVERT(VARCHAR, StartDateTime, 111), 10) = @DateFrom "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
            sqlStr = sqlStr & "LEFT(CONVERT(VARCHAR, StartDateTime, 111), 10) = @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        End If

        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable

    End Function

    Public Function SelectRpaStatusDetails(ByVal queryParams As RpaStatusModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        'Dim sqlStr As String = "SELECT SeqId,Ssc,TaskName,Status,StartDateTime,EndDateTime,ProcessId,ReRunCount,SrcStatusFlg,SrcStatusText,SrcRecordCount,TargetStatusFlg,TargetStatusText,IpAddress,SrcFileName,CvtFileName,Remarks,ErrorLogs,DelFlg,'' as TargetStatusFlgTxt, ' ' as SrcRecordCountTxt "
        Dim sqlStr As String = "SELECT RpaLiveLog.SeqId as SeqId, RpaLiveLog.Ssc as Ssc, RpaLiveLog.TaskName as TaskName, RpaLiveLog.Status as Status, RpaLiveLog.StartDateTime as StartDateTime, RpaLiveLog.EndDateTime as EndDateTime, RpaLiveLog.ProcessId as ProcessId, RpaLiveLog.ReRunCount as ReRunCount, RpaLiveLog.SrcStatusFlg as SrcStatusFlg, RpaLiveLog.SrcStatusText as SrcStatusText, RpaLiveLog.SrcRecordCount as SrcRecordCount, RpaLiveLog.TargetStatusFlg as TargetStatusFlg, RpaLiveLog.TargetStatusText as TargetStatusText, RpaLiveLog.IpAddress as IpAddress, RpaLiveLog.SrcFileName as SrcFileName, RpaLiveLog.CvtFileName as CvtFileName, RpaLiveLog.Remarks as Remarks, RpaLiveLog.ErrorLogs as ErrorLogs, RpaLiveLog.DelFlg as DelFlg, '' AS TargetStatusFlgTxt, ' ' AS SrcRecordCountTxt, RpaLiveStatus.SCHEDULER_NAME "
        'sqlStr = sqlStr & "DELFG,UNQ_NO,UPLOAD_USER,UPLOAD_DATE,SHIP_TO_BRANCH_CODE,SHIP_TO_BRANCH,INVOICE_DATE,INVOICE_NO,LOCAL_INVOICE_NO,DELIVERY_NO,ITEMS,AMOUNT,SGST_UTGST,CGST,IGST,CESS,TAX,TOTAL,GR_STATUS,FILE_NAME"
        'sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE as 'Ship-to-Branch-Code',SHIP_TO_BRANCH as 'Ship-to-Branch',INVOICE_DATE as 'Invoice Date',INVOICE_NO as 'Invoice No',LOCAL_INVOICE_NO as 'Local Invoice No',DELIVERY_NO as 'Delivery No',ITEMS as 'Items',AMOUNT as 'Amount',SGST_UTGST as 'SGST / UTGST',CGST as 'CGST',IGST as 'IGST',CESS as 'Cess',TAX as 'Tax',TOTAL as 'Total',GR_STATUS as 'GR Status' "
        'sqlStr = sqlStr & " * "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "RpaLiveLog INNER JOIN RpaLiveStatus ON RpaLiveLog.ProcessId = RpaLiveStatus.ProcessId "
        sqlStr = sqlStr & "WHERE "
        If Not String.IsNullOrEmpty(queryParams.ProcessId) Then
            sqlStr = sqlStr & " RpaLiveLog.ProcessId = @ProcessId "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ProcessId", queryParams.ProcessId))
        End If
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable

    End Function

    Public Function UpdateRpaReRun(ByVal queryParams As RpaStatusModel) As String
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "update RPA_SCHEDULER "
        sqlStr = sqlStr & " set status='Run Underprocess...' "
        sqlStr = sqlStr & "WHERE "
        If Not String.IsNullOrEmpty(queryParams.SchedulerName) Then
            sqlStr = sqlStr & " SCHEDULER_NAME = @SchedulerName "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SchedulerName", queryParams.SchedulerName))
        End If
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return "Success"

    End Function
End Class
