<%@ Page Title="QMVAR-Login" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Login.aspx.vb" Inherits="Ganges33.Login" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>


    <link href="assets/jquery-ui_theme.css" rel="stylesheet" />
    <link href="assets/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript"  src="assets/jquery-ui.min_lips.js"></script>



    <link href="CSS/Common/Login.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/login.css" rel="stylesheet" />
    <style type="text/css">
         .dropdwn
        {
            width: 224px;
            height: 30px;
        }
/*         .ui-icon-closethick {
    background-position: -96px -128px;
}
         ui-state-default .ui-icon {
    background-image: url(images/ui-icons_e0fdff_256x240.png);
}
.ui-widget-header .ui-icon {
    background-image: url(images/ui-icons_d8e7f3_256x240.png);
}
.ui-icon, .ui-widget-content .ui-icon {
    background-image: url(images/ui-icons_0078ae_256x240.png);
}*/




.ui-widget-header {
	border: 2px #5e0376 solid;
	background: #9c27b0 url(images/ui-bg_gloss-wave_75_2191c0_500x100.png) 50% 50% repeat-x;
	color: #eaf5f7;
	font-weight: bold;
}

	.ui-widget-header a {
		color: #eaf5f7;
	}

	/* Interaction states
----------------------------------*/
	.ui-state-default,
	.ui-widget-content .ui-state-default,
	.ui-widget-header .ui-state-default {
		border: 1px solid #77d5f7;
		background: #0078ae url(images/ui-bg_glass_45_0078ae_1x400.png) 50% 50% repeat-x;
		font-weight: normal;
		color: #ffffff;
	}

		.ui-state-default a,
		.ui-state-default a:link,
		.ui-state-default a:visited {
			color: #ffffff;
			text-decoration: none;
		}

	.ui-state-hover,
	.ui-widget-content .ui-state-hover,
	.ui-widget-header .ui-state-hover,
	.ui-state-focus,
	.ui-widget-content .ui-state-focus,
	.ui-widget-header .ui-state-focus {
		border: 1px solid #448dae;
		background: #9c27b0 url(images/ui-bg_glass_75_79c9ec_1x400.png) 50% 50% repeat-x;
		font-weight: normal;
		color: #fff;
	}

		.ui-state-hover a,
		.ui-state-hover a:hover,
		.ui-state-hover a:link,
		.ui-state-hover a:visited,
		.ui-state-focus a,
		.ui-state-focus a:hover,
		.ui-state-focus a:link,
		.ui-state-focus a:visited {
			color: #026890;
			text-decoration: none;
		}

	.ui-state-active,
	.ui-widget-content .ui-state-active,
	.ui-widget-header .ui-state-active {
		border: 1px solid #acdd4a;
		background: #9c27b0 url(images/ui-bg_gloss-wave_50_6eac2c_500x100.png) 50% 50% repeat-x;
		font-weight: normal;
		color: #ffffff;
	}

		.ui-state-active a,
		.ui-state-active a:link,
		.ui-state-active a:visited {
			color: #ffffff;
			text-decoration: none;
		}

	/* Interaction Cues
----------------------------------*/
	.ui-state-highlight,
	.ui-widget-content .ui-state-highlight,
	.ui-widget-header .ui-state-highlight {
		border: 1px solid #fcd113;
		background: #9c27b0 url(images/ui-bg_glass_55_f8da4e_1x400.png) 50% 50% repeat-x;
		color: #915608;
	}

		.ui-state-highlight a,
		.ui-widget-content .ui-state-highlight a,
		.ui-widget-header .ui-state-highlight a {
			color: #915608;
		}

	.ui-state-error,
	.ui-widget-content .ui-state-error,
	.ui-widget-header .ui-state-error {
		border: 1px solid #cd0a0a;
		background: #e14f1c url(images/ui-bg_gloss-wave_45_e14f1c_500x100.png) 50% top repeat-x;
		color: #ffffff;
	}

		.ui-state-error a,
		.ui-widget-content .ui-state-error a,
		.ui-widget-header .ui-state-error a {
			color: #ffffff;
		}

	.ui-state-error-text,
	.ui-widget-content .ui-state-error-text,
	.ui-widget-header .ui-state-error-text {
		color: #ffffff;
	}

	.ui-priority-primary,
	.ui-widget-content .ui-priority-primary,
	.ui-widget-header .ui-priority-primary {
		font-weight: bold;
	}

	.ui-priority-secondary,
	.ui-widget-content .ui-priority-secondary,
	.ui-widget-header .ui-priority-secondary {
		opacity: .7;
		filter: Alpha(Opacity=70);
		font-weight: normal;
	}

	.ui-state-disabled,
	.ui-widget-content .ui-state-disabled,
	.ui-widget-header .ui-state-disabled {
		opacity: .35;
		filter: Alpha(Opacity=35);
		background-image: none;
	}

		.ui-state-disabled .ui-icon {
			filter: Alpha(Opacity=35); /* For IE8 - See #6059 */
		}





    </style>
              
<script type="text/javascript">
    $(function () {
        $('[class*=date]').datepicker({
            changeMonth: true,
            changeYear: true,
            format: "DD/MM/YYYY",
            language: "tr"
           
        });
	});
    $(document).ready(function () {
        $('#ingredients').multiselect();
    });
</script>

  <!DOCTYPE html>

  

<meta charset="utf-8">
<meta content="width=device-width, initial-scale=1.0" name="viewport">

<title>Login - QMVAR</title>
<meta content="" name="descriptison">
<meta content="" name="keywords">
<!-- Favicons 
<link href="assets/img/faviconn.png" rel="icon">
<link href="assets/img/apple-touch-iconn.png" rel="apple-touch-iconn">-->

<!-- Google Fonts -->
<link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Roboto:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet">

<!-- Vendor CSS Files -->
<link href="assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
<link href="assets/vendor/icofont/icofont.min.css" rel="stylesheet">
<link href="assets/vendor/boxicons/css/boxicons.min.css" rel="stylesheet">
<link href="assets/vendor/animate.css/animate.min.css" rel="stylesheet">
<link href="assets/vendor/venobox/venobox.css" rel="stylesheet">
<link href="assets/vendor/owl.carousel/assets/owl.carousel.min.css" rel="stylesheet">
<link href="assets/vendor/aos/aos.css" rel="stylesheet">
<link href="assets/vendor/remixicon/remixicon.css" rel="stylesheet">

<!-- Template Main CSS File -->
<link href="assets/css/login.css" rel="stylesheet">


    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div class="workSpace">
        
        
            <div class="avatar">
                
                    <img style="width: 70px;margin-top: 24px;margin-left: 29px; " src="assets/image/user.png">
                              <div id="infinity"></div>
            </div>
                
                
                <div class="left">
                    <img src="assets/image/gss-logo.png" alt="responsive" style="height: 53px;width: 222px;">
                
                
            </div>   
                        
            <div class="right">
              
                    <h1>QMVAR- <asp:Label ID="lblServer" runat="server" Text=" " ></asp:Label> </h1>
      

                    <asp:TextBox ID="TextId" placeholder="User ID" class="input" runat="server"></asp:TextBox>
     
                    <asp:TextBox ID="TextPass" runat="server" class="input" placeholder="Password" autocomplete = "OFF" TextMode="Password"></asp:TextBox>
                        
                        <span class="checkmark"></span>
                        
                          <asp:Button ID="BtnSubmit" runat="server" text="Submit" class="button"/>
      
                  <asp:DropDownList ID="DropListLocation" placeholder="Location" class="input dropdwn" style=" width: 224px;  height: 30px;" runat="server" >
            </asp:DropDownList>
                      
                     <asp:Button ID="BtnLogin" runat="server" text="Log In" class="button" />
      
				

                
            </div> 
            

        </div>
      
    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>


 </asp:Content>
<%--<asp:Button ID="Button1" runat="server" Text="Button" style="display:none;" />
      
        <p align = "right">
            <asp:Label ID="Label1" runat="server" Text="QMVAR-" CssClass="lbl-QMVAR-UserID-Password-Location"></asp:Label>
            <asp:Label ID="" runat="server" Text=" " CssClass="lbl-QMVAR-UserID-Password-Location"></asp:Label>
        <br />
            <asp:Label ID="Label2" runat="server" Text="UserID" CssClass="lbl-QMVAR-UserID-Password-Location"></asp:Label>
      
        <br />
            <asp:Label ID="Label3" runat="server" Text="Password" CssClass="lbl-QMVAR-UserID-Password-Location"></asp:Label>
            
        <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Location" CssClass="lbl-QMVAR-UserID-Password-Location"></asp:Label>
            
        <br />
             </p>--%>

