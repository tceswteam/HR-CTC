<%@ Page Debug="true" Language="C#" AutoEventWireup="true" CodeFile="frmEmpCTCRestructNewWindow.aspx.cs" Inherits="frmEmpCTCRestructNewWindow" %>

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
    </script> 
  
</head>
<body>
    <form id="form1" runat="server">
      <div style="background-color:white;   " >
      
          <table  style="background-color:white; width: 70%;margin:auto; border: 2px solid #8B8989;border-radius: 15px 15px 15px 15px;      ">
              
              <tr>
                    <td  >
                      <asp:Image ID="Image1" runat="server" width="100%" ImageUrl="~/Layout/Images/Sample 2.png" />
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
                      
                              <li>CompStruct will be opened two times in the financial year as given below:<ol type="a"><li>Once for restructuring at the beginning of Financial Year where employees can choose for Meal Card options and decide on Mobile Reimbursement amount. After employee chooses “Meal Card” and “Mobile Reimbursement” amount, it will continue for the rest of the financial year.</li><li>Second time for restructuring after reward letters are issued to employees. Employees will be allowed to do restructuring on components other than Meal Card and Mobile Reimbursement.</li></ol></li>
                              
                              <li>CompStruct is opened now for employees to choose for Meal Card options and decide on Mobile Reimbursement amount. The amount chosen in these two components can be restructured with Additional Allowance (AA) only. After employee chooses Meal Card and Mobile Reimbursement amount, it will continue for the rest of the financial year.  </li>
                              <li>Employees having Company provided mobile phones or employees who are permitted to claim reimbursement of personal mobile bill for official calls (due to demand of the role) will not be eligible to claim above mobile reimbursement for tax exemption purpose. Hence such employees should not opt for Mobile Reimbursement.</li>
                              <li><strong>Meal Card : </strong> The amount in the Meal Card will be loaded at the beginning of each month and shall be fully utilised on expenses towards meal during the working hours. The electronic meal cards can be used in the company provided canteen facility or select restaurants. This amount is exempted from tax as per current IT rules. Balance amount in the Meal Card as on 31st March, will be added to the taxable income of the employee.</li>
                              <li><strong>Mobile Reimbursement :</strong> Mobile Reimbursement can be claimed through Expense Management System (EMS) every month. Employee can claim Mobile Reimbursement of bill related to post-paid mobile for one SIM card (in the name of employee only) on monthly basis. Mobile bill should be uploaded on EMS for claiming the bill amount. The claim should be done in EMS within 15 days from the date of the bill.  Unclaimed mobile reimbursement amount will be paid at the end of the financial year along with April month’s salary and taxed as per IT rules. </li>
                              <li>Employees are requested to understand prevailing income tax rules before restructuring compensation suiting to their individual needs.  </li>
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
                        <%--<asp:ImageButton ID="ImageButton1" ImageUrl="~/Layout/Images/Save As Draft.png" runat="server" Text="Save As Draft" OnClick="btnApplyCTC_Click"  ForeColor="white" Width="94px" Height="25px" />--%>&nbsp;&nbsp;<asp:ImageButton ID="btnPrintCTCRestruc0" Text="Save File"  ImageUrl="~/Layout/Images/Submit.png" runat="server" OnClick="OnConfirmPrint" OnClientClick="return submitResult();"  Height="25px" Width="94px"  ForeColor="white" />
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
                    <td colspan="6" style="text-align:center;font-family:Verdana;font-size :16px;">
                       <b>Compensation Restructuring : FY
                            <asp:Literal ID="ltrFinYr" runat="server"></asp:Literal></b>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <hr />
                    </td>

                </tr>
                <tr>
                    <td colspan="6" style="text-align:center;font-family:Verdana;font-size :16px;">
                        Regime option selected by you is : <asp:Label ID="lblRegimeOption" runat="server" Font-Size="16px" Font-Names="Verdana" ForeColor="Red"></asp:Label>
                        <br />
                        <span class="Required"  style="text-align:center;font-family:Verdana;font-size :16px;">

                             If  Employee  opted New Regime Meal Card and Mobile Reimbursement changes are not applicable.</span>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <hr />
                    </td>

                </tr>
                 
                <tr>
                    <td class="form-fld-lbl" style="width: 5%;text-align:center;"></td>
                    <td class="form-fld-lbl" style="width: 35%;text-align:center;vertical-align:top">Description</td>
                    <td class="form-fld-lbl" style="width: 15%;text-align:center;vertical-align:top">Existing Amount as on <asp:Label ID="lblYearEndDate" runat="server"></asp:Label> </td>
                    <td class="form-fld-lbl" style="width: 15%;text-align:center;vertical-align:top">Revised Amount as on <asp:Label ID="lblYearStartDate" runat="server"></asp:Label></td>                    
                    <td class="form-fld-lbl" style="width: 10%;text-align:center;vertical-align:top">Ceiling amount for tax exemption (Rs.)</td>
                    <td class="form-fld-lbl" style="width: 20%">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6">
                        <hr />
                    </td>
                </tr>
                <tr id="trMobile">
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Mobile Reimbursement</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblMobileExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblMobile" runat="server" CssClass="numerictextboxlabel" Width="92%">0</asp:Label>
                        <asp:TextBox ID="txtMobile" runat="server" CssClass="numerictextboxlabel" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;"  OnTextChanged="txtMobile_TextChanged" AutoPostBack="true" Width="92%" ToolTip="New component introduced with ceiling of Rs. 2000/- per month. Restructuring allowed with AA only. Restructuring not applicable for M Grades." >0</asp:TextBox>                                              
                    </td>  
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblMobileMaxCeiling" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label></td>
                    <td class="form-fld-lbl" >
                        <div id="1238" class="container">
                            <div id="pop-up5">
                                <h4>Mobile Reimbursement </h4>                               
                                <p>
                                Employee can claim reimbursement of bill related to post-paid mobile for one SIM card (in the name of employee only) on monthly basis. 
                                The claim should be done within 15 days from the date of the bill. Mobile bill should be uploaded on EMS for claiming bill amount. 
                                </p>
                                <p>
                                Employees having Company provided mobile or employees who are permitted to claim reimbursement (due to demand of the role) will not 
                                be eligible to claim above reimbursement. 
                                </p>
                                <p>
                                     Unclaimed amount under Mobile Reimbursement will be paid quarterly along with salary and taxed.
                                     e.g. if employee claims bill of July and August through EMS but forgets to claim September bill. 
                                     Then unclaimed amount of July, August and September will be paid along with October Salary.
                                </p>
                                <p>
                                    <b>
                                        Employees can restructure amount from zero to Rs. 2000/- per month.
                                    </b>
                                </p>
                            </div>
                        </div>
                        <a href="Documents/FAQ - Mobile Reimbursement.pdf"   style="font-size:10px;" target="_blank">Mobile FAQ</a>
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl" style="vertical-align:middle;">Meal Card</td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblMealExisting" runat="server" CssClass="numerictextboxlabel" Width="92%">0</asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblCanteenRestruct" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>


                    <td class="form-fld-lbl">
                            <div id="12315" class="container">
                                <div id="pop-up9">
                                    <h4>Meal card</h4>
                                    <p>Employees have a choice to opt for electronic meal card for availing tax benefits. </p>
                                    <p>This option will be given once in the beginning of the financial year. Once employee chooses to opt for meal card, it will be applicable for the rest of Financial Year.</p>
                                    <p>Employees opting for electronic meal card can choose the meal card amount to be –</p>
                                    <p>•Rs. 1100/- per month</p>
                                    <p>•Rs. 2200/- per month</p>
                                    <p>Accordingly above amount will be carved out from AA and loaded in the meal card. This amount is exempted from tax as per current IT rules.</p>
                                    <p>The meal card will be reloaded at the beginning of every month.</p>
                                    <p>The electronic meal cards can be used in the company provided canteen facility or select restaurants.</p>
                                    <p>Balance amount in the Meal Card as on 31st March, will be added to the taxable income of the employee.</p>
                                </div>
                            </div>
                            </td>


                </tr>

                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl"><asp:Label ID ="lblMealCardCaption" runat="server" > Meal Card Options (Yes/No) </asp:Label></td>
                    <td class="form-fld-lbl" colspan="4" style="background-color: #dee0ef">
                        <asp:RadioButtonList ID="rdoGetMealVoucher" class="form-fld-lbl" runat="server" OnSelectedIndexChanged="rdoGetMealVoucher_SelectedIndexChanged" AutoPostBack="True" >
                            <asp:listitem Value="2200">Yes I want to avail Meal Card of Rs. 2200</asp:listitem> 
                            <asp:listitem Value="1100">Yes I want to avail Meal Card of Rs. 1100</asp:listitem> 
                            <asp:listitem Value="0" >No I do not want to avail Meal Card</asp:listitem>

                        </asp:RadioButtonList>
                        <asp:Label ID="lblGetMealVoucher" runat="server"></asp:Label>
                    </td>
                    
                   <%-- <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>


                    <td class="form-fld-lbl">&nbsp;</td>--%>


                </tr>
               <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">
                                        <asp:Label ID="lblAASuperAnuationOption0" class="form-fld-lbl" Text ="Additional Allowance(AA)" runat="server"></asp:Label>
                                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblAAExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="txtAA" runat="server" CssClass="numerictextboxlabel"  Width="92%">0</asp:Label>
                     


                    </td>
                    <td class="form-fld-lbl" >
                        <asp:Label ID="lblAAMaxCeiling" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label></td>




                    <td class="form-fld-lbl" >
                        <div id="1239" class="container">
                            <div id="pop-up7">
                                <h4>Additional Allowance (AA) </h4>
                                <p>
                                    This amount can be restructured with Mobile Reimbursement and Meal Card only. AA is completely taxable.
                                    This amount will be automatically calculated based on the restructuring of other components.
                                </p>
                            </div>
                        </div>
                    </td>





                </tr>
                <tr>
                    <td colspan="7">
                        <hr />
                    </td>

                </tr>
               
               
              


           
<%--                <tr>
                    <td colspan="7">
                        <table style="width: 100%">
                            <tr>
                                <td style="vertical-align: top; background-color: #dee0ef; font-family: Verdana; padding: 10px; font-size: 12px; background-repeat: no-repeat; width: 33%">
                                    <hr /> 
                                    <span  class="form-fld-lbl" style="color: maroon;font-family :Verdana;font-size:12px">Notes:</span>
                                    <ol type="1">
                                        <li>HRA, PF, Gratuity and Superannuation (applicable to E6/A6 and above grades) are linked to the Basic Salary.</li>
                                        <li>Additional Allowance is an adjusting factor in the remuneration package and may vary due to any restructuring/revision of allowances/benefits.</li>
                                        <li>Site Allowance will be applicable to technical staff posted at site only. The Site Allowance will cease to be given to an employee if the employee is posted in any design/ project office.</li>                                        
                                        <li>Canteen Subsidy (as mentioned in your CTC, if applicable) is now merged in Additional Allowance. </li>
                                        <li>Performance Bonus (Individual Component of Variable Pay) amount will be inclusive of Bonus, as applicable under Payment of Bonus Act, 1965 and amendments thereof.</li>
                                        <li>Computed CTC does not include Company’s contribution towards premium for Mediclaim/Health Insurance Scheme/ Term Life/Personal Accident coverage.</li>
                                        <li>Tax liability, if any, on all the above payments will be to the employees account</li>
                                    </ol>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>--%>
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
