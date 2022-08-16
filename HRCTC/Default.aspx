<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="frmGenerateSystemPwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <style type="text/css">
body {
	font-size: 12px;
	font-family: arial, helvetica, sans-serif;
	color: #333;
}
p {
	margin: 1em;
}
.comments {
	background-color: #e3e3e3;
	border-top: 1px solid #ccc;
	border-bottom: 1px solid #ccc;
	padding: 2px;
}

.mydiv {
	position:absolute;
	top: 50%;
	left: 50%;
	width:30em;
	height:18em;
	margin-top: -9em; /*set to a negative number 1/2 of your height*/
	margin-left: -15em; /*set to a negative number 1/2 of your width*/
	border: 1px solid #ccc;
	background-color: #f3f3f3;
}

     
  </style>



<script type="text/javascript">

    function myFunction()
    {
        alert('OTP generated for Compensation Re-structuring and an email is sent you.');
    }

</script>

</head>
<body onunload="myFunction()">
    <form id="form1" runat="server">
        <div class="mydiv">
            <p>One Time Password</p>
              <asp:HiddenField ID="hdnFinancialYearCurr" runat="server" />
            <div class="comments">
                <p><strong>Email ID:</strong></p>
                <p>


                    <asp:Label ID="txtEmail" BorderStyle="Solid" Width="95%" BorderWidth="1px" BorderColor="Black" BackColor="White" runat="server"></asp:Label>
                </p>
                <p style="text-align: center">

                    <asp:Button ID="btnGeneratePwd" runat="server" Text="Generate OTP " OnClick="btnGeneratePwd_Click" />
                    <asp:Label ID="lblMsg" runat="server" Width="95%"></asp:Label>

                </p>
            </div>
        </div>



    </form>
</body>
</html>
