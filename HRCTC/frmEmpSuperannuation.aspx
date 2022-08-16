<%@ Page Debug="true" Language="C#" AutoEventWireup="true" CodeFile="frmEmpSuperannuation.aspx.cs" Inherits="frmEmpCTCRestructNewWindow" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Layout/Forms.css" rel="stylesheet" />
    <%-- <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />--%>
    <title></title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.0/jquery.min.js"></script>
    <style type="text/css">
         .modalBackground
            {
                background-color: Black;
                filter: alpha(opacity=60);
                opacity: 0.6;
            }
            .modalPopup
            {
                background-color: #FFFFFF;
                width: 500px;
                /*border: 3px solid #0DA9D0;
                border-radius: 12px;*/
                padding:0
            }

            .modalPopup .header
            {
                background-color: darkgray;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                /*border-top-left-radius: 6px;
                border-top-right-radius: 6px;*/
            }

            .modalPopup .body
            {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .footer
            {
                padding: 6px;
            }

            .modalPopup .yes, .modalPopup .no
            {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }

            .modalPopup .yes
            {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }
            .modalPopup .no
            {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }
    </style>
    <style>     
        /*input[type=text]::-ms-clear { display: none; }*/
        /*input[type=text]::-ms-clear { width:0;height:0; }*/
        input[type=text]::-ms-clear 
        {
        color: red; /* This sets the cross color as red. */
        /* The cross can be hidden by setting the display attribute as "none" */
        }
       
        .inline 
        { 
        display: inline-block; 
        width:80%;
        }
            
         #rcorners4 
         {
                border-radius: 15px 15px 15px 15px;          
                padding: 20px;
                width: 100%;
                margin: 0px auto;
                height: 20px;
                text-align: center;
                font-family: sans-serif;
                font-size: 17px;            
         }
        
         #rcorners5
         {
                background: #dee0ef ; 
                padding: 20px;
                width: 70%;
                height: 30%;
                margin: 0px auto;
                font-family: sans-serif;
                font-size: 17px;
                color: GrayText;
         }

         #rcorners6 
         {
                border-radius: 15px 15px 15px 15px;
                background: #dee0ef   ; 
                border: 2px solid #8B8989;
                padding: 20px;
                width: 70%;
                height: 50%;
                margin: 0px auto;
                font-family: sans-serif;
                font-size: 17px;
                color: black;
         }

         #rcorners7 
         {
                border-radius: 15px 15px 15px 15px;
                background: #F1F1F2;
                padding: 20px;
                width: 70%;
                height: 50%;
                margin: 0px auto;
                font-family: sans-serif;
                font-size: 17px;
                color: black;
         }

     div#container 
     {
            width: 0px;     
            padding: 0px;
            border: 0px solid #1a1a1a;
     }

     .container 
     {
            padding: 0px;
            border: 0px solid #1a1a1a;
     }

     div#pop-up 
     {
          -webkit-border-radius: 8px 8px 8px 8px;
          -moz-border-radius: 8px 8px 8px 8px;

          border-radius:10px 10px 10px 10px;
          border-color:#969aff;
            display: none;
            position: absolute;
            width: 300px;
            padding: 10px;
            background-color:#f0f0ff;
            color: navy;
            border: 1px solid #1a1a1a;
            font-size: 90%;
            overflow:auto;
     }

     div#pop-up1 
     {
          -webkit-border-radius: 8px 8px 8px 8px;
          -moz-border-radius: 8px 8px 8px 8px;

           border-radius:10px 10px 10px 10px;
          border-color:#969aff;
            display: none;
            position: absolute;
            width: 300px;
            padding: 10px;
            background-color:#f0f0ff;
            color: navy;
            border: 1px solid #1a1a1a;
            font-size: 90%;
            overflow:auto;
     }

     div#pop-up2 
     {
          -webkit-border-radius: 8px 8px 8px 8px;
  -moz-border-radius: 8px 8px 8px 8px;

           border-radius:10px 10px 10px 10px;
          border-color:#969aff;
            display: none;
            position: absolute;
            width: 300px;
            padding: 10px;
            background-color:#f0f0ff;
            color: navy;
            border: 1px solid #1a1a1a;
            font-size: 90%;
            overflow:auto;
     }

      div#pop-up3
      {
           -webkit-border-radius: 8px 8px 8px 8px;
           -moz-border-radius: 8px 8px 8px 8px;
            border-radius:10px 10px 10px 10px;
            border-color:#969aff;
            display: none;
            position: absolute;
            width: 300px;
            padding: 10px;
            background-color:#f0f0ff;
            color: navy;
            border: 1px solid #1a1a1a;
            font-size: 90%;
            overflow:auto;
      }

      div#pop-up4 
      {
        -webkit-border-radius: 8px 8px 8px 8px;
        -moz-border-radius: 8px 8px 8px 8px;
        border-radius:10px 10px 10px 10px;
        border-color:#969aff;
        display: none;
        position: absolute;
        width: 300px;
        padding: 10px;
        background-color:#f0f0ff;
        color: navy;
        border: 1px solid #1a1a1a;
        font-size: 90%;
        overflow:auto;
      }

      div#pop-up5 
      {
        -webkit-border-radius: 8px 8px 8px 8px;
        -moz-border-radius: 8px 8px 8px 8px;
        border-radius:10px 10px 10px 10px;
        border-color:#969aff;
        display: none;
        position: absolute;
        width: 300px;
        padding: 10px;
        background-color:#f0f0ff;
        color: navy;
        border: 1px solid #1a1a1a;
        font-size: 90%;
        overflow:auto;
      }

      div#pop-up6 
      {
        -webkit-border-radius: 8px 8px 8px 8px;
        -moz-border-radius: 8px 8px 8px 8px;
        border-radius:10px 10px 10px 10px;
        border-color:#969aff;
        display: none;
        position: absolute;
        width: 300px;
        padding: 10px;
        background-color:#f0f0ff;
        color: navy;
        border: 1px solid #1a1a1a;
        font-size: 90%;
        overflow:auto;
      }

      div#pop-up7 
      {
        -webkit-border-radius: 8px 8px 8px 8px;
        -moz-border-radius: 8px 8px 8px 8px;
        border-radius:10px 10px 10px 10px;
        border-color:#969aff;
        display: none;
        position: absolute;
        width: 300px;
        padding: 10px;
        background-color:#f0f0ff;
        color: navy;
        border: 1px solid #1a1a1a;
        font-size: 90%;
        overflow:auto;
      }

      div#pop-up8 
      {
        -webkit-border-radius: 8px 8px 8px 8px;
        -moz-border-radius: 8px 8px 8px 8px;
        text-align:left;
        border-radius:10px 10px 10px 10px;
        border-color:#969aff;
        display: none;
        position: absolute;
        width: 300px;
        padding: 10px;
        background-color:#f0f0ff;
        color: navy;
        border: 1px solid #1a1a1a;
        font-size: 11px;
        overflow:auto;
       }
       
      div#pop-up9
      {
        -webkit-border-radius: 8px 8px 8px 8px;
        -moz-border-radius: 8px 8px 8px 8px;
        border-radius:10px 10px 10px 10px;
        border-color:#969aff;
        display: none;
        position: absolute;
        width: 300px;
        padding: 10px;
        background-color:#f0f0ff;
        color: navy;
        border: 1px solid #1a1a1a;
        font-size: 90%;
        overflow:auto;
        }

       div#pop-up10
      {
        -webkit-border-radius: 8px 8px 8px 8px;
        -moz-border-radius: 8px 8px 8px 8px;
        border-radius:10px 10px 10px 10px;
        border-color:#969aff;
        display: none;
        position: absolute;
        width: 500px;
        padding: 10px;
        background-color:#f0f0ff;
        color: navy;
        border: 1px solid #1a1a1a;
        font-size: 90%;
        overflow:auto;
        }
         div#pop-up11
      {
        -webkit-border-radius: 8px 8px 8px 8px;
        -moz-border-radius: 8px 8px 8px 8px;
        border-radius:10px 10px 10px 10px;
        border-color:#969aff;
        display: none;
        position: absolute;
        width: 300px;
        padding: 10px;
        background-color:#f0f0ff;
        color: navy;
        border: 1px solid #1a1a1a;
        font-size: 90%;
        overflow:auto;
        }
     
           div#pop-up12
      {
        -webkit-border-radius: 8px 8px 8px 8px;
        -moz-border-radius: 8px 8px 8px 8px;
        border-radius:10px 10px 10px 10px;
        border-color:#969aff;
        display: none;
        position: absolute;
        width: 300px;
        padding: 10px;
        background-color:#f0f0ff;
        color: navy;
        border: 1px solid #1a1a1a;
        font-size: 90%;
        overflow:auto;
        }
       div#pop-up13
      {
        -webkit-border-radius: 8px 8px 8px 8px;
        -moz-border-radius: 8px 8px 8px 8px;
        border-radius:10px 10px 10px 10px;
        border-color:#969aff;
        display: none;
        position: absolute;
        width: 300px;
        padding: 10px;
        background-color:#f0f0ff;
        color: navy;
        border: 1px solid #1a1a1a;
        font-size: 90%;
        overflow:auto;
        }
     
     
        .hidden 
        {
            display: none;
        }
        .lblForWhite 
        {
            background-color: white;
            width: 100px;
        }
        .auto-style1 {
            text-align: left;
            font-family: Verdana, Geneva, Tahoma, sans-serif; /*background-color: rgb(245,245,245);
     background-color: yellow;*/ /*background-color: rgb(230,234,247);*/;
            font-size: 12px;
            width: 15%;
        }
        ul
	{margin-bottom:0cm;}
        </style>  
    <script>
        function RedirectToLogin()
        {
            alert('CTC Changes has been successfully saved !');
            window.close();
        }
        function IsNumeric(textbox, evt) {
            if (!textbox.value.match('^[0-9]+$') && textbox.value.length > 0) {
                alert('Only Numeric Values [0-9] are Allowed.');
                textbox.value = "";
                textbox.focus();
            }
            if (textbox.value == '')
                textbox.value = "0";

        };

        function IsNumberOrDecimal(textbox) {

            if (!textbox.value.match('^[-]?([1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?|0(\.[0-9]{0,2})?|\.[0-9]{1,2})$') && textbox.value.length > 0) {
                alert('Please enter valid integer or decimal number with 2 decimal places.');
                textbox.value = "";
                textbox.focus();
            }
        };

        function Compare_Data(Compare1, Compare2, e) {
            // alert('hi');
            try {

                if (e.keyCode == 13) {
                    PerformCalculation();
                }

                var a = document.getElementById(Compare1.id).value;
                var b = document.getElementById(Compare2.id).innerHTML;
                if (e.keyCode == 9) {
                    if (eval(a) > eval(b)) {
                        {
                            alert("Value cannot exceed the limit :-" + b);
                            document.getElementById(Compare1.id).value = "";
                            document.getElementById(Compare1.id).focus();
                        }
                    }
                }
                else {
                    if (eval(a) > eval(b)) {
                        {
                            alert("Value cannot exceed the limit " + b);
                            document.getElementById(Compare1.id).value = "";
                            document.getElementById(Compare1.id).focus();
                        }
                    }
                }
                PerformCalculation();
            }
            catch (e) {
            }
        }

        function submitResult() {
            if (confirm("Are you sure for your CTC changes?") == false) {
                return false;
            } else {
                return true;
            }
        }

        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 10);
        window.onunload = function () { null };

        function CheckForValueRangeNPS(minRange, currentvalue, valueoffield) {
            var minval = minRange;

            var currentval = eval(document.getElementById(currentvalue.id).value);
            //alert(eval(currentval) + ' ' + eval(minRange) + ' ' + eval(maxRange));
            //if (eval(currentvalue) < eval(minRange) || eval(currentvalue) > eval(maxRange))
            if (currentval == "")
                currentval = "0";
            if (currentval < minRange) {

                document.getElementById(currentvalue.id).value = "0";
                alert("Value of " + valueoffield + " should be at least :-  " + minval);
            }

        }

        function CheckForValueRangeHRA(minRange, maxRange, currentvalue, valueoffield) {
            var minval = minRange;
            var maxval = maxRange;
            var currentval = eval(document.getElementById(currentvalue.id).value);
            //alert(eval(currentval) + ' ' + eval(minRange) + ' ' + eval(maxRange));
            //if (eval(currentvalue) < eval(minRange) || eval(currentvalue) > eval(maxRange))
            if (currentval == "")
                currentval = "0";

            //alert(eval(maxRange));
            if (eval(currentval) < eval(minRange) || eval(currentval) > eval(maxRange)) {
              //  alert('hi');
                document.getElementById(currentvalue.id).value = "0";
                alert("Value of NPS " + valueoffield + " should be in range of " + minval + "-" + maxval);
            }
        }
    </script> 
  
</head>
<body>
    <form id="form1" runat="server">
      <div style="background-color:white;   " >
      
          <table  style="background-color:white; width: 70%;margin:auto; border: 2px solid #8B8989;border-radius: 15px 15px 15px 15px;      ">
              
              <tr>
                    <td  >
                      <asp:Image ID="Image1" runat="server" width="100%" ImageUrl="~/Layout/Images/Sample 3.png" />
                  </td>
                
              </tr>
              <tr>
                  <td >
                      <table style="width: 100%; padding: 10px">

                          <tr>
                              <td>
                                  <table style="width: 100%">
                                      <tr>
                                          <td class="form-fld-lbl" style="width: 20%;">Employee Name (Code) </td>
                                          <td class="form-fld-lbl" style="width: 1%">:</td>
                                          <td class="form-fld-lbl" style="width: 34%">
                                              <asp:Label ID="lblEmployeeName" class="form-fld-lbl" runat="server"></asp:Label>&nbsp;&nbsp;(<asp:Label ID="lblEmpCode" class="form-fld-lbl" runat="server">
                                              </asp:Label>)
                                          </td>
                                          <td class="form-fld-lbl" style="width: 14%;">Date of Joining</td>
                                          <td class="form-fld-lbl" style="width: 1%">:</td>
                                          <td class="form-fld-lbl" style="width: 30%">
                                              <asp:Label ID="lblDateofJoining" class="form-fld-lbl" runat="server"></asp:Label>
                                          </td>
                                      </tr>
                                      <tr>


                              <td class="form-fld-lbl" style="width: 20%;">Pan Card</td>
                              <td style="width: 1%">:</td>
                              <td style="width: 34%" class="form-fld-lbl">
                                  <asp:Label ID="lblEmpPanCard" class="form-fld-lbl" runat="server"></asp:Label>
                              </td>

                              <td class="form-fld-lbl" style="width: 14%;">Grade</td>
                              <td style="width: 1%">:</td>
                              <td style="width: 30%">
                                  <asp:Label ID="lblGrade" runat="server" class="form-fld-lbl"></asp:Label>
                              </td>
                          </tr>
                                  </table>
                              </td>
                          </tr>
                          

                      </table>
                  </td>
              </tr>
              <tr>
                    <td colspan="2" >
                        <hr />
                         <span class="form-fld-lbl" style="font-family: Verdana; font-size: 12px; color: #800000;">Instructions:</span>
                          <ol type="1" style="font-family: Verdana; font-size: 12px;">
                      
                              <li>In view of the COVID situation, option to choose Superannuation contribution is being given again to employees in E6/A6/T6 and above grades as a special case. Eligible employees will be able to reduce/ increase their current contribution in Superannuation to be 5% OR 10% OR 15% of basic salary. Accordingly the amount will be adjusted with Additional Allowance (AA).</li>
                              <li>Employees who are interested to increase their retirement corpus may choose to invest in NPS. In such case, employees are required to open corporate NPS account and submit relevant details to DC-HR before 15th of the month. If an employee already has NPS amount in CTC, s/he can increase their current investment in NPS or continue with same amount in rest of the financial year. Any increase in current NPS amount will be adjusted against AA.</li>
                              <li>
                                  Please refer to <a href="https://ind01.safelinks.protection.outlook.com/?url=https%3A%2F%2Fctc.tce.co.in%3A8282%2FDocuments%2FM9HR36AR6%2520-%2520Compensation%2520Restructuring%2520(CompStruct)_User%2520Manual.pdf&amp;data=02%7C01%7Cchetnak%40tce.co.in%7Cabc316ebaf4b458d0b5a08d860481859%7C5af76741f8864d20ad04775dee0ce762%7C0%7C0%7C637365210043653749&amp;sdata=sL3TDWh%2BafREQx6R51JdxZJh%2BEibeKH1yqqrbCrimc8%3D&amp;reserved=0" originalsrc="https://ctc.tce.co.in:8282/Documents/M9HR36AR6%20-%20Compensation%20Restructuring%20(CompStruct)_User%20Manual.pdf" shash="rjqyuMwhWmjXLNH3cDGjqMm/xqKqxqBNdvgX+skyvNWVCAJXXPwIlhyykn+uimM4UABhK2EeII6NGpEQy6jZ748RG0hztsecxrdQkoIDdKRxwH1pVlESfPUMqbYRHXH/HT7HJC4t6JOYzYbU80OVk/Nd3JO2EcFjMzADPgdVQ5w=" target="_blank">
                                      TCE.M9-HR-PA-36A : Compensation Restructuring (CompStruct) – User Manual.pdf” </a>before restructuring your CTC. 
                                 
                              </li>
                              <li>
                                  The compensation restructuring will be 
                                          <ul >
                                              <li >effective 1st April 2020 for employees with date of joining till 31<sup>st</sup> March 2020.
                                              <li >effective as per guidelines mentioned&nbsp; in Compensation Restructuring User Manual for employees with date of joining after 31<sup>st</sup> March 2020.
                                          </ul>
                                      </li>
                                 
                              <li><strong>CompStruct will be open till 15th October 2020. Employees will be required to restructure and submit restructured CTC online by 15th October 2020. In case, an employee does not submit the restructured CTC by 15th October 2020, prevailing CTC will continue for the rest of the Financial Year.</strong></li>
                              <li>Employees are requested to understand the impact of their choice on their Retirement corpus and Income Tax calculations before restructuring compensation suiting to their individual needs. Once the restructured compensation is submitted, no modification will be permitted after submission.  </li>
                              <li>For any query write to  <a href="mailto:corporatehr@tce.co.in" target="_top">corporatehr@tce.co.in </a> </li>
                        </ol>
                    </td>
                 
              </tr>
          </table>
            
      </div>
        <div style="background-color:white;" >
            
             
            <table style="margin: 0px auto; width: 70%; text-align: center;">
                <tr>
                    <td >
                        <%--<asp:ImageButton ID="ImageButton1" ImageUrl="~/Layout/Images/Save As Draft.png" runat="server" Text="Save As Draft" OnClick="btnApplyCTC_Click"  ForeColor="white" Width="94px" Height="25px" />--%>&nbsp;&nbsp;
                        <asp:ImageButton ID="btnPrintCTCRestruc0" Text="Save File"  ImageUrl="~/Layout/Images/Submit.png" runat="server" OnClick="OnConfirmPrint" OnClientClick="return submitResult();"  Height="25px" Width="94px"  ForeColor="white" />
                        &nbsp;&nbsp;<asp:ImageButton ID="btnPullback" Text="Revoke" runat="server"  ImageUrl="~/Layout/Images/Revoke.png" OnClick="btnPullback0_Click" CssClass="hidden"  Width="94px"  Height="20px" ForeColor="white" />
                        &nbsp;<asp:ImageButton ID="btnPrintCTCRestruc"  Height="25px" Text="Print Restructured CTC"  ImageUrl="~/Layout/Images/Save As Draft.png" runat="server" OnClick="btnPrintCTCRestruc_Click"  Width="94px"  CssClass="hidden" />
                        <a href="../Logout.aspx" onclick="preventBack();"  style="color: White;">   <img alt="" src="Layout/Images/Logout.png" height="25px" width="94px" />   </a></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEmpCTCRestrucStatus" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table id="rcorners6"  >

                <tr>
                    <td colspan="4" style="text-align:center;font-family:Verdana;font-size :16px;">
                       <b>Superannuation Restructuring : FY
                            <asp:Literal ID="ltrFinYr" runat="server"></asp:Literal></b>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
                
                <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl" style="width: 5%;text-align:center;"></td>
                    <td class="form-fld-lbl" style="width: 35%;text-align:center;vertical-align:top">Description</td>
                    <td class="auto-style1" style="text-align:center;vertical-align:top">Existing Amount&nbsp; <asp:Label ID="lblYearEndDate" runat="server" Visible="false"></asp:Label> </td>
                    <td class="form-fld-lbl" style="width: 15%;text-align:center;vertical-align:top">Revised Amount&nbsp; <asp:Label ID="lblYearStartDate" runat="server" Visible="false"></asp:Label></td>                    
                </tr>
                <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
                <tr id="trMobile">
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Basic</td>
                    <td class="auto-style1" style="background-color: white;">
                        <asp:Label ID="lblBasicExisting" runat="server" CssClass="numerictextboxlabel" Width="92%">0</asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblBasicNew" runat="server" CssClass="numerictextboxlabel" Width="92%">0</asp:Label>
                    </td>  
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl" style="vertical-align:middle;">Additional Allowance</td>
                    <td class="auto-style1" style="background-color: #FFFFFF">
                        <asp:Label ID="lblAAExisting" runat="server" CssClass="numerictextboxlabel" Width="92%">0</asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblAA" runat="server" CssClass="numerictextboxlabel"  Width="92%">0</asp:Label>
                    </td>
                </tr>

                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Superannuation Percentage</td>
                    <td  style="background-color: #ffffff">
                        <asp:Label ID="lblSAPercExisting" runat="server" CssClass="numerictextboxlabel" Width="92%">0</asp:Label>
                    </td>
                    <td class="form-fld-lbl">
                        <asp:RadioButtonList ID="rdoSAPerc" class="form-fld-lbl" runat="server" OnSelectedIndexChanged="rdoSAPerc_SelectedIndexChanged" AutoPostBack="True" RepeatDirection="Horizontal" >
                            <asp:listitem Value="5">5%</asp:listitem> 
                            <asp:listitem Value="10">10%</asp:listitem> 
                            <asp:listitem Value="15" >15%</asp:listitem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
               <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">
                                        Superannuation Amount</td>
                    <td class="auto-style1" style="background-color: white;">
                        <asp:Label ID="lblSAExisting" runat="server" CssClass="numerictextboxlabel" Width="92%">0</asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblSAAmount" runat="server" CssClass="numerictextboxlabel" Width="92%">0</asp:Label>
                    </td>
                </tr>
               <tr>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">
                                        NPS</td>
                    <td class="auto-style1" style="background-color: white;">
                        <asp:Label ID="lblNPSExisting" runat="server" CssClass="numerictextboxlabel" Width="92%">0</asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txtNPS" runat="server" CssClass="numerictextboxlabel"  Width="92%" AutoPostBack="True" OnTextChanged="txtNPS_TextChanged">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
            </table>


        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:HiddenField ID="hdnIsEmpRestructuredCTC" runat="server" />
        <asp:HiddenField ID="hdnIsEmpFinalizedCTCRestruc" runat="server" />
        <asp:HiddenField ID="hdnFormMode" runat="server" Value="0" />
        <asp:HiddenField ID="hdnEmpSelectedSuperanuationOption" runat="server" />
        <asp:HiddenField ID="hdnAddtionalAllowanceApp" runat="server" />
        <asp:HiddenField ID="hdnSN" runat="server" />
        <asp:HiddenField ID="hdnSNWithAAOrTrfAA" runat="server" />
        <asp:HiddenField ID="hdnSNNPS" runat="server" />
        <asp:HiddenField ID="hdnComputedCTCRestruct" runat="server" />
        <asp:HiddenField ID="hdnMealCardValue" runat="server" />

        <asp:HiddenField ID="hdnSuperAnnuationLimit" runat="server" />

        <asp:HiddenField ID="hdnSuperAnnuationUpperLimit" runat="server" />
        <asp:HiddenField ID="hdnFinancialYearPrev" runat="server" />
        <asp:HiddenField ID="hdnFinancialYearCurr" runat="server" />

        <asp:Button ID="hdnbtnForRevoke" runat="server" Style="display: none" OnClientClick1="showProgress()" OnClick="hdnbtnForRevoke_Click" CausesValidation="false" />
    </form>
</body>
</html>
