<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmUploadCTCData.aspx.cs" Inherits="frmUploadCTCData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <td colspan="6">
                Upload Base CTC
            </td>
        </tr>
        <tr>
            <td>Select Excel to Upload</td><td><asp:FileUpload ID="flup" runat="server" /> </td><td><asp:Button Text="Upload" runat="server" ID="btnUpload" /> </td><td><asp:Button ID="btnDownloadTemplt" runat="server" Text="Download Template" /> </td><td><asp:Button ID="btnClose" runat="server" Text="Close" /> </td>
        </tr>
        <tr>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
