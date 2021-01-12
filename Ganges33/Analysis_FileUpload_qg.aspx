<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Analysis.Master" CodeBehind="Analysis_FileUpload_qg.aspx.vb" Inherits="Ganges33.Analysis_FileUpload_qg" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>

    <link type="text/css" href="CSS/Common/Analysis_FileUpload_qg.css" rel="stylesheet" /> 
        <style type="text/css">
       
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-btnAnalysis" style="background-image: url('pagewall_2/wall_analysis-2.png')">
      <div class="div-EntirePage">
        <br />
          <table class="table-EntirePage">
              <tr>
                  <td class="td-Blank1"></td>
                  <td class="td-imgbtnAnalysis">
                      <asp:ImageButton ID="btnAnalysis" runat="server" CssClass="btnAnalysis" ImageUrl="~/icon/analysis.png" />
                  </td>
                  <td class="td-lblAnalysisFileUpload">
                      <asp:Label ID="Label3" runat="server" CssClass="lbl-AnalysisFileUpload" Text="Analysis file upload"></asp:Label>
                  </td>
                  <td class="td-Blank2"></td>
                  <td class="td-Blank3"></td>
                  <td class="td-Blank4"></td>
              </tr>
              <tr>
                  <td class="td-Blank5-Blank6"></td>
                  <td class="td-Blank7-Blank8" colspan = "2">
                      <asp:Label ID="Label1" runat="server" Text="current location : " CssClass="fontFamily"></asp:Label>
                      <asp:Label ID="lblLoc" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-Blank9-Blank10"></td>
                  <td class="td-Blank11-Blank12">
                      &nbsp;&nbsp;&nbsp;
                      </td>
                  <td class="td-Blank13-Blank14"></td>
              </tr>
              <tr>
                  <td class="td-Blank5-Blank6"></td>
                  <td class="td-Blank7-Blank8" colspan = "2">
                      <asp:Label ID="Label2" runat="server" Text="current username : " CssClass="fontFamily"></asp:Label>
                      <asp:Label ID="lblName" runat="server" CssClass="fontFamily"></asp:Label>
                      <br />
                  </td>
                  <td class="td-Blank9-Blank10"></td>
                  <td class="td-Blank11-Blank12"></td>
                  <td class="td-Blank13-Blank14"></td>
              </tr>
              <tr>
                  <td class="td-Blank15"></td>
                  <td class="td-lbl4UploadBranch" colspan = "2" rowspan = "2">
                      <asp:Label ID="Label4" runat="server" CssClass="fontFamily" Text="Upload Branch"></asp:Label>
                      <br />
                      <asp:DropDownList ID="DropListLocation" runat="server" CssClass="DropListLocation">
                      </asp:DropDownList>
                      <br />
                      <asp:Label ID="Label10" runat="server" Text="Invoice Date" CssClass="fontFamily"></asp:Label> 
                      <asp:RadioButtonList runat="server" ID="rbtnDate" RepeatDirection="Horizontal"  RepeatLayout="Flow" CssClass="labels">
                    <asp:ListItem Text="mm/dd/yyyy" Value="MM" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="dd/mm/yyyy" Value="DD"></asp:ListItem>
                        </asp:RadioButtonList> <br /><br /> 
                      &nbsp;&nbsp;&nbsp
                      <span class="fontFamily">&nbsp;&nbsp;</span>&nbsp;&nbsp;<br /><br />
                      <asp:FileUpload ID="FileUploadAnalysis" runat="server" CssClass="FileUploadAnalysis"/>
                  </td>
                  <td class="td-Blank16"></td>
                  <td class="td-Blank17">
                      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                      &nbsp;&nbsp;&nbsp;
                      <br class="fontFamily" /><br />
                      &nbsp;&nbsp;
                      <br class="fontFamily" /><br />
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br />
                  </td>
                  <td class="td-Blank18"></td>
              </tr>
              <tr>
                  <td class="td-Blank19"></td>

                  <td class="td-btnSend">
                      <asp:ImageButton ID="btnSend" runat="server" ImageUrl="~/icon/send.png" CssClass="btnSend-btnBack" />
                  </td>
                  <td align = "right" >
                      <asp:ImageButton ID="btnback" runat="server" ImageUrl="~/icon/back.png" CssClass="btnSend-btnBack" />
                  </td>
                  <td class="td-Blank20"></td>
              </tr>
               <tr>
                  <td class="td-UpdatePanel-BtnUpdateCashTrack-Label5-listMsg" colspan = "6">
                       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                      <asp:GridView ID="gvCashTrack" runat="server" Width="99%" AllowPaging="true"
                PageSize="100" AutoGenerateColumns="False" EmptyDataText="There was no corresponding information." BackColor="White"
                BorderColor="Black" BorderStyle="Double" EnableViewState="true">
                <Columns>
                      <asp:TemplateField HeaderText="Service Order No">
                        <ItemTemplate>
                             <asp:Label ID="lblServiceOrderNo"  Text='<%# Eval("claim_no")  %>' runat="server"></asp:Label>
                            <asp:HiddenField ID="hidBranch" runat="server" Value='<%# Eval("location") %>'  />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Engineer">
                        <ItemTemplate>
                             <asp:Label ID="lblEngineer"  Text='<%# Eval("input_user")  %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                    </asp:TemplateField>
                         <asp:TemplateField HeaderText="Goods Delivered">
                        <ItemTemplate>
                             <asp:Label ID="lblGoodsDelivered" Text='<%# Eval("invoice_date")  %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Collected Amount">
                        <ItemTemplate>
                             <asp:Label ID="lblCollectAmt"  Text='<%# Eval("total_amount")  %>'  runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                           <asp:TemplateField HeaderText="Type ( Cash / Card / Cash & Card )">
                        <ItemTemplate>
                            <table style="width:100%"><tr><td style="width:140px;">
                            <asp:DropDownList ID="drpType1" runat="server" onselectedindexchanged="drpType1_SelectedIndexChanged" AutoPostBack="true" Width="100px">
                                <asp:ListItem >Cash</asp:ListItem>
                                 <asp:ListItem >Card</asp:ListItem>
                                <asp:ListItem >Both</asp:ListItem>
                            </asp:DropDownList>
                                </td><td>
                            <asp:Label ID="lblCash" runat="server" Text="Cash" Visible="false" ></asp:Label>
                                     </td><td>
                             <asp:TextBox ID="txtCash" runat="server" Width="100px" Visible="false" ></asp:TextBox>
                                          </td><td>
                                    <asp:Label ID="lblCard" runat="server" Text="Card" Visible="false" ></asp:Label>
                                               </td><td>
                             <asp:TextBox ID="txtCard" runat="server" Width="100px" Visible="false" ></asp:TextBox>
                                </td>
                                </tr></table>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="30%" />
                    </asp:TemplateField>                  
                     <asp:TemplateField HeaderText="Discount">
                        <ItemTemplate>
                           <asp:TextBox ID="txtDiscount" runat="server" Width="50px" ></asp:TextBox>
                       </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Check Only Incomplete">
                        <ItemTemplate>
                           <asp:CheckBox ID="chkIncomplete" runat="server" ></asp:CheckBox>
                       </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                </Columns>
               <PagerSettings FirstPageText="" LastPageText="" NextPageText="" 
            PreviousPageText="" Position="Top" >
          </PagerSettings>
                <HeaderStyle BackColor="LightBlue" Font-Bold="True" ForeColor="Black" />
                <PagerStyle HorizontalAlign="Left" />
            </asp:GridView>
              </ContentTemplate>
    </asp:UpdatePanel>
                  </td> 
              </tr>
            <tr>
                  <td class="td-UpdatePanel-BtnUpdateCashTrack-Label5-listMsg" colspan = "6">
                      <asp:Button ID="BtnUpdateCashTrack" runat="server" Text="Update Cash Track" />
                </td>
            </tr>
              <tr>
                  <td class="td-Blank21"></td>
                  <td class="td-UpdatePanel-BtnUpdateCashTrack-Label5-listMsg" colspan = "3">
                      <asp:Label ID="Label5" runat="server" Text="Message" CssClass="fontFamily"></asp:Label> <br />
                      <asp:ListBox ID="ListMsg" runat="server" CssClass="listMsg"></asp:ListBox>
                  </td>
                  <td class="td-Blank22">
                      <asp:Label ID="Label6" runat="server" Text="History" CssClass="fontFamily"></asp:Label> <br />
                      <asp:ListBox ID="ListHistory" runat="server" CssClass="ListHistory"></asp:ListBox>
                  </td>
                  <td class="td-Blank23"></td>
              </tr>
              <tr>
                  <td class="td-Blank24">&nbsp;</td>
                  <td class="td-Blank25">&nbsp;</td>
                  <td class="td-Blank26">&nbsp;</td>
                  <td class="td-Blank27">&nbsp;</td>
                  <td class="td-Blank28">
                  </td>
                  <td>&nbsp;</td>
              </tr>
          </table>        
        <asp:Button ID="BtnCancel" runat="server" Text="Button" style="display:none;"/>
        <asp:Button ID="BtnOK" runat="server" Text="Button" style="display:none;" />
      </div>
   </div>
    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
