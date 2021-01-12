Imports System.Text
Imports System.IO
Imports System.Data.SqlClient
Public Class Class_repair

    'CSV出力場所 (T_repair1検索データ結果)
    Public ExportCsvFilePass As String = "C:\\repair\\EXPORT\\"

    'Description,ラベル表示時の１行の長さ（半角文字数）
    Public colLenDescription As Integer = 24

    'M_PARTS 部品マスターの項目設定
    Public Structure M_PARTS
        Dim CRTDT As String
        Dim CRTCD As String
        Dim UPDDT As String
        Dim UPDCD As String
        Dim UPDPG As String
        Dim DELFG As Integer
        Dim maker As String
        Dim parts_no As String
        Dim parts_name As String
        Dim loc_1 As String
        Dim loc_2 As String
        Dim loc_3 As String
        Dim unit_price As Decimal
        Dim first_prc As Decimal
        Dim end_prc As Decimal
        Dim ship_code As String
        Dim eos_code As Integer
        Dim category As String
        Dim g_unit_price As Decimal
        Dim comoensation As Decimal
        Dim techfee_paid As Decimal
        Dim techfee_guarantee As Decimal
        Dim unit_flg As Integer
        Dim product_name As String
        Dim Assing_type As String
        Dim parts_flg As Integer
        Dim count_flg As Integer
    End Structure

    Public Sub exportData(ByVal dsT_repair1 As DataSet, ByRef buf As StringBuilder)

        'ヘッダ
        buf.Append("ASC_Code,Branch_Code,ASC_Claim_No,Samsung_Claim_No,rec_datetime,rec_yuser,rpt_counter,rpt_repair,close_datetime,denomi,amount,asc_c_num,sam_c_num,po_no,comment,comment2,Service_Type,Consumer_Name,Consumer_Addr1,Consumer_Addr2,Consumer_Telephone,Consumer_MailAddress,Consumer_Fax,Postal_Code,State_Name,Model,Serial_No,IMEI_No,Defect_Type,Product_Type,Maker,warranty,Condition,Symptom,Defect_Code,Repair_Code,Defect_Desc,Repair_Description,Purchase_Date,Repair_Received_Date,Completed_Date,Delivery_Date,Production_Date,Labor_Amount,Labor_No,Labor_Qty,Parts_Amount,Freight,Freight_Qty,Freight_Price,Other,Other_Qty,Other_Price,Other_Freight_Amount,Other_Freight_SGST,Other_Freight_CGST,Other_Freight_IGST,Parts_SGST,Parts_UTGST,Parts_CGST,Parts_IGST,Parts_Cess,SGST,UTGST,CGST,IGST,Cess,Total_Invoice_Amount,Remark,Tr_No,Tr_Type,Status,Engineer,Collection_Point,Collection_Point_Name,Location_1,Part_1,Qty_1,Unit_Price_1,Doc_Num_1,Matrial_Serial_1,Location_2,Part_2,Qty_2,Unit_Price_2,Doc_Num_2,Matrial_Serial_2,Location_3,Part_3,Qty_3,Unit_Price_3,Doc_Num_3,Matrial_Serial_3,Location_4,Part_4,Qty_4,Unit_Price_4,Doc_Num_4,Matrial_Serial_4,Location_5,Part_5,Qty_5,Unit_Price_5,Doc_Num_5,Matrial_Serial_5,Location_6,Part_6,Qty_6,Unit_Price_6,Doc_Num_6,Matrial_Serial_6,Location_7,Part_7,Qty_7,Unit_Price_7,Doc_Num_7,Matrial_Serial_7,Location_8,Part_8,Qty_8,Unit_Price_8,Doc_Num_8,Matrial_Serial_8,Location_9,Part_9,Qty_9,Unit_Price_9,Doc_Num_9,Matrial_Serial_9,Location_10,Part_10,Qty_10,Unit_Price_10,Doc_Num_10,Matrial_Serial_10,Location_11,Part_11,Qty_11,Unit_Price_11,Doc_Num_11,Matrial_Serial_11,Location_12,Part_12,Qty_12,Unit_Price_12,Doc_Num_12,Matrial_Serial_12,Location_13,Part_13,Qty_13,Unit_Price_13,Doc_Num_13,Matrial_Serial_13,Location_14,Part_14,Qty_14,Unit_Price_14,Doc_Num_14,Matrial_Serial_14,Location_15,Part_15,Qty_15,Unit_Price_15,Doc_Num_15,Matrial_Serial_15")
        buf.Append(vbCrLf)

        For i = 0 To dsT_repair1.Tables(0).Rows.Count - 1

            Dim dr As DataRow = dsT_repair1.Tables(0).Rows(i)

            If dr("ASC_Code") IsNot DBNull.Value Then
                buf.Append(dr("ASC_Code"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Branch_Code") IsNot DBNull.Value Then
                buf.Append(dr("Branch_Code"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("ASC_Claim_No") IsNot DBNull.Value Then
                buf.Append(dr("ASC_Claim_No"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Samsung_Claim_No") IsNot DBNull.Value Then
                buf.Append(dr("Samsung_Claim_No"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("rec_datetime") IsNot DBNull.Value Then
                buf.Append(dr("rec_datetime"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("rec_yuser") IsNot DBNull.Value Then
                buf.Append(dr("rec_yuser"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("rpt_counter") IsNot DBNull.Value Then
                buf.Append(dr("rpt_counter"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("rpt_repair") IsNot DBNull.Value Then
                buf.Append(dr("rpt_repair"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("close_datetime") IsNot DBNull.Value Then
                buf.Append(dr("close_datetime"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("denomi") IsNot DBNull.Value Then
                buf.Append(dr("denomi"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("amount") IsNot DBNull.Value Then
                buf.Append(dr("amount"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("asc_c_num") IsNot DBNull.Value Then
                buf.Append(dr("asc_c_num"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("sam_c_num") IsNot DBNull.Value Then
                buf.Append(dr("sam_c_num"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("po_no") IsNot DBNull.Value Then
                buf.Append(dr("po_no"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("comment") IsNot DBNull.Value Then
                buf.Append(dr("comment"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("comment2") IsNot DBNull.Value Then
                buf.Append(dr("comment2"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Service_Type") IsNot DBNull.Value Then
                buf.Append(dr("Service_Type"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Consumer_Name") IsNot DBNull.Value Then
                buf.Append(dr("Consumer_Name"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Consumer_Addr1") IsNot DBNull.Value Then
                buf.Append(dr("Consumer_Addr1"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Consumer_Addr2") IsNot DBNull.Value Then
                buf.Append(dr("Consumer_Addr2"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Consumer_Telephone") IsNot DBNull.Value Then
                buf.Append(dr("Consumer_Telephone"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Consumer_MailAddress") IsNot DBNull.Value Then
                buf.Append(dr("Consumer_MailAddress"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Consumer_Fax") IsNot DBNull.Value Then
                buf.Append(dr("Consumer_Fax"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Postal_Code") IsNot DBNull.Value Then
                buf.Append(dr("Postal_Code"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("State_Name") IsNot DBNull.Value Then
                buf.Append(dr("State_Name"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Model") IsNot DBNull.Value Then
                buf.Append(dr("Model"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Serial_No") IsNot DBNull.Value Then
                buf.Append(dr("Serial_No"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("IMEI_No") IsNot DBNull.Value Then
                buf.Append(dr("IMEI_No"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Defect_Type") IsNot DBNull.Value Then
                buf.Append(dr("Defect_Type"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Product_Type") IsNot DBNull.Value Then
                buf.Append(dr("Product_Type"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Maker") IsNot DBNull.Value Then
                buf.Append(dr("Maker"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("warranty") IsNot DBNull.Value Then
                buf.Append(dr("warranty"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Condition") IsNot DBNull.Value Then
                buf.Append(dr("Condition"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Symptom") IsNot DBNull.Value Then
                buf.Append(dr("Symptom"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Defect_Code") IsNot DBNull.Value Then
                buf.Append(dr("Defect_Code"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If
            If dr("Repair_Code") IsNot DBNull.Value Then
                buf.Append(dr("Repair_Code"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Defect_Desc") IsNot DBNull.Value Then
                buf.Append(dr("Defect_Desc"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Repair_Description") IsNot DBNull.Value Then
                buf.Append(dr("Repair_Description"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Purchase_Date") IsNot DBNull.Value Then
                buf.Append(dr("Purchase_Date"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Repair_Received_Date") IsNot DBNull.Value Then
                buf.Append(dr("Repair_Received_Date"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Completed_Date") IsNot DBNull.Value Then
                buf.Append(dr("Completed_Date"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Delivery_Date") IsNot DBNull.Value Then
                buf.Append(dr("Delivery_Date"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Production_Date") IsNot DBNull.Value Then
                buf.Append(dr("Production_Date"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Labor_Amount") IsNot DBNull.Value Then
                buf.Append(dr("Labor_Amount"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Labor_No") IsNot DBNull.Value Then
                buf.Append(dr("Labor_No"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Labor_Qty") IsNot DBNull.Value Then
                buf.Append(dr("Labor_Qty"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Parts_Amount") IsNot DBNull.Value Then
                buf.Append(dr("Parts_Amount"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Freight") IsNot DBNull.Value Then
                buf.Append(dr("Freight"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Freight_Qty") IsNot DBNull.Value Then
                buf.Append(dr("Freight_Qty"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Freight_Price") IsNot DBNull.Value Then
                buf.Append(dr("Freight_Price"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Other") IsNot DBNull.Value Then
                buf.Append(dr("Other"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Other_Qty") IsNot DBNull.Value Then
                buf.Append(dr("Other_Qty"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Other_Price") IsNot DBNull.Value Then
                buf.Append(dr("Other_Price"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Other_Freight_Amount") IsNot DBNull.Value Then
                buf.Append(dr("Other_Freight_Amount"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Other_Freight_SGST") IsNot DBNull.Value Then
                buf.Append(dr("Other_Freight_SGST"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Other_Freight_CGST") IsNot DBNull.Value Then
                buf.Append(dr("Other_Freight_CGST"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Other_Freight_IGST") IsNot DBNull.Value Then
                buf.Append(dr("Other_Freight_IGST"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Parts_SGST") IsNot DBNull.Value Then
                buf.Append(dr("Parts_SGST"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Parts_UTGST") IsNot DBNull.Value Then
                buf.Append(dr("Parts_UTGST"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Parts_CGST") IsNot DBNull.Value Then
                buf.Append(dr("Parts_CGST"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Parts_IGST") IsNot DBNull.Value Then
                buf.Append(dr("Parts_IGST"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Parts_Cess") IsNot DBNull.Value Then
                buf.Append(dr("Parts_Cess"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("SGST") IsNot DBNull.Value Then
                buf.Append(dr("SGST"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If
            If dr("UTGST") IsNot DBNull.Value Then
                buf.Append(dr("UTGST"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("CGST") IsNot DBNull.Value Then
                buf.Append(dr("CGST"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("IGST") IsNot DBNull.Value Then
                buf.Append(dr("IGST"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Cess") IsNot DBNull.Value Then
                buf.Append(dr("Cess"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Total_Invoice_Amount") IsNot DBNull.Value Then
                buf.Append(dr("Total_Invoice_Amount"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Remark") IsNot DBNull.Value Then
                buf.Append(dr("Remark"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Tr_No") IsNot DBNull.Value Then
                buf.Append(dr("Tr_No"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Tr_Type") IsNot DBNull.Value Then
                buf.Append(dr("Tr_Type"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Status") IsNot DBNull.Value Then
                buf.Append(dr("Status"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Engineer") IsNot DBNull.Value Then
                buf.Append(dr("Engineer"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Collection_Point") IsNot DBNull.Value Then
                buf.Append(dr("Collection_Point"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Collection_Point_Name") IsNot DBNull.Value Then
                buf.Append(dr("Collection_Point_Name"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Location_1") IsNot DBNull.Value Then
                buf.Append(dr("Location_1"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Part_1") IsNot DBNull.Value Then
                buf.Append(dr("Part_1"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Qty_1") IsNot DBNull.Value Then
                buf.Append(dr("Qty_1"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Unit_Price_1") IsNot DBNull.Value Then
                buf.Append(dr("Unit_Price_1"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Doc_Num_1") IsNot DBNull.Value Then
                buf.Append(dr("Doc_Num_1"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Matrial_Serial_1") IsNot DBNull.Value Then
                buf.Append(dr("Matrial_Serial_1"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Location_2") IsNot DBNull.Value Then
                buf.Append(dr("Location_2"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Part_2") IsNot DBNull.Value Then
                buf.Append(dr("Part_2"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Qty_2") IsNot DBNull.Value Then
                buf.Append(dr("Qty_2"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Unit_Price_2") IsNot DBNull.Value Then
                buf.Append(dr("Unit_Price_2"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Doc_Num_2") IsNot DBNull.Value Then
                buf.Append(dr("Doc_Num_2"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Matrial_Serial_2") IsNot DBNull.Value Then
                buf.Append(dr("Matrial_Serial_2"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Location_3") IsNot DBNull.Value Then
                buf.Append(dr("Location_3"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Part_3") IsNot DBNull.Value Then
                buf.Append(dr("Part_3"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Qty_3") IsNot DBNull.Value Then
                buf.Append(dr("Qty_3"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Unit_Price_3") IsNot DBNull.Value Then
                buf.Append(dr("Unit_Price_3"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Doc_Num_3") IsNot DBNull.Value Then
                buf.Append(dr("Doc_Num_3"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Matrial_Serial_3") IsNot DBNull.Value Then
                buf.Append(dr("Matrial_Serial_3"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Location_4") IsNot DBNull.Value Then
                buf.Append(dr("Location_4"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Part_4") IsNot DBNull.Value Then
                buf.Append(dr("Part_4"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Qty_4") IsNot DBNull.Value Then
                buf.Append(dr("Qty_4"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Unit_Price_4") IsNot DBNull.Value Then
                buf.Append(dr("Unit_Price_4"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Doc_Num_4") IsNot DBNull.Value Then
                buf.Append(dr("Doc_Num_4"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Matrial_Serial_4") IsNot DBNull.Value Then
                buf.Append(dr("Matrial_Serial_4"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Location_5") IsNot DBNull.Value Then
                buf.Append(dr("Location_5"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Part_5") IsNot DBNull.Value Then
                buf.Append(dr("Part_5"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Qty_5") IsNot DBNull.Value Then
                buf.Append(dr("Qty_5"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Unit_Price_5") IsNot DBNull.Value Then
                buf.Append(dr("Unit_Price_5"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Doc_Num_5") IsNot DBNull.Value Then
                buf.Append(dr("Doc_Num_5"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Matrial_Serial_5") IsNot DBNull.Value Then
                buf.Append(dr("Matrial_Serial_5"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Location_6") IsNot DBNull.Value Then
                buf.Append(dr("Location_6"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Part_6") IsNot DBNull.Value Then
                buf.Append(dr("Part_6"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Qty_6") IsNot DBNull.Value Then
                buf.Append(dr("Qty_6"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Unit_Price_6") IsNot DBNull.Value Then
                buf.Append(dr("Unit_Price_6"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Doc_Num_6") IsNot DBNull.Value Then
                buf.Append(dr("Doc_Num_6"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Matrial_Serial_6") IsNot DBNull.Value Then
                buf.Append(dr("Matrial_Serial_6"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Location_7") IsNot DBNull.Value Then
                buf.Append(dr("Location_7"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Part_7") IsNot DBNull.Value Then
                buf.Append(dr("Part_7"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Qty_7") IsNot DBNull.Value Then
                buf.Append(dr("Qty_7"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Unit_Price_7") IsNot DBNull.Value Then
                buf.Append(dr("Unit_Price_7"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Doc_Num_7") IsNot DBNull.Value Then
                buf.Append(dr("Doc_Num_7"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Matrial_Serial_7") IsNot DBNull.Value Then
                buf.Append(dr("Matrial_Serial_7"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Location_8") IsNot DBNull.Value Then
                buf.Append(dr("Location_8"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Part_8") IsNot DBNull.Value Then
                buf.Append(dr("Part_8"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Qty_8") IsNot DBNull.Value Then
                buf.Append(dr("Qty_8"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Unit_Price_8") IsNot DBNull.Value Then
                buf.Append(dr("Unit_Price_8"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Doc_Num_8") IsNot DBNull.Value Then
                buf.Append(dr("Doc_Num_8"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Matrial_Serial_8") IsNot DBNull.Value Then
                buf.Append(dr("Matrial_Serial_8"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Location_9") IsNot DBNull.Value Then
                buf.Append(dr("Location_9"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Part_9") IsNot DBNull.Value Then
                buf.Append(dr("Part_9"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Qty_9") IsNot DBNull.Value Then
                buf.Append(dr("Qty_9"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Unit_Price_9") IsNot DBNull.Value Then
                buf.Append(dr("Unit_Price_9"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Doc_Num_9") IsNot DBNull.Value Then
                buf.Append(dr("Doc_Num_9"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Matrial_Serial_9") IsNot DBNull.Value Then
                buf.Append(dr("Matrial_Serial_9"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Location_10") IsNot DBNull.Value Then
                buf.Append(dr("Location_10"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Part_10") IsNot DBNull.Value Then
                buf.Append(dr("Part_10"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Qty_10") IsNot DBNull.Value Then
                buf.Append(dr("Qty_10"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Unit_Price_10") IsNot DBNull.Value Then
                buf.Append(dr("Unit_Price_10"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Doc_Num_10") IsNot DBNull.Value Then
                buf.Append(dr("Doc_Num_10"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Matrial_Serial_10") IsNot DBNull.Value Then
                buf.Append(dr("Matrial_Serial_10"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Location_11") IsNot DBNull.Value Then
                buf.Append(dr("Location_11"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Part_11") IsNot DBNull.Value Then
                buf.Append(dr("Part_11"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Qty_11") IsNot DBNull.Value Then
                buf.Append(dr("Qty_11"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Unit_Price_11") IsNot DBNull.Value Then
                buf.Append(dr("Unit_Price_11"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Doc_Num_11") IsNot DBNull.Value Then
                buf.Append(dr("Doc_Num_11"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Matrial_Serial_11") IsNot DBNull.Value Then
                buf.Append(dr("Matrial_Serial_11"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Location_12") IsNot DBNull.Value Then
                buf.Append(dr("Location_12"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Part_12") IsNot DBNull.Value Then
                buf.Append(dr("Part_12"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Qty_12") IsNot DBNull.Value Then
                buf.Append(dr("Qty_12"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Unit_Price_12") IsNot DBNull.Value Then
                buf.Append(dr("Unit_Price_12"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Doc_Num_12") IsNot DBNull.Value Then
                buf.Append(dr("Doc_Num_12"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Matrial_Serial_12") IsNot DBNull.Value Then
                buf.Append(dr("Matrial_Serial_12"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Location_13") IsNot DBNull.Value Then
                buf.Append(dr("Location_13"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Part_13") IsNot DBNull.Value Then
                buf.Append(dr("Part_13"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Qty_13") IsNot DBNull.Value Then
                buf.Append(dr("Qty_13"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Unit_Price_13") IsNot DBNull.Value Then
                buf.Append(dr("Unit_Price_13"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Doc_Num_13") IsNot DBNull.Value Then
                buf.Append(dr("Doc_Num_13"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Matrial_Serial_13") IsNot DBNull.Value Then
                buf.Append(dr("Matrial_Serial_13"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Location_14") IsNot DBNull.Value Then
                buf.Append(dr("Location_14"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Part_14") IsNot DBNull.Value Then
                buf.Append(dr("Part_14"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Qty_14") IsNot DBNull.Value Then
                buf.Append(dr("Qty_14"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Unit_Price_14") IsNot DBNull.Value Then
                buf.Append(dr("Unit_Price_14"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Doc_Num_14") IsNot DBNull.Value Then
                buf.Append(dr("Doc_Num_14"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Matrial_Serial_14") IsNot DBNull.Value Then
                buf.Append(dr("Matrial_Serial_14"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Location_15") IsNot DBNull.Value Then
                buf.Append(dr("Location_15"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Part_15") IsNot DBNull.Value Then
                buf.Append(dr("Part_15"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Qty_15") IsNot DBNull.Value Then
                buf.Append(dr("Qty_15"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Unit_Price_15") IsNot DBNull.Value Then
                buf.Append(dr("Unit_Price_15"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Doc_Num_15") IsNot DBNull.Value Then
                buf.Append(dr("Doc_Num_15"))
                buf.Append(",")
            Else
                buf.Append(",")
            End If

            If dr("Matrial_Serial_15") IsNot DBNull.Value Then
                buf.Append(dr("Matrial_Serial_15"))
            Else
                buf.Append(",")
            End If

            buf.Append(vbCrLf)

        Next i

    End Sub
    '****************************************************
    '処理：文字列改行処理
    '引数：strLabel   　 データセットの表示する各項目の文字列　
    '　　　setStrLabel   labelにセットする改行を追加した文字列
    '      lines         labelに表示する行数
    '****************************************************
    Public Sub labelSet(ByVal strLabel As String, ByRef setStrLabel As String, ByVal lines As Integer)

        Dim tmp As String

        While (Len(strLabel) > 0)

            If Len(strLabel) < colLenDescription Then
                setStrLabel = Left(strLabel, colLenDescription)
                setStrLabel &= vbCrLf
                setStrLabel = Replace(setStrLabel, vbCrLf, "<br/>")
                Exit While
            Else
                'lines行まで表示
                For i = 0 To lines
                    tmp = ""
                    If i * colLenDescription < strLabel.Length Then
                        If i = 0 Then
                            setStrLabel = Left(strLabel, colLenDescription)
                        Else
                            If strLabel.Length - i * colLenDescription >= colLenDescription Then
                                tmp = strLabel.Substring(i * colLenDescription, colLenDescription)
                            Else
                                tmp = strLabel.Substring(i * colLenDescription, strLabel.Length - i * colLenDescription)
                            End If
                            If tmp = "" Then
                                Exit For
                            End If
                        End If
                    Else
                        Exit For
                    End If
                    setStrLabel = setStrLabel & tmp & vbCrLf
                Next i
                Exit While
            End If

            setStrLabel = Replace(setStrLabel, vbCrLf, "<br/>")

        End While

    End Sub
    '****************************************************
    '処理名　：setPrice
    '処理概要：部品NOより部品情報を取得
    '引数　　：partsNo
    '          shipCode
    '          unitPrice
    '      　　gUnitPrice
    '      　　Name
    '　　　　　errMsg       戻り値　エラー内容
    '****************************************************'
    Public Sub setPrice(ByVal partsNo As String, ByVal shipCode As String, ByRef unitPrice As String, ByRef gUnitPrice As String, ByRef name As String, ByRef errMsg As String)

        Dim errflg As Integer
        Dim strSQL As String = "SELECT * FROM dbo.M_PARTS WHERE parts_no = '" & partsNo & "' AND DELFG = 0 AND ship_code = '" & shipCode & "';"

        Dim DT_M_PARTS As DataTable = DBCommon.ExecuteGetDT(strSQL, errflg)

        If errflg = 1 Then
            errMsg = "部品情報の取得に失敗しました。"
            Exit Sub
        End If

        If DT_M_PARTS IsNot Nothing Then

            If DT_M_PARTS.Rows(0)("unit_price") IsNot DBNull.Value Then
                unitPrice = DT_M_PARTS.Rows(0)("unit_price")
            End If

            If DT_M_PARTS.Rows(0)("g_unit_price") IsNot DBNull.Value Then
                gUnitPrice = DT_M_PARTS.Rows(0)("g_unit_price")
            End If

            If DT_M_PARTS.Rows(0)("parts_name") IsNot DBNull.Value Then
                name = DT_M_PARTS.Rows(0)("parts_name")
            End If

        Else
            errMsg = "masterに存在しません。部品名を確認ください。"
        End If

    End Sub
    '****************************************************
    '処理：部品情報の登録
    '引数：partsMasterData   画面の入力情報
    '　　　userid　　　　　　
    '　　　errFlg      　　　戻り値　0:正常　1:異常
    '****************************************************
    Public Sub insert_M_PARTS(ByRef partsMasterData As M_PARTS, ByVal userid As String, ByRef errFlg As Integer)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try

            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia

            '■INSERT　M_PARTSの空テーブル取得
            Dim select_sql1 As String = ""
            select_sql1 = "SELECT * FROM dbo.M_PARTS WHERE parts_no IS NULL;"

            Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
            Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
            Dim Builder1 As New SqlCommandBuilder(Adapter1)
            Dim ds1 As New DataSet
            Adapter1.Fill(ds1)

            '新規DR取得
            Dim dr1 As DataRow = ds1.Tables(0).NewRow

            dr1("CRTDT") = dtNow
            dr1("CRTCD") = userid
            'dr1("UPDDT") = dtNow
            'dr1("UPDCD") = userid
            dr1("UPDPG") = "Class_repair.vb"
            dr1("DELFG") = 0
            dr1("maker") = partsMasterData.maker
            dr1("parts_no") = partsMasterData.parts_no
            dr1("parts_name") = partsMasterData.parts_name
            dr1("loc_1") = partsMasterData.loc_1
            dr1("loc_2") = partsMasterData.loc_2
            dr1("loc_3") = partsMasterData.loc_3
            dr1("unit_price") = partsMasterData.unit_price
            dr1("ship_code") = partsMasterData.ship_code
            dr1("category") = partsMasterData.category
            dr1("g_unit_price") = partsMasterData.g_unit_price
            dr1("comoensation") = partsMasterData.comoensation
            dr1("techfee_paid") = partsMasterData.techfee_paid
            dr1("techfee_guarantee") = partsMasterData.techfee_guarantee
            dr1("product_name") = partsMasterData.product_name
            dr1("Assing_type") = partsMasterData.Assing_type
            dr1("unit_flg") = partsMasterData.unit_flg
            dr1("parts_flg") = partsMasterData.parts_flg

            '新規DRをDataTableに追加
            ds1.Tables(0).Rows.Add(dr1)
            Adapter1.Update(ds1)

            '■コミット
            trn.Commit()

        Catch ex As Exception
            trn.Rollback()
            errFlg = 1
        Finally
            'DB接続クローズ
            If con.State <> ConnectionState.Closed Then
                con.Close()
            End If
        End Try

    End Sub

    '****************************************************
    '処理：部品情報の更新
    '引数：partsMasterData   画面の入力情報
    '　　　userid　　　　　　
    '　　　errFlg      　　　戻り値　0:正常　1:異常
    '****************************************************
    Public Sub update_M_PARTS(ByRef partsMasterData As M_PARTS, ByVal userid As String, ByRef errFlg As Integer)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try

            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia

            '■UPDATE
            Dim select_sql1 As String = ""
            select_sql1 = "SELECT * FROM dbo.M_PARTS WHERE DELFG = 0 AND ship_code = '" & partsMasterData.ship_code & "' AND parts_no = '" & partsMasterData.parts_no & "';"
            Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
            Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
            Dim Builder1 As New SqlCommandBuilder(Adapter1)
            Dim ds1 As New DataSet
            Adapter1.Fill(ds1)

            If ds1.Tables(0).Rows.Count = 1 Then

                Dim dr1 As DataRow = ds1.Tables(0).Rows(0)

                'dr1("CRTDT") = dtNow
                'dr1("CRTCD") = userid
                dr1("UPDDT") = dtNow
                dr1("UPDCD") = userid
                dr1("UPDPG") = "Class_repair.vb"
                dr1("DELFG") = 0
                dr1("maker") = partsMasterData.maker
                dr1("parts_no") = partsMasterData.parts_no
                dr1("parts_name") = partsMasterData.parts_name
                dr1("loc_1") = partsMasterData.loc_1
                dr1("loc_2") = partsMasterData.loc_2
                dr1("loc_3") = partsMasterData.loc_3
                dr1("unit_price") = partsMasterData.unit_price
                dr1("ship_code") = partsMasterData.ship_code
                dr1("category") = partsMasterData.category
                dr1("g_unit_price") = partsMasterData.g_unit_price
                dr1("comoensation") = partsMasterData.comoensation
                dr1("techfee_paid") = partsMasterData.techfee_paid
                dr1("techfee_guarantee") = partsMasterData.techfee_guarantee
                dr1("product_name") = partsMasterData.product_name
                dr1("Assing_type") = partsMasterData.Assing_type
                dr1("unit_flg") = partsMasterData.unit_flg
                dr1("parts_flg") = partsMasterData.parts_flg

                '更新
                Adapter1.Update(ds1)

            End If

            '■コミット
            trn.Commit()

        Catch ex As Exception
            trn.Rollback()
            errFlg = 1
        Finally
            'DB接続クローズ
            If con.State <> ConnectionState.Closed Then
                con.Close()
            End If
        End Try

    End Sub

    '****************************************************
    '処理：get_M_PARTS
    '引数：partsMasterData　出力対象をセット
    '      shipCode
    '　　　errFlg           戻り値　0:正常　1:異常
    '****************************************************
    Public Sub get_M_PARTS(ByRef partsMasterData() As M_PARTS, ByVal shipCode As String, ByRef errFlg As Integer)

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try
            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia

            '■出力対象を取得
            Dim dsM_PARTS As New DataSet
            Dim select_sql As String = ""
            select_sql &= "SELECT * FROM dbo.M_PARTS WHERE DELFG = 0 AND ship_code = '" & shipCode & "'"

            Dim sqlSelect As New SqlCommand(select_sql, con, trn)
            Dim Adapter As New SqlDataAdapter(sqlSelect)

            Adapter.Fill(dsM_PARTS)

            '出力対象を構造体に設定
            If dsM_PARTS.Tables(0).Rows.Count <> 0 Then

                ReDim partsMasterData(dsM_PARTS.Tables(0).Rows.Count - 1)

                For i = 0 To dsM_PARTS.Tables(0).Rows.Count - 1

                    Dim dr As DataRow = dsM_PARTS.Tables(0).Rows(i)

                    If dr("CRTDT") IsNot DBNull.Value Then
                        Dim workDt As DateTime
                        workDt = dr("CRTDT")
                        partsMasterData(i).CRTDT = workDt.ToString
                    End If

                    If dr("CRTCD") IsNot DBNull.Value Then
                        partsMasterData(i).CRTCD = dr("CRTCD")
                    End If

                    If dr("UPDDT") IsNot DBNull.Value Then
                        Dim workDt As DateTime
                        workDt = dr("UPDDT")
                        partsMasterData(i).UPDDT = workDt.ToString
                    End If

                    If dr("UPDCD") IsNot DBNull.Value Then
                        partsMasterData(i).UPDCD = dr("UPDCD")
                    End If

                    If dr("UPDPG") IsNot DBNull.Value Then
                        partsMasterData(i).UPDPG = dr("UPDPG")
                    End If

                    If dr("DELFG") IsNot DBNull.Value Then
                        partsMasterData(i).DELFG = dr("DELFG")
                    End If

                    If dr("maker") IsNot DBNull.Value Then
                        partsMasterData(i).maker = dr("maker")
                    End If

                    If dr("parts_no") IsNot DBNull.Value Then
                        partsMasterData(i).parts_no = dr("parts_no")
                    End If

                    If dr("parts_name") IsNot DBNull.Value Then
                        partsMasterData(i).parts_name = dr("parts_name")
                    End If

                    If dr("loc_1") IsNot DBNull.Value Then
                        partsMasterData(i).loc_1 = dr("loc_1")
                    End If

                    If dr("loc_2") IsNot DBNull.Value Then
                        partsMasterData(i).loc_2 = dr("loc_2")
                    End If

                    If dr("loc_3") IsNot DBNull.Value Then
                        partsMasterData(i).loc_3 = dr("loc_3")
                    End If

                    If dr("unit_price") IsNot DBNull.Value Then
                        partsMasterData(i).unit_price = dr("unit_price")
                    End If

                    If dr("first_prc") IsNot DBNull.Value Then
                        partsMasterData(i).first_prc = dr("first_prc")
                    End If

                    If dr("end_prc") IsNot DBNull.Value Then
                        partsMasterData(i).end_prc = dr("end_prc")
                    End If

                    If dr("eos_code") IsNot DBNull.Value Then
                        partsMasterData(i).eos_code = dr("eos_code")
                    End If

                    If dr("category") IsNot DBNull.Value Then
                        partsMasterData(i).category = dr("category")
                    End If

                    If dr("g_unit_price") IsNot DBNull.Value Then
                        partsMasterData(i).g_unit_price = dr("g_unit_price")
                    End If

                    If dr("comoensation") IsNot DBNull.Value Then
                        partsMasterData(i).comoensation = dr("comoensation")
                    End If

                    If dr("techfee_paid") IsNot DBNull.Value Then
                        partsMasterData(i).techfee_paid = dr("techfee_paid")
                    End If

                    If dr("techfee_guarantee") IsNot DBNull.Value Then
                        partsMasterData(i).techfee_guarantee = dr("techfee_guarantee")
                    End If

                    If dr("product_name") IsNot DBNull.Value Then
                        partsMasterData(i).product_name = dr("product_name")
                    End If

                    If dr("Assing_type") IsNot DBNull.Value Then
                        partsMasterData(i).Assing_type = dr("Assing_type")
                    End If

                    If dr("unit_flg") IsNot DBNull.Value Then
                        partsMasterData(i).unit_flg = dr("unit_flg")
                    End If

                    If dr("parts_flg") IsNot DBNull.Value Then
                        partsMasterData(i).parts_flg = dr("parts_flg")
                    End If

                    If dr("ship_code") IsNot DBNull.Value Then
                        partsMasterData(i).ship_code = dr("ship_code")
                    End If

                Next i

            End If

        Catch ex As Exception
            trn.Rollback()
            errFlg = 1
        Finally
            'DB接続クローズ
            If con.State <> ConnectionState.Closed Then
                con.Close()
            End If
        End Try

    End Sub

End Class
