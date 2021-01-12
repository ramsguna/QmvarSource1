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

Partial Public Class test
    Inherits System.Web.UI.Page

    Private conn As SqlConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("cnstr").ToString())
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If Not IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub BindData()
        Dim ds As DataSet = New DataSet()
        Dim FromTable As DataTable = New DataTable()
        conn.Open()
        Dim cmdstr As String = "Select * from RPA_SCHEDULER"
        Dim cmd As SqlCommand = New SqlCommand(cmdstr, conn)
        Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
        adp.Fill(ds)
        cmd.ExecuteNonQuery()
        FromTable = ds.Tables(0)

        If FromTable.Rows.Count > 0 Then
            gvRPADetails.DataSource = FromTable
            gvRPADetails.DataBind()
        Else
            FromTable.Rows.Add(FromTable.NewRow())
            gvRPADetails.DataSource = FromTable
            gvRPADetails.DataBind()
            Dim TotalColumns As Integer = gvRPADetails.Rows(0).Cells.Count
            gvRPADetails.Rows(0).Cells.Clear()
            gvRPADetails.Rows(0).Cells.Add(New TableCell())
            gvRPADetails.Rows(0).Cells(0).ColumnSpan = TotalColumns
            gvRPADetails.Rows(0).Cells(0).Text = "No records Found"
        End If

        ds.Dispose()
        conn.Close()
    End Sub

    Protected Sub gvRPADetails_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim lblRPASCHID As Label = CType(gvRPADetails.Rows(e.RowIndex).FindControl("lblRPASCHID"), Label) 'gvRPADetails.FooterRow.FindControl("txtAddTASK_NAME")
        Dim lblTASK_NAME As Label = CType(gvRPADetails.Rows(e.RowIndex).FindControl("lblTASK_NAME"), Label)
        conn.Open()
        Dim cmdstr As String = "delete from RPA_SCHEDULER where RPASCHID=@RPASCHID"
        Dim cmd As SqlCommand = New SqlCommand(cmdstr, conn)
        cmd.Parameters.AddWithValue("@RPASCHID", lblRPASCHID.Text)
        cmd.ExecuteNonQuery()
        conn.Close()

        'Variable Declartion
        Dim strRootDirectory As String = HostingEnvironment.MapPath("~/")
        Dim strPath As String = strRootDirectory & "\rpa\sceduler\queue"
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
            sw.Write("echo  %date%-%time% >> log.txt ")
            'Next Line
            sw.Write(vbCrLf)
            'Sheduler Command
            sw.Write("schtasks ")
            'Action =>Create
            sw.Write("/delete ")
            'TaskName -
            sw.Write("/tn " & Chr(34) & lblTASK_NAME.Text & Chr(34) & " /f ")
            'Write to Log
            sw.Write(">> log.txt ")
            sw.Write(vbCrLf)
            sw.Close()
        End Using
        '  sw.Write(">> log.txt ")
        BindData()
    End Sub

    Protected Sub gvRPADetails_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName.Equals("ADD") Then
            'Variable Declartion
            Dim strRootDirectory As String = HostingEnvironment.MapPath("~/")
            Dim strPath As String = strRootDirectory & "\rpa\sceduler\queue"
            Dim strBatchFile As String = DateTime.Now.ToString("yyyyMMddHHmmss") & ".bat"
            Dim fileNameBas As String = strPath & "\" & "queue.bat"

            'Update to Database
            ' Dim txtAddRPASCHID As TextBox = CType(gvRPADetails.FooterRow.FindControl("txtAddRPASCHID"), TextBox)
            Dim txtAddTASK_NAME As TextBox = CType(gvRPADetails.FooterRow.FindControl("txtAddTASK_NAME"), TextBox)
            Dim txtAddTASK_SOURCE As TextBox = CType(gvRPADetails.FooterRow.FindControl("txtAddTASK_SOURCE"), TextBox)
            Dim txtAddSTART_DATETIME As TextBox = CType(gvRPADetails.FooterRow.FindControl("txtAddSTART_DATETIME"), TextBox)
            Dim txtAddEND_DATETIME As TextBox = CType(gvRPADetails.FooterRow.FindControl("txtAddEND_DATETIME"), TextBox)
            Dim txtAddRECURRING_TYPE As TextBox = CType(gvRPADetails.FooterRow.FindControl("txtAddRECURRING_TYPE"), TextBox)
            Dim txtAddREPEAT_TIME As TextBox = CType(gvRPADetails.FooterRow.FindControl("txtAddREPEAT_TIME"), TextBox)

            conn.Open()
            Dim cmdstr As String = "insert into RPA_SCHEDULER(TASK_NAME,TASK_SOURCE,START_DATETIME,END_DATETIME,RECURRING_TYPE,REPEAT_TIME,STATUS,BATCH_FILE) values(@TASK_NAME,@TASK_SOURCE,@START_DATETIME,@END_DATETIME,@RECURRING_TYPE,@REPEAT_TIME,@STATUS,@BATCH_FILE)"
            Dim cmd As SqlCommand = New SqlCommand(cmdstr, conn)
            'cmd.Parameters.AddWithValue("@RPASCHID", txtAddRPASCHID.Text)
            cmd.Parameters.AddWithValue("@TASK_NAME", txtAddTASK_NAME.Text)
            cmd.Parameters.AddWithValue("@TASK_SOURCE", txtAddTASK_SOURCE.Text)
            cmd.Parameters.AddWithValue("@START_DATETIME", txtAddSTART_DATETIME.Text)
            If Trim(txtAddEND_DATETIME.Text) = "" Then
                cmd.Parameters.AddWithValue("@END_DATETIME", "")
            Else
                cmd.Parameters.AddWithValue("@END_DATETIME", txtAddEND_DATETIME.Text)
            End If

            cmd.Parameters.AddWithValue("@RECURRING_TYPE", txtAddRECURRING_TYPE.Text)
            If Trim(txtAddREPEAT_TIME.Text) = "" Then
                cmd.Parameters.AddWithValue("@REPEAT_TIME", "")
            Else
                cmd.Parameters.AddWithValue("@REPEAT_TIME", txtAddREPEAT_TIME.Text)
            End If

            cmd.Parameters.AddWithValue("@STATUS", "Not Started")

            cmd.Parameters.AddWithValue("@BATCH_FILE", strBatchFile)

            cmd.ExecuteNonQuery()
            conn.Close()

            '********************

            Dim fileMasterBatch As FileInfo = New FileInfo(fileNameBas)
            If Not File.Exists(fileNameBas) Then
                Using sw As StreamWriter = File.CreateText(fileNameBas)
                    'sw.WriteLine("")
                End Using
            End If

            Using sw As StreamWriter = fileMasterBatch.AppendText()
                'System DateTime
                sw.Write("echo  %date%-%time% >> log.txt ")
                'Next Line
                sw.Write(vbCrLf)
                'Sheduler Command
                sw.Write("schtasks ")
                'Action =>Create
                sw.Write("/Create ")
                'TaskName -
                sw.Write("/tn " & Chr(34) & txtAddTASK_NAME.Text & Chr(34) & " ")
                'Schedule Frequency
                sw.Write("/sc MINUTE ")
                'Schedule Modifier
                sw.Write("/mo 1 ")

                Select Case txtAddTASK_NAME.Text
                    Case "calc"
                        'calc
                        sw.Write("/tr " & Chr(34) & "C:\Windows\System32\calc.exe" & Chr(34) & " ")
                    Case "notepad"
                        'notepad
                        sw.Write("/tr " & Chr(34) & "C:\Windows\System32\notepad.exe" & Chr(34) & " ")
                    Case "help"
                        'help
                        sw.Write("/tr " & Chr(34) & "C:\Windows\System32\help.exe" & Chr(34) & " ")

                    Case Else
                        'Task Command
                        sw.Write("/tr " & Chr(34) & "C:\Windows\System32\calc.exe" & Chr(34) & " ")
                End Select
                'Write to Log
                sw.Write(">> log.txt ")
                sw.Write(vbCrLf)
                sw.Close()

            End Using

            BindData()
        End If
    End Sub

    Protected Sub gvRPADetails_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim lblEditRPASCHID As Label = CType(gvRPADetails.Rows(e.RowIndex).FindControl("lblEditRPASCHID"), Label)
        Dim txtEditTASK_NAME As TextBox = CType(gvRPADetails.Rows(e.RowIndex).FindControl("txtEditTASK_NAME"), TextBox)
        Dim txtEditTASK_SOURCE As TextBox = CType(gvRPADetails.Rows(e.RowIndex).FindControl("txtEditTASK_SOURCE"), TextBox)
        Dim txtEditSTART_DATETIME As TextBox = CType(gvRPADetails.Rows(e.RowIndex).FindControl("txtEditSTART_DATETIME"), TextBox)
        Dim txtEditRECURRING_TYPE As TextBox = CType(gvRPADetails.Rows(e.RowIndex).FindControl("txtEditRECURRING_TYPE"), TextBox)
        conn.Open()
        Dim cmdstr As String = "update RPA_SCHEDULER set TASK_NAME=@TASK_NAME,TASK_SOURCE=@TASK_SOURCE,START_DATETIME=@START_DATETIME,RECURRING_TYPE=@RECURRING_TYPE where RPASCHID=@RPASCHID"
        Dim cmd As SqlCommand = New SqlCommand(cmdstr, conn)
        cmd.Parameters.AddWithValue("@RPASCHID", lblEditRPASCHID.Text)
        cmd.Parameters.AddWithValue("@TASK_NAME", txtEditTASK_NAME.Text)
        cmd.Parameters.AddWithValue("@TASK_SOURCE", txtEditTASK_SOURCE.Text)
        cmd.Parameters.AddWithValue("@START_DATETIME", txtEditSTART_DATETIME.Text)
        cmd.Parameters.AddWithValue("@RECURRING_TYPE", txtEditRECURRING_TYPE.Text)
        cmd.ExecuteNonQuery()
        conn.Close()
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


End Class


