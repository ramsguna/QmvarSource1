Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class WarrantyCsvModel

        Public Sub WarrantyCsvModel()
            AscCode = String.Empty
            BranchCode = String.Empty
            AscClaimNo = String.Empty
            ModelName = String.Empty
            PartAmountRetailPriceWithoutTax = 0.00
            SawDiscountPartsAmount = 0.00
            SawDiscountLaborAmount = 0.00
            InvoiceAmountCollectedFromCust = 0.00
            PartInvoiceNo = String.Empty
            LabourInvoiceNo = String.Empty
            SawRemarks = String.Empty



        End Sub
        Public Property AscCode As String
        Public Property BranchCode As String
        Public Property AscClaimNo As String
        Public Property ModelName As String
        Public Property PartAmountRetailPriceWithoutTax As Decimal
        Public Property SawDiscountPartsAmount As Decimal
        Public Property SawDiscountLaborAmount As Decimal
        Public Property InvoiceAmountCollectedFromCust As Decimal
        Public Property PartInvoiceNo As String
        Public Property LabourInvoiceNo As String
        Public Property SawRemarks As String




        ''' <summary>
        ''' ''''Backup
        ''' </summary>
        ''' <returns></returns>
        ''''''''Public Property ASC_Code As String
        ''''''''Public Property Branch_Code As String
        ''''''''Public Property ASC_Claim_No As String
        ''''''''Public Property Parts_invoice_No As String
        ''''''''Public Property Labor_Invoice_No As String
        ''''''''Public Property Samsung_Claim_No As String
        ''''''''Public Property Service_Type As String
        ''''''''Public Property Consumer_Name As String
        ''''''''Public Property Consumer_Addr1 As String
        ''''''''Public Property Consumer_Addr2 As String
        ''''''''Public Property Consumer_Telephone As String
        ''''''''Public Property Consumer_Fax As String
        ''''''''Public Property Postal_Code As String
        ''''''''Public Property Model As String
        ''''''''Public Property Serial_No As String
        ''''''''Public Property IMEI_No As String
        ''''''''Public Property Defect_Type As String
        ''''''''Public Property Condition As String
        ''''''''Public Property Symptom As String
        ''''''''Public Property Defect_Code As String
        ''''''''Public Property Repair_Code As String
        ''''''''Public Property Defect_Desc As String
        ''''''''Public Property Repair_Description As String
        ''''''''Public Property Purchase_Date As String
        ''''''''Public Property Repair_Received_Date As String
        ''''''''Public Property Completed_Date As String
        ''''''''Public Property Delivery_Date As String
        ''''''''Public Property Production_Date As String
        ''''''''Public Property OW_Labor As String
        ''''''''Public Property OW_Parts As String
        ''''''''Public Property Freight As String
        ''''''''Public Property Other As String
        ''''''''Public Property Parts_SGST As String
        ''''''''Public Property Parts_UTGST As String
        ''''''''Public Property Parts_CGST As String
        ''''''''Public Property Parts_IGST As String
        ''''''''Public Property Parts_Cess As String
        ''''''''Public Property SGST As String
        ''''''''Public Property UTGST As String
        ''''''''Public Property CGST As String
        ''''''''Public Property IGST As String
        ''''''''Public Property Cess As String
        ''''''''Public Property OW_total As String
        ''''''''Public Property Remark As String
        ''''''''Public Property Tr_No As String
        ''''''''Public Property Tr_Type As String
        ''''''''Public Property Status As String
        ''''''''Public Property Engineer As String
        ''''''''Public Property Collection_Point As String
        ''''''''Public Property Collection_Point_Name As String
        ''''''''Public Property Location_1 As String
        ''''''''Public Property Part_1 As String
        ''''''''Public Property Qty_1 As String
        ''''''''Public Property Unit_Price_1 As String
        ''''''''Public Property Doc_Num_1 As String
        ''''''''Public Property Matrial_Serial_1 As String
        ''''''''Public Property Location_2 As String
        ''''''''Public Property Part_2 As String
        ''''''''Public Property Qty_2 As String
        ''''''''Public Property Unit_Price_2 As String
        ''''''''Public Property Doc_Num_2 As String
        ''''''''Public Property Matrial_Serial_2 As String
        ''''''''Public Property Location_3 As String
        ''''''''Public Property Part_3 As String
        ''''''''Public Property Qty_3 As String
        ''''''''Public Property Unit_Price_3 As String
        ''''''''Public Property Doc_Num_3 As String
        ''''''''Public Property Matrial_Serial_3 As String
        ''''''''Public Property Location_4 As String
        ''''''''Public Property Part_4 As String
        ''''''''Public Property Qty_4 As String
        ''''''''Public Property Unit_Price_4 As String
        ''''''''Public Property Doc_Num_4 As String
        ''''''''Public Property Matrial_Serial_4 As String
        ''''''''Public Property Location_5 As String
        ''''''''Public Property Part_5 As String
        ''''''''Public Property Qty_5 As String
        ''''''''Public Property Unit_Price_5 As String
        ''''''''Public Property Doc_Num_5 As String
        ''''''''Public Property Matrial_Serial_5 As String
        ''''''''Public Property Location_6 As String
        ''''''''Public Property Part_6 As String
        ''''''''Public Property Qty_6 As String
        ''''''''Public Property Unit_Price_6 As String
        ''''''''Public Property Doc_Num_6 As String
        ''''''''Public Property Matrial_Serial_6 As String
        ''''''''Public Property Location_7 As String
        ''''''''Public Property Part_7 As String
        ''''''''Public Property Qty_7 As String
        ''''''''Public Property Unit_Price_7 As String
        ''''''''Public Property Doc_Num_7 As String
        ''''''''Public Property Matrial_Serial_7 As String
        ''''''''Public Property Location_8 As String
        ''''''''Public Property Part_8 As String
        ''''''''Public Property Qty_8 As String
        ''''''''Public Property Unit_Price_8 As String
        ''''''''Public Property Doc_Num_8 As String
        ''''''''Public Property Matrial_Serial_8 As String
        ''''''''Public Property Location_9 As String
        ''''''''Public Property Part_9 As String
        ''''''''Public Property Qty_9 As String
        ''''''''Public Property Unit_Price_9 As String
        ''''''''Public Property Doc_Num_9 As String
        ''''''''Public Property Matrial_Serial_9 As String
        ''''''''Public Property Location_10 As String
        ''''''''Public Property Part_10 As String
        ''''''''Public Property Qty_10 As String
        ''''''''Public Property Unit_Price_10 As String
        ''''''''Public Property Doc_Num_10 As String
        ''''''''Public Property Matrial_Serial_10 As String
        ''''''''Public Property Location_11 As String
        ''''''''Public Property Part_11 As String
        ''''''''Public Property Qty_11 As String
        ''''''''Public Property Unit_Price_11 As String
        ''''''''Public Property Doc_Num_11 As String
        ''''''''Public Property Matrial_Serial_11 As String
        ''''''''Public Property Location_12 As String
        ''''''''Public Property Part_12 As String
        ''''''''Public Property Qty_12 As String
        ''''''''Public Property Unit_Price_12 As String
        ''''''''Public Property Doc_Num_12 As String
        ''''''''Public Property Matrial_Serial_12 As String
        ''''''''Public Property Location_13 As String
        ''''''''Public Property Part_13 As String
        ''''''''Public Property Qty_13 As String
        ''''''''Public Property Unit_Price_13 As String
        ''''''''Public Property Doc_Num_13 As String
        ''''''''Public Property Matrial_Serial_13 As String
        ''''''''Public Property Location_14 As String
        ''''''''Public Property Part_14 As String
        ''''''''Public Property Qty_14 As String
        ''''''''Public Property Unit_Price_14 As String
        ''''''''Public Property Doc_Num_14 As String
        ''''''''Public Property Matrial_Serial_14 As String
        ''''''''Public Property Location_15 As String
        ''''''''Public Property Part_15 As String
        ''''''''Public Property Qty_15 As String
        ''''''''Public Property Unit_Price_15 As String
        ''''''''Public Property Doc_Num_15 As String
        ''''''''Public Property Matrial_Serial_15 As String
        '''''''' ASC_Code = String.Empty
        ''''''''    Branch_Code = String.Empty
        ''''''''    ASC_Claim_No = String.Empty
        ''''''''    Parts_invoice_No = String.Empty
        ''''''''    Labor_Invoice_No = String.Empty
        ''''''''    Samsung_Claim_No = String.Empty
        ''''''''    Service_Type = String.Empty
        ''''''''    Consumer_Name = String.Empty
        ''''''''    Consumer_Addr1 = String.Empty
        ''''''''    Consumer_Addr2 = String.Empty
        ''''''''    Consumer_Telephone = String.Empty
        ''''''''    Consumer_Fax = String.Empty
        ''''''''    Postal_Code = String.Empty
        ''''''''    Model = String.Empty
        ''''''''    Serial_No = String.Empty
        ''''''''    IMEI_No = String.Empty
        ''''''''    Defect_Type = String.Empty
        ''''''''    Condition = String.Empty
        ''''''''    Symptom = String.Empty
        ''''''''    Defect_Code = String.Empty
        ''''''''    Repair_Code = String.Empty
        ''''''''    Defect_Desc = String.Empty
        ''''''''    Repair_Description = String.Empty
        ''''''''    Purchase_Date = String.Empty
        ''''''''    Repair_Received_Date = String.Empty
        ''''''''    Completed_Date = String.Empty
        ''''''''    Delivery_Date = String.Empty
        ''''''''    Production_Date = String.Empty
        ''''''''    OW_Labor = String.Empty
        ''''''''    OW_Parts = String.Empty
        ''''''''    Freight = String.Empty
        ''''''''    Other = String.Empty
        ''''''''    Parts_SGST = String.Empty
        ''''''''    Parts_UTGST = String.Empty
        ''''''''    Parts_CGST = String.Empty
        ''''''''    Parts_IGST = String.Empty
        ''''''''    Parts_Cess = String.Empty
        ''''''''    SGST = String.Empty
        ''''''''    UTGST = String.Empty
        ''''''''    CGST = String.Empty
        ''''''''    IGST = String.Empty
        ''''''''    Cess = String.Empty
        ''''''''    OW_total = String.Empty
        ''''''''    Remark = String.Empty
        ''''''''    Tr_No = String.Empty
        ''''''''    Tr_Type = String.Empty
        ''''''''    Status = String.Empty
        ''''''''    Engineer = String.Empty
        ''''''''    Collection_Point = String.Empty
        ''''''''    Collection_Point_Name = String.Empty
        ''''''''    Location_1 = String.Empty
        ''''''''    Part_1 = String.Empty
        ''''''''    Qty_1 = String.Empty
        ''''''''    Unit_Price_1 = String.Empty
        ''''''''    Doc_Num_1 = String.Empty
        ''''''''    Matrial_Serial_1 = String.Empty
        ''''''''    Location_2 = String.Empty
        ''''''''    Part_2 = String.Empty
        ''''''''    Qty_2 = String.Empty
        ''''''''    Unit_Price_2 = String.Empty
        ''''''''    Doc_Num_2 = String.Empty
        ''''''''    Matrial_Serial_2 = String.Empty
        ''''''''    Location_3 = String.Empty
        ''''''''    Part_3 = String.Empty
        ''''''''    Qty_3 = String.Empty
        ''''''''    Unit_Price_3 = String.Empty
        ''''''''    Doc_Num_3 = String.Empty
        ''''''''    Matrial_Serial_3 = String.Empty
        ''''''''    Location_4 = String.Empty
        ''''''''    Part_4 = String.Empty
        ''''''''    Qty_4 = String.Empty
        ''''''''    Unit_Price_4 = String.Empty
        ''''''''    Doc_Num_4 = String.Empty
        ''''''''    Matrial_Serial_4 = String.Empty
        ''''''''    Location_5 = String.Empty
        ''''''''    Part_5 = String.Empty
        ''''''''    Qty_5 = String.Empty
        ''''''''    Unit_Price_5 = String.Empty
        ''''''''    Doc_Num_5 = String.Empty
        ''''''''    Matrial_Serial_5 = String.Empty
        ''''''''    Location_6 = String.Empty
        ''''''''    Part_6 = String.Empty
        ''''''''    Qty_6 = String.Empty
        ''''''''    Unit_Price_6 = String.Empty
        ''''''''    Doc_Num_6 = String.Empty
        ''''''''    Matrial_Serial_6 = String.Empty
        ''''''''    Location_7 = String.Empty
        ''''''''    Part_7 = String.Empty
        ''''''''    Qty_7 = String.Empty
        ''''''''    Unit_Price_7 = String.Empty
        ''''''''    Doc_Num_7 = String.Empty
        ''''''''    Matrial_Serial_7 = String.Empty
        ''''''''    Location_8 = String.Empty
        ''''''''    Part_8 = String.Empty
        ''''''''    Qty_8 = String.Empty
        ''''''''    Unit_Price_8 = String.Empty
        ''''''''    Doc_Num_8 = String.Empty
        ''''''''    Matrial_Serial_8 = String.Empty
        ''''''''    Location_9 = String.Empty
        ''''''''    Part_9 = String.Empty
        ''''''''    Qty_9 = String.Empty
        ''''''''    Unit_Price_9 = String.Empty
        ''''''''    Doc_Num_9 = String.Empty
        ''''''''    Matrial_Serial_9 = String.Empty
        ''''''''    Location_10 = String.Empty
        ''''''''    Part_10 = String.Empty
        ''''''''    Qty_10 = String.Empty
        ''''''''    Unit_Price_10 = String.Empty
        ''''''''    Doc_Num_10 = String.Empty
        ''''''''    Matrial_Serial_10 = String.Empty
        ''''''''    Location_11 = String.Empty
        ''''''''    Part_11 = String.Empty
        ''''''''    Qty_11 = String.Empty
        ''''''''    Unit_Price_11 = String.Empty
        ''''''''    Doc_Num_11 = String.Empty
        ''''''''    Matrial_Serial_11 = String.Empty
        ''''''''    Location_12 = String.Empty
        ''''''''    Part_12 = String.Empty
        ''''''''    Qty_12 = String.Empty
        ''''''''    Unit_Price_12 = String.Empty
        ''''''''    Doc_Num_12 = String.Empty
        ''''''''    Matrial_Serial_12 = String.Empty
        ''''''''    Location_13 = String.Empty
        ''''''''    Part_13 = String.Empty
        ''''''''    Qty_13 = String.Empty
        ''''''''    Unit_Price_13 = String.Empty
        ''''''''    Doc_Num_13 = String.Empty
        ''''''''    Matrial_Serial_13 = String.Empty
        ''''''''    Location_14 = String.Empty
        ''''''''    Part_14 = String.Empty
        ''''''''    Qty_14 = String.Empty
        ''''''''    Unit_Price_14 = String.Empty
        ''''''''    Doc_Num_14 = String.Empty
        ''''''''    Matrial_Serial_14 = String.Empty
        ''''''''    Location_15 = String.Empty
        ''''''''    Part_15 = String.Empty
        ''''''''    Qty_15 = String.Empty
        ''''''''    Unit_Price_15 = String.Empty
        ''''''''    Doc_Num_15 = String.Empty
        ''''''''    Matrial_Serial_15 = String.Empty


    End Class


End Namespace