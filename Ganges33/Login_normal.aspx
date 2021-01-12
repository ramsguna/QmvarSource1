<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login_normal.aspx.vb" MasterPageFile="~/Site1.Master" Inherits="Ganges33.Login_normal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
    <link href="CSS/Common/Login_normal.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-image: url('image/login-gss-logo.png');" class="BackgroundImage">
        <br />
    <asp:Button ID="Button1" runat="server" Text="Button" style="display:none;" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <p align = "right">
            <asp:Label ID="Label1" runat="server" Text="QuickGarage Location" CssClass="lbl-QMVAR-UserID-Password-Location"></asp:Label>
            <asp:Label ID="lblServer" runat="server" Text=" " CssClass="lbl-QMVAR-UserID-Password-Location"></asp:Label>
        <br />
            <asp:Label ID="Label2" runat="server" Text="UserID" CssClass="lbl-QMVAR-UserID-Password-Location"></asp:Label>
            <asp:TextBox ID="TextId" runat="server"></asp:TextBox>
        <br />
            <asp:Label ID="Label3" runat="server" Text="Password" CssClass="lbl-QMVAR-UserID-Password-Location"></asp:Label>
            <asp:TextBox ID="TextPass" runat="server" autocomplete = "OFF" TextMode="Password"></asp:TextBox>
        <br />
            <asp:ImageButton ID="BtnSubmit" runat="server" ImageUrl="~/icon/submit.png" CssClass="btn-Login-Submit"/>
        <br />
            <asp:Label ID="Label4" runat="server" Text="Location" CssClass="lbl-QMVAR-UserID-Password-Location"></asp:Label>
            <asp:DropDownList ID="DropListLocation" runat="server" CssClass="DropDownListLocation">
            </asp:DropDownList>
        <br />
            <asp:ImageButton ID="BtnLogin" runat="server" ImageUrl="~/icon/login.png" CssClass="btn-Login-Submit"/>
        </p>
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
 </asp:Content>
