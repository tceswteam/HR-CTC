
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CalcTaxAsPerOption.aspx.cs" Inherits="SalaryIncomeTaxCalc" %>


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
        .vl 
        {
            border-left: 2px solid maroon;
            height: 00px;
        }
        .modalPopup 
        {
            background-color: #FFFFFF;
            width: 500px;
            /*
            border: 3px solid #0DA9D0;
            border-radius: 12px;
            */
            padding: 0;
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
    </style>
    <script>


        function Compare_Data(Compare1, Compare2, e)
        {
           //  alert('hi');
            try
            {
                //if (e.keyCode == 13) {
                //    PerformCalculation();
                //}              
                var a = document.getElementById(Compare1.id).value;
                var b = document.getElementById(Compare2.id).value;
                //alert(a);
                //alert(b);
                if (e.keyCode == 9)
                {
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
                //PerformCalculation();
            }
            catch (e) {
            }
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

        
        function PerformCalculation() {

            //alert('hi');

            var txtEstimatedAnnual = (document.getElementById('<%=txtEstimatedAnnual.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtEstimatedAnnual.ClientID %>').value;
            var txtstandardded = (document.getElementById('<%=txtstandardded.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtstandardded.ClientID %>').value;
            var txtHRAExisting = (document.getElementById('<%=txtHRAExisting.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtHRAExisting.ClientID %>').value;
            var txtConAlloExisting = (document.getElementById('<%=txtConAlloExisting.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtConAlloExisting.ClientID %>').value;
            var txtLTAExisting = (document.getElementById('<%=txtLTAExisting.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtLTAExisting.ClientID %>').value;
            var txtOthers = (document.getElementById('<%=txtOthers.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtOthers.ClientID %>').value;
            var txtProfessional = (document.getElementById('<%=txtProfessional.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtProfessional.ClientID %>').value;
            var txthousingloan = (document.getElementById('<%=txthousingloan.ClientID %>').value == "") ? "0" : document.getElementById('<%=txthousingloan.ClientID %>').value;
            var txtMediclaim = (document.getElementById('<%=txtMediclaim.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtMediclaim.ClientID %>').value;
            var txtdonation = (document.getElementById('<%=txtdonation.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtdonation.ClientID %>').value;
            var txtDisability = (document.getElementById('<%=txtDisability.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtDisability.ClientID %>').value;
            var txtEduLoan = (document.getElementById('<%=txtEduLoan.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtEduLoan.ClientID %>').value;
            var txtEmployeeContriNPS80CCD = (document.getElementById('<%=txtEmployeeContriNPS80CCD.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtEmployeeContriNPS80CCD.ClientID %>').value;
            var txtEmployerContriNPS80CCD2 = (document.getElementById('<%=txtEmployerContriNPS80CCD2.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtEmployerContriNPS80CCD2.ClientID %>').value;
            var txtRebate80C = (document.getElementById('<%=txtRebate80C.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtRebate80C.ClientID %>').value;

            var lblGroupATotalRestruc = eval(txtstandardded) + eval(txtHRAExisting) + eval(txtConAlloExisting) + eval(txtLTAExisting) + eval(txtOthers) +
                eval(txtProfessional) + eval(txthousingloan) + eval(txtMediclaim) + eval(txtdonation) + eval(txtDisability) + eval(txtEduLoan) +
                eval(txtEmployeeContriNPS80CCD) + eval(txtEmployerContriNPS80CCD2) + eval(txtRebate80C);
            //alert(lblGroupATotalExisting);
            document.getElementById('<%=lblGroupATotalRestruc.ClientID %>').innerHTML = lblGroupATotalRestruc;
            document.getElementById('<%=hdnTotal.ClientID %>').value = lblGroupATotalRestruc;
            

            //if (eval(txtEstimatedAnnual) < eval(lblGroupATotalRestruc))
            //{
            //    alert('Please recheck your deductions as it cannot cross the Estimated annual income');
            //    return;
            //}

            var lblOldRegimeInc = eval(txtEstimatedAnnual) - eval(lblGroupATotalRestruc);//(document.getElementById('<%=lblOldRegimeInc.ClientID %>').value == "") ? "0" : document.getElementById('<%=lblOldRegimeInc.ClientID %>').value;
            var lblNewRegimeInc = eval(txtEstimatedAnnual) - eval(txtEmployerContriNPS80CCD2);//(document.getElementById('<%=txtstandardded.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtstandardded.ClientID %>').value;
            document.getElementById('<%=lblOldRegimeInc.ClientID %>').innerHTML = lblOldRegimeInc;
            document.getElementById('<%=lblNewRegimeInc.ClientID %>').innerHTML = lblNewRegimeInc;
            document.getElementById('<%=hdnOLDRegimeNetIncome.ClientID %>').value = lblOldRegimeInc;
            document.getElementById('<%=hdnNEWRegimeNetIncome.ClientID %>').value = lblNewRegimeInc;
            

        };

        
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
               
            </table>
        <table id="rcorners6">
                <tr>
                    <td colspan="4" style="text-align: center; font-family: Verdana; font-size: 16px;">
                        <b>Tax Calculation for: FY 
                            <asp:Literal ID="ltrFinYr" runat="server"></asp:Literal></b>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl" style="width: 5%; text-align: center;">&nbsp;</td>
                    <td class="form-fld-lbl" style="width: 38%; text-align: center; vertical-align: top">&nbsp;</td>
                    <td class="form-fld-lbl" style="width: 7%; text-align: center; vertical-align: top">
                        As Per Previous Fin Yr
                        <b> 
                            <asp:Literal ID="ltrFinYr0" runat="server"></asp:Literal></b> 
                    </td>
                    <td class="form-fld-lbl" style="width: 10%; text-align: center; vertical-align: top">
                        For Current FinYr</td>
                    
                </tr>
                <tr>
                    <td class="form-fld-lbl" style="width: 5%; text-align: center;"></td>
                    <td class="form-fld-lbl" style="width: 38%; text-align: center; vertical-align: top">Estimated Annual Income</td>
                    <td class="form-fld-lbl" style="background-color: white;" >
                        <asp:Label ID="lblEstimatedAnnual" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="width: 10%; text-align: center; vertical-align: top">
                        <asp:TextBox ID="txtEstimatedAnnual" runat="server" onblur="javascript:IsNumeric(this,event);" CssClass="numerictextboxlabel" Width="92%">0</asp:TextBox>
                        </td>
                    
                </tr>
                <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl" style="font-family: Verdana; font-size: 12px; color: #800000;">&nbsp;</td>
                    <td class="form-fld-lbl" style="font-family: Verdana; font-size: 12px; color: #800000;">Less Exemptions/Deduction</td>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl"></td>
                    
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Standard Deduction</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblstandardded" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label></td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txtstandardded" runat="server" onblur="javascript:IsNumeric(this,event);" CssClass="numerictextboxlabel" Width="92%">0</asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">HRA Rebate</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblHRAExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txtHRAExisting" runat="server" onblur="javascript:IsNumeric(this,event);" CssClass="numerictextboxlabel" Width="92%">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Conveyance Rebate</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblConAlloExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txtConAlloExisting" runat="server" CssClass="numerictextboxlabel" onblur="javascript:IsNumeric(this,event);" Width="92%">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">LTA Rebate</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblLTAExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txtLTAExisting" runat="server" CssClass="numerictextboxlabel" onblur="javascript:IsNumeric(this,event);" Width="92%">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Others</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblOthers" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txtOthers" runat="server" CssClass="numerictextboxlabel" onblur="javascript:IsNumeric(this,event);" Width="92%">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Professional Tax</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblProfessional" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txtProfessional" runat="server" CssClass="numerictextboxlabel" onblur="javascript:IsNumeric(this,event);" Width="92%">0</asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Housing Loan Intereset</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblhousingloan" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txthousingloan" runat="server" CssClass="numerictextboxlabel" onblur="javascript:IsNumeric(this,event);" Width="92%">0</asp:TextBox>
                    </td>
                </tr>
                <tr id="trMobile">
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Mediclaim(80D)</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblMediclaim" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txtMediclaim" runat="server" CssClass="numerictextboxlabel" onblur="javascript:IsNumeric(this,event);" Width="92%" ToolTip="New component introduced with ceiling of Rs. 2000/- per month. Restructuring allowed with AA only. Restructuring not applicable for M Grades.">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">
                        Donation(80G)</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lbldonation" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txtdonation" runat="server" CssClass="numerictextboxlabel" onblur="javascript:IsNumeric(this,event);" Width="92%">0</asp:TextBox>
                    </td>
                </tr>
              <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">
                        Disablity(80DD/80U)</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblDisability" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txtDisability" runat="server" CssClass="numerictextboxlabel" onblur="javascript:IsNumeric(this,event);" Width="92%">0</asp:TextBox>
                    </td>
                </tr>
              <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">
                        Education Loan(80E)</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblEduLoan" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txtEduLoan" runat="server" CssClass="numerictextboxlabel" onblur="javascript:IsNumeric(this,event);" Width="92%">0</asp:TextBox>
                    </td>
                </tr>
              <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">
                        Employere contri. NPS 80CCD (1B)</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblEmployeeContriNPS80CCD" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txtEmployeeContriNPS80CCD" runat="server" CssClass="numerictextboxlabel" onblur="javascript:IsNumeric(this,event);" Width="92%">0</asp:TextBox>
                    </td>
                </tr>
              <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">
                        Employer contri. NPS 80CCD(2)</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblEmployerContriNPS80CCD2" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txtEmployerContriNPS80CCD2" runat="server" CssClass="numerictextboxlabel" onblur="javascript:IsNumeric(this,event);" Width="92%">0</asp:TextBox>
                    </td>
                </tr>
              <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">
                        Rebate U/s 80C(Maximum 150000)</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblRebate80C" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txtRebate80C" runat="server" CssClass="numerictextboxlabel" onblur="javascript:IsNumeric(this,event);" Width="92%">0</asp:TextBox>
                    </td>
                </tr>
              
              
                <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl" style="text-align: right;"><strong>Total Excemption/Deduction</strong></td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblGroupATotalExisting" runat="server" Width="92%" CssClass="numerictextboxlabel"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblGroupATotalRestruc" runat="server" Width="92%" CssClass="numerictextboxlabel"></asp:Label></td>
                   
                </tr>
                <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
            <tr>
                <td colspan="4" style="text-align:center" >
                                                             <asp:ImageButton ID="btnCalcTax" Text="Save File" ImageUrl="~/Layout/Images/CalcTax.png" 
                                            runat="server" Height="25px" Width="94px" ForeColor="white" OnClick="btnCalcTax_Click"  />      
                                          
                        &nbsp;   <asp:ImageButton ID="btnBack" Text="Save File" ImageUrl="~/Layout/Images/Back.png" 
                                            runat="server" Height="25px" Width="94px" ForeColor="white" OnClick="btnBack_Click1" />     
                    
                    
                       
                        </td>
            </tr>
                <tr>  
                     <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl" style="text-align:right"></td>                 
                    <td class="form-fld-lbl" style="font-family: Verdana;text-align:right; font-size: 16px; color: #800000;">Net Taxable Income</td>                   
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl" style="text-align:right">@ OLD Regime</td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblOldRegimeInc" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                   
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl" style="text-align:right">@ New Regime</td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblNewRegimeInc" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                  
                </tr>
            <tr>                   
                    <td colspan="3" class="form-fld-lbl" style="font-family: Verdana;text-align:right; font-size: 12px; color: #800000;">Tax Payable </td>                   
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl" style="text-align:right">@ OLD Regime</td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lnlOLDRegimeTaxCalc" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                   
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl" style="text-align:right">@ New Regime</td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblNewRegimeTaxCalc" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                  
                </tr>
                
               
                <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
            
                <tr>
                    <td colspan="4">
                        <table style="width: 100%">
                            <tr>
                                <td style="vertical-align: top; background-color: #dee0ef; font-family: Verdana; padding: 10px; font-size: 12px; background-repeat: no-repeat; width: 33%">
                                    <hr />
                                    <span class="form-fld-lbl" style="color: maroon; font-family: Verdana; font-size: 12px">Disclaimer:</span>
                                    <ol >
                                        <li>This calculator is only ment to provide a basic idea of the estimated impact of the new provisions.
                                           
                                            Refer to the Income Tax Provisions for the actual provisions and eligibility.</li>
                                       
                                    </ol>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        <asp:HiddenField ID="hdnFinancialYearPrev" runat="server" />
        <asp:HiddenField ID="hdnFinancialYearCurr" runat="server" />
         <asp:HiddenField ID="hdnTotal" runat="server" />
          <asp:HiddenField ID="hdnOLDRegimeNetIncome" runat="server" />
        <asp:HiddenField ID="hdnNEWRegimeNetIncome" runat="server" />
        <asp:HiddenField ID="hdnStdDed" runat="server" />
        <asp:HiddenField ID="hdnProfTax" runat="server" />
        <asp:HiddenField ID="hdnHouInts" runat="server" />
        <asp:HiddenField ID="hdnMedClm" runat="server" />
        <asp:HiddenField ID="hdnNPS80cc" runat="server" />
        <asp:HiddenField ID="hdnc80reb" runat="server" />
        </div>
    </form>
</body>
</html>
