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

Public Class rpa_scheduler
    Inherits System.Web.UI.Page
    Private conn As SqlConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("cnstr").ToString())
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'When page is loading, find out that task has been completed or underprocess
        '*****************************
        'All Task
        '*****************************
        Dim _RpaSchedulerModel As RpaSchedulerModel = New RpaSchedulerModel()
        Dim _RpaSchedulerControl As RpaSchedulerControl = New RpaSchedulerControl()
        Dim dbTranStatus As Boolean = False
        Dim strRootDirectory As String = HostingEnvironment.MapPath("~/")

        Dim dtScheduler As DataTable = _RpaSchedulerControl.SelectRpaScheduler(_RpaSchedulerModel)
        If dtScheduler.Rows.Count > 0 Then
            'Step1 
            'Check 
            'DRS Run / End Status  ====================>\rpa\temp\drs.txt
            Dim strStatus As String = ""
            Dim strDateTime As String = ""
            Dim i As Integer = 0
            Dim strPath As String

            For Each dr As DataRow In dtScheduler.Rows
                'For DRS / All Task 
                'Status Processing & Ready Status
                If dr("SCHEDULER_NAME") IsNot DBNull.Value Then
                    strPath = strRootDirectory & "\rpa\temp\" & dr("SCHEDULER_NAME") & ".txt"
                    If System.IO.File.Exists(strPath) = True Then
                        Dim objReader As New System.IO.StreamReader(strPath)
                        Do While objReader.Peek() <> -1
                            i = i + 1
                            If IsOdd(i) Then
                                strStatus = objReader.ReadLine()
                            End If
                            If IsEven(i) Then
                                strDateTime = objReader.ReadLine()
                                strDateTime = Trim(strDateTime.Replace("-", " "))
                            End If
                            ''''''''''If i = 3 Then
                            ''''''''''    Exit Do
                            ''''''''''End If
                            'TextLine = TextLine & objReader.ReadLine() & vbNewLine
                        Loop
                        objReader.Close()
                        'Update to Database
                        strStatus = LCase(Trim(strStatus))
                        If (strStatus = "start") Or (strStatus = "end") Then
                            If strStatus = "start" Then
                                _RpaSchedulerModel.Status = "Processing"
                                _RpaSchedulerModel.LastRunStatus = "Started"
                            ElseIf strStatus = "end" Then
                                _RpaSchedulerModel.Status = "Ready"
                                _RpaSchedulerModel.LastRunStatus = "Completed"
                            End If
                            _RpaSchedulerModel.LastRunDateTime = strDateTime
                            _RpaSchedulerModel.TaskName = dr("TASK_NAME")
                            _RpaSchedulerModel.SchedulerName = dr("SCHEDULER_NAME")
                            'Update Start /end Status to db
                            dbTranStatus = _RpaSchedulerControl.RpaSchedulerStatusUpdate(_RpaSchedulerModel)
                        End If
                        'Delete the file
                        System.IO.File.Delete(strPath)
                    End If



                    'Reinstailaize 
                    i = 0
                    strStatus = ""
                    strDateTime = ""
                    'DRS disabled / Run Started / Activated Scheduler ====================>\rpa\scheduler\queue\drs.txt
                    'Verify Scheduler updation
                    strPath = strRootDirectory & "\rpa\scheduler\queue\" & dr("SCHEDULER_NAME") & ".txt"
                    If System.IO.File.Exists(strPath) = True Then
                        Dim objReader As New System.IO.StreamReader(strPath)
                        Do While objReader.Peek() <> -1
                            i = i + 1
                            If IsOdd(i) Then
                                strStatus = objReader.ReadLine()
                            End If
                            If IsEven(i) Then
                                strDateTime = objReader.ReadLine()
                                strDateTime = Trim(strDateTime.Replace("-", " "))
                            End If
                            '''''''''If i = 3 Then
                            '''''''''    Exit Do
                            '''''''''End If
                            'TextLine = TextLine & objReader.ReadLine() & vbNewLine
                        Loop
                        objReader.Close()

                        strStatus = LCase(Trim(strStatus))
                        If strStatus = "disabled" Then
                            strStatus = "Disabled"
                        ElseIf strStatus = "run" Then
                            strStatus = "Run Started..."
                        Else
                            strStatus = "Ready"
                        End If
                        'Update to Database
                        _RpaSchedulerModel.Status = strStatus
                        _RpaSchedulerModel.TaskName = dr("TASK_NAME")
                        _RpaSchedulerModel.SchedulerName = dr("SCHEDULER_NAME")
                        'Update Start /end Status to db
                        dbTranStatus = _RpaSchedulerControl.RpaSchedulerStatusUpdate(_RpaSchedulerModel)
                        'Delete the file
                        System.IO.File.Delete(strPath)
                    End If
                End If
            Next


        Else
            'There is no Task in the scheduler
        End If

        If Not IsPostBack Then
            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If
            BindData()
        End If
    End Sub

    Function IsOdd(ByVal number As Integer) As Boolean
        ' Handle negative numbers by returning the opposite of IsEven.
        Return IsEven(number) = False
    End Function
    Function IsEven(ByVal number As Integer) As Boolean
        ' Handles all numbers because it tests for 0 remainder.
        ' ... This works for negative and positive numbers.
        Return number Mod 2 = 0
    End Function
    Protected Sub BindData()
        Dim _RpaSchedulerModel As RpaSchedulerModel = New RpaSchedulerModel()
        Dim _RpaSchedulerControl As RpaSchedulerControl = New RpaSchedulerControl()
        Dim FromTable As DataTable = _RpaSchedulerControl.SelectRpaScheduler(_RpaSchedulerModel)
        If FromTable.Rows.Count > 0 Then
            For Each dr As DataRow In FromTable.Rows
                If dr("status") = "Processing" Then
                    dr("status") = "<font color=green>Processing</font><progress></progress>"
                End If
            Next
            gvRPADetails.DataSource = FromTable
            gvRPADetails.DataBind()
            'Add New Task - ByDefault Select All The Checkbox
            Dim chkLstAddSsc As CheckBoxList = CType(gvRPADetails.FooterRow.FindControl("chkLstAddSsc"), CheckBoxList)
            For Each item As ListItem In chkLstAddSsc.Items
                item.Selected = True
            Next

            Dim strSource As String = ""
            Dim text As String = ""

            For Each gvr As GridViewRow In gvRPADetails.Rows
                Dim lblTASK_SOURCE As Label = gvr.FindControl("lblTASK_SOURCE")
                If lblTASK_SOURCE Is Nothing Then 'If Edit Mode
                    Dim txtEditTASK_SOURCE As TextBox = gvr.FindControl("txtEditTASK_SOURCE")
                    strSource = txtEditTASK_SOURCE.Text
                    Dim chkLstSsc As CheckBoxList = gvr.FindControl("chkLstEditSsc")
                    For i As Integer = 0 To chkLstSsc.Items.Count - 1
                        If strSource.Contains(chkLstSsc.Items(i).Text) Then
                            chkLstSsc.Items(i).Selected = True
                        End If
                    Next
                    'For Task Name Dropdownlist
                    Dim txtEditTASK_NAME As TextBox = gvr.FindControl("txtEditTASK_NAME")
                    Dim drpEditTASK_NAME As DropDownList = gvr.FindControl("drpEditTASK_NAME")
                    text = txtEditTASK_NAME.Text
                    Dim item = drpEditTASK_NAME.Items.FindByText(text)
                    If item IsNot Nothing Then item.Selected = True
                Else 'If page load
                    strSource = lblTASK_SOURCE.Text
                    Dim chkLstSsc As CheckBoxList = gvr.FindControl("chkLstSsc")
                    For i As Integer = 0 To chkLstSsc.Items.Count - 1
                        chkLstSsc.Items(i).Enabled = False
                        If strSource.Contains(chkLstSsc.Items(i).Text) Then
                            chkLstSsc.Items(i).Selected = True
                        End If
                    Next
                    'For Task Name Dropdownlist
                    Dim lblTASK_NAME As Label = gvr.FindControl("lblTASK_NAME")
                    Dim drpTASK_NAME As DropDownList = gvr.FindControl("drpTASK_NAME")
                    text = lblTASK_NAME.Text
                    'By Default 
                    Dim item = drpTASK_NAME.Items.FindByText(text)
                    If item IsNot Nothing Then item.Selected = True
                End If
            Next
        Else
            FromTable.Rows.Add(FromTable.NewRow())
            gvRPADetails.DataSource = FromTable
            gvRPADetails.DataBind()
            Dim TotalColumns As Integer = gvRPADetails.Rows(0).Cells.Count
            gvRPADetails.Rows(0).Cells.Clear()
            gvRPADetails.Rows(0).Cells.Add(New TableCell())
            gvRPADetails.Rows(0).Cells(0).ColumnSpan = TotalColumns
            gvRPADetails.Rows(0).Cells(0).Text = "No records Found"

            Dim chkLstAddSsc As CheckBoxList = CType(gvRPADetails.FooterRow.FindControl("chkLstAddSsc"), CheckBoxList)
            For Each item As ListItem In chkLstAddSsc.Items
                item.Selected = True
            Next
        End If
    End Sub

    Protected Sub gvRPADetails_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim lblRPASCHID As Label = CType(gvRPADetails.Rows(e.RowIndex).FindControl("lblRPASCHID"), Label) 'gvRPADetails.FooterRow.FindControl("txtAddTASK_NAME")
        Dim lblTASK_NAME As Label = CType(gvRPADetails.Rows(e.RowIndex).FindControl("lblTASK_NAME"), Label)
        Dim lblSCHEDULER_NAME As Label = CType(gvRPADetails.Rows(e.RowIndex).FindControl("lblSCHEDULER_NAME"), Label)
        Dim lblBATCH_FILE As Label = CType(gvRPADetails.Rows(e.RowIndex).FindControl("lblBATCH_FILE"), Label)
        Dim _RpaSchedulerModel As RpaSchedulerModel = New RpaSchedulerModel()
        Dim _RpaSchedulerControl As RpaSchedulerControl = New RpaSchedulerControl()
        Dim dbTranStatus As Boolean = False
        'Update to Database
        _RpaSchedulerModel.RpaSchId = lblRPASCHID.Text
        'Update Start /end Status to db
        dbTranStatus = _RpaSchedulerControl.RpaSchedulerDelete(_RpaSchedulerModel)
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
            'Next Line
            sw.Write(vbCrLf)
            'Sheduler Command
            sw.Write("schtasks ")
            'Action =>Create
            sw.Write("/delete ")
            'TaskName -
            sw.Write("/tn " & Chr(34) & lblSCHEDULER_NAME.Text & Chr(34) & " /f ")
            'Write to Log
            sw.Write(">> " & strRootDirectory & "\rpa\scheduler\log\queue.txt ")
            sw.Write(vbCrLf)
            sw.Close()
        End Using
        'Deleting Batch File
        Dim fileN As String = strRootDirectory & "\rpa\tasks\" & lblBATCH_FILE.Text
        If File.Exists(fileN) Then
            File.Delete(fileN)
        End If
        'Deleting XML FILE EXIST
        Dim fileNx As String = strRootDirectory & "\rpa\tasks\" & lblBATCH_FILE.Text & ".xml"
        If File.Exists(fileNx) Then
            File.Delete(fileNx)
        End If
        'Deleting Run SSC List
        fileN = strRootDirectory & "\rpa\scheduler\run\" & lblSCHEDULER_NAME.Text & ".txt"
        If File.Exists(fileN) Then
            File.Delete(fileN)
        End If
        'Deleting ReRun SSC List
        fileN = strRootDirectory & "\rpa\scheduler\rerun\" & lblSCHEDULER_NAME.Text & ".txt"
        If File.Exists(fileN) Then
            File.Delete(fileN)
        End If


        '  sw.Write(">> log.txt ")
        BindData()
    End Sub

    Protected Sub gvRPADetails_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Dim _RpaSchedulerModel As RpaSchedulerModel = New RpaSchedulerModel()
        Dim _RpaSchedulerControl As RpaSchedulerControl = New RpaSchedulerControl()
        Dim dbTranStatus As Boolean = False
        Dim strSsc As String = ""
        Dim strTaskType As String = ""



        If e.CommandName.Equals("ADD") Then
            'Variable Declartion
            Dim strRootDirectory As String = HostingEnvironment.MapPath("~/")
            Dim strPath As String = strRootDirectory & "\rpa\scheduler\queue"
            Dim fileNameBas As String = strPath & "\" & "queue.bat"

            'Update to Database
            Dim txtAddSCHEDULER_NAME As TextBox = CType(gvRPADetails.FooterRow.FindControl("txtAddSCHEDULER_NAME"), TextBox)
            Dim txtAddTASK_SOURCE As TextBox = CType(gvRPADetails.FooterRow.FindControl("txtAddTASK_SOURCE"), TextBox)
            Dim txtAddSTART_DATETIME As TextBox = CType(gvRPADetails.FooterRow.FindControl("txtAddSTART_DATETIME"), TextBox)
            Dim txtAddEND_DATETIME As TextBox = CType(gvRPADetails.FooterRow.FindControl("txtAddEND_DATETIME"), TextBox)
            Dim drpAddTASK_NAME As DropDownList = CType(gvRPADetails.FooterRow.FindControl("drpAddTASK_NAME"), DropDownList)
            Dim strBatchFile As String = drpAddTASK_NAME.SelectedItem.Text & DateTime.Now.ToString("yyyyMMddHHmmss") & ".bat"

            If txtAddSCHEDULER_NAME.Text = "" Then
                Call showMsg("Scheduler Name Is Empty!!! ", "")
                Exit Sub
            End If

            If txtAddSTART_DATETIME.Text = "" Then
                Call showMsg("Scheduler Start Date Time Is Not Valid!!! ", "")
                Exit Sub
            End If


            Dim chkLstAddSsc As CheckBoxList = CType(gvRPADetails.FooterRow.FindControl("chkLstAddSsc"), CheckBoxList)
                Dim IsSelect As Boolean = False
                strSsc = ""
                For i As Integer = 0 To chkLstAddSsc.Items.Count - 1
                    If chkLstAddSsc.Items(i).Selected Then
                        IsSelect = True
                        strSsc = strSsc & chkLstAddSsc.Items(i).Text & ","
                    End If
                Next
            If IsSelect Then
                strSsc = Left(strSsc, Len(strSsc) - 1)
            Else
                Call showMsg("Please Select SSC!!! ", "")
                Exit Sub
            End If


            strSsc = UCase(Trim(strSsc))
                txtAddSCHEDULER_NAME.Text = UCase(txtAddSCHEDULER_NAME.Text)
            _RpaSchedulerModel.SchedulerName = txtAddSCHEDULER_NAME.Text
            Dim dtScheduler As DataTable = _RpaSchedulerControl.SelectRpaScheduler(_RpaSchedulerModel)
            If dtScheduler.Rows.Count > 0 Then
                Call showMsg(txtAddSCHEDULER_NAME.Text & " Scheduler Name Is Already Exist!!! ", "")
                Exit Sub
            End If

            Dim colsSplit() As String
            Dim lnSpl As Integer = 0
            colsSplit = Split(drpAddTASK_NAME.SelectedItem.Text, "_")
            lnSpl = colsSplit.Length
            lnSpl = lnSpl - 1
            strTaskType = colsSplit(lnSpl)


            Dim fileN As String = ""
            'Deleting Run SSC List
            fileN = strRootDirectory & "\rpa\scheduler\run\" & txtAddSCHEDULER_NAME.Text & ".txt"
            If File.Exists(fileN) Then
                File.Delete(fileN)
            End If
            'Deleting ReRun SSC List
            fileN = strRootDirectory & "\rpa\scheduler\rerun\" & txtAddSCHEDULER_NAME.Text & ".txt"
            If File.Exists(fileN) Then
                File.Delete(fileN)
            End If

            If (Trim(txtAddSCHEDULER_NAME.Text <> "")) Then

                'Creating DRS File 
                '1st Delete Existing one if exist
                '2nd Creatiing new file 
                fileN = strRootDirectory & "\rpa\scheduler\run\" & txtAddSCHEDULER_NAME.Text & ".txt"
                Dim fileM As FileInfo = New FileInfo(fileN)
                    If File.Exists(fileN) Then
                        File.Delete(fileN)
                    End If
                    Using sw As StreamWriter = File.CreateText(fileN)
                        sw.Write(strSsc)
                        sw.Close()
                    End Using
                    'BatchFile Creation:


                    'New Task to Database
                    _RpaSchedulerModel.SchedulerName = txtAddSCHEDULER_NAME.Text
                    _RpaSchedulerModel.TaskSource = strSsc
                    _RpaSchedulerModel.StartDateTime = txtAddSTART_DATETIME.Text
                    _RpaSchedulerModel.EndDateTime = txtAddEND_DATETIME.Text
                    _RpaSchedulerModel.TaskName = drpAddTASK_NAME.SelectedItem.Text
                    ' _RpaSchedulerModel.RepeatTime = txtAddREPEAT_TIME.Text
                    _RpaSchedulerModel.Status = "Not Started"
                    _RpaSchedulerModel.BatchFile = strBatchFile
                    dbTranStatus = _RpaSchedulerControl.AddScheduler(_RpaSchedulerModel)

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
                        sw.Write("echo scheduler >> " & txtAddSCHEDULER_NAME.Text & ".txt ") ' rpa\scheduler\queue => Saved
                        sw.Write(vbCrLf)
                        sw.Write("echo %date%-%time% >> " & txtAddSCHEDULER_NAME.Text & ".txt ") ' rpa\scheduler\queue = > Saved

                        'Next Line
                        sw.Write(vbCrLf)
                        'Sheduler Command
                        sw.Write("schtasks ")
                        'Action =>Create
                        sw.Write("/Create ")
                    'TaskName -
                    sw.Write("/tn " & Chr(34) & txtAddSCHEDULER_NAME.Text & Chr(34) & " ")

                    Dim sd As String = Left(txtAddSTART_DATETIME.Text, 10)
                    Dim st As String = Right(txtAddSTART_DATETIME.Text, Len(txtAddSTART_DATETIME.Text) - 10)

                    Select Case UCase(strTaskType)
                        Case "DAILY"
                            'Schedule Frequency
                            sw.Write("/sc daily ")    'For One Minute frequence: =>sw.Write("/sc MINUTE ")   sw.Write("/sc once ") 
                            'Schedule Modifier
                            'sw.Write("/mo 1 ")
                            'Starting Date
                            sw.Write("/sd " & Trim(sd) & " ")
                            'Starting Time
                            sw.Write("/st " & Trim(st) & " ")
                            sw.Write("/tr " & Chr(34) & strRootDirectory & "\rpa\tasks\" & strBatchFile & Chr(34) & " ")
                            ' Run with highest privileges
                            '/RU username /RP password /RL HIGHEST
                            If LCase(ConfigurationManager.AppSettings("RpaWindowShow")) = "true" Then
                                sw.Write("/RU " & ConfigurationManager.AppSettings("RpaU") & " /RP " & ConfigurationManager.AppSettings("RpaP") & " ")
                            End If

                        Case "WEEKLY"
                            'Schedule Frequency
                            sw.Write("/sc weekly ")
                            sw.Write("/d MON ")
                            'Starting Time
                            sw.Write("/st " & Trim(st) & " ")
                            sw.Write("/tr " & Chr(34) & strRootDirectory & "\rpa\tasks\" & strBatchFile & Chr(34) & " ")
                            ' Run with highest privileges
                            '/RU username /RP password /RL HIGHEST
                            If LCase(ConfigurationManager.AppSettings("RpaWindowShow")) = "true" Then
                                sw.Write("/RU " & ConfigurationManager.AppSettings("RpaU") & " /RP " & ConfigurationManager.AppSettings("RpaP") & " ")
                            End If

                        Case "10DAYS"
                            strPath = strRootDirectory & "\rpa\tasks\" & strBatchFile & ".xml"
                            sw.Write("/XML " & strPath & " ")
                            '/RU username /RP password /RL HIGHEST
                            If LCase(ConfigurationManager.AppSettings("RpaWindowShow")) = "true" Then
                                sw.Write("/RU " & ConfigurationManager.AppSettings("RpaU") & " /RP " & ConfigurationManager.AppSettings("RpaP") & " ")
                            End If

                            Dim strStartBoundary As String = ""
                            strStartBoundary = sd.Replace("/", "-")
                            strStartBoundary = strStartBoundary & "T" & Trim(st)

                            strPath = strRootDirectory & "\rpa\tasks\" & strBatchFile & ".xml"
                            If Not File.Exists(strPath) Then
                                Using swXml As StreamWriter = File.CreateText(strPath)
                                    swXml.WriteLine("<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "UTF-16" & Chr(34) & "?>")
                                    swXml.WriteLine("<Task version=" & Chr(34) & "1.2" & Chr(34) & " xmlns=" & Chr(34) & "http://schemas.microsoft.com/windows/2004/02/mit/task" & Chr(34) & ">")
                                    swXml.WriteLine("<RegistrationInfo>")
                                    swXml.WriteLine("<Date>2020-01-09T22:58:45.8135288</Date>")
                                    swXml.WriteLine("<Author>GSS</Author>")
                                    swXml.WriteLine("</RegistrationInfo>")
                                    swXml.WriteLine("<Triggers>")
                                    swXml.WriteLine("<CalendarTrigger>")
                                    swXml.WriteLine("<StartBoundary>" & strStartBoundary & ":00</StartBoundary>")
                                    swXml.WriteLine("<Enabled>true</Enabled>")
                                    swXml.WriteLine("<ScheduleByMonth>")
                                    swXml.WriteLine("<DaysOfMonth>")
                                    swXml.WriteLine("<Day>01</Day>")
                                    swXml.WriteLine("<Day>11</Day>")
                                    swXml.WriteLine("<Day>21</Day>")
                                    swXml.WriteLine("</DaysOfMonth>")
                                    swXml.WriteLine("<Months>")
                                    swXml.WriteLine("<January />")
                                    swXml.WriteLine("<February />")
                                    swXml.WriteLine("<March />")
                                    swXml.WriteLine("<April />")
                                    swXml.WriteLine("<May />")
                                    swXml.WriteLine("<June />")
                                    swXml.WriteLine("<July />")
                                    swXml.WriteLine("<August />")
                                    swXml.WriteLine("<September />")
                                    swXml.WriteLine("<October  />")
                                    swXml.WriteLine("<November />")
                                    swXml.WriteLine("<December />")
                                    swXml.WriteLine("</Months>")
                                    swXml.WriteLine("</ScheduleByMonth>")
                                    swXml.WriteLine("</CalendarTrigger>")
                                    swXml.WriteLine("</Triggers>")
                                    swXml.WriteLine("<Actions Context=" & Chr(34) & "Author" & Chr(34) & ">")
                                    swXml.WriteLine("<Exec>")
                                    swXml.WriteLine("<Command>" & Chr(34) & strRootDirectory & "\rpa\tasks\" & strBatchFile & Chr(34) & "</Command>")
                                    swXml.WriteLine("</Exec>")
                                    swXml.WriteLine("</Actions>")
                                    swXml.WriteLine("</Task>")
                                End Using
                            End If
                        Case "MONTHLY"
                            strPath = strRootDirectory & "\rpa\tasks\" & strBatchFile & ".xml"
                            sw.Write("/XML " & strPath & " ")
                            '/RU username /RP password /RL HIGHEST
                            If LCase(ConfigurationManager.AppSettings("RpaWindowShow")) = "true" Then
                                sw.Write("/RU " & ConfigurationManager.AppSettings("RpaU") & " /RP " & ConfigurationManager.AppSettings("RpaP") & " ")
                            End If

                            strPath = strRootDirectory & "\rpa\tasks\" & strBatchFile & ".xml"
                            Dim strStartBoundary As String = ""
                            Dim dtDay As String = ""
                            dtDay = Right(sd, 2)
                            strStartBoundary = sd.Replace("/", "-")
                            strStartBoundary = strStartBoundary & "T" & Trim(st)


                            If Not File.Exists(strPath) Then
                                Using swXml As StreamWriter = File.CreateText(strPath)
                                    swXml.WriteLine("<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "UTF-16" & Chr(34) & "?>")
                                    swXml.WriteLine("<Task version=" & Chr(34) & "1.2" & Chr(34) & " xmlns=" & Chr(34) & "http://schemas.microsoft.com/windows/2004/02/mit/task" & Chr(34) & ">")
                                    swXml.WriteLine("<RegistrationInfo>")
                                    swXml.WriteLine("<Date>2020-01-09T22:58:45.8135288</Date>")
                                    swXml.WriteLine("<Author>GSS</Author>")
                                    swXml.WriteLine("</RegistrationInfo>")
                                    swXml.WriteLine("<Triggers>")
                                    swXml.WriteLine("<CalendarTrigger>")
                                    swXml.WriteLine("<StartBoundary>" & strStartBoundary & ":00</StartBoundary>")
                                    swXml.WriteLine("<Enabled>true</Enabled>")
                                    swXml.WriteLine("<ScheduleByMonth>")
                                    swXml.WriteLine("<DaysOfMonth>")
                                    swXml.WriteLine("<Day>" & dtDay & "</Day>")
                                    swXml.WriteLine("</DaysOfMonth>")
                                    swXml.WriteLine("<Months>")
                                    swXml.WriteLine("<January />")
                                    swXml.WriteLine("<February />")
                                    swXml.WriteLine("<March />")
                                    swXml.WriteLine("<April />")
                                    swXml.WriteLine("<May />")
                                    swXml.WriteLine("<June />")
                                    swXml.WriteLine("<July />")
                                    swXml.WriteLine("<August />")
                                    swXml.WriteLine("<September />")
                                    swXml.WriteLine("<October  />")
                                    swXml.WriteLine("<November />")
                                    swXml.WriteLine("<December />")
                                    swXml.WriteLine("</Months>")
                                    swXml.WriteLine("</ScheduleByMonth>")
                                    swXml.WriteLine("</CalendarTrigger>")
                                    swXml.WriteLine("</Triggers>")
                                    swXml.WriteLine("<Actions Context=" & Chr(34) & "Author" & Chr(34) & ">")
                                    swXml.WriteLine("<Exec>")
                                    swXml.WriteLine("<Command>" & Chr(34) & strRootDirectory & "\rpa\tasks\" & strBatchFile & Chr(34) & "</Command>")
                                    swXml.WriteLine("</Exec>")
                                    swXml.WriteLine("</Actions>")
                                    swXml.WriteLine("</Task>")
                                End Using
                            End If
                    End Select

                    'Write to Log
                    sw.Write(">> " & strRootDirectory & "\rpa\scheduler\log\queue.txt ")
                        sw.Write(vbCrLf)
                        sw.Close()
                    End Using

                    'Create Execution Batch File 
                    Dim strPath1 As String = ""
                    Dim strReadLine As String = ""
                    'Read from Defauly Batch File
                    strPath1 = strRootDirectory & "\rpa\tasks\" & drpAddTASK_NAME.SelectedItem.Text & ".bat"
                    strPath = strRootDirectory & "\rpa\tasks\" & strBatchFile
                    If Not File.Exists(strPath) Then
                        Using sw As StreamWriter = File.CreateText(strPath)
                            'sw.WriteLine("")
                            If System.IO.File.Exists(strPath1) = True Then
                                Dim objReader As New System.IO.StreamReader(strPath1)
                                Do While objReader.Peek() <> -1
                                    strReadLine = objReader.ReadLine()
                                    strReadLine = strReadLine.Replace("schedulername", txtAddSCHEDULER_NAME.Text)
                                    sw.WriteLine(strReadLine)
                                Loop
                                objReader.Close()
                            End If
                        End Using
                    End If
                    BindData()
                End If


            ElseIf e.CommandName.Equals("Pause") Then
                'Variable Declartion
                Dim strRootDirectory As String = HostingEnvironment.MapPath("~/")
                Dim strPath As String = strRootDirectory & "\rpa\scheduler\queue"
                Dim strBatchFile As String = DateTime.Now.ToString("yyyyMMddHHmmss") & ".bat"
                Dim fileNameBas As String = strPath & "\" & "queue.bat"

                Dim selectedIndex As Integer = 0
                selectedIndex = Convert.ToInt32(e.CommandArgument.ToString)

                Dim lblRPASCHID As Label = CType(gvRPADetails.Rows(selectedIndex).FindControl("lblRPASCHID"), Label)
                Dim lblTASK_NAME As Label = CType(gvRPADetails.Rows(selectedIndex).FindControl("lblTASK_NAME"), Label)
                Dim lblSCHEDULER_NAME As Label = CType(gvRPADetails.Rows(selectedIndex).FindControl("lblSCHEDULER_NAME"), Label)
                Dim lblSTATUS As Label = CType(gvRPADetails.Rows(selectedIndex).FindControl("lblSTATUS"), Label)

                If (Trim(lblSCHEDULER_NAME.Text <> "")) And (LCase(Trim(lblSTATUS.Text)) <> "disabled") Then

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
                        sw.Write("echo disabled >> " & lblSCHEDULER_NAME.Text & ".txt ") 'rpa\scheduler\queue => Saved
                        sw.Write(vbCrLf)
                        sw.Write("echo %date%-%time% >> " & lblSCHEDULER_NAME.Text & ".txt ") 'rpa\scheduler\queue => Saved


                        'Next Line
                        sw.Write(vbCrLf)
                        'Sheduler Command
                        sw.Write("schtasks ")
                        'Action =>Create
                        sw.Write("/Change ")
                        'TaskName -
                        sw.Write("/tn " & Chr(34) & lblSCHEDULER_NAME.Text & Chr(34) & " ")
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
                        'Write to Log
                        sw.Write(">> " & strRootDirectory & "\rpa\scheduler\log\queue.txt ")
                        sw.Write(vbCrLf)
                        sw.Close()

                    End Using
                    'Update to Database
                    _RpaSchedulerModel.Status = "Disable under process"
                    _RpaSchedulerModel.TaskName = lblTASK_NAME.Text
                    _RpaSchedulerModel.SchedulerName = lblSCHEDULER_NAME.Text
                    'Update Start /end Status to db
                    dbTranStatus = _RpaSchedulerControl.RpaSchedulerStatusUpdate(_RpaSchedulerModel)
                    BindData()
                End If
            ElseIf e.CommandName.Equals("Run") Then
                'Variable Declartion
                Dim strRootDirectory As String = HostingEnvironment.MapPath("~/")
            Dim strPath As String = strRootDirectory & "\rpa\scheduler\queue"
            Dim strBatchFile As String = DateTime.Now.ToString("yyyyMMddHHmmss") & ".bat"
            Dim fileNameBas As String = strPath & "\" & "queue.bat"

            Dim selectedIndex As Integer = 0
            selectedIndex = Convert.ToInt32(e.CommandArgument.ToString)

            Dim lblRPASCHID As Label = CType(gvRPADetails.Rows(selectedIndex).FindControl("lblRPASCHID"), Label)
            Dim lblTASK_NAME As Label = CType(gvRPADetails.Rows(selectedIndex).FindControl("lblTASK_NAME"), Label)
            Dim lblSCHEDULER_NAME As Label = CType(gvRPADetails.Rows(selectedIndex).FindControl("lblSCHEDULER_NAME"), Label)
            Dim lblSTATUS As Label = CType(gvRPADetails.Rows(selectedIndex).FindControl("lblSTATUS"), Label)

            If (Trim(lblTASK_NAME.Text <> "")) Then

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
                    sw.Write("echo run >> " & lblSCHEDULER_NAME.Text & ".txt ")   'rpa\scheduler\queue  =>Saved
                    sw.Write(vbCrLf)
                    sw.Write("echo %date%-%time% >> " & lblSCHEDULER_NAME.Text & ".txt ") 'rpa\scheduler\queue =>Saved

                    'Next Line
                    sw.Write(vbCrLf)
                    'Sheduler Command
                    sw.Write("schtasks ")
                    'Action =>Run
                    sw.Write("/Run ")
                    'TaskName -
                    sw.Write("/tn " & Chr(34) & lblSCHEDULER_NAME.Text & Chr(34) & " ")
                    'Write to Log
                    sw.Write(">> " & strRootDirectory & "\rpa\scheduler\log\queue.txt ")
                    sw.Write(vbCrLf)
                    sw.Close()

                End Using

                'Update to Database
                _RpaSchedulerModel.Status = "Run Underprocess..."
                _RpaSchedulerModel.TaskName = lblTASK_NAME.Text
                _RpaSchedulerModel.SchedulerName = lblSCHEDULER_NAME.Text
                'Update Start /end Status to db
                dbTranStatus = _RpaSchedulerControl.RpaSchedulerStatusUpdate(_RpaSchedulerModel)
                BindData()
            End If

        End If


    End Sub

    Protected Sub gvRPADetails_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        'Variable Declartion
        Dim strRootDirectory As String = HostingEnvironment.MapPath("~/")
        Dim strPath As String = strRootDirectory & "\rpa\scheduler\queue"
        Dim strBatchFile As String = DateTime.Now.ToString("yyyyMMddHHmmss") & ".bat"
        Dim fileNameBas As String = strPath & "\" & "queue.bat"

        Dim lblEditRPASCHID As Label = CType(gvRPADetails.Rows(e.RowIndex).FindControl("lblEditRPASCHID"), Label)
        'Dim txtEditTASK_NAME As TextBox = CType(gvRPADetails.Rows(e.RowIndex).FindControl("txtEditTASK_NAME"), TextBox)
        'Dim txtEditTASK_SOURCE As TextBox = CType(gvRPADetails.Rows(e.RowIndex).FindControl("txtEditTASK_SOURCE"), TextBox)
        Dim txtEditSTART_DATETIME As TextBox = CType(gvRPADetails.Rows(e.RowIndex).FindControl("txtEditSTART_DATETIME"), TextBox)
        Dim txtEditSCHEDULER_NAME As TextBox = CType(gvRPADetails.Rows(e.RowIndex).FindControl("txtEditSCHEDULER_NAME"), TextBox)

        Dim drpEditTASK_NAME As DropDownList = CType(gvRPADetails.Rows(e.RowIndex).FindControl("drpEditTASK_NAME"), DropDownList)

        Dim strSsc As String = ""

        Dim chkLstEditSsc As CheckBoxList = CType(gvRPADetails.Rows(e.RowIndex).FindControl("chkLstEditSsc"), CheckBoxList)


        Dim IsSelect As Boolean = False
        strSsc = ""
        For i As Integer = 0 To chkLstEditSsc.Items.Count - 1
            If chkLstEditSsc.Items(i).Selected Then
                IsSelect = True
                strSsc = strSsc & chkLstEditSsc.Items(i).Text & ","
            End If
        Next
        If IsSelect Then
            strSsc = Left(strSsc, Len(strSsc) - 1)
        Else

            Exit Sub
        End If


        strSsc = UCase(Trim(strSsc))
        txtEditSCHEDULER_NAME.Text = UCase(txtEditSCHEDULER_NAME.Text)

        If (Trim(txtEditSCHEDULER_NAME.Text <> "")) Then
            'Initialize
            Dim _RpaSchedulerModel As RpaSchedulerModel = New RpaSchedulerModel()
            Dim _RpaSchedulerControl As RpaSchedulerControl = New RpaSchedulerControl()
            Dim dbTranStatus As Boolean = False
            'Update to Database
            _RpaSchedulerModel.Status = "Schedule Changed"
            _RpaSchedulerModel.TaskName = drpEditTASK_NAME.SelectedItem.Text
            _RpaSchedulerModel.TaskSource = strSsc
            _RpaSchedulerModel.StartDateTime = txtEditSTART_DATETIME.Text
            _RpaSchedulerModel.SchedulerName = txtEditSCHEDULER_NAME.Text
            _RpaSchedulerModel.RpaSchId = lblEditRPASCHID.Text
            'Update Start /end Status to db
            dbTranStatus = _RpaSchedulerControl.RpaSchedulerUpdate(_RpaSchedulerModel)
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
                'Select Case LCase(txtEditTASK_NAME.Text)
                '    Case "drs"
                '        sw.Write("echo scheduler >> drs.txt ")
                '        sw.Write(vbCrLf)
                '        sw.Write("echo %date%-%time% >> drs.txt ")
                '    Case Else
                'End Select
                sw.Write("echo scheduler >> " & txtEditSCHEDULER_NAME.Text & ".txt ")
                sw.Write(vbCrLf)
                sw.Write("echo %date%-%time% >> " & txtEditSCHEDULER_NAME.Text & ".txt ")


                'Next Line
                sw.Write(vbCrLf)
                'Sheduler Command
                sw.Write("schtasks ")
                'Action =>Create
                sw.Write("/Change ")
                'TaskName -
                sw.Write("/tn " & Chr(34) & txtEditSCHEDULER_NAME.Text & Chr(34) & " ")
                'Schedule Frequency
                ' sw.Write("/sc daily ")    'For One Minute frequence: =>sw.Write("/sc MINUTE ")   sw.Write("/sc once ") 
                'Schedule Modifier
                'sw.Write("/mo 1 ")
                Dim sd As String = Left(txtEditSTART_DATETIME.Text, 10)
                Dim st As String = Right(txtEditSTART_DATETIME.Text, Len(txtEditSTART_DATETIME.Text) - 10)
                'Starting Date
                sw.Write("/sd " & Trim(sd) & " ")
                'Starting Time
                sw.Write("/st " & Trim(st) & " ")

                sw.Write("/tr " & Chr(34) & strRootDirectory & "\rpa\tasks\" & drpEditTASK_NAME.SelectedItem.Text & ".bat" & Chr(34) & " ")


                '********************
                'Change of schedule is neccessary username and password to change the scheduler
                '*********************
                ' Run with highest privileges
                '/RU username /RP password /RL HIGHEST
                'If UCase(ConfigurationManager.AppSettings("RpaWindowShow")) = "TRUE" Then
                sw.Write("/RU " & ConfigurationManager.AppSettings("RpaU") & " /RP " & ConfigurationManager.AppSettings("RpaP") & " ")
                'End If
                'Write to Log
                sw.Write(">> " & strRootDirectory & "\rpa\scheduler\log\queue.txt ")
                sw.Write(vbCrLf)
                'ByDefault Enable When Edit is happening
                'Sheduler Command
                sw.Write("schtasks ")
                'Action =>Create
                sw.Write("/Change ")
                'TaskName -
                sw.Write("/tn " & Chr(34) & txtEditSCHEDULER_NAME.Text & Chr(34) & " ")
                'Disable
                sw.Write("/enable ")
                sw.Write("/RU " & ConfigurationManager.AppSettings("RpaU") & " /RP " & ConfigurationManager.AppSettings("RpaP") & " ")
                sw.Write(vbCrLf)

                sw.Close()

            End Using
        End If

        gvRPADetails.EditIndex = -1
        BindData()
    End Sub

    Protected Sub gvRPADetails_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        gvRPADetails.EditIndex = -1
        BindData()
    End Sub

    Protected Sub gvRPADetails_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        gvRPADetails.EditIndex = e.NewEditIndex
        BindData()
    End Sub


    Public Function SelectSSC() As DataTable
        Dim _RpaTaskAppControl As RpaTaskAppControl = New RpaTaskAppControl()
        Dim dtTaskApp As DataTable = _RpaTaskAppControl.SelectRpaTaskApp()
        Return dtTaskApp
    End Function

    Public Function SelectTask() As DataTable
        Dim _RpaTaskMstControl As RpaTaskMstControl = New RpaTaskMstControl()
        Dim dtTask As DataTable = _RpaTaskMstControl.SelectRpaTask()
        Return dtTask
    End Function
    Protected Sub showMsg(ByVal Msg As String, ByVal msgChk As String)

        lblMsg.Text = Msg
        Dim sScript As String

        If msgChk = "CancelMsg" Then
            'OKとキャンセルボタン
            sScript = "$(function () {$(""#dialog"" ).dialog({width: 400,buttons:{""OK"": function () {$(this).dialog('close');$('[id$=""BtnOK""]').click();},""CANCEL"": function () {$(this).dialog('close');$('[id$=""BtnCancel""]').click();}}});});"
        Else
            'OKボタンのみ
            sScript = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        End If

        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub

    ''' <summary>
    ''' Not used
    ''' </summary>
    ''' <returns></returns>
    'Public Function SelectSchedulerType() As List(Of CodeMasterModel)
    '    Dim codeMasterControl As CodeMasterControl = New CodeMasterControl()
    '    Dim codeMasterList As List(Of CodeMasterModel) = codeMasterControl.RpaSchedulerType()
    '    Return codeMasterList
    'End Function


End Class