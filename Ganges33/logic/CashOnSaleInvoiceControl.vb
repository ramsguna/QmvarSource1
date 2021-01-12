Imports iTextSharp.text
Imports iFont = iTextSharp.text.Font
Imports iTextSharp.text.pdf
Imports System.Text
Imports System.IO
Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class CashOnSaleInvoiceControl
    Public logoQuickGarage As String = "C:\logo\qg.jpg"
    Public logoGSS As String = "C:\logo\gss.jpg"


    Public Function SelectPoNo(queryParams As CashOnSaleInvoiceModel) As String
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "PO_no "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "M_ship_base "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        If Not String.IsNullOrEmpty(queryParams.ShipMark) Then
            sqlStr = sqlStr & "AND ship_mark = @ShipMark "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipMark", queryParams.ShipMark))
        End If
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return dt.Rows(0)("PO_no").ToString()
    End Function


    Public Function SelectSlipNo(queryParams As CashOnSaleInvoiceModel) As String
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & " max(slipno) as  slipno "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "VCORPPO "


        If Not String.IsNullOrEmpty(queryParams.LocationNo) Then
            sqlStr = sqlStr & "WHERE LocationNo = @LocationNo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LocationNo", queryParams.LocationNo))
        End If
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return dt.Rows(0)("slipno").ToString()
    End Function

    Public Function SelectCorpPo(ByVal queryParams As CashOnSaleInvoiceModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        If Not String.IsNullOrEmpty(queryParams.CorpPoNo) Then
            sqlStr = sqlStr & "'" & queryParams.CorpPoNo & "' as CorpPoNo,"
        End If
        sqlStr = sqlStr & " claim_no,total_amount "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "cash_track "
        '''sqlStr = sqlStr & "WHERE "
        ''''sqlStr = sqlStr & "DELFG=0 AND Warranty='OOW' AND payment='Cash' AND incomplete='0' "
        '''sqlStr = sqlStr & "DELFG=0 AND Warranty='OOW' AND payment='Cash' AND incomplete='1' "
        '''If Not String.IsNullOrEmpty(queryParams.ClaimNo) Then
        '''    sqlStr = sqlStr & "AND claim_no = @ClaimNo "
        '''    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ClaimNo", queryParams.ClaimNo))
        '''End If


        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function

    Public Function SelectCorporateNumber(queryParams As CorporateModel) As String
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dtStatus As StatusModel = New StatusModel()
        'Sample Query
        'SELECT LEFT(corp_number, CHARINDEX('-', corp_number) - 1) [before_delim], Right(corp_number, Len(corp_number) - CHARINDEX('-', corp_number)) [after_delim] From [M_CORP] Where response_ship ='0002203913'

        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        'sqlStr = sqlStr & "max(RIGHT(corp_number, LEN(corp_number) - CHARINDEX('-', corp_number))) [CORP_NUMBER] "
        sqlStr = sqlStr & "max(cast(RIGHT(corp_number, LEN(corp_number) - CHARINDEX('-', corp_number)) as int)) [CORP_NUMBER] "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "M_CORP "
        '      sqlStr = sqlStr & "WHERE "
        'sqlStr = sqlStr & "DELFG=0 "
        '       If Not String.IsNullOrEmpty(queryParams.CRTCD) Then
        '       sqlStr = sqlStr & " response_ship = @BranchCode "
        '       dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BranchCode", queryParams.BranchCode))
        '        End If
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return dt.Rows(0)("CORP_NUMBER").ToString()
    End Function

    Public Function UpdateCashOnInvoice(ByVal lstCashOnSalesInvoiceCreateModel As List(Of CashOnSalesInvoiceCreateModel)) As Boolean
        Dim dbConn As DBUtility = New DBUtility()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        Dim rowIndex As Integer = 0
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))

        For Each target As CashOnSalesInvoiceCreateModel In lstCashOnSalesInvoiceCreateModel
            rowIndex = rowIndex + 1
            dbConn.sqlCmd.Transaction = dbConn.sqlTrn
            Dim sqlStr As String = "" '
            sqlStr = "UPDATE cash_track 
						SET
		                   corp_PO = @corp_PO{0}
                           ,  invoicedate = @invoicedate{0}
						WHERE
						  claim_no = @claim_no{0} 
							"
            sqlStr = String.Format(sqlStr, rowIndex)
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter(String.Format("@corp_PO{0}", rowIndex), target.CorpPoNo))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter(String.Format("@invoicedate{0}", rowIndex), dtNow.Date))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter(String.Format("@claim_no{0}", rowIndex), target.ClaimNo))
            sqlStr = String.Format(sqlStr, rowIndex)

            Dim updateFlg As Boolean = dbConn.ExecSQL(sqlStr)
            If Not updateFlg Then
                dbConn.sqlTrn.Rollback()
                Return False
            End If

        Next

        dbConn.sqlTrn.Commit()
        dbConn.CloseConnection()
        Return True
    End Function

    Public Sub CreatePDF(userName As String, filenameFullPath As String)

        Dim fileStream As FileStream

        '  Try
        Dim doc As Document
        Dim pdfWriter As PdfWriter


        'フォントの設定
        Dim fnt As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 9)
        '       Dim fntBa2 As New Font(BaseFont.CreateFont("c:\windows\fonts\CODE39.ttf", BaseFont.IDENTITY_H, True), 12) 'バーコード
        Dim fntBa2 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 12)
        Dim fnt1 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 10)
        Dim fnt2 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 24, iTextSharp.text.Font.BOLD)
        Dim fnt3 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 12, iTextSharp.text.Font.UNDERLINE)
        Dim fnt4 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 16, iTextSharp.text.Font.UNDERLINE)
        Dim fnt5 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 10, iTextSharp.text.Font.UNDERLINE)
        Dim fnt6 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 8)
        Dim fnt7 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 10)

        'FileStreamを生成 
        fileStream = New FileStream(filenameFullPath, FileMode.Create)
        'Documentを生成 
        'doc = New Document(PageSize.A4, 0, 0, 0, 0)
        'doc = New Document(PageSize.A4.Rotate(), 10.0F, 10.0F, 100.0F, 0F)
        doc = New Document(PageSize.A4.Rotate())
        'PdfWriter生成 
        pdfWriter = PdfWriter.GetInstance(doc, fileStream)
        'Documentのオープン 
        doc.Open()
        Dim table5 As PdfPTable = New PdfPTable(3)
        table5.TotalWidth = 100%
        ''Dim logo As New Paragraph()
        ''' logo.Add(image1)
        ''logo.Add(New Chunk(image1))
        Dim width1 As Single() = New Single() {1.0F, 4.0F, 1.0F}
        table5.SetWidths(width1)
        Dim cell As PdfPCell
        cell = New PdfPCell(New Phrase(""))
        cell.Rowspan = "3"
        Dim image1 As Image = Image.GetInstance(logoQuickGarage) '画像
        image1.SetAbsolutePosition(159.0F, 159.0F)
        image1.ScalePercent(25) '大きさ
        cell.AddElement(image1)
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        table5.AddCell(cell)

        cell = New PdfPCell(New Paragraph("GSS Quick Garage India Private Limited", fnt2))
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        table5.AddCell(cell)

        cell = New PdfPCell(New Paragraph("2G Century Plaza, New # 526, Old # 560-562, Anna Salai, Teynampet, Chennai, Tamil Nadu, PIN 600 018", fnt))
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        table5.AddCell(cell)


        cell = New PdfPCell(New Paragraph("PHONE: +91-44-48590055  /FAX: +91-44-48535600 / CIN : U74999TN2016FTC112554  ", fnt))
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        table5.AddCell(cell)

        cell = New PdfPCell(New Phrase(""))
        cell.Rowspan = "3"
        Dim image2 As Image = Image.GetInstance(logoGSS) '画像
        image2.SetAbsolutePosition(159.0F, 159.0F)
        image2.ScalePercent(25) '大きさ
        cell.AddElement(image2)
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        table5.AddCell(cell)

        doc.Add(table5)



        'Title: INVOICE
        Dim table3 As PdfPTable
        table3 = New PdfPTable(1)
        'Dim widths3 As Single()
        'widths3 = New Single() {1.0F, 1.0F, 1.0F, 1.0F, 1.0F}
        '  table3.SetWidthPercentage(20)
        cell = New PdfPCell(New Paragraph("INVOICE", fnt2))
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        'table6.AddCell(cell)
        table3.AddCell(cell)
        doc.Add(table3)


        'Title: 
        Dim table4 As PdfPTable
        table4 = New PdfPTable(5)

        cell = New PdfPCell(New Paragraph("Invoice No", fnt7))
        'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        'cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        'cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        'cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE

        cell.Rowspan = "2"
        table4.AddCell(cell)

        cell = New PdfPCell(New Paragraph("SS0600100001", fnt7))
        'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        'cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        'cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        'cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        cell.Rowspan = "2"
        table4.AddCell(cell)


        cell = New PdfPCell(New Paragraph("*SS0600100001*", fnt7))
        'cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        'cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        'cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        'cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        cell.Rowspan = "2"
        table4.AddCell(cell)


        cell = New PdfPCell(New Paragraph("GSTIN", fnt))
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        table4.AddCell(cell)

        cell = New PdfPCell(New Paragraph("33AAGCG5356G1ZO", fnt))
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER

        cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        table4.AddCell(cell)

        cell = New PdfPCell(New Paragraph("State Code", fnt))
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        table4.AddCell(cell)

        cell = New PdfPCell(New Paragraph("location state code", fnt))
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        table4.AddCell(cell)


        cell = New PdfPCell(New Paragraph("Invoice Date", fnt7))
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        cell.Rowspan = "2"
        table4.AddCell(cell)


        cell = New PdfPCell(New Paragraph("payment date :yyyy/mm/dd", fnt))
        table4.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt7))

        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        cell.Rowspan = "2"
        table4.AddCell(cell)

        cell = New PdfPCell(New Paragraph("service　classification", fnt))
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        table4.AddCell(cell)

        cell = New PdfPCell(New Paragraph("samsung CE", fnt))
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        table4.AddCell(cell)

        cell = New PdfPCell(New Paragraph("payment date :yyyy/mm/dd", fnt))
        table4.AddCell(cell)

        cell = New PdfPCell(New Paragraph("claimant", fnt))
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        table4.AddCell(cell)

        cell = New PdfPCell(New Paragraph("shimada", fnt))
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        table4.AddCell(cell)


        cell = New PdfPCell(New Paragraph("Details of Customer / Service Recipient", fnt))
        cell.Colspan = "5"
        'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        'cell.VerticalAlignment = PdfPCell.ALIGN_CENTER
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        table4.AddCell(cell)


        cell = New PdfPCell(New Paragraph("corporation name: ", fnt))
        cell.Colspan = "3"
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        table4.AddCell(cell)

        cell = New PdfPCell(New Paragraph("State Name", fnt))
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        table4.AddCell(cell)

        cell = New PdfPCell(New Paragraph(" corp location state name", fnt))
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        table4.AddCell(cell)

        cell = New PdfPCell(New Paragraph("name: ", fnt))
        cell.Colspan = "3"
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        table4.AddCell(cell)

        cell = New PdfPCell(New Paragraph("State Code", fnt))
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        table4.AddCell(cell)

        cell = New PdfPCell(New Paragraph("corp location state code", fnt))
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        table4.AddCell(cell)


        cell = New PdfPCell(New Paragraph("Address: ", fnt))
        cell.Colspan = "3"
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        table4.AddCell(cell)
        cell = New PdfPCell(New Paragraph("GSTIN", fnt))
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        table4.AddCell(cell)

        cell = New PdfPCell(New Paragraph("corp GSTIN", fnt))
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        table4.AddCell(cell)

        doc.Add(table4)

        'Line 
        Dim tableLn1 As PdfPTable
        tableLn1 = New PdfPTable(5)
        cell = New PdfPCell(New Paragraph("mohan", fnt))
        cell.Colspan = "5"
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        '  cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        tableLn1.AddCell(cell)

        doc.Add(tableLn1)

        'Items 
        Dim tableItemHeaders As PdfPTable
        tableItemHeaders = New PdfPTable(17)
        Dim width2 As Single() = New Single() {0.6F, 3.0F, 1.5F, 1.5F, 1.5F, 1.5F, 1.5F, 1.5F, 0.1F, 0.6F, 3.0F, 1.5F, 1.5F, 1.5F, 1.5F, 1.5F, 1.5F}
        tableItemHeaders.SetWidths(width2)

        cell = New PdfPCell(New Paragraph("Sr.No", fnt))
        tableItemHeaders.AddCell(cell)
        cell = New PdfPCell(New Paragraph("Service Order No", fnt))
        tableItemHeaders.AddCell(cell)
        cell = New PdfPCell(New Paragraph("Labor", fnt))
        tableItemHeaders.AddCell(cell)
        cell = New PdfPCell(New Paragraph("Parts", fnt))
        tableItemHeaders.AddCell(cell)
        cell = New PdfPCell(New Paragraph("transport", fnt))
        tableItemHeaders.AddCell(cell)
        cell = New PdfPCell(New Paragraph("other", fnt))
        tableItemHeaders.AddCell(cell)
        cell = New PdfPCell(New Paragraph("tax", fnt))
        tableItemHeaders.AddCell(cell)
        cell = New PdfPCell(New Paragraph("total amount", fnt))
        tableItemHeaders.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt))
        tableItemHeaders.AddCell(cell)

        cell = New PdfPCell(New Paragraph("Sr.No", fnt))
        tableItemHeaders.AddCell(cell)
        cell = New PdfPCell(New Paragraph("Service Order No", fnt))
        tableItemHeaders.AddCell(cell)
        cell = New PdfPCell(New Paragraph("Labor", fnt))
        tableItemHeaders.AddCell(cell)
        cell = New PdfPCell(New Paragraph("Parts", fnt))
        tableItemHeaders.AddCell(cell)
        cell = New PdfPCell(New Paragraph("transport", fnt))
        tableItemHeaders.AddCell(cell)
        cell = New PdfPCell(New Paragraph("other", fnt))
        tableItemHeaders.AddCell(cell)
        cell = New PdfPCell(New Paragraph("tax", fnt))
        tableItemHeaders.AddCell(cell)
        cell = New PdfPCell(New Paragraph("total amount", fnt))
        tableItemHeaders.AddCell(cell)


        doc.Add(tableItemHeaders)




        'Service Items
        Dim tableItems As PdfPTable
        tableItems = New PdfPTable(17)
        Dim width3 As Single() = New Single() {0.6F, 3.0F, 1.5F, 1.5F, 1.5F, 1.5F, 1.5F, 1.5F, 0.1F, 0.6F, 3.0F, 1.5F, 1.5F, 1.5F, 1.5F, 1.5F, 1.5F}
        tableItems.SetWidths(width3)

        'Line 1 & 11
        cell = New PdfPCell(New Paragraph("1", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt))
        tableItems.AddCell(cell)

        cell = New PdfPCell(New Paragraph("11", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)


        'Line 2 & 12
        cell = New PdfPCell(New Paragraph("2", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt))
        tableItems.AddCell(cell)

        cell = New PdfPCell(New Paragraph("12", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)

        'Line 3 & 13
        cell = New PdfPCell(New Paragraph("3", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt))
        tableItems.AddCell(cell)

        cell = New PdfPCell(New Paragraph("13", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)

        'Line 4 & 14
        cell = New PdfPCell(New Paragraph("4", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt))
        tableItems.AddCell(cell)

        cell = New PdfPCell(New Paragraph("14", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)


        'Line 5 & 15
        cell = New PdfPCell(New Paragraph("5", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt))
        tableItems.AddCell(cell)

        cell = New PdfPCell(New Paragraph("15", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)


        'Line 6 & 16
        cell = New PdfPCell(New Paragraph("6", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt))
        tableItems.AddCell(cell)

        cell = New PdfPCell(New Paragraph("16", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)


        'Line 7 & 17
        cell = New PdfPCell(New Paragraph("7", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt))
        tableItems.AddCell(cell)

        cell = New PdfPCell(New Paragraph("17", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)


        'Line 8 & 18
        cell = New PdfPCell(New Paragraph("8", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt))
        tableItems.AddCell(cell)

        cell = New PdfPCell(New Paragraph("18", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)


        'Line 9 & 19
        cell = New PdfPCell(New Paragraph("9", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt))
        tableItems.AddCell(cell)

        cell = New PdfPCell(New Paragraph("19", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)


        'Line 10 & 20
        cell = New PdfPCell(New Paragraph("10", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt))
        tableItems.AddCell(cell)

        cell = New PdfPCell(New Paragraph("20", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        tableItems.AddCell(cell)

        'tableItems.Rows(0).WriteCells = "mooooooooo"
        'tableItems.Rows.ElementAt(0).WriteCells = ""
        'Dim str As String = ""
        'For Each c In tableItems.Rows.SelectMany(Function(r) r.GetCells())
        '    ' c.Border = PdfPCell.NO_BORDER
        '    'c.Column.SetText = "m"
        '    ' c.Column(0).SetText(New Paragraph(" Mohan", fnt))
        '    c..SetText(New Paragraph(" Mohan", fnt))
        'Next



        doc.Add(tableItems)



        'Line 
        Dim tableLn2 As PdfPTable
        tableLn2 = New PdfPTable(5)
        cell = New PdfPCell(New Paragraph(" ", fnt))
        cell.Colspan = "5"
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        '  cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        tableLn2.AddCell(cell)

        doc.Add(tableLn2)

        'cell = New PdfPCell(New Paragraph(" ", fnt))
        'cell.Colspan = "5"
        'cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        ''  cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        'table4.AddCell(cell)



        Dim table8 As PdfPTable
        table8 = New PdfPTable(7)

        cell = New PdfPCell(New Paragraph("total invoice amount", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.Colspan = "3"
        table8.AddCell(cell)


        'Empty
        cell = New PdfPCell(New Paragraph("", fnt7))
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("total 1", fnt7))
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("total 2", fnt7))
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("total", fnt7))
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("13343.44", fnt7))
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        cell.Colspan = "3"
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("Labor", fnt7))
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("915", fnt7))
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt7))
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("915", fnt7))
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)



        'BankName
        cell = New PdfPCell(New Paragraph("Bank Name", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("Bank Branch IFSC", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt7))
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("Parts", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("7749.73", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("7749.73", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)


        'Value of BankName
        cell = New PdfPCell(New Paragraph("", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE

        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt7))
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("transport", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("0.00", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("0.00", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)



        'Bank Account Name
        cell = New PdfPCell(New Paragraph("Bank Account Name", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("Bank Account Number", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt7))
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("other", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("0.00", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("0.00", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        'Value of Bank Account name
        cell = New PdfPCell(New Paragraph("", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE

        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt7))
        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("tax", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("1559.65", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("1559.65", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)


        'Terms of 
        cell = New PdfPCell(New Paragraph("Terms and conditions", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER
        cell.Colspan = "2"
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt7))

        cell.BorderWidthLeft = iTextSharp.text.Rectangle.NO_BORDER
        cell.BorderWidthRight = iTextSharp.text.Rectangle.NO_BORDER

        table8.AddCell(cell)


        cell = New PdfPCell(New Paragraph("total amount", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("10224.38", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        cell = New PdfPCell(New Paragraph("10224.38", fnt7))
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        table8.AddCell(cell)

        'doc.Add(table4)
        doc.Add(table8)


        'クローズ 
        doc.Close()

            ' Catch ex As Exception
            '   errFlg = 1
            'Finally
            If fileStream IsNot Nothing Then
                fileStream.Close()
            End If
       ' End Try




    End Sub

End Class
