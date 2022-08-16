<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryIncomeTaxCalc.aspx.cs" Inherits="SalaryIncomeTaxCalc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Layout/Forms.css" rel="stylesheet" />
    <%-- <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />--%>
    <title></title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.0/jquery.min.js"></script>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
        .vl {
        border-left: 2px solid maroon;
        height: 00px;
        }
        .modalPopup {
            background-color: #FFFFFF;
            width: 500px;
            /*border: 3px solid #0DA9D0;
                border-radius: 12px;*/
            padding: 0;
        }

            .modalPopup .header {
                background-color: darkgray;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                /*border-top-left-radius: 6px;
                border-top-right-radius: 6px;*/
            }

            .modalPopup .body {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .footer {
                padding: 6px;
            }

            .modalPopup .yes, .modalPopup .no {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }

            .modalPopup .yes {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }

            .modalPopup .no {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }
    </style>
    <style>
        /*input[type=text]::-ms-clear { display: none; }*/
        /*input[type=text]::-ms-clear { width:0;height:0; }*/
        input[type=text]::-ms-clear {
            color: red; /* This sets the cross color as red. */
            /* The cross can be hidden by setting the display attribute as "none" */
        }

        .inline {
            display: inline-block;
            width: 80%;
        }

        #rcorners4 {
            border-radius: 15px 15px 15px 15px;
            padding: 20px;
            width: 100%;
            margin: 0px auto;
            height: 20px;
            text-align: center;
            font-family: sans-serif;
            font-size: 17px;
        }

        #rcorners5 {
            background: #dee0ef;
            padding: 20px;
            width: 70%;
            height: 30%;
            margin: 0px auto;
            font-family: sans-serif;
            font-size: 17px;
            color: GrayText;
        }

        #rcorners6 {
            border-radius: 15px 15px 15px 15px;
            background: #dee0ef;
            border: 2px solid #8B8989;
            padding: 20px;
            width: 70%;
            height: 50%;
            margin: 0px auto;
            font-family: sans-serif;
            font-size: 17px;
            color: black;
        }

        #rcorners7 {
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

        div#container {
            width: 0px;
            padding: 0px;
            border: 0px solid #1a1a1a;
        }

        .container {
            padding: 0px;
            border: 0px solid #1a1a1a;
        }

        div#pop-up {
            -webkit-border-radius: 8px 8px 8px 8px;
            -moz-border-radius: 8px 8px 8px 8px;
            border-radius: 10px 10px 10px 10px;
            border-color: #969aff;
            display: none;
            position: absolute;
            width: 300px;
            padding: 10px;
            background-color: #f0f0ff;
            color: navy;
            border: 1px solid #1a1a1a;
            font-size: 90%;
            overflow: auto;
        }

        div#pop-up1 {
            -webkit-border-radius: 8px 8px 8px 8px;
            -moz-border-radius: 8px 8px 8px 8px;
            border-radius: 10px 10px 10px 10px;
            border-color: #969aff;
            display: none;
            position: absolute;
            width: 300px;
            padding: 10px;
            background-color: #f0f0ff;
            color: navy;
            border: 1px solid #1a1a1a;
            font-size: 90%;
            overflow: auto;
        }

        div#pop-up2 {
            -webkit-border-radius: 8px 8px 8px 8px;
            -moz-border-radius: 8px 8px 8px 8px;
            border-radius: 10px 10px 10px 10px;
            border-color: #969aff;
            display: none;
            position: absolute;
            width: 300px;
            padding: 10px;
            background-color: #f0f0ff;
            color: navy;
            border: 1px solid #1a1a1a;
            font-size: 90%;
            overflow: auto;
        }

        div#pop-up3 {
            -webkit-border-radius: 8px 8px 8px 8px;
            -moz-border-radius: 8px 8px 8px 8px;
            border-radius: 10px 10px 10px 10px;
            border-color: #969aff;
            display: none;
            position: absolute;
            width: 300px;
            padding: 10px;
            background-color: #f0f0ff;
            color: navy;
            border: 1px solid #1a1a1a;
            font-size: 90%;
            overflow: auto;
        }

        div#pop-up4 {
            -webkit-border-radius: 8px 8px 8px 8px;
            -moz-border-radius: 8px 8px 8px 8px;
            border-radius: 10px 10px 10px 10px;
            border-color: #969aff;
            display: none;
            position: absolute;
            width: 300px;
            padding: 10px;
            background-color: #f0f0ff;
            color: navy;
            border: 1px solid #1a1a1a;
            font-size: 90%;
            overflow: auto;
        }

        div#pop-up5 {
            -webkit-border-radius: 8px 8px 8px 8px;
            -moz-border-radius: 8px 8px 8px 8px;
            border-radius: 10px 10px 10px 10px;
            border-color: #969aff;
            display: none;
            position: absolute;
            width: 300px;
            padding: 10px;
            background-color: #f0f0ff;
            color: navy;
            border: 1px solid #1a1a1a;
            font-size: 90%;
            overflow: auto;
        }

        div#pop-up6 {
            -webkit-border-radius: 8px 8px 8px 8px;
            -moz-border-radius: 8px 8px 8px 8px;
            border-radius: 10px 10px 10px 10px;
            border-color: #969aff;
            display: none;
            position: absolute;
            width: 300px;
            padding: 10px;
            background-color: #f0f0ff;
            color: navy;
            border: 1px solid #1a1a1a;
            font-size: 90%;
            overflow: auto;
        }

        div#pop-up7 {
            -webkit-border-radius: 8px 8px 8px 8px;
            -moz-border-radius: 8px 8px 8px 8px;
            border-radius: 10px 10px 10px 10px;
            border-color: #969aff;
            display: none;
            position: absolute;
            width: 300px;
            padding: 10px;
            background-color: #f0f0ff;
            color: navy;
            border: 1px solid #1a1a1a;
            font-size: 90%;
            overflow: auto;
        }

        div#pop-up8 {
            -webkit-border-radius: 8px 8px 8px 8px;
            -moz-border-radius: 8px 8px 8px 8px;
            text-align: left;
            border-radius: 10px 10px 10px 10px;
            border-color: #969aff;
            display: none;
            position: absolute;
            width: 300px;
            padding: 10px;
            background-color: #f0f0ff;
            color: navy;
            border: 1px solid #1a1a1a;
            font-size: 11px;
            overflow: auto;
        }

        div#pop-up9 {
            -webkit-border-radius: 8px 8px 8px 8px;
            -moz-border-radius: 8px 8px 8px 8px;
            border-radius: 10px 10px 10px 10px;
            border-color: #969aff;
            display: none;
            position: absolute;
            width: 300px;
            padding: 10px;
            background-color: #f0f0ff;
            color: navy;
            border: 1px solid #1a1a1a;
            font-size: 90%;
            overflow: auto;
        }

        div#pop-up10 {
            -webkit-border-radius: 8px 8px 8px 8px;
            -moz-border-radius: 8px 8px 8px 8px;
            border-radius: 10px 10px 10px 10px;
            border-color: #969aff;
            display: none;
            position: absolute;
            width: 500px;
            padding: 10px;
            background-color: #f0f0ff;
            color: navy;
            border: 1px solid #1a1a1a;
            font-size: 90%;
            overflow: auto;
        }

        div#pop-up11 {
            -webkit-border-radius: 8px 8px 8px 8px;
            -moz-border-radius: 8px 8px 8px 8px;
            border-radius: 10px 10px 10px 10px;
            border-color: #969aff;
            display: none;
            position: absolute;
            width: 300px;
            padding: 10px;
            background-color: #f0f0ff;
            color: navy;
            border: 1px solid #1a1a1a;
            font-size: 90%;
            overflow: auto;
        }

        div#pop-up12 {
            -webkit-border-radius: 8px 8px 8px 8px;
            -moz-border-radius: 8px 8px 8px 8px;
            border-radius: 10px 10px 10px 10px;
            border-color: #969aff;
            display: none;
            position: absolute;
            width: 300px;
            padding: 10px;
            background-color: #f0f0ff;
            color: navy;
            border: 1px solid #1a1a1a;
            font-size: 90%;
            overflow: auto;
        }

        div#pop-up13 {
            -webkit-border-radius: 8px 8px 8px 8px;
            -moz-border-radius: 8px 8px 8px 8px;
            border-radius: 10px 10px 10px 10px;
            border-color: #969aff;
            display: none;
            position: absolute;
            width: 300px;
            padding: 10px;
            background-color: #f0f0ff;
            color: navy;
            border: 1px solid #1a1a1a;
            font-size: 90%;
            overflow: auto;
        }


        .hidden {
            display: none;
        }

        .lblForWhite {
            background-color: white;
            width: 100px;
        }
        .auto-style1 {
            height: 18px;
        }
    </style>
    <script type="text/javascript">
        function Confirm_Print() {
            // alert('hi');
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value_print";
            
            if (confirm("Are you sure to go with the selected Regime option?"))
            {                   
                if (confirm("Once submitted no modification is permitted. Are you sure about this action?"))
                {
                            confirm_value.value = "Yes";
                }
                else {
                            confirm_value.value = "No";
                        }
                   
                }
                else {
                    confirm_value.value = "No";
                }
            
            document.forms[0].appendChild(confirm_value);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="background-color: white;">
            <table style="background-color: white; width: 70%; margin: auto; border: 2px solid #8B8989; border-radius: 15px 15px 15px 15px;">
                <tr>
                    <td>
                        <asp:Image ID="Image1" runat="server" Width="100%" ImageUrl="~/Layout/Images/SalaryIncTxCalcOp.png" />
                    </td>
                </tr>
                <tr>
                    <td>
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
               <tr><td><hr /></td></tr>
                <tr><td> <span class="form-fld-lbl" style="font-family: Verdana; font-size: 12px; color: #800000;">Instructions:</span>
                        <ol type="1" style="font-family: Verdana; font-size: 12px;">
                            <li>Tax exemptions specified herein are based on current IT rules and are liable to change as per prevailing IT rules.   </li>
                           
                            <li>Employees are required to understand prevailing income tax rules before selecting ‘Option I’ or ‘Option II’ suiting to their
                                <br />
                                individual needs. Once the option is submitted, it will continue for the rest of the Financial Year.</li>
                         </ol></td></tr>
                <tr><td><hr /></td></tr>
               <tr>
                   <td colspan="2">
                       <table style="width: 100%" class="form-fld-lbl">
                           <tr>
                               <td style ="vertical-align:top">
                                   <table style="width: 100%;" class="form-fld-lbl">
                                       <tr>
                                           <td colspan="4"  style="text-align:center"><strong>Option - I - Old Regime</strong></td>
                                       </tr>
                                       <tr>
                                           <td colspan="4" style="text-align:left">Tax Slab</td>
                                       </tr>
                                       <tr>
                                           <td></td><td>Tax</td><td>Surcharge</td><td>Cess on tax</td>
                                       </tr>
                                       <tr>
                                           <td>Up to 2,50,000</td><td>Nil</td><td>Nil</td><td>4%</td>
                                       </tr>
                                        <tr>
                                           <td>2,51,000 to 5,00,000</td><td>5%</td><td>Nil</td><td>4%</td>
                                       </tr>
                                        <tr>
                                           <td>5,00,001 to 10,00,000</td><td>20%</td><td>Nil</td><td>4%</td>
                                       </tr>
                                        <tr>
                                           <td>Above 10,00,000</td><td>30%</td><td>Nil</td><td>4%</td>
                                       </tr>
                                       <tr>
                                           <td  colspan="4" ></td>
                                       </tr>
                                       <tr>
                                           <td  colspan="4" ></td>
                                       </tr>
                                       <tr>
                                           <td  colspan="4" ></td>
                                       </tr>
                                        <tr>
                                           <td colspan="3">Surcharge above 50,00,000 is</td><td>10%</td>
                                       </tr>
                                        <tr>
                                           <td colspan="3">Above 1,00,00,000</td><td>15%</td>
                                       </tr>
                                        <tr>
                                           <td colspan="3">Above 2,00,00,000</td><td>25%</td>
                                       </tr>
                                       <tr>
                                           <td colspan="4"></td>
                                       </tr>
                                       <tr>
                                           <td colspan="4" class="auto-style1"></td>
                                       </tr>
                                       <tr>
                                           <td colspan="4"></td>
                                       </tr>
                                        <tr>
                                           <td colspan="4">87A Tax rebate 12500 given up to 5,00,000 Lakh</td>
                                       </tr>
                                        <tr>
                                           <td colspan="4">All existing Exemptions/Deductions are applicable for computation</td>
                                       </tr>
                                       
                                   </table>
                               </td>
                               <td>
                       <td class="vl" ></td>                  <td style ="vertical-align:top">
                                   <table style="width: 100%;" class="form-fld-lbl" >
                                       <tr>
                                           <td colspan="4"  style="text-align:center"><strong>Option - II - New Regime</strong></td>
                                       </tr>
                                       <tr>
                                           <td colspan="4" style="text-align:left">Tax Slab</td>
                                       </tr>
                                       <tr>
                                           <td></td><td>Tax</td><td>Surcharge</td><td>Cess on tax</td>
                                       </tr>
                                       <tr>
                                           <td>Up to 2,50,000</td><td>Nil</td><td>Nil</td><td>4%</td>
                                       </tr>
                                        <tr>
                                           <td>2,51,000 to 5,00,000</td><td>5%</td><td>Nil</td><td>4%</td>
                                       </tr>
                                        <tr>
                                           <td>5,00,001 to 7,50,000</td><td>10%</td><td>Nil</td><td>4%</td>
                                       </tr>
                                        <tr>
                                           <td>7,50,001 to 10,00,000</td><td>15%</td><td>Nil</td><td>4%</td>
                                       </tr>
                                        <tr>
                                           <td>10,00,001 to 12,50,000</td><td>20%</td><td>Nil</td><td>4%</td>
                                       </tr>
                                        <tr>
                                           <td>12,50,001 to 15,00,000</td><td>25%</td><td>Nil</td><td>4%</td>
                                       </tr>
                                        <tr>
                                           <td>Above 15,00,001</td><td>30%</td><td>Nil</td><td>4%</td>
                                       </tr>
                                       <tr>
                                           <td  colspan="4" ></td>
                                       </tr>
                                       
                                        <tr>
                                           <td colspan="3">Surcharge above 50,00,000 is</td><td>10%</td>
                                       </tr>
                                        <tr>
                                           <td colspan="3">Above 1,00,00,000</td><td>15%</td>
                                       </tr>
                                        <tr>
                                           <td colspan="3">Above 2,00,00,000</td><td>25%</td>
                                       </tr>
                                       <tr>
                                           <td  colspan="4" ></td>
                                       </tr>
                                      
                                        <tr>
                                           <td colspan="4">87A Tax rebate 12500 given up to 5,00,000 Lakh</td>
                                       </tr>
                                        <tr>
                                           <td colspan="4">Only Employer contribution to NPS considered as exemption / deduction for computation...</td>
                                       </tr>
                                       
                                   </table>
                               </td>
                           </tr>
                       </table>
                   </td>
               </tr>
                <tr>
                    <td colspan="2">
                         <hr />
                        <table class="form-fld-lbl" style="width:100%">
                            <tr>
                                <td style="text-align:center">
                                    <center>
                                          <span class="form-fld-lbl" >For better understanding of Income Tax calculations as per old and new regime please click on ‘Calculate Tax’  &nbsp; &nbsp; &nbsp;
                                              <span style="background-color:yellow;" class="form-fld-lbl">
                                              <a href="Documents/Tax%20Notification%20for%202020-2021.pdf">Click here for Tax Notification</a></span> </span>
                                      <br />
                                         <asp:ImageButton ID="btnCalcTax" Text="Save File" ImageUrl="~/Layout/Images/CalcTax.png" 
                                            runat="server" Height="25px" Width="94px" ForeColor="white" OnClick="btnCalcTax_Click1"  />      
                                          <a href="Logout.aspx" onclick="preventBack();"  style="color: White;">   <img alt="" src="Layout/Images/Logout.png" height="25px" width="94px" /></a> </center>
                                    </td>
                              </tr>
                            <tr id="trRegimeOptionNotSelect" runat="server" visible="false">
                               <td  style="text-align:center">
                                   <center>
                               <asp:RadioButtonList ID="rdbRegime" RepeatDirection="Horizontal" runat="server">
                                        <asp:ListItem Text="Option I Old Regime" Value="OLDRegime"> </asp:ListItem>
                                        <asp:ListItem Text="Option II New Regime" Value="NewRegime"> </asp:ListItem>
                                    </asp:RadioButtonList>
                                           <asp:ImageButton ID="btnSubmitRegime" Text="Save File" ImageUrl="~/Layout/Images/Submit.png" 
                                            OnClientClick="Confirm_Print();" runat="server" Height="25px" Width="94px" ForeColor="white" OnClick="btnSubmitRegime_Click" />        
                                            </center>              
                               </td>
                             </tr>
                            <tr  >
                                <td>
                                        <br />
                                      
                                </td>
                            </tr>
                            <tr id="trRegimeOptionSelected" runat="server" visible="false">
                                <td  style="text-align:center;font-family:Verdana;font-size :16px;" class="Required">
                                    
                                    You have updated the Regime as  : <asp:Label ID="lblEmpRegimeOption" runat="server" Text="What you selected was " Font-Names="Verdana" Font-Size="16px"></asp:Label>
                                    <br />
                                    <asp:Button ID="btnProceed" Text="Proceed to Restructure" runat="server"  ForeColor="White" BackColor="Maroon" Height="30px" OnClick="btnProceed_Click" />
                                       
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
<asp:HiddenField ID="hdnFinancialYearPrev" runat="server" />
<asp:HiddenField ID="hdnFinancialYearCurr" runat="server" />
        </div>
    </form>
</body>
</html>
