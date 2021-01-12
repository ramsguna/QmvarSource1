Imports System.IO
Imports System.Text
Imports System.Data.SqlClient
Imports Ganges33.Ganges33.logic

Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
            con.Open()

            'トランザクション開始＆コネクションオープン
            Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)
            Dim Year = Today.Year
            Dim month = Today.Month
            Dim Mon = Year & "/" & month.ToString

            Dim strSQL = "If EXISTS (SELECT * FROM Activity_report WHERE month = '" & Mon & "')  BEGIN"
            strSQL = strSQL & " select day   ,(Customer_Visit),(Call_Registerd),(repair_completed),(goods_delivered),(Pending_Calls),(Cancelled_Calls) from Activity_report where month = '" & Mon & "' order by day  END"
            strSQL = strSQL & " ELSE BEGIN Select  TempTableName.Day, TempTableName1.Customer_Visit,TempTableName2.Call_Registerd,TempTableName3.repair_completed,TempTableName4.goods_delivered,"
            strSQL = strSQL & " TempTableName5.Pending_Calls, TempTableName6.Cancelled_Calls From "
            strSQL = strSQL & " (VALUES(1),(2),(3),(4),(5),(6),(7),(9),(10),(11),(12),(13),(14),(15),(16),(17),(18),(19),(20),(21),(22),(23),(24),(25),(26),(27),(28),(29),(30) ) AS TempTableName (Day),"
            strSQL = strSQL & " (VALUES (0)) TempTableName1 (Customer_Visit),(VALUES (0)) TempTableName2 (Call_Registerd),(VALUES (0)) TempTableName3 (repair_completed),(VALUES (0)) TempTableName4 (goods_delivered),"
            strSQL = strSQL & " (VALUES (0)) TempTableName5 (Pending_Calls), (VALUES (0)) TempTableName6 (Cancelled_Calls) END"
            Dim sqlSelect As New SqlCommand(strSQL, con, trn)
            Dim Adapter As New SqlDataAdapter(sqlSelect)
            Dim ds As New DataSet

            Adapter.Fill(ds)

            Dim strSQL1 = "select day   ,(Call_Registerd),(repair_completed),(goods_delivered),(Pending_Calls),(Cancelled_Calls) from Activity_report where month = '" & Mon & "' order by day"

            Dim sqlSelect1 As New SqlCommand(strSQL1, con, trn)
            Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
            Dim ds1 As New DataSet
            Adapter.Fill(ds1)

            Chart1.ChartAreas("ChartArea1").AxisX.MajorGrid.Enabled = False
            Chart1.ChartAreas("ChartArea1").AxisY.MajorGrid.Enabled = False
            '  Chart1.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True

            Chart2.ChartAreas("ChartArea1").AxisX.MajorGrid.Enabled = False
            Chart2.ChartAreas("ChartArea1").AxisY.MajorGrid.Enabled = False
            ' Chart2.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True

            Chart3.ChartAreas("ChartArea1").AxisX.MajorGrid.Enabled = False
            Chart3.ChartAreas("ChartArea1").AxisY.MajorGrid.Enabled = False
            ' Chart3.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True

            Chart4.ChartAreas("ChartArea1").AxisX.MajorGrid.Enabled = False
            Chart4.ChartAreas("ChartArea1").AxisY.MajorGrid.Enabled = False
            ' Chart4.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True

            Chart5.ChartAreas("ChartArea1").AxisX.MajorGrid.Enabled = False
            Chart5.ChartAreas("ChartArea1").AxisY.MajorGrid.Enabled = False
            ' Chart5.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True

            Chart6.ChartAreas("ChartArea1").AxisX.MajorGrid.Enabled = False
            Chart6.ChartAreas("ChartArea1").AxisY.MajorGrid.Enabled = False
            ' Chart6.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True

            Chart7.ChartAreas("ChartArea1").AxisX.MajorGrid.Enabled = False
            Chart7.ChartAreas("ChartArea1").AxisY.MajorGrid.Enabled = False
            ' Chart7.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True

            Chart1.DataSource = ds
            Chart2.DataSource = ds1
            Chart3.DataSource = ds1
            Chart4.DataSource = ds1
            Chart5.DataSource = ds1
            Chart6.DataSource = ds1
            Chart7.DataSource = ds1
            con.Close()

        Catch ex As Exception
            Log4NetControl.ComErrorLogWrite(ex.ToString())
            Exit Sub
        End Try

    End Sub
End Class