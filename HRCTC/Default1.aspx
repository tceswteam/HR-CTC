<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default1.aspx.cs" Inherits="Default1" %>

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
    text-align:center;
}
.comments {
	background-color: #e3e3e3;
	border-top: 1px solid #ccc;
	border-bottom: 1px solid #ccc;
	padding: 2px;
}

#mydiv {
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
</head>
<body>
    <form id="form1" runat="server">
    <div id="mydiv">
            <p>One Time Password</p>

            <div class="comments">
                <p><strong>OTP for Compstruct:</strong></p>
                <p>

   <asp:TextBox ID="txtOTP" runat="server"></asp:TextBox>
                </p>
                <p style="text-align: center">

               <asp:Button ID="btnLoginForCompRestruc" runat="server" OnClick="btnLoginForCompRestruc_Click" Text="Login To Comp Restruc" />
                     <asp:Label ID="lblMessage" runat="server"></asp:Label>

                </p>
            </div>
        </div>
    
       <%-- <div id="divforOTP" style="width: auto; margin: 0 ;text-align:center" >
            <asp:Label ID="lbl" runat="server" Text="OTP for Compstruct"></asp:Label>
         
          
        
        </div>--%>
    
   
    </form>
</body>
</html>
