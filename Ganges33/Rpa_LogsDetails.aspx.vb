Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Xml
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Web.Hosting
Imports System.IO
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class Rpa_LogsDetails
    Inherits System.Web.UI.Page
    'Private conn As SqlConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("cnstr").ToString())

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If
            btnClose.Attributes.Add("OnClick", "window.close();")
            BindGridview()
        End If

    End Sub

    Protected Sub BindGridview()
        'Dim ds As New DataSet()
        'Using con As New SqlConnection(WebConfigurationManager.ConnectionStrings("cnstr").ToString())
        '    con.Open()
        '    Dim cmd As New SqlCommand("select * from RpaLiveLog where ProcessId='" & Request.QueryString("id") & "'", con)
        '    Dim da As New SqlDataAdapter(cmd)
        '    da.Fill(ds)
        '    con.Close()
        '    gvDetails.DataSource = ds
        '    gvDetails.DataBind()


        'End Using


        Dim _RpaStatusModel As RpaStatusModel = New RpaStatusModel()
        Dim _RpaStatusControl As RpaStatusControl = New RpaStatusControl()
        _RpaStatusModel.ProcessId = Request.QueryString("id")
        Dim dtRpaStatus As DataTable = _RpaStatusControl.SelectRpaStatusDetails(_RpaStatusModel)
        If (dtRpaStatus Is Nothing) Or (dtRpaStatus.Rows.Count = 0) Then
            gvDetails.DataSource = Nothing
            gvDetails.DataBind()
            gvDetails.Visible = False
            lblInfo.Text = "<font color=red>There is no transaction log found (" & _RpaStatusModel.ProcessId & ")</font>"
        Else

            For Each dr As DataRow In dtRpaStatus.Rows
                If dr("SrcStatusFlg") = -99 Then
                    dr("SrcStatusText") = "<span style=color:darkgray>NotExecuted</span>"
                ElseIf dr("SrcStatusFlg") = 0 Then
                    dr("SrcStatusText") = "<span style=color:green>Downloaded</span>"
                ElseIf dr("SrcStatusFlg") = 1 Then
                    dr("SrcStatusText") = "<span style=color:midnightblue>NoData Found</span>"
                End If

                If dr("TargetStatusFlg") = -99 Then
                    dr("TargetStatusFlgTxt") = "<span style=color:darkgray>NotExecuted</span>"
                ElseIf dr("TargetStatusFlg") = 0 Then
                    dr("TargetStatusFlgTxt") = "<span style=color:green>Uploaded</span>"
                ElseIf dr("TargetStatusFlg") = 1 Then
                    dr("TargetStatusFlgTxt") = "<span style=color:red>NotUploaded</span>"
                End If

                If dr("SrcRecordCount") = -99 Then
                    dr("SrcRecordCountTxt") = "<span style=color:darkgray>NotExecuted</span>"
                Else
                    dr("SrcRecordCountTxt") = dr("SrcRecordCount")
                End If

                If dr("Status") = "Success" Then
                    dr("Status") = "<span style=color:green>Success</span>"
                Else
                    dr("Status") = "<span style=color:red>Failure</span>"
                End If

            Next

            gvDetails.DataSource = dtRpaStatus
            gvDetails.DataBind()
            gvDetails.Visible = True
            lblInfo.Text = ""
        End If




    End Sub
    Protected Sub gvDetails_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvDetails.PageIndex = e.NewPageIndex
        BindGridview()
    End Sub

    Protected Sub btnReRun_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim strSsc As String = ""
        Dim strTaskName As String = ""
        Dim strSchedulerName As String = ""

        lblInfo.Text = ""

        For Each gvrow As GridViewRow In gvDetails.Rows
            Dim checkbox = TryCast(gvrow.FindControl("chkReRun"), CheckBox)
            If checkbox.Checked Then
                Dim lblSsc = TryCast(gvrow.FindControl("lblSsc"), Label)
                Dim lblTaskName = TryCast(gvrow.FindControl("lblTaskName"), Label)
                Dim lblSchedulerName = TryCast(gvrow.FindControl("lblSchedulerName"), Label)
                strTaskName = lblTaskName.Text
                strSchedulerName = lblSchedulerName.Text
                strSsc = lblSsc.Text & ","
            End If
        Next

        If Len(strSsc) > 3 Then
            'Remove Comman End
            strSsc = Left(strSsc, Len(strSsc) - 1)
            'Variable Declartion
            Dim strRootDirectory As String = HostingEnvironment.MapPath("~/")
            Dim strPath As String = strRootDirectory & "\rpa\scheduler\rerun"
            Dim fileNameBas As String = strPath & "\" & strSchedulerName & ".txt"
            Dim fileMasterBatch As FileInfo = New FileInfo(fileNameBas)
            If File.Exists(fileNameBas) Then
                File.Delete(fileNameBas)
            End If
            Using sw As StreamWriter = File.CreateText(fileNameBas)
                'sw.WriteLine("")
            End Using

            Using sw As StreamWriter = fileMasterBatch.AppendText()
                'SSC
                sw.Write(strSsc)
                sw.Close()
            End Using
            'ReRun
            ReRun(strSchedulerName, strTaskName)

            lblInfo.Text = "ReRun Task has been assigned and it is underprocess..."
            btnReRun.Visible = False

        Else
            lblInfo.Text = "Select the checkbox of SSC to ReRun"
        End If

    End Sub

    Private Sub ReRun(strSchedulerName As String, strTaskName As String)
        'Variable Declartion
        Dim strRootDirectory As String = HostingEnvironment.MapPath("~/")
        Dim strPath As String = strRootDirectory & "\rpa\scheduler\queue"
        Dim strBatchFile As String = DateTime.Now.ToString("yyyyMMddHHmmss") & ".bat"
        Dim fileNameBas As String = strPath & "\" & "queue.bat"
        Dim fileMasterBatch As FileInfo = New FileInfo(fileNameBas)
        If Not File.Exists(fileNameBas) Then
            Using sw As StreamWriter = File.CreateText(fileNameBas)
                'sw.WriteLine("")
            End Using
        End If

        Using sw As StreamWriter = fileMasterBatch.AppendText()
            'System DateTime
            sw.Write("echo  %date%-%time% >> " & strRootDirectory & "\rpa\scheduler\log\queue.txt ")
            sw.Write(vbCrLf)


            sw.Write("echo run >> " & strSchedulerName & ".txt ")   'rpa\scheduler\queue  =>Saved
            sw.Write(vbCrLf)
            sw.Write("echo %date%-%time% >> " & strSchedulerName & ".txt ") 'rpa\scheduler\queue =>Saved

            'Next Line
            sw.Write(vbCrLf)
            'Sheduler Command
            sw.Write("schtasks ")
            'Action =>Run
            sw.Write("/Run ")
            'TaskName -
            sw.Write("/tn " & Chr(34) & strSchedulerName & Chr(34) & " ")
            'Write to Log
            sw.Write(">> " & strRootDirectory & "\rpa\scheduler\log\queue.txt ")
            sw.Write(vbCrLf)
            sw.Close()

        End Using

        '    conn.Open()
        '    Dim cmdstr As String = "update RPA_SCHEDULER set status='Run Underprocess...' where TASK_NAME=@TASK_NAME"
        '    Dim cmd As SqlCommand = New SqlCommand(cmdstr, conn)
        'cmd.Parameters.AddWithValue("@TASK_NAME", "drs")
        'cmd.ExecuteNonQuery()
        '    conn.Close()

        '
        Dim _RpaStatusModel As RpaStatusModel = New RpaStatusModel()
        Dim _RpaStatusControl As RpaStatusControl = New RpaStatusControl()
        _RpaStatusModel.SchedulerName = strSchedulerName
        Dim strResult = _RpaStatusControl.UpdateRpaReRun(_RpaStatusModel)
    End Sub




End Class