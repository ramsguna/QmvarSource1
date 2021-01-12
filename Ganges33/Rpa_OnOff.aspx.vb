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
Public Class Rpa_OnOff
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If
        End If
    End Sub

    Protected Sub btnSuspendTask_Click(sender As Object, e As EventArgs) Handles btnSuspendTask.Click
        Dim _RpaOnOffControl As RpaOnOffControl = New RpaOnOffControl()
        Dim dtRpaTask As DataTable = _RpaOnOffControl.SelectRpaTask()
        If (dtRpaTask Is Nothing) Or (dtRpaTask.Rows.Count = 0) Then
            gvDetails.DataSource = Nothing
            gvDetails.DataBind()
            gvDetails.Visible = False
            btnUpdate.Visible = False
            btnUpdate.Text = ""

            lblInfo.Text = "<font color=red>No Task is running...</font>"
        Else
            gvDetails.DataSource = dtRpaTask
            gvDetails.DataBind()
            gvDetails.Visible = True
            lblInfo.Text = ""
            btnUpdate.Visible = True
            btnUpdate.Text = "Update Tasks Suspend"
        End If
    End Sub

    'Protected Sub gvDetails_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
    '    gvDetails.PageIndex = e.NewPageIndex
    '    BindGridview()
    'End Sub

    Protected Sub BindGridview()
        ' Dim _RpaStatusModel As RpaStatusModel = New RpaStatusModel()

    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Select Case btnUpdate.Text
            Case "Update Tasks Suspend"
                Dim strSsc As String = ""
                lblInfo.Text = ""
                Dim chkSelect As Boolean = False
                Dim RpaStatus As List(Of RpaSchTskModel) = New List(Of RpaSchTskModel)()
                Dim i As Integer = 0
                For Each gvrow As GridViewRow In gvDetails.Rows
                    Dim checkbox = TryCast(gvrow.FindControl("chk"), CheckBox)
                    If checkbox.Checked Then
                        chkSelect = True
                        Dim Rpa As RpaSchTskModel = New RpaSchTskModel()
                        Dim lblId = TryCast(gvrow.FindControl("lblId"), Label)
                        Dim lblStatus = TryCast(gvrow.FindControl("lblStatus"), Label)
                        Rpa.Id = lblId.Text
                        Rpa.Column1 = lblStatus.Text
                        RpaStatus.Add(Rpa)
                    End If
                    i = i + 1
                Next
                If chkSelect Then
                    Dim _RpaOnOffModel As RpaOnOffModel = New RpaOnOffModel()
                    Dim _RpaOnOffControl As RpaOnOffControl = New RpaOnOffControl()
                    chkSelect = _RpaOnOffControl.UpdateRpaTaskStatus(RpaStatus)

                    If chkSelect Then
                        lblInfo.Text = "Successfully Updated..."
                    End If
                End If
            Case "Update Schedulers Suspend"
                Dim strSsc As String = ""
                lblInfo.Text = ""
                Dim chkSelect As Boolean = False
                Dim RpaStatus As List(Of RpaSchTskModel) = New List(Of RpaSchTskModel)()
                Dim i As Integer = 0
                For Each gvrow As GridViewRow In gvDetails.Rows
                    Dim checkbox = TryCast(gvrow.FindControl("chk"), CheckBox)
                    If checkbox.Checked Then
                        chkSelect = True
                        Dim Rpa As RpaSchTskModel = New RpaSchTskModel()
                        Dim lblId = TryCast(gvrow.FindControl("lblId"), Label)
                        Dim lblStatus = TryCast(gvrow.FindControl("lblStatus"), Label)
                        Rpa.Id = lblId.Text
                        Rpa.Column1 = lblStatus.Text
                        RpaStatus.Add(Rpa)
                    End If
                    i = i + 1
                Next
                If chkSelect Then
                    Dim _RpaOnOffModel As RpaOnOffModel = New RpaOnOffModel()
                    Dim _RpaOnOffControl As RpaOnOffControl = New RpaOnOffControl()
                    chkSelect = _RpaOnOffControl.UpdateRpaSchedulerStatus(RpaStatus)

                    If chkSelect Then
                        lblInfo.Text = "Successfully Updated..."
                    End If
                End If


            Case Else

        End Select
    End Sub

    Protected Sub btnSuspendScheduler_Click(sender As Object, e As EventArgs) Handles btnSuspendScheduler.Click
        Dim _RpaOnOffControl As RpaOnOffControl = New RpaOnOffControl()
        Dim dtRpaTask As DataTable = _RpaOnOffControl.SelectRpaScheduler()
        If (dtRpaTask Is Nothing) Or (dtRpaTask.Rows.Count = 0) Then
            gvDetails.DataSource = Nothing
            gvDetails.DataBind()
            gvDetails.Visible = False
            btnUpdate.Visible = False
            btnUpdate.Text = ""

            lblInfo.Text = "<font color=red>No Scheduler is running...</font>"
        Else
            gvDetails.DataSource = dtRpaTask
            gvDetails.DataBind()
            gvDetails.Visible = True
            lblInfo.Text = ""
            btnUpdate.Visible = True
            btnUpdate.Text = "Update Schedulers Suspend"
        End If
    End Sub

    Protected Sub btnDisableAutoChecker_Click(sender As Object, e As EventArgs) Handles btnDisableAutoChecker.Click
        'Diable All AutoChecker
        'Variable Declartion
        Dim strRootDirectory As String = HostingEnvironment.MapPath("~/")
        Dim strPath As String = strRootDirectory & "\rpa\scheduler\queue"
        'Dim strBatchFile As String = DateTime.Now.ToString("yyyyMMddHHmmss") & ".bat"
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
            'sw.Write("echo disabled >> " & lblSCHEDULER_NAME.Text & ".txt ") 'rpa\scheduler\queue => Saved
            'sw.Write(vbCrLf)
            'sw.Write("echo %date%-%time% >> " & lblSCHEDULER_NAME.Text & ".txt ") 'rpa\scheduler\queue => Saved
            'Next Line
            'sw.Write(vbCrLf)
            'Sheduler Command
            sw.Write("schtasks ")
                'Action =>Create
                sw.Write("/Change ")
            'TaskName -
            sw.Write("/tn " & Chr(34) & "AutoCheck" & Chr(34) & " ")
            'Disable
            sw.Write("/DISABLE ")
                '********************
                'Change of schedule is neccessary username and password to change the scheduler
                '*********************
                ' Run with highest privileges
                '/RU username /RP password /RL HIGHEST
                'If UCase(ConfigurationManager.AppSettings("RpaWindowShow")) = "TRUE" Then
                sw.Write("/RU " & ConfigurationManager.AppSettings("RpaU") & " /RP " & ConfigurationManager.AppSettings("RpaP") & " ")
            'End If
            sw.Write(">> " & strRootDirectory & "\rpa\scheduler\log\queue.txt ")
            sw.Write(vbCrLf)
            sw.Write("schtasks ")
            'Action =>Create
            sw.Write("/Change ")
            'TaskName -
            sw.Write("/tn " & Chr(34) & "AutoCheck2" & Chr(34) & " ")
            'Disable
            sw.Write("/DISABLE ")
            '********************
            'Change of schedule is neccessary username and password to change the scheduler
            '*********************
            ' Run with highest privileges
            '/RU username /RP password /RL HIGHEST
            'If UCase(ConfigurationManager.AppSettings("RpaWindowShow")) = "TRUE" Then
            sw.Write("/RU " & ConfigurationManager.AppSettings("RpaU") & " /RP " & ConfigurationManager.AppSettings("RpaP") & " ")
            'End If
            sw.Write(">> " & strRootDirectory & "\rpa\scheduler\log\queue.txt ")
            sw.Write(vbCrLf)
            sw.Write("schtasks ")
            'Action =>Create
            sw.Write("/Change ")
            'TaskName -
            sw.Write("/tn " & Chr(34) & "AutoCheck3" & Chr(34) & " ")
            'Disable
            sw.Write("/DISABLE ")
            '********************
            'Change of schedule is neccessary username and password to change the scheduler
            '*********************
            ' Run with highest privileges
            '/RU username /RP password /RL HIGHEST
            'If UCase(ConfigurationManager.AppSettings("RpaWindowShow")) = "TRUE" Then
            sw.Write("/RU " & ConfigurationManager.AppSettings("RpaU") & " /RP " & ConfigurationManager.AppSettings("RpaP") & " ")
            'End If
            sw.Write(">> " & strRootDirectory & "\rpa\scheduler\log\queue.txt ")
            sw.Write(vbCrLf)
            sw.Write("schtasks ")
            'Action =>Create
            sw.Write("/Change ")
            'TaskName -
            sw.Write("/tn " & Chr(34) & "AutoCheck4" & Chr(34) & " ")
            'Disable
            sw.Write("/DISABLE ")
            '********************
            'Change of schedule is neccessary username and password to change the scheduler
            '*********************
            ' Run with highest privileges
            '/RU username /RP password /RL HIGHEST
            'If UCase(ConfigurationManager.AppSettings("RpaWindowShow")) = "TRUE" Then
            sw.Write("/RU " & ConfigurationManager.AppSettings("RpaU") & " /RP " & ConfigurationManager.AppSettings("RpaP") & " ")
            'End If
            sw.Write(">> " & strRootDirectory & "\rpa\scheduler\log\queue.txt ")
            sw.Write(vbCrLf)
            sw.Write("schtasks ")
            'Action =>Create
            sw.Write("/Change ")
            'TaskName -
            sw.Write("/tn " & Chr(34) & "AutoCheck5" & Chr(34) & " ")
            'Disable
            sw.Write("/DISABLE ")
            '********************
            'Change of schedule is neccessary username and password to change the scheduler
            '*********************
            ' Run with highest privileges
            '/RU username /RP password /RL HIGHEST
            'If UCase(ConfigurationManager.AppSettings("RpaWindowShow")) = "TRUE" Then
            sw.Write("/RU " & ConfigurationManager.AppSettings("RpaU") & " /RP " & ConfigurationManager.AppSettings("RpaP") & " ")
            'End If
            sw.Write(">> " & strRootDirectory & "\rpa\scheduler\log\queue.txt ")
            sw.Close()

        End Using

        lblInfo.Text = "<font color=green>Scheduler disable is underprocess...</font>"
    End Sub

    Protected Sub btnEnableAutoChecker_Click(sender As Object, e As EventArgs) Handles btnEnableAutoChecker.Click
        'Diable All AutoChecker
        'Variable Declartion
        Dim strRootDirectory As String = HostingEnvironment.MapPath("~/")
        Dim strPath As String = strRootDirectory & "\rpa\scheduler\queue"
        'Dim strBatchFile As String = DateTime.Now.ToString("yyyyMMddHHmmss") & ".bat"
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
            'sw.Write("echo disabled >> " & lblSCHEDULER_NAME.Text & ".txt ") 'rpa\scheduler\queue => Saved
            'sw.Write(vbCrLf)
            'sw.Write("echo %date%-%time% >> " & lblSCHEDULER_NAME.Text & ".txt ") 'rpa\scheduler\queue => Saved
            'Next Line
            'sw.Write(vbCrLf)
            'Sheduler Command
            sw.Write("schtasks ")
            'Action =>Create
            sw.Write("/Change ")
            'TaskName -
            sw.Write("/tn " & Chr(34) & "AutoCheck" & Chr(34) & " ")
            'Disable
            sw.Write("/ENABLE ")
            '********************
            'Change of schedule is neccessary username and password to change the scheduler
            '*********************
            ' Run with highest privileges
            '/RU username /RP password /RL HIGHEST
            'If UCase(ConfigurationManager.AppSettings("RpaWindowShow")) = "TRUE" Then
            sw.Write("/RU " & ConfigurationManager.AppSettings("RpaU") & " /RP " & ConfigurationManager.AppSettings("RpaP") & " ")
            'End If
            sw.Write(">> " & strRootDirectory & "\rpa\scheduler\log\queue.txt ")
            sw.Write(vbCrLf)
            sw.Write("schtasks ")
            'Action =>Create
            sw.Write("/Change ")
            'TaskName -
            sw.Write("/tn " & Chr(34) & "AutoCheck2" & Chr(34) & " ")
            'Disable
            sw.Write("/ENABLE ")
            '********************
            'Change of schedule is neccessary username and password to change the scheduler
            '*********************
            ' Run with highest privileges
            '/RU username /RP password /RL HIGHEST
            'If UCase(ConfigurationManager.AppSettings("RpaWindowShow")) = "TRUE" Then
            sw.Write("/RU " & ConfigurationManager.AppSettings("RpaU") & " /RP " & ConfigurationManager.AppSettings("RpaP") & " ")
            'End If
            sw.Write(">> " & strRootDirectory & "\rpa\scheduler\log\queue.txt ")
            sw.Write(vbCrLf)
            sw.Write("schtasks ")
            'Action =>Create
            sw.Write("/Change ")
            'TaskName -
            sw.Write("/tn " & Chr(34) & "AutoCheck3" & Chr(34) & " ")
            'Disable
            sw.Write("/ENABLE ")
            '********************
            'Change of schedule is neccessary username and password to change the scheduler
            '*********************
            ' Run with highest privileges
            '/RU username /RP password /RL HIGHEST
            'If UCase(ConfigurationManager.AppSettings("RpaWindowShow")) = "TRUE" Then
            sw.Write("/RU " & ConfigurationManager.AppSettings("RpaU") & " /RP " & ConfigurationManager.AppSettings("RpaP") & " ")
            'End If
            sw.Write(">> " & strRootDirectory & "\rpa\scheduler\log\queue.txt ")
            sw.Write(vbCrLf)
            sw.Write("schtasks ")
            'Action =>Create
            sw.Write("/Change ")
            'TaskName -
            sw.Write("/tn " & Chr(34) & "AutoCheck4" & Chr(34) & " ")
            'Disable
            sw.Write("/ENABLE ")
            '********************
            'Change of schedule is neccessary username and password to change the scheduler
            '*********************
            ' Run with highest privileges
            '/RU username /RP password /RL HIGHEST
            'If UCase(ConfigurationManager.AppSettings("RpaWindowShow")) = "TRUE" Then
            sw.Write("/RU " & ConfigurationManager.AppSettings("RpaU") & " /RP " & ConfigurationManager.AppSettings("RpaP") & " ")
            'End If
            sw.Write(">> " & strRootDirectory & "\rpa\scheduler\log\queue.txt ")
            sw.Write(vbCrLf)
            sw.Write("schtasks ")
            'Action =>Create
            sw.Write("/Change ")
            'TaskName -
            sw.Write("/tn " & Chr(34) & "AutoCheck5" & Chr(34) & " ")
            'Disable
            sw.Write("/ENABLE ")
            '********************
            'Change of schedule is neccessary username and password to change the scheduler
            '*********************
            ' Run with highest privileges
            '/RU username /RP password /RL HIGHEST
            'If UCase(ConfigurationManager.AppSettings("RpaWindowShow")) = "TRUE" Then
            sw.Write("/RU " & ConfigurationManager.AppSettings("RpaU") & " /RP " & ConfigurationManager.AppSettings("RpaP") & " ")
            'End If
            sw.Write(">> " & strRootDirectory & "\rpa\scheduler\log\queue.txt ")
            sw.Close()

        End Using

        lblInfo.Text = "<font color=green>Scheduler enable is underprocess...</font>"
    End Sub
End Class