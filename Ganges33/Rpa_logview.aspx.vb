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
Public Class Rpa_logview
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            btnClose.Attributes.Add("OnClick", "window.close();")

            Dim fileName As String = ""
            fileName = Request.QueryString("id")

            Dim src As String = ""
            src = Request.QueryString("src")
            src = UCase(src)

            If Left(src, 3) = "SID" Then 'Sony
                'fileName = fileName.Replace("drs", "/drs")
                fileName = fileName.Insert(6, "/")
                Dim txt As String = ""
                'if file exists
                If File.Exists(Server.MapPath("~/rpa/logs/sony/" & fileName)) Then
                    txt = File.ReadAllText(Server.MapPath("~/rpa/logs/sony/" & fileName))
                    txtViewLog.Text = txt
                End If
            Else
                'fileName = fileName.Replace("drs", "/drs")
                fileName = fileName.Insert(6, "/")
                Dim txt As String = ""
                'if file exists
                If File.Exists(Server.MapPath("~/rpa/logs/" & fileName)) Then
                    txt = File.ReadAllText(Server.MapPath("~/rpa/logs/" & fileName))
                    txtViewLog.Text = txt
                End If
            End If




        End If
    End Sub

End Class