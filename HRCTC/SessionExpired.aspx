<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SessionExpired.aspx.cs" Inherits="SessionExpired" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Your Session has Expired !</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 90%; font-family: Verdana;">
        <div>
            <div style="margin-left: 40px; width: 82%; margin-top: 80px; margin-right: 20px; margin-bottom: 20px;">
                <table border="0" cellpadding="10" cellspacing="0" style="color: rgb(66,130,206); width: 78%;">
                    <tr valign="top">
                        <td style="width: 80px;">
                            <img src="Images/imgNoSession.gif" alt="Session Expired" />
                        </td>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" style="color: rgb(82,85,82);">
                                <tr valign="top">
                                    <td style="height: 1px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td style="color: rgb(24,56,165); font-size: large; font-weight: bold;">
                                        Warning : Your Login Session Has Expired.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <hr style="background-color: rgb(181,190,198); height: 1px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 1px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 10pt; padding-left1: 10px;">
                                        Your current login session has expired.<br />
                                        For security reasons, Application Session expires after a certain amount of time.
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 10pt;">
                                        Possible reasons:
                                        <ul style="padding-top: 0px; margin-top: 4px;">
                                            <li>You might have left the page idle for a long time. </li>
                                            <li>The application is encountering problems. </li>
                                        </ul>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 10pt;">
                                        What you can try:
                                        <ul style="padding-top: 0px; margin-top: 2px;">
                                            <li><a href="Login.aspx<%=this.ReturnURL %>" style="color: rgb(8,138,206); cursor: hand; text-decoration: underline;">
                                                click here to login again.</a> </li>
                                        </ul>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
