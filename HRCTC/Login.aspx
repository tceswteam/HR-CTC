<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" EnableEventValidation="true" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Compstruct : Login</title>
    <%--<link rel="shortcut icon" href="<%=ResolveUrl("../Images/favicon.ico")%>" />--%>
    <link href="CSS/Common.css" type="text/css" rel="stylesheet" />
   
    <link href="CSS/LoginPage.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="Resx/JQry/jquery-1.4.2.js"></script>
    <script language="javascript" type="text/javascript" src="Scripts/Common.js"></script>
    <script type="text/javascript" language="javascript">

        //        $(document).ready(function () {

        //            var txtUserName = $("#txtUserName");
        //            txtUserName.focus();

        //            if (($.browser.msie) || ($.browser.version == '7.0' || $.browser.version == '8.0')) {
        //                document.getElementById("btnSignIn").disabled = false;
        //                document.getElementById("txtUserName").disabled = false;
        //                $("#divErrorMsg").hide();
        //            }
        //            else {
        //                document.getElementById("btnSignIn").disabled = true;
        //                document.getElementById("txtUserName").disabled = true;
        //                ShowPopupDlg("divErrorMsg");
        //            }
        //        });
        function ValUserName() {
            var UserID = document.getElementById("txtUserName");
            var mPassWord = document.getElementById("txtPassword");

            if (!IsValid(UserID, "Login ID", "")) {
                return false;
            }
            if (!IsValid(mPassWord, "Password", "")) {
                return false;
            }

            else {
                return true;
            }
        }

        function Resize() {
            $(window).resize(function () {

                var box = $('.popup-window');

                //Get the screen height and width
                var popup_maskHeight = $(document).height();
                var popup_maskWidth = $(window).width();

                //Set height and width to popup_mask to fill up the whole screen
                $('#popup_mask').css({ 'width': popup_maskWidth, 'height': popup_maskHeight });

                //Get the window height and width
                var winH = $(window).height();
                var winW = $(window).width();

                //Set the popup window to center
                box.css('top', winH / 2 - box.height() / 2);
                box.css('left', winW / 2 - box.width() / 2);

            });
        }

        function ShowPopupDlg(vObjID) {

            //Get the A tag
            var id = "#" + vObjID; //$("#" + vObjID).attr('href');

            //Get the screen height and width
            var popup_maskHeight = $(document).height();
            var popup_maskWidth = $(window).width();

            //	//Set heigth and width to popup_mask to fill up the whole screen
            $('#popup_mask').css({ 'width': popup_maskWidth, 'height': popup_maskHeight });


            //transition effect
            $('#popup_mask').show();
            $('#popup_mask').fadeTo("fast", 0.8);


            //Get the window height and width
            var winH = $(window).height();
            var winW = $(window).width();

            //Set the popup window to center
            $(id).css('top', winH / 2 - $(id).height() / 2);
            $(id).css('left', winW / 2 - $(id).width() / 2);

            //transition effect
            $(id).show();
            $('#imgErr').animate({ height: 30 }, "slow");
            $('#imgErr').animate({ width: 150 }, "slow");
            $('#imgErr').animate({ height: 70 }, "slow");
            $('#imgErr').animate({ width: 70 }, "slow");
            //$('#AttnMsg').slidedown("slow");
        }

    </script>
    <style type="text/css">
        #popup_mask {
            position: absolute;
            left: 0;
            top: 0;
            z-index: 9000;
            background-color: rgb(245,245,245);
            display: none;
            opacity: 0.1;
            moz-opacity: 0.1;
        }

        .popup-window {
            position: fixed;
            left: 0;
            top: 0;
            width: 440px;
            height: 160px;
            background-color: white;
            border: solid 3px gray;
            display: none;
            z-index: 9999;
            padding: 20px;
        }
    </style>
</head>
<body style="margin: 0;">
    <form id="frmRoot" runat="server" style="height: 93%">
        <%--<div style="margin-bottom: 2px; width: 100%;">
        <table width="99%" cellpadding="0" cellspacing="0" border="0" style="background-image: url('Images/hdr-y.png');
            background-repeat: repeat-x; table-layout: fixed;">
            <tr>
                <td style="width: 85px;">
                    <img src="Images/Logo-no-text.png" style="height: 55px; width: 85px; visibility: hidden;" />
                </td>
                <td>
                    <div class="div-banner">
                        <span>E</span>xpense <span>M</span>anagement <span>S</span>ystem.
                    </div>
                </td>
                <td style="width: 16%; padding-top: 7px;" valign="middle">
                    <%--<img src="Images/TCE-Logo-small.png" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <div class1="div-banner-bottom" style="height: 2px; background-color: White;">
                    </div>
                    <div class1="div-banner-bottom" style="height: 5px; background-color: rgb(98,98,98);">
                    </div>
                </td>
            </tr>
        </table>
    </div>--%> <div style="width:100%; text-align: center">
                <img alt="logo_text" src="/Layout/Images/Sample 2.png" width="100%"  />
            </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <div class="CenterContainer">
           
            
            <div style="text-align: center;">
                <table style="border: 1px solid red;border-spacing:10px;width:50%; text-align: left;"
                    align="center" cellspacing="5px">
                   
                    <tr>
                        <td ></td> <td ></td>
                    </tr>
                    <tr>
                        <td  style="width: 40%;font-family:Verdana;font-size:12px">&nbsp;&nbsp;&nbsp;&nbsp;<strong>Login ID:<br />
                            </strong>&nbsp;&nbsp;&nbsp;(Employee Code)</td>
                        <td >
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="LoginInput" Width="95%" OnTextChanged="txtUserName_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td ></td>
                    </tr>
                    <tr>
                        <td style="width: 40%;font-family:Verdana;font-size:12px">&nbsp;&nbsp;&nbsp;&nbsp;<strong>Password:<br />
                            </strong>&nbsp;&nbsp;&nbsp;(System Password)</td>
                        <td class="InputArea">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="LoginInput" Width="95%" OnTextChanged="txtPassword_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td ></td>
                    </tr>
                    <tr>
                        <td colspan="2" class="LoginActions" style="text-align: right; padding-right: 10px;">
                            <asp:Button ID="btnSignIn" runat="server" Text="Login" CssClass="ActionBtn" OnClientClick1="return Validate();"
                                OnClick="btnSignIn_Click" BorderColor="#666666" BorderStyle="Solid" />
                        </td>
                    </tr>
                    <tr style="vertical-align: top;">
                        <td colspan="2" style="text-align: center;">
                            <div id="divProcess" style="display: none;">
                                <img src="<%= ResolveUrl("~") %>Resx/Common/img/Processing.gif" alt="" width="30px"
                                    height="25px" style="vertical-align: middle;" />
                                Authenticating, please wait...
                            </div>
                            <div id="divErrMsg" runat="Server" style="color: Red; text-align: center; display: none;">
                                <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="Copyright" style="text-align: justify;">
            </div>
            <div id="divErrorMsg" align="center" style="display: none;" class="popup-window">
                <div>
                    <img id="imgErr" runat="server" alt="" src="~/Images/Exclamation.png" />
                </div>
                <div style="margin-top: 2px; font-weight: bold; text-align: center;">
                    <u>Attention:</u>
                </div>
                <div id="AttnMsg">
                    <p>
                        This Application is compatible to <u>Microsoft Internet Explorer </u>[Version 7
                    or higher]. Please <u>Re-open in Internet Explorer</u> or <u>install Internet Explorer</u>
                        to run this application.
                    </p>
                </div>
            </div>
        </div>
        <div class="sitedetails" style="background-color: rgb(220,220,220); padding-top: 2px; margin-top: 120px; text-align: center;">
            All rights reserved by Tata Consulting Engineers Limited, Mumbai, India. &copy;
            <%=DateTime.Now.Year%>
            <br />
            This site supports Google Chrome and Firefox.</div>
        <div id="popup_mask">
        </div>
    </form>
</body>
</html>
