<%@ Page Debug="true" Language="C#" AutoEventWireup="true" CodeFile="frmEmpCTCRestruct_Version1.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
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
    </style>
    <script type="text/javascript">

        function ShowModalPopup() {
            $find("mpe").show();
            return false;
        }
        function HideModalPopup() {
            $find("mpe").hide();
            return false;
        }
    </script>
    <script type="text/javascript">
        $(function () {
            var moveLeft = 20;
            var moveDown = 10;
            /////////////////////////////////////////
            $('#trigger').hover(function (e) {
                $('div#pop-up').show();
                //.css('top', e.pageY + moveDown)
                //.css('left', e.pageX + moveLeft)
                //.appendTo('body');
            }, function () {
                $('div#pop-up').hide();
            });
            $('a#trigger').mousemove(function (e) {
                $("div#pop-up").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft);
            });

            /////////////////////////////////////////
            $('#trigger1').hover(function (e) {
                $('div#pop-up1').show();
                //.css('top', e.pageY + moveDown)
                //.css('left', e.pageX + moveLeft)
                //.appendTo('body');
            }, function () {
                $('div#pop-up1').hide();
            });

            $('a#trigger1').mousemove(function (e) {
                $("div#pop-up1").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft);
            });
            /////////////////////////////////////////
            $('#trigger2').hover(function (e) {
                $('div#pop-up2').show();
                //.css('top', e.pageY + moveDown)
                //.css('left', e.pageX + moveLeft)
                //.appendTo('body');
            }, function () {
                $('div#pop-up2').hide();
            });

            $('a#trigger2').mousemove(function (e) {
                $("div#pop-up2").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft);
            });
            /////////////////////////////////////////
            $('#trigger3').hover(function (e) {
                $('div#pop-up3').show();
                //.css('top', e.pageY + moveDown)
                //.css('left', e.pageX + moveLeft)
                //.appendTo('body');
            }, function () {
                $('div#pop-up3').hide();
            });
            $('a#trigger3').mousemove(function (e) {
                $("div#pop-up3").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft);
            });
            /////////////////////////////////////////
            $('#trigger4').hover(function (e) {
                $('div#pop-up4').show();
                //.css('top', e.pageY + moveDown)
                //.css('left', e.pageX + moveLeft)
                //.appendTo('body');
            }, function () {
                $('div#pop-up4').hide();
            });
            $('a#trigger4').mousemove(function (e) {
                $("div#pop-up4").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft);
            });
            /////////////////////////////////////////
            $('#trigger5').hover(function (e) {
                $('div#pop-up5').show();
                //.css('top', e.pageY + moveDown)
                //.css('left', e.pageX + moveLeft)
                //.appendTo('body');
            }, function () {
                $('div#pop-up5').hide();
            });
            $('a#trigger5').mousemove(function (e) {
                $("div#pop-up5").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft);
            });
            /////////////////////////////////////////
            $('#trigger6').hover(function (e) {
                $('div#pop-up6').show();
                //.css('top', e.pageY + moveDown)
                //.css('left', e.pageX + moveLeft)
                //.appendTo('body');
            }, function () {
                $('div#pop-up6').hide();
            });
            $('a#trigger6').mousemove(function (e) {
                $("div#pop-up6").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft);
            });
            /////////////////////////////////////////
            $('#trigger7').hover(function (e) {
                $('div#pop-up7').show();
                //.css('top', e.pageY + moveDown)
                //.css('left', e.pageX + moveLeft)
                //.appendTo('body');
            }, function () {
                $('div#pop-up7').hide();
            });
            $('a#trigger7').mousemove(function (e) {
                $("div#pop-up7").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft);
            });
            /////////////////////////////////////////
            $('#trigger8').hover(function (e) {
                $('div#pop-up8').show();
                //.css('top', e.pageY + moveDown)
                //.css('left', e.pageX + moveLeft)
                //.appendTo('body');
            }, function () {
                $('div#pop-up8').hide();
            });
            $('a#trigger8').mousemove(function (e) {
                $("div#pop-up8").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft);
            });
            /////////////////////////////////////////
            $('#trigger10').hover(function (e) {
                $('div#pop-up10').show();
                //.css('top', e.pageY + moveDown)
                //.css('left', e.pageX + moveLeft)
                //.appendTo('body');
            }, function () {
                $('div#pop-up10').hide();
            });
            $('a#trigger10').mousemove(function (e) {
                $("div#pop-up10").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft);
            });
            /////////////////////////////////////////
            $('#trigger11').hover(function (e) {
                $('div#pop-up11').show();
                //.css('top', e.pageY + moveDown)
                //.css('left', e.pageX + moveLeft)
                //.appendTo('body');
            }, function () {
                $('div#pop-up11').hide();
            });
            $('a#trigger11').mousemove(function (e) {
                $("div#pop-up11").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft);
            });
            /////////////////////////////////////////
            $('#trigger12').hover(function (e) {
                $('div#pop-up12').show();
                //.css('top', e.pageY + moveDown)
                //.css('left', e.pageX + moveLeft)
                //.appendTo('body');
            }, function () {
                $('div#pop-up12').hide();
            });
            $('a#trigger12').mousemove(function (e) {
                $("div#pop-up12").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft);
            });
            /////////////////////////////////////////
            $('#trigger13').hover(function (e) {
                $('div#pop-up13').show();
                //.css('top', e.pageY + moveDown)
                //.css('left', e.pageX + moveLeft)
                //.appendTo('body');
            }, function () {
                $('div#pop-up13').hide();
            });
            $('a#trigger13').mousemove(function (e) {
                $("div#pop-up13").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft);
            });
            /////////////////////////////////////////
            $('#trigger9').hover(function (e) {
                $('div#pop-up9').show();
                //.css('top', e.pageY + moveDown)
                //.css('left', e.pageX + moveLeft)
                //.appendTo('body');
            }, function () {
                $('div#pop-up9').hide();
            });
            $('a#trigger9').mousemove(function (e) {
                $("div#pop-up9").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft);
            });
            /////////////////////////////////////////
        });
    </script>
    <script type="text/javascript">


        var style = document.createElement('style');
        style.type = 'text/css';
        style.innerHTML = '::-ms-clear{display:none};';
        document.getElementsByTagName('head')[0].appendChild(style);

        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 10);
        window.onunload = function () { null };


        function CloseWindow() {
            window.close();
        }

        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }

        document.onkeypress = stopRKey;
    </script>
    <script>
        function Confirm_Pullback() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value_pullback";
            if (confirm("Do you want to revoke the restructured salary?")) {
                // alert(1);
                confirm_value.value = "Yes";
            } else {
                //  alert(2);
                confirm_value.value = "No";

            }

            document.forms[0].appendChild(confirm_value);

        }
      //  document.getElementById('<%=btnPrintFinal.ClientID %>').style.display = "none";
        function Confirm_Print() {
            // alert('hi');
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value_print";
            if (document.getElementById('<%=hdnIsEmpFinalizedCTCRestruc.ClientID %>').value == 'false') {
                if (confirm("Have you finished restructuring CTC?")) {
                    if (confirm("Do you want submit restructured CTC?")) {
                        if (confirm("Once submitted no modification is permitted. Are you sure about this action?")) {
                            confirm_value.value = "Yes";
                        }
                        else {
                            confirm_value.value = "No";
                        }
                    }
                    else {
                        confirm_value.value = "No";
                    }
                }
                else {
                    confirm_value.value = "No";
                }
            }
            else {
                confirm_value.value = "Yes";
            }
            document.forms[0].appendChild(confirm_value);
        }

    </script>

    <script type="text/javascript">
        function HideSuperAnnuation() {
            document.getElementById('trSuperanuationMerged').style.display = 'none';
            //document.getElementById('trNPS').style.display = 'none';
            document.getElementById('trSuperAnnuation').style.display = 'none';
        }

        function RadioButtonList() {
            var RB1 = document.getElementById("<%=rdoGetMealVoucher.ClientID%>");
            var radio = RB1.getElementsByTagName("input");
            var label = RB1.getElementsByTagName("label");
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    if (radio[i].value = 'Yes')
                        alert('Please attach provisional certificate...!');
                    else
                        alert('Please attach Agreement Copy and possission certificate and bank provisional statement !');
                }
            }
            return false;
        }

        function CheckForValueRangeHRA(minRange, maxRange, currentvalue, valueoffield) {
            var minval = minRange;
            var maxval = maxRange;
            var currentval = eval(document.getElementById(currentvalue.id).value);
            //alert(eval(currentval) + ' ' + eval(minRange) + ' ' + eval(maxRange));
            //if (eval(currentvalue) < eval(minRange) || eval(currentvalue) > eval(maxRange))
            if (currentval == "")
                currentval = "0";
            if (currentval < minRange || currentvalue > maxRange) {

                document.getElementById(currentvalue.id).value = "0";
                alert("Value of " + valueoffield + " should be in range of " + minval + "-" + maxval);
            }
        }

        //Added By Mohan On 27 May 2019
        function CheckForValueRangeNPS(minRange, maxRange, currentvalue) {
            var minval = minRange;
            var maxval = maxRange;
            var ExistingNPS;

            var currentval = eval(document.getElementById(currentvalue.id).value);

            if (currentval == "")
                currentval = "0";
            if (currentval < minRange) {
                document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML = "0";
                document.getElementById(currentvalue.id).value = "0";
                alert("Value of NPS should be in range of " + minval + "-" + maxval);
                return;
            }
            else if (currentval > maxRange) {
                document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML = "0";
                document.getElementById(currentvalue.id).value = "0";
                alert("Value of NPS should be in range of " + minval + "-" + maxval);
                return;
            }

            ExistingNPS = eval(document.getElementById('<%=lblNPSExistingAmt.ClientID%>').innerHTML);

            if (ExistingNPS != 0) {

                if (ExistingNPS > currentval) {
                    document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML = eval(ExistingNPS - currentval);
                }
                else {
                    document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML = "-" + eval(currentval - ExistingNPS);
                }
            }
            else {
                if (currentval == "0") {
                    document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML = document.getElementById(currentvalue.id).value;
                }
                else {
                    document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML = "-" + document.getElementById(currentvalue.id).value;
                }
            }

            PerformCalculation();
        }

        //End By Mohan

        function CheckForValueRangeCA(minRange, currentvalue, valueoffield) {
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

        function OnNPSValueEnter() {
            var BalanceForTransfer;
            var NPSval;
            var AANew;
            BalanceForTransfer = document.getElementById('<%=hdnBalnceSA.ClientID%>').value;
            //Commented By Mohan On 27 May 2019
            <%-- AANew = eval(BalanceForTransfer) - eval(document.getElementById('<%=txtNPSSA.ClientID%>').value);
            document.getElementById('<%=txtAASA.ClientID%>').innerHTML = eval(BalanceForTransfer) - eval(document.getElementById('<%=txtNPSSA.ClientID%>').value);--%>
            //End By Mohan
            //Added By Mohan On 27 May 2019
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    document.getElementById('<%=txtAA.ClientID %>').innerHTML = (eval(lblMobileExisting) + eval(lblAAExisting)) - (eval(mobile));
                    var newval = eval(lblAAExisting) + eval(lblMobileExisting) - eval(mobile) + eval(lblAAWithCanteenSubsRestruc) + eval(lblAAWithSuperannuation) - eval(radio[i].value);
                    document.getElementById('<%=lblAACalculation.ClientID %>').innerHTML =
                        eval(document.getElementById('<%=lblAAExisting.ClientID %>').innerHTML) +
                        eval(lblMobileExisting) + eval(lblAAWithCanteenSubsRestruc) +
                        eval(lblAAWithSuperannuation) - eval(radio[i].value);
                }
            }
            PerformCalculation();
        }

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
                //$("#btnFireRadioEvent").click();
            }
            catch (e) {
            }
        }

        function addCommas(nStr) {
            nStr += '';
            x = nStr.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1)) {
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            }
            return x1 + x2;
        }


        function ApplySAChanges() {
            var lblBasicExisting = (document.getElementById('<%=lblBasicExisting.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblBasicExisting.ClientID %>').innerHTML;
            var basic = eval(lblBasicExisting);
            var basic_5perc;
            var basic_10perc;
            var basic_15perc;
            var BalanceForTransfer;
            //var NPSval;
            var AANew

            basic_5perc = Math.ceil((eval(basic) * 5) / 100);
            basic_10perc = Math.ceil((eval(basic) * 10) / 100);
            basic_15perc = Math.ceil((eval(basic) * 15) / 100);
            BalanceForTransfer = document.getElementById('<%=hdnBalnceSA.ClientID%>').value;
            <%--NPSval = document.getElementById('<%=txtNPSSA.ClientID%>').value;--%>
            AANew = document.getElementById('<%=txtAASA.ClientID%>').value;

            //eval(NPSval) +
            if (eval(AANew) == eval(BalanceForTransfer)) {
                    //Commented by Mohan On 27 May 2019
                    <%-- document.getElementById('<%=lblNPS.ClientID%>').innerHTML = document.getElementById('<%=txtNPSSA.ClientID%>').value;--%>
                //End By Mohan
                document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML = document.getElementById('<%=txtAASA.ClientID%>').value;

                //$find("mpe").hide();

            }
            else { //NPS and
                alert("Sum of AA transfer should be less than balance transfer. That is " + BalanceForTransfer);

            }
            // PerformCalculation();
        }

        function OnSuperAnnuationChange() {
            var parm;
            var listBoxSelectonValue;

            if (document.getElementById("<%=hdnEmpSelectedSuperanuationOption.ClientID %>").value == "-1") {
                parm = document.getElementById("<%=hdnEmpSelectedSuperanuationOption.ClientID %>").value;
                listBoxSelectonValue = parm;
            }
            else {
                if (document.getElementById('<%=hdnFormMode.ClientID%>').value == "0")//Form Mode 0 means new/Edit mode
                {
                    parm = document.getElementById("<%=ddlSuperanuation.ClientID %>");
                    listBoxSelectonValue = parm.options[parm.selectedIndex].value;
                }
                else {
                    parm = document.getElementById("<%=hdnEmpSelectedSuperanuationOption.ClientID %>").value;
                    listBoxSelectonValue = parm;
                }
            }

            var confirm_value;
            if (listBoxSelectonValue == "1") {
                msgForDisplay = "No change in existing supernnuation option.";
            }
            if (listBoxSelectonValue == "3") {
                msgForDisplay = "Once the % of Basic to be contributed to Superannuation is reduced, it will continue to be the same throughout tenure in TCE. This will reduce your future contribution towards Superannuation corpus. Since you have chosen to reduce your contribution towards Superannuation, please divert the balance amount in AA";
            }
            if (listBoxSelectonValue == "5") {
                msgForDisplay = "Once the % of Basic to be contributed to Superannuation is reduced, it will continue to be the same throughout tenure in TCE. This will reduce your future contribution towards Superannuation corpus. Since you have chosen to reduce your contribution towards Superannuation, please divert the balance amount in AA";
            }
            // alert(msgForDisplay);
            var IsEmpRestructuredCTC = document.getElementById("<%=hdnIsEmpRestructuredCTC.ClientID%>").value;
            var isCTCFinalized = document.getElementById("<%=hdnIsEmpFinalizedCTCRestruc.ClientID%>").value;
            var FormMode = document.getElementById('<%=hdnFormMode.ClientID%>').value;
            var msgForDisplay;
            //alert(isCTCFinalized);
            if (isCTCFinalized == 'true') {
                confirm_value = "Yes";
            }
            else {
                if (FormMode == "0") {
                    if (confirm(msgForDisplay)) {
                        confirm_value = "Yes";
                    }
                    else {
                        confirm_value = "No"
                    }
                }
                else {
                    confirm_value = "Yes";
                }
            }
            //alert(confirm_value);
            if (confirm_value == 'Yes') {
                //alert(listBoxSelectonValue);
                OnSelectionOfNoChangeInSuperanuation123(listBoxSelectonValue);
            }
            else {
                document.getElementById("<%=ddlSuperanuation.ClientID%>").selectedIndex = 0;
            }
        }

        function OnSelectionOfNoChangeInSuperanuation123(id) {
            // alert(id);
            var SuperannuationLimit = document.getElementById('<%=hdnSuperAnnuationLimit.ClientID%>').value;
            var SuperAnnuationUpperLimit = document.getElementById('<%=hdnSuperAnnuationUpperLimit.ClientID%>').value;
                // alert(document.getElementById("<%=hdnEmpSelectedSuperanuationOption.ClientID %>").value );
            var parm;
            var listBoxSelectonValue;

            if (document.getElementById("<%=hdnEmpSelectedSuperanuationOption.ClientID %>").value == "-1") {
                parm = document.getElementById("<%=hdnEmpSelectedSuperanuationOption.ClientID %>").value;
                listBoxSelectonValue = parm;
            }
            else {
                if (document.getElementById('<%=hdnFormMode.ClientID%>').value == "0")//Form Mode 0 means new/Edit mode
                {
                    parm = document.getElementById("<%=ddlSuperanuation.ClientID %>");
                    listBoxSelectonValue = parm.options[parm.selectedIndex].value;
                }
                else {
                    parm = document.getElementById("<%=hdnEmpSelectedSuperanuationOption.ClientID %>").value;
                    listBoxSelectonValue = parm;
                }
            }
            //alert(listBoxSelectonValue);

            var lblBasicExisting = (document.getElementById('<%=lblBasicExisting.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblBasicExisting.ClientID %>').innerHTML;
            var basic = eval(lblBasicExisting);
            var basic_5perc;
            var basic_10perc;
            var basic_15perc;

            basic_5perc = Math.ceil((eval(basic) * 5) / 100);
            basic_10perc = Math.ceil((eval(basic) * 10) / 100);
            basic_15perc = Math.ceil((eval(basic) * 15) / 100);
            document.getElementById('<%=lblSAOLD.ClientID %>').innerHTML = document.getElementById('<%=lblSNExisting.ClientID %>').innerHTML;

            if (listBoxSelectonValue == "-1") {
                //document.getElementById('trNPS').style.display = 'none';
                document.getElementById('trSuperanuationMerged').style.display = 'none';
                document.getElementById('trSuperAnnuation').style.display = 'none';
            }
            else if (listBoxSelectonValue == "0") {
                document.getElementById('trSuperAnnuation').style.display = 'none';
            }
            else if (listBoxSelectonValue == "1") {
                    // document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML = "0";
                document.getElementById('<%=lblSNRestruct.ClientID %>').innerHTML = basic_15perc;
                document.getElementById('<%=lblSARevised.ClientID %>').innerHTML = basic_15perc;
                document.getElementById('<%=hdnSN.ClientID %>').value = basic_15perc;
                    <%--document.getElementById('<%=lblNPS.ClientID %>').innerHTML = "0";--%>
                document.getElementById('<%=hdnSNNPS.ClientID%>').value = "0";
                document.getElementById('<%=hdnSNWithAAOrTrfAA.ClientID%>').value = "0";

                //document.getElementById('trNPS').style.display = 'none';
                document.getElementById('trSuperanuationMerged').style.display = 'none';
                document.getElementById('trSuperAnnuation').style.display = 'block';
                document.getElementById('trSuperAnnuation').style.width = '100%';

                    <%--document.getElementById('<%=txtNPSSA.ClientID%>').value = "0";--%>
                    <%--document.getElementById('<%=lblNPS.ClientID%>').innerHTML = "0";--%>
                document.getElementById('<%=txtAASA.ClientID%>').innerHTML = eval(document.getElementById('<%=lblSAOLD.ClientID %>').innerHTML) - eval(document.getElementById('<%=lblSARevised.ClientID %>').innerHTML);
                    // alert(document.getElementById('<%=txtAASA.ClientID%>').innerHTML);
                document.getElementById('<%=hdnBalnceSA.ClientID%>').value = eval(document.getElementById('<%=lblSAOLD.ClientID %>').innerHTML) - eval(document.getElementById('<%=lblSARevised.ClientID %>').innerHTML);

                    // document.getElementById('<%=txtAASA.ClientID%>').value = eval(document.getElementById('<%=lblSNRestruct.ClientID %>').innerHTML) - eval(document.getElementById('<%=lblSAOLD.ClientID %>').innerHTML);
                    // document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML = "0";
                    // To be added for the recalculating Revised AA

                //below commented by chetna on 11 aug 2022
              <%--   document.getElementById('<%=lblAACalculation.ClientID %>').innerHTML =
                    eval(document.getElementById('<%=txtAA.ClientID %>').innerHTML) +
                    eval(document.getElementById('<%=lblMobileExisting.ClientID%>').innerHTML) +
                    eval(document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML) +
                    eval(document.getElementById('<%=lblMeamCardDeducFrmAA.ClientID%>').innerHTML) +
                    eval(document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML); //Added by Mohan On 30 May 2019
                    $find("mpe").show();--%>
                //ended commented bu chetna on 11 aug 2022

                var mobileVal = eval(document.getElementById('<%=lblMobileExisting.ClientID%>').innerHTML) - eval(document.getElementById('<%=txtMobile.ClientID%>').value);
               <%-- alert('22');
                alert(document.getElementById('<%=lblAACalculation.ClientID %>').innerHTML);
                alert(eval(document.getElementById('<%=txtAA.ClientID %>').innerHTML));
                alert(mobileVal);
                alert(eval(document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML));
                alert(eval(document.getElementById('<%=lblMeamCardDeducFrmAA.ClientID%>').innerHTML));
                alert(eval(document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML));--%>
                document.getElementById('<%=lblAACalculation.ClientID %>').innerHTML =
                    eval(document.getElementById('<%=txtAA.ClientID %>').innerHTML) +
                    mobileVal +
                    eval(document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML) +
                eval(document.getElementById('<%=lblMeamCardDeducFrmAA.ClientID%>').innerHTML) +
                eval(document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML); //Added by Mohan On 30 May 2019
                $find("mpe").show();
            }
            else if (listBoxSelectonValue == "3") {
                document.getElementById('<%=txtAASA.ClientID%>').value = "0";
                document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML = "0";
                //alert(basic_5perc);
                document.getElementById('<%=lblSARevised.ClientID %>').innerHTML = basic_5perc;
                document.getElementById('<%=hdnSN.ClientID %>').value = basic_5perc;
                document.getElementById('<%=txtAASA.ClientID%>').innerHTML = eval(document.getElementById('<%=lblSNRestruct.ClientID %>').innerHTML) - eval(document.getElementById('<%=lblSAOLD.ClientID %>').innerHTML);

                <%--document.getElementById('<%=txtNPSSA.ClientID%>').value = "0";--%>
                <%-- document.getElementById('<%=lblNPS.ClientID%>').innerHTML = "0";--%>                
                <%--document.getElementById('<%=lblBalance.ClientID%>').innerHTML = basic_15perc - basic_5perc;--%>
                <%--document.getElementById('<%=txtNPSSA.ClientID%>').value = "0";--%>              
                <%--document.getElementById('<%=lblNPS.ClientID%>').innerHTML = "0";--%>
                //document.getElementById('<%=txtAASA.ClientID%>').value = basic_15perc - basic_5perc;
                //alert(document.getElementById('<%=txtAASA.ClientID%>').value);
                document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML = "0";

                //Added By mohan On 27 May 2019
                document.getElementById('<%=txtAASA.ClientID%>').innerHTML = eval(document.getElementById('<%=lblSAOLD.ClientID %>').innerHTML) - eval(document.getElementById('<%=lblSARevised.ClientID %>').innerHTML);
                // alert(document.getElementById('<%=txtAASA.ClientID%>').innerHTML);
                document.getElementById('<%=hdnBalnceSA.ClientID%>').value = eval(document.getElementById('<%=lblSAOLD.ClientID %>').innerHTML) - eval(document.getElementById('<%=lblSARevised.ClientID %>').innerHTML);

                //End By Mohan

                document.getElementById('trSuperAnnuation').style.display = 'block';
                document.getElementById('trSuperAnnuation').style.width = '100%';

                //document.getElementById('trNPS').style.display = 'block';
                //document.getElementById('trNPS').style.width = '100%';
                document.getElementById('trSuperanuationMerged').style.display = 'block';
                document.getElementById('trSuperanuationMerged').style.width = '100%';

                <%--var isCTCFinalized = document.getElementById("<%=hdnIsEmpFinalizedCTCRestruc.ClientID%>").value;
                if (isCTCFinalized == 'true') {
                    document.getElementById('trSuperanuationMerged').style.display = 'none';
                    document.getElementById('<%=lblAA.ClientID %>').innerHTML = eval(document.getElementById('<%=lblAA.ClientID %>').innerHTML) + eval(document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML);
                }--%>

                document.getElementById('<%=lblSuperAnnPerc.ClientID %>').innerHTML = "5";
                $find("mpe").show();
            }  //else if (ddlSuperanuation.SelectedItem.Text == "Whole SA amount can be merged in AA")
            else if (listBoxSelectonValue == "5") {
            <%--document.getElementById('<%=txtNPSSA.ClientID%>').value = "0";--%>
            <%--document.getElementById('<%=lblNPS.ClientID%>').innerHTML = "0";--%>
                document.getElementById('<%=txtAASA.ClientID%>').innerHTML = "0";
                document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML = "0";

                //document.getElementById('<%=hdnBalnceSA.ClientID%>').value = eval(document.getElementById('<%=lblSAOLD.ClientID %>').innerHTML) - basic_10perc;
                document.getElementById('<%=hdnSN.ClientID %>').value = basic_10perc;
                document.getElementById('<%=lblSARevised.ClientID %>').innerHTML = basic_10perc;
                <%--document.getElementById('<%=lblBalance.ClientID%>').innerHTML = basic_15perc - basic_10perc;--%>
                document.getElementById('trSuperAnnuation').style.display = 'block';
                document.getElementById('trSuperAnnuation').style.width = '100%';

            <%--document.getElementById('<%=txtNPSSA.ClientID%>').value = "0";--%>
            <%--document.getElementById('<%=txtAASA.ClientID%>').innerHTML = basic_15perc - basic_10perc;--%>
                //Added By mohan On 27 May 2019

                document.getElementById('<%=txtAASA.ClientID%>').innerHTML = eval(document.getElementById('<%=lblSAOLD.ClientID %>').innerHTML) - eval(document.getElementById('<%=lblSARevised.ClientID %>').innerHTML);
            // alert(document.getElementById('<%=txtAASA.ClientID%>').innerHTML);
                document.getElementById('<%=hdnBalnceSA.ClientID%>').value = eval(document.getElementById('<%=lblSAOLD.ClientID %>').innerHTML) - eval(document.getElementById('<%=lblSARevised.ClientID %>').innerHTML);
            //End By Mohan
            <%--document.getElementById('<%=lblNPS.ClientID%>').innerHTML = "0";--%>
                document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML = "0";

                //document.getElementById('trNPS').style.display = 'block';
                //document.getElementById('trNPS').style.width = '100%';
                document.getElementById('trSuperanuationMerged').style.display = 'block';
                document.getElementById('trSuperanuationMerged').style.width = '100%';
                document.getElementById('<%=lblSuperAnnPerc.ClientID %>').innerHTML = "10";
                $find("mpe").show();
            }
            PerformCalculation();
        }


        function PerformCalculation() {
            // alert('hi');
            var lblBasicExisting = (document.getElementById('<%=lblBasicExisting.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblBasicExisting.ClientID %>').innerHTML;
            var txthra;
            var txtCA;
            var txtMARestruct;
            var txtCEA;
            var txtCHA;
            var txtLTA;
            var displayAA
            var mobile;
            var AA;
            var AAWithSuperannuation;
            var lblMeamCardDeducFrmAA;
            //Added By Mohan On 27 May 2019
            var txtNewNPS;
            var lblNPSDeducFrmAA;
            //End By Mohan


            //Added by Chetna 16 May 22
            var lblMedPre;
            //End by Chetna 16 May 22

            //Added by Chetna 9 Aug 22
            var lblCCA;
            //Ended By Chetna

            var mobileVal1 = eval(document.getElementById('<%=lblMobileExisting.ClientID%>').innerHTML) - eval(document.getElementById('<%=txtMobile.ClientID%>').value);

            document.getElementById('<%=txtAA.ClientID%>').innerHTML = mobileVal1 +
                eval(document.getElementById('<%=lblAAExisting.ClientID%>').innerHTML);


            if (document.getElementById('<%=hdnFormMode.ClientID%>').value == "0")//Form Mode 0 means new/Edit mode
            {
                txthra = (document.getElementById('<%=txtHRA.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtHRA.ClientID %>').value;
                txtCA = (document.getElementById('<%=txtCA.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtCA.ClientID %>').value;
                txtMARestruct = (document.getElementById('<%=txtMARestruct.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtMARestruct.ClientID %>').value;
                txtCEA = (document.getElementById('<%=txtCEA.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtCEA.ClientID %>').value;
                txtCHA = (document.getElementById('<%=txtCHA.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtCHA.ClientID %>').value;
                txtLTA = (document.getElementById('<%=txtLTA.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtLTA.ClientID %>').value;
                AAWithSuperannuation = (document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML == "" ? "0" : document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML);
                //Added by Chetna 16 May 22
                lblMedPre = (document.getElementById('<%=lblMediPreNew.ClientID%>').innerHTML == "" ? "0" : document.getElementById('<%=lblMediPreNew.ClientID%>').innerHTML);
                //End by Chetna 16 May 22

                //Added by Chetna 9 Aug 22
                lblCCA = (document.getElementById('<%=lblCCARestruct.ClientID%>').innerHTML == "" ? "0" : document.getElementById('<%=lblCCARestruct.ClientID%>').innerHTML);
                //End by Chetna 9 May 22

                if (eval(lblMedPre) == 0) { document.getElementById('trMedPre').style.display = 'none'; }
                else { document.getElementById('trMedPre').style.display = 'block'; }


                if (document.getElementById('<%=hdnAddtionalAllowanceApp.ClientID%>').value == "X" && document.getElementById('<%=hdnIsNewEmployee.ClientID%>').value.trim() == "Yes")//Form Mode 0 means new/Edit mode
                {
                    mobile = (document.getElementById('<%=txtMobile.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtMobile.ClientID %>').value;
            AA = (document.getElementById('<%=txtAA.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=txtAA.ClientID %>').innerHTML;
        }
        else {
            mobile = (document.getElementById('<%=lblMobile.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblMobile.ClientID %>').innerHTML;
            try {
                AA = (document.getElementById('<%=lblAA.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblAA.ClientID %>').innerHTML;
                    }
                    catch (err) {
                        AA = (document.getElementById('<%=txtAA.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=txtAA.ClientID %>').innerHTML;
                    }
                }

                lblMeamCardDeducFrmAA = (document.getElementById('<%=lblMeamCardDeducFrmAA.ClientID%>').innerHTML == "" ? "0" : document.getElementById('<%=lblMeamCardDeducFrmAA.ClientID%>').innerHTML);
                //Added By Moha-n On 30 May 2019
                txtNewNPS = (document.getElementById('<%=txtNPSNewAmt.ClientID %>').value == "") ? "0" : document.getElementById('<%=txtNPSNewAmt.ClientID %>').value;
                lblNPSDeducFrmAA = (document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML == "" ? "0" : document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML);

                var mobileDiff = eval(document.getElementById('<%=lblMobileExisting.ClientID%>').innerHTML) - eval(document.getElementById('<%=txtMobile.ClientID%>').value);
                var mealcardDiff = eval(document.getElementById('<%=lblMealCardExisting.ClientID %>').innerHTML) - eval(document.getElementById('<%=lblMealCardExisting.ClientID %>').innerHTML);
                var superannDiff = eval(document.getElementById('<%=lblSNExisting.ClientID %>').innerHTML) - eval(document.getElementById('<%=lblSNRestruct.ClientID %>').innerHTML);
                var NPSDiff = eval(document.getElementById('<%=lblNPSExistingAmt.ClientID %>').innerHTML) - eval(document.getElementById('<%=txtNPSNewAmt.ClientID %>').value);


                if (document.getElementById('<%=hdnSuperanution.ClientID %>').value == "IsNotApplicable"
                    && document.getElementById('<%=hdnIsSAApplicable.ClientID %>').value.trim() == "No") //Added By Mohan On 30 May 2019
                {
                    HideSuperAnnuation();
                }
                else {
                    document.getElementById('trSuperanuationMerged').style.display = 'block';
                    //document.getElementById('trNPS').style.display = 'block';
                    document.getElementById('trSuperAnnuation').style.display = 'block';
                }

                try {

                    // To be added for the recalculating Revised AA
                   <%-- document.getElementById('<%=lblAACalculation.ClientID %>').innerHTML =
                        eval(document.getElementById('<%=lblAA.ClientID %>').innerHTML) +
                        eval(document.getElementById('<%=lblMobileExisting.ClientID%>').innerHTML) +
                        eval(document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML) +
                        eval(document.getElementById('<%=lblMeamCardDeducFrmAA.ClientID%>').innerHTML) +
                        eval(document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML); --%>//Added by Mohan On 30 May 2019

            var mobileVal = eval(document.getElementById('<%=lblMobileExisting.ClientID%>').innerHTML) - eval(document.getElementById('<%=txtMobile.ClientID%>').value);


            if (document.getElementById('<%=hdnIsNewEmployee.ClientID%>').value.trim() == "Yes"
                && (document.getElementById('<%=hdnIsSAApplicable.ClientID%>').value.trim() == "Yes"
                            || document.getElementById('<%=hdnIsSAApplicable.ClientID%>').value.trim() == "No")) {

<%--                        document.getElementById('<%=lblAACalculation.ClientID %>').innerHTML =
                        eval(document.getElementById('<%=lblAA.ClientID %>').innerHTML) +
                        eval(document.getElementById('<%=lblMobileExisting.ClientID%>').innerHTML) +
                        eval(document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML) +
                        eval(document.getElementById('<%=lblMeamCardDeducFrmAA.ClientID%>').innerHTML) +
                        eval(document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML);--%>

                        // var mobileVal = eval(document.getElementById('<%=lblMobileExisting.ClientID%>').innerHTML) - eval(document.getElementById('<%=txtMobile.ClientID%>').value);

                        //var mobileVal = Convert.ToInt64(lblMobileExisting.Text) - Convert.ToInt64(lblMobile.Text);


                            //lblAACalculation.Text = Convert.ToString(
                            //Convert.ToInt32(lblAAExisting.Text == "" ? "0" : lblAAExisting.Text) +
                            //Convert.ToInt64(mobileVal) +
                            //Convert.ToInt64(lblAAWithSuperannuation.Text == "" ? "0" : lblAAWithSuperannuation.Text) +
                            //Convert.ToInt64(lblMeamCardDeducFrmAA.Text == "" ? "0" : lblMeamCardDeducFrmAA.Text) +
                            //Convert.ToInt64(txtMobile.Text == "" ? "0" : txtMobile.Text) +
                            //Convert.ToInt64(lblNPSDeducFrmAA.Text == "" ? "0" : lblNPSDeducFrmAA.Text)); //Added By Chetna On 10 Aug 2022


                        <%-- alert('23');
                alert(document.getElementById('<%=lblAACalculation.ClientID %>').innerHTML);
                alert(eval(document.getElementById('<%=txtAA.ClientID %>').innerHTML));
                alert(mobileVal);
                alert(eval(document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML));
                alert(eval(document.getElementById('<%=lblMeamCardDeducFrmAA.ClientID%>').innerHTML));
                        alert(eval(document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML));--%>

                        document.getElementById('<%=lblAACalculation.ClientID %>').innerHTML =
                            eval(document.getElementById('<%=lblAAExisting.ClientID %>').innerHTML) +
                                mobileVal +
                                eval(document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML) +
                            eval(document.getElementById('<%=lblMeamCardDeducFrmAA.ClientID%>').innerHTML) +
                            eval(document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML); //Added by Mohan On 30 May 2019

                    }
                    else if (document.getElementById('<%=hdnIsNewEmployee.ClientID%>').value.trim() == "No"
                        && document.getElementById('<%=hdnIsSAApplicable.ClientID%>').value.trim() == "No") {

                        document.getElementById('<%=lblAACalculation.ClientID %>').innerHTML =
                            eval(document.getElementById('<%=lblAA.ClientID %>').innerHTML) + mobileVal +
                            eval(document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML);
                    }
                    else if (document.getElementById('<%=hdnIsNewEmployee.ClientID%>').value.trim() == "No"
                        && document.getElementById('<%=hdnIsSAApplicable.ClientID%>').value.trim() == "Yes") {

                        document.getElementById('<%=lblAACalculation.ClientID %>').innerHTML =
                            eval(document.getElementById('<%=lblAA.ClientID %>').innerHTML) + mobileVal +
                            eval(document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML) +
                        eval(document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML);
                    }
                    //End By Mohan
                } catch (e) {

                    // To be added for the recalculating Revised AA
                   <%-- document.getElementById('<%=lblAACalculation.ClientID %>').innerHTML = eval(document.getElementById('<%=txtAA.ClientID %>').innerHTML) +
                    eval(document.getElementById('<%=lblMobileExisting.ClientID%>').innerHTML) +
                    eval(document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML) +
                    eval(document.getElementById('<%=lblMeamCardDeducFrmAA.ClientID%>').innerHTML) +
                    eval(document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML); --%>//Added by Mohan On 30 May 2019

                       var mobileVal = eval(document.getElementById('<%=lblMobileExisting.ClientID%>').innerHTML) - eval(document.getElementById('<%=txtMobile.ClientID%>').value);

                    if (document.getElementById('<%=hdnIsNewEmployee.ClientID%>').value.trim() == "Yes"
                        && (document.getElementById('<%=hdnIsSAApplicable.ClientID%>').value.trim() == "Yes"
                            || document.getElementById('<%=hdnIsSAApplicable.ClientID%>').value.trim() == "No")) {

 <%--                        document.getElementById('<%=lblAACalculation.ClientID %>').innerHTML =
                        eval(document.getElementById('<%=txtAA.ClientID %>').innerHTML) +
                        eval(document.getElementById('<%=lblMobileExisting.ClientID%>').innerHTML) +
                        eval(document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML) +
                        eval(document.getElementById('<%=lblMeamCardDeducFrmAA.ClientID%>').innerHTML) +
                        eval(document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML);--%>

                        var mobileVal = eval(document.getElementById('<%=lblMobileExisting.ClientID%>').innerHTML) - eval(document.getElementById('<%=txtMobile.ClientID%>').value);

                       <%--  alert('24');
                alert(document.getElementById('<%=lblAACalculation.ClientID %>').innerHTML);
                alert(eval(document.getElementById('<%=txtAA.ClientID %>').innerHTML));
                alert(mobileVal);
                alert(eval(document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML));
                alert(eval(document.getElementById('<%=lblMeamCardDeducFrmAA.ClientID%>').innerHTML));
                        alert(eval(document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML));--%>

                        document.getElementById('<%=lblAACalculation.ClientID %>').innerHTML =
                            eval(document.getElementById('<%=txtAA.ClientID %>').innerHTML) +
                            mobileVal +
                            eval(document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML) +
                        eval(document.getElementById('<%=lblMeamCardDeducFrmAA.ClientID%>').innerHTML) +
                        eval(document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML); //Added by Mohan On 30 May 2019

                    }
                    else if (document.getElementById('<%=hdnIsNewEmployee.ClientID%>').value.trim() == "No"
                        && document.getElementById('<%=hdnIsSAApplicable.ClientID%>').value.trim() == "No") {

                        document.getElementById('<%=lblAACalculation.ClientID %>').innerHTML =
                            eval(document.getElementById('<%=txtAA.ClientID %>').innerHTML) + mobileVal +
                            eval(document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML);
                    }
                    else if (document.getElementById('<%=hdnIsNewEmployee.ClientID%>').value.trim() == "No"
                        && document.getElementById('<%=hdnIsSAApplicable.ClientID%>').value.trim() == "Yes") {

                        document.getElementById('<%=lblAACalculation.ClientID %>').innerHTML =
                            eval(document.getElementById('<%=txtAA.ClientID %>').innerHTML) + mobileVal +
                            eval(document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML) +
                        eval(document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML);
            }
            //End By Mohan
        }
    }
    else {

        txthra = (document.getElementById('<%=lblHRA.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblHRA.ClientID %>').innerHTML;
        txtCA = (document.getElementById('<%=lblCA.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblCA.ClientID %>').innerHTML;
        txtMARestruct = (document.getElementById('<%=lblMA.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblMA.ClientID %>').innerHTML;
        txtCEA = (document.getElementById('<%=lblCEA.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblCEA.ClientID %>').innerHTML;
        txtCHA = (document.getElementById('<%=lblCHA.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblCHA.ClientID %>').innerHTML;
        txtLTA = (document.getElementById('<%=lblLTA.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblLTA.ClientID %>').innerHTML;
        mobile = (document.getElementById('<%=lblMobile.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblMobile.ClientID %>').innerHTML;
        AA = (document.getElementById('<%=lblAA.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblAA.ClientID %>').innerHTML;
        lblMeamCardDeducFrmAA = (document.getElementById('<%=lblMeamCardDeducFrmAA.ClientID%>').innerHTML == "" ? "0" : document.getElementById('<%=lblMeamCardDeducFrmAA.ClientID%>').innerHTML);
        //Added By Mohan on 27 May 2019 
        txtNewNPS = (document.getElementById('<%=lblNPSNewAmt.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblNPSNewAmt.ClientID %>').innerHTML;
        lblNPSDeducFrmAA = (document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML == "" ? "0" : document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML);

        //Added by Chetna 16 May 22
        lblMedPre = (document.getElementById('<%=lblMediPreNew.ClientID%>').innerHTML == "" ? "0" : document.getElementById('<%=lblMediPreNew.ClientID%>').innerHTML);
        //End by Chetna 16 May 22

        //Added by Chetna 9 Aug 22
        lblCCA = (document.getElementById('<%=lblCCARestruct.ClientID%>').innerHTML == "" ? "0" : document.getElementById('<%=lblCCARestruct.ClientID%>').innerHTML);
        //End by Chetna 9 Aug 22

        if (eval(lblMedPre) == 0) { document.getElementById('trMedPre').style.display = 'none'; }
        else { document.getElementById('trMedPre').style.display = 'block'; }

        var mobileVal = eval(document.getElementById('<%=lblMobileExisting.ClientID%>').innerHTML) - eval(document.getElementById('<%=txtMobile.ClientID%>').value);

                if (document.getElementById('<%=hdnIsNewEmployee.ClientID%>').value.trim() == "Yes"
            && (document.getElementById('<%=hdnIsSAApplicable.ClientID%>').value.trim() == "Yes"
                        || document.getElementById('<%=hdnIsSAApplicable.ClientID%>').value.trim() == "No")) {

                    document.getElementById('<%=lblAACalculation.ClientID %>').innerHTML =
                        eval(document.getElementById('<%=lblAA.ClientID %>').innerHTML) +
                        mobileVal +
                    eval(document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML) +
                    eval(document.getElementById('<%=lblMeamCardDeducFrmAA.ClientID%>').innerHTML) +
                    eval(document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML); //Added by Mohan On 30 May 2019
                }
                else if (document.getElementById('<%=hdnIsNewEmployee.ClientID%>').value.trim() == "No"
                    && document.getElementById('<%=hdnIsSAApplicable.ClientID%>').value.trim() == "No") {

                    document.getElementById('<%=lblAACalculation.ClientID %>').innerHTML =
                        eval(document.getElementById('<%=lblAA.ClientID %>').innerHTML) + mobileVal +
                        eval(document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML);
                }
                else if (document.getElementById('<%=hdnIsNewEmployee.ClientID%>').value.trim() == "No"
                    && document.getElementById('<%=hdnIsSAApplicable.ClientID%>').value.trim() == "Yes") {

                    document.getElementById('<%=lblAACalculation.ClientID %>').innerHTML =
                        eval(document.getElementById('<%=lblAA.ClientID %>').innerHTML) + mobileVal +
                        eval(document.getElementById('<%=lblAAWithSuperannuation.ClientID%>').innerHTML) +
                    eval(document.getElementById('<%=lblNPSDeducFrmAA.ClientID%>').innerHTML);
                }
                //End By Mohan
            }
            //  alert(AA);
            var lblHRAExisting = (document.getElementById('<%=lblHRAExisting.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblHRAExisting.ClientID %>').innerHTML;
            var lblConAlloExisting = (document.getElementById('<%=lblConAlloExisting.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblConAlloExisting.ClientID %>').innerHTML;
            var lblLTAExisting = (document.getElementById('<%=lblLTAExisting.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblLTAExisting.ClientID %>').innerHTML;
            var lblCEAExisting = (document.getElementById('<%=lblCEAExisting.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblCEAExisting.ClientID %>').innerHTML;
            var lblCHAExisting = (document.getElementById('<%=lblCHAExisting.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblCHAExisting.ClientID %>').innerHTML;
            var lblMAExisting = (document.getElementById('<%=lblMAExisting.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblMAExisting.ClientID %>').innerHTML;

            var isCTCFinalized = document.getElementById("<%=hdnIsEmpFinalizedCTCRestruc.ClientID%>").value;
            var lblGroupALevEncMax = (eval(lblHRAExisting) + eval(lblConAlloExisting) + eval(lblLTAExisting) + eval(lblCEAExisting) + eval(lblCHAExisting) + eval(lblMAExisting));

            if ((eval(lblGroupALevEncMax) - (eval(txthra) + eval(txtCA) + eval(txtMARestruct) + eval(txtLTA) + eval(txtCEA) + eval(txtCHA))) < 0) {
                // alert("Exceeding group limit !...Please clear and re-enter value.");
            }


           // alert('1');
            var TextBox1 = (eval(lblGroupALevEncMax) - (eval(txthra) + eval(txtCA) + eval(txtMARestruct) + eval(txtLTA) + eval(txtCEA) + eval(txtCHA)));
            document.getElementById('<%=TextBox1.ClientID %>').value = TextBox1;
            var lblMobileExisting = (document.getElementById('<%=lblMobileExisting.ClientID %>').value == "") ? "0" : document.getElementById('<%=lblMobileExisting.ClientID %>').innerHTML;
            //alert(document.getElementById('<%=lblAAExisting.ClientID %>').value); 
            var lblAAExisting = (document.getElementById('<%=lblAAExisting.ClientID %>').value == "") ? "0" : document.getElementById('<%=lblAAExisting.ClientID %>').innerHTML;
           // alert(lblAAExisting);
            var lblAAWithSuperannuation = (document.getElementById('<%=lblAAWithSuperannuation.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblAAWithSuperannuation.ClientID %>').innerHTML;
            var lblAAWithCanteenSubsRestruc = "0";

            var lblGroupBTotalLimit;
            var IsMealVoucherOpted = document.getElementById('<%=hdnIsMealVoucherOpted.ClientID %>').value;

            //if (lblMeamCardDeducFrmAA != "0")
            //{
            //    lblMeamCardDeducFrmAA = - eval(lblMeamCardDeducFrmAA);
            //}

            //lblAAWithCanteenSubsRestruc
            //lblMeamCardDeducFrmAA
            //lblAAWithSuperannuation

            var lblGroupATotalRestruc;
            //alert(lblAAWithSuperannuation);
            // alert(lblAAWithCanteenSubsRestruc);
            // alert(eval(lblMeamCardDeducFrmAA));
            var mobileVal = eval(document.getElementById('<%=lblMobileExisting.ClientID%>').innerHTML) - eval(document.getElementById('<%=txtMobile.ClientID%>').value);

            if (isCTCFinalized == 'true') {
                //alert('eval(lblBasicExisting)' + eval(lblBasicExisting) + '(eval(txthra)' + eval(txthra) + 'eval(txtCA)' + eval(txtCA) + 'eval(txtMARestruct)' +eval(txtMARestruct) + ' eval(txtLTA)'  + eval(txtLTA) + 'eval(txtCEA' +  eval(txtCEA) + 'eval(mobile)' + eval(mobile) + 'eval(AA)' + eval(AA) + 'eval(lblAAWithCanteenSubsRestruc)' + eval(lblAAWithCanteenSubsRestruc));

                
        ////alert(eval(AA));
        //lblGroupATotalRestruc = eval(lblBasicExisting) + (eval(txthra) + eval(txtCA) + eval(txtMARestruct) + eval(txtLTA) + eval(txtCEA) + eval(txtCHA)) + eval(mobileVal) + eval(AA) + eval(lblAAWithCanteenSubsRestruc) + eval(lblAAWithSuperannuation) + eval(lblMeamCardDeducFrmAA) + eval(lblNPSDeducFrmAA);
        //lblGroupBTotalLimit = (eval(lblMobileExisting) + eval(lblAAExisting)) - (eval(mobileVal) + eval(AA));
        //Commented by Chetna on 12 Aug 22
        lblGroupATotalRestruc = eval(lblBasicExisting) + (eval(txthra) + eval(txtCA) + eval(txtMARestruct) + eval(txtLTA) + eval(txtCEA) + eval(txtCHA)) + eval(AA) + eval(lblAAWithCanteenSubsRestruc) + eval(lblAAWithSuperannuation) + eval(lblMeamCardDeducFrmAA) + eval(lblNPSDeducFrmAA);
        lblGroupBTotalLimit = (eval(lblMobileExisting) + eval(lblAAExisting)) - (eval(document.getElementById('<%=txtMobile.ClientID%>').value) + eval(AA));
                lblGroupBTotalLimit = "0";
                //Ended Comment By chetna on 12 Aug 22
        //lblGroupBTotalLimit = (eval(lblMobileExisting) + eval(lblAAExisting)) - (eval(mobile) + eval(AA) - eval(lblAAWithSuperannuation));
    }
    else {
        //alert('eval(lblBasicExisting)' + eval(lblBasicExisting) + '(eval(txthra)' + eval(txthra) + 'eval(txtCA)' + eval(txtCA) + 'eval(txtMARestruct)' + eval(txtMARestruct) + ' eval(txtLTA)' + eval(txtLTA) + 'eval(txtCEA' + eval(txtCEA) + 'eval(mobile)' + eval(mobile) + 'eval(AA)' + eval(AA) + 'eval(lblAAWithCanteenSubsRestruc)' + eval(lblAAWithCanteenSubsRestruc));
        //lblGroupATotalRestruc = (eval(lblBasicExisting) + (eval(txthra) + eval(txtCA) + eval(txtMARestruct) + eval(txtLTA) + eval(txtCEA)) + eval(mobile) + eval(AA)) + eval(lblAAWithSuperannuation);


        //alert(eval(AA));
                <%--eval(document.getElementById('<%=txtMobile.ClientID%>').value) -
                    eval(document.getElementById('<%=lblMobileExisting.ClientID%>').innerHTML) +--%>
                var OrigAA = 
                    eval(document.getElementById('<%=lblAAExisting.ClientID%>').innerHTML);
                var NewAA = eval(document.getElementById('<%=txtAA.ClientID %>').innerHTML);
                //alert(NewAA);
                var NewMobile1 = eval(document.getElementById('<%=txtMobile.ClientID%>').value);
                var mobileVal = eval(document.getElementById('<%=lblMobileExisting.ClientID%>').innerHTML) - eval(document.getElementById('<%=txtMobile.ClientID%>').value);
                //alert(mobileVal);
        if (eval(document.getElementById('<%=lblMobileExisting.ClientID%>').innerHTML) != eval(document.getElementById('<%=txtMobile.ClientID%>').value)) {
            
                    //  mobileVal = 0;
            
                    lblGroupATotalRestruc = (eval(lblBasicExisting) + (eval(txthra) + eval(txtCA) + eval(txtMARestruct) + eval(txtLTA) +
                        eval(txtCEA) + eval(txtCHA)) + eval(NewAA)) + NewMobile1 +
                        eval(lblAAWithSuperannuation) + eval(lblAAWithCanteenSubsRestruc) + eval(lblMeamCardDeducFrmAA) + eval(lblNPSDeducFrmAA);

                    //lblGroupATotalRestruc = (eval(lblBasicExisting) + (eval(txthra) + eval(txtCA) + eval(txtMARestruct) + eval(txtLTA) +
                    //    eval(txtCEA) + eval(txtCHA)) + eval(mobileVal) + eval(AA)) +
                    //    eval(lblAAWithSuperannuation) + eval(lblAAWithCanteenSubsRestruc) + eval(lblMeamCardDeducFrmAA) + eval(lblNPSDeducFrmAA);

                    lblGroupBTotalLimit = (eval(lblMobileExisting) + eval(lblAAExisting)) - (eval(document.getElementById('<%=txtMobile.ClientID%>').value) + eval(AA));
                lblGroupBTotalLimit = "0";
        }
        else {
            
            lblGroupATotalRestruc = (eval(lblBasicExisting) + (eval(txthra) + eval(txtCA) + eval(txtMARestruct) + eval(txtLTA) +
                eval(txtCEA) + eval(txtCHA)) + eval(NewAA)) + NewMobile1 +
                eval(lblAAWithSuperannuation) + eval(lblAAWithCanteenSubsRestruc) + eval(lblMeamCardDeducFrmAA) + eval(lblNPSDeducFrmAA);
                        //eval(NewMobile1);

                        lblGroupBTotalLimit = "0";

                  //  lblGroupBTotalLimit = ((eval(lblMobileExisting) + eval(lblAAExisting)) - (eval(mobileVal) + eval(AA))) - (eval(mobileVal) + eval(AA));
                   // lblGroupBTotalLimit = ((eval(lblMobileExisting) + eval(lblAAExisting)) -
                   //     ((eval(document.getElementById('<%=txtMobile.ClientID%>').value) + eval(lblAAExisting)) - (eval(mobileVal) + eval(AA)));
                }




            }

            document.getElementById('<%=lblGroupATotalRestruc.ClientID %>').innerHTML = lblGroupATotalRestruc;
            document.getElementById('<%=lblGroupBTotalLimit.ClientID %>').innerHTML = lblGroupBTotalLimit;

            var lblPFRestruct = (document.getElementById('<%=lblPFRestruct.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblPFRestruct.ClientID %>').innerHTML;
            var lblGratuityRestruct = (document.getElementById('<%=lblGratuityRestruct.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblGratuityRestruct.ClientID %>').innerHTML;
            var lblSNRestruct = (document.getElementById('<%=lblSNRestruct.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblSNRestruct.ClientID %>').innerHTML;
            <%--var lblNPS = (document.getElementById('<%=lblNPS.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblNPS.ClientID %>').innerHTML;--%>

            var lblGroupBTotalRestruc = eval(lblPFRestruct) + eval(lblGratuityRestruct) + eval(lblSNRestruct) + eval(txtNewNPS); //+ eval(lblNPS)
            document.getElementById('<%=lblGroupBTotalRestruc.ClientID %>').innerHTML = lblGroupBTotalRestruc;

            var lblSiteAllowaRestruct = (document.getElementById('<%=lblSiteAllowaRestruct.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblSiteAllowaRestruct.ClientID %>').innerHTML;
            var lblESICRestruct = (document.getElementById('<%=lblESICRestruct.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblESICRestruct.ClientID %>').innerHTML;

            var lblBonusRestruct = (document.getElementById('<%=lblBonusRestruct.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblBonusRestruct.ClientID %>').innerHTML;
            document.getElementById('<%=lblESICRestruct.ClientID %>').innerHTML = lblESICRestruct;
            //var lblGroupCTotalRestruc = eval(lblSiteAllowaRestruct) + eval(lblESICRestruct) + +eval(lblBonusRestruct);
            var lblGroupCTotalRestruc = eval(lblSiteAllowaRestruct) + eval(lblESICRestruct) + +eval(lblBonusRestruct) + eval(lblCCA);


            document.getElementById('<%=lblGroupCTotalRestruc.ClientID %>').innerHTML = lblGroupCTotalRestruc;

            var lblCanteenRestruct = (document.getElementById('<%=lblMealCardRestruc.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblMealCardRestruc.ClientID %>').innerHTML;

            var lblTotalABCDRestruc = eval(lblGroupCTotalRestruc) + eval(lblGroupBTotalRestruc) + eval(lblGroupATotalRestruc) + eval(lblCanteenRestruct) + eval(lblMedPre);
            document.getElementById('<%=lblTotalABCDRestruc.ClientID %>').innerHTML = lblTotalABCDRestruc;

            var lblComputedFixedRestructur = (eval(lblMedPre) + eval(lblCanteenRestruct) + eval(lblGroupCTotalRestruc) + eval(lblGroupBTotalRestruc) + eval(lblGroupATotalRestruc)) * 12;
            document.getElementById('<%=lblComputedFixedRestructur.ClientID %>').innerHTML = lblComputedFixedRestructur;

            var lblICRestruc = (document.getElementById('<%=lblICRestruc.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblICRestruc.ClientID %>').innerHTML;
            var lblCCRestruc = (document.getElementById('<%=lblCCRestruc.ClientID %>').innerHTML == "") ? "0" : document.getElementById('<%=lblCCRestruc.ClientID %>').innerHTML;
            var lblTotalERestructur = eval(lblICRestruc) + eval(lblCCRestruc);
            document.getElementById('<%=lblTotalERestructur.ClientID %>').innerHTML = lblTotalERestructur;

            var lblComputedCTCRestruct = eval(lblTotalERestructur) + eval(lblComputedFixedRestructur);
            document.getElementById('<%=lblComputedCTCRestruct.ClientID %>').innerHTML = lblComputedCTCRestruct;
            document.getElementById('<%=hdnComputedCTCRestruct.ClientID %>').value = lblComputedCTCRestruct;
            //alert(document.getElementById('<%=hdnSuperanution.ClientID %>').value);

            if (document.getElementById('<%=hdnSuperanution.ClientID %>').value == "IsNotApplicable"
                && document.getElementById('<%=hdnIsSAApplicable.ClientID %>').value.trim() == "No") //Added By Mohan On 30 May 2019
            {

                HideSuperAnnuation();
            }
            else {

                document.getElementById('trSuperanuationMerged').style.display = 'block';
                //document.getElementById('trNPS').style.display = 'block';
                document.getElementById('trSuperAnnuation').style.display = 'block';
            }

            }

        function toggleVisibility() {
            x.style.visibility = "hidden";
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

        function PromptForWhetherCTCRestrucFinalization() {
            if (confirm("Are You sure your CTC restructure is Finalized ? \nIf not please review and then Print. \nAfter printing CTC cannot be revoked.")) {
                document.getElementById('<%= btnPrintCTCRestruc.ClientID %>').click();
            }
            else {
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="background-color: white;">
            <table style="background-color: white; width: 70%; margin: auto; border: 2px solid #8B8989; border-radius: 15px 15px 15px 15px;">
                <tr>
                    <td>
                        <asp:Image ID="Image1" runat="server" Width="100%" ImageUrl="~/Layout/Images/Sample 2.png" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="div1" runat="server" style="display: none;">
                            <asp:Button ID="btnFireRadioEvent" runat="server" OnClick="btnFireRadioEvent_Click" />
                        </div>
                        <table style="width: 100%; padding: 10px">
                            <tr>
                                <td>
                                    <table style="width: 100%">
                                        <tr>
                                            <td class="form-fld-lbl" style="width: 20%;">Employee Name (Code) 
                                                
                                            </td>
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
                    <td colspan="2">
                        <hr />
                        <span class="form-fld-lbl" style="font-family: Verdana; font-size: 12px; color: #800000;">Instructions:</span>
                        <ol type="1" style="font-family: Verdana; font-size: 12px;">
                            <li>Compensation Restructuring is allowed only once in the Financial Year.  </li>
                            <li>Please refer to <a href="Documents/M9HR36AR8 - Compensation Restructuring (CompStruct)_User Manual.pdf" target="_blank">TCE.M9-HR-PA-36A : Compensation Restructuring (CompStruct) – User Manual.pdf” </a>before restructuring your CTC.</li>
                            <li>The compensation restructuring will be effective 1st April 2022 for existing employees (with date of Joining till 31st March 2022). <strong>CompStruct will be open till 15th June 2022 for existing employees.</strong> Employees will be required to restructure and submit restructured CTC online by 15th June 2022. <strong>In case, an employee does not submit the restructured CTC by 15th June 2022, prevailing CTC will continue for the rest of the Financial Year.</strong>
                            </li>
                            <li>For new joinees (Date of Joining 1st April 2022 onwards), Compensation restructuring will be as per timelines mentioned in TCE.M9-HR-PA-36A. In case, an employee does not submit the restructured CTC, prevailing CTC will continue for the rest of the Financial Year.
                            </li>
                            <li>Employees are requested to understand prevailing income tax rules before restructuring compensation suiting to their individual needs. Once the restructured compensation is submitted, it will continue for the rest of the Financial Year. No modification will be permitted after submission.
                            </li>
                            <li>The amount in HRA, CA, LTA, CEA and MA can be restructured within these components only. </li>
                            <li>“Mobile Reimbursement” and “Meal Card” can be restructured with Additional Allowance (AA). For existing employees, this option was already given in April 2022 so it is frozen for the FY 22-23.
                            </li>
                            <li>“Mobile Reimbursement” can be claimed through Expense Management System (EMS) every month. Unclaimed amount will be paid through salary and taxed as per IT rules. Employees having Company provided mobile phones or employees who are permitted to claim reimbursement of personal mobile bill for official calls (due to demand of the role) will not be eligible to claim above reimbursement.
                            </li>
                            <li>The amount in Superannuation can be restructured with AA (Applicable to E6/A6/T6 & above grades only).</li>

                            <li>Employee may choose to invest in NPS and that amount will be restructured with AA. Employee must open corporate NPS account and submit relevant details to your DC-HR before 15th of the month.</li>
                            <li>For any query write to <a href="mailto:nayank@tce.co.in" target="_top">nayank@tce.co.in</a>  </li>
                        </ol>
                    </td>
                </tr>
            </table>
        </div>
        <div style="background-color: white;">
            <table style="margin: 0px auto; width: 70%; text-align: center;">
                <tr>
                    <td>
                        <%--<asp:ImageButton ID="ImageButton1" ImageUrl="~/Layout/Images/Save As Draft.png" runat="server" Text="Save As Draft" OnClick="btnApplyCTC_Click"  ForeColor="white" Width="94px" Height="25px" />--%>
                        <asp:ImageButton ID="btnApplyCTC" OnClick="btnApplyCTC_Click" ImageUrl="~/Layout/Images/Save As Draft1.png" runat="server" Text="Save As Draft" ForeColor="white" Width="94px" Height="25px" />
                        &nbsp;<asp:ImageButton ID="btnPullback0" runat="server" ImageUrl="~/Layout/Images/Reset.png" Text="Revoke" OnClientClick="Confirm_Pullback()" Width="94px" Height="25px" ForeColor="white" OnClick="OnConfirmPullback" />
                        &nbsp;<asp:ImageButton ID="btnPrintCTCRestruc0" Text="Save File" ImageUrl="~/Layout/Images/Submit.png" OnClientClick="Confirm_Print();" runat="server" OnClick="OnConfirmPrint" Height="25px" Width="94px" ForeColor="white" />
                        &nbsp;<asp:ImageButton ID="btnPrintFinal" Text="Print" runat="server" ImageUrl="~/Layout/Images/Save File.png" OnClick="btnPrintFinal_Click1" ForeColor="white" Height="25px" Width="94px" Visible="False" />
                        &nbsp;<%--<asp:ImageButton ID="btnPrintFinal" Text="Print" runat="server" ImageUrl="~/Layout/Images/Save File.png" OnClick="btnPrintFinal_Click" OnClientClick="document.forms[0].target = '_blank';" ForeColor="white" Height="25px" Width="94px" />--%>
                        &nbsp;<asp:ImageButton ID="btnPullback" Text="Revoke" runat="server" ImageUrl="~/Layout/Images/Revoke.png" OnClick="btnPullback0_Click" CssClass="hidden" Width="94px" Height="20px" ForeColor="white" />
                        &nbsp;<asp:ImageButton ID="btnPrintCTCRestruc" Height="25px" Text="Print Restructured CTC" ImageUrl="~/Layout/Images/Save As Draft.png" runat="server" OnClick="btnPrintCTCRestruc_Click" Width="94px" CssClass="hidden" />
                        <a href="../Logout.aspx" onclick="preventBack();" style="color: White;">
                            <img alt="" src="Layout/Images/Logout.png" height="25px" width="94px" />
                        </a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEmpCTCRestrucStatus" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table id="rcorners6">
                <tr>
                    <td colspan="7" style="text-align: center; font-family: Verdana; font-size: 16px;">
                        <b>Compensation Restructuring : FY
                            <asp:Literal ID="ltrFinYr" runat="server"></asp:Literal></b>
                        <br />
                        <b class="Required">You Opted Regime :
                         <asp:Literal ID="ltrRegime" runat="server"></asp:Literal></b>

                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl" style="width: 5%; text-align: center;"></td>
                    <td class="form-fld-lbl" style="width: 38%; text-align: center; vertical-align: top">Description</td>
                    <td class="form-fld-lbl" style="width: 7%; text-align: center; vertical-align: top">Existing CTC(Rs.)</td>
                    <td class="form-fld-lbl" style="width: 10%; text-align: center; vertical-align: top">Input by employee (Rs.)</td>
                    <td class="form-fld-lbl" style="width: 15%; text-align: center; vertical-align: top">Amount to be restructured (Rs.)</td>
                    <td class="form-fld-lbl" style="width: 10%; text-align: center; vertical-align: top">Ceiling amount for tax exemption (Rs.)</td>
                    <td class="form-fld-lbl" style="width: 20%">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="7">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl" style="font-family: Verdana; font-size: 12px; color: #800000;">[A]</td>
                    <td class="form-fld-lbl" style="font-family: Verdana; font-size: 12px; color: #800000;">Salary & Allowances</td>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">&nbsp;</td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Basic</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblBasicExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label></td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblBasicFinal" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label></td>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl" style="background-color: white;"></td>
                    <td class="form-fld-lbl">
                        <asp:ImageButton ID="trigger6" runat="server" ImageUrl="~/Layout/Images/help_16x16.ico" />
                        <div id="1231" class="container">
                            <div id="pop-up6">
                                <h4>Basic </h4>
                                <p>
                                    Since retiral benefits (PF, Gratuity and Superannuation) are linked to basic salary, it cannot be restructured and will remain same.                    
                                </p>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">House Rent Allowance (HRA)</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblHRAExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txtHRA" runat="server" onblur="javascript:IsNumeric(this,event);" CssClass="numerictextboxlabel" Width="92%">0</asp:TextBox>
                        <asp:Label ID="lblHRA" runat="server" CssClass="numerictextboxlabel" Width="92%">0</asp:Label>
                    </td>
                    <td rowspan="6" style="background-image: url('Layout/Images/GroupAImg_New.png'); background-repeat: no-repeat;">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="numerictextboxlabel" TabIndex="1000"
                            BorderStyle="none" BackColor="transparent" Text="0" Width="55px"></asp:TextBox>
                        <%-- <asp:Label ID="lblGroupATotalLimit" runat="server" Text="0" CssClass="numerictextboxlabel"></asp:Label>--%>

                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblHRAMaxCeiling" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label></td>
                    <td class="form-fld-lbl">
                        <div id="1232" class="container">
                            <asp:ImageButton ID="trigger" runat="server" ImageUrl="~/Layout/Images/help_16x16.ico" />
                            <%-- <a href="#" id="trigger">this link</a>.--%>
                            <!-- HIDDEN / POP-UP DIV -->
                            <div id="pop-up">
                                <h4>House Rent Allowance (HRA) </h4>
                                <p>
                                    Currently HRA is 40% of Basic by default. Employees staying in rental accommodation in Mumbai, Chennai,
                                    Kolkata and Delhi can increase HRA amount upto 50% of Basic to avail maximum tax exemption as per current IT rules. 
                                </p>
                                <p><b>Employee can restructure HRA amount within a range from 40% of Basic to 50% of Basic.</b></p>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Conveyance Allowance (CA)</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblConAlloExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txtCA" runat="server" CssClass="numerictextboxlabel" onblur="javascript:IsNumeric(this,event);" Width="92%">0</asp:TextBox>
                        <asp:Label ID="lblCA" runat="server" CssClass="numerictextboxlabel" Width="92%">0</asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblCAMaxCeiling" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label></td>
                    <td class="form-fld-lbl">
                        <div id="1233" class="container">
                            <asp:ImageButton ID="trigger1" Visible="false" runat="server" onmouseover1="HideAndShowHelpDiv('1');" ImageUrl="~/Layout/Images/help_16x16.ico" />
                            <div id="pop-up1">
                                <h4>Conveyance (Transport  ) Allowance </h4>
                                <p>
                                    Currently CA amount are fixed as per grade. Tax exemption is available maximum @ Rs.1600/- per month. 
                                </p>
                                <p><b>Employees can restructure the amount with minimum amount as Rs. 1600/- per month. </b></p>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Leave Travel Allowance (LTA)</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblLTAExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txtLTA" runat="server" CssClass="numerictextboxlabel" onblur="javascript:IsNumeric(this,event);" Width="92%">0</asp:TextBox>
                        <asp:Label ID="lblLTA" runat="server" CssClass="numerictextboxlabel" Width="92%">0</asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblLTAMaxCeiling" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label></td>
                    <td class="form-fld-lbl">
                        <div id="1234" class="container">
                            <asp:ImageButton ID="trigger2" runat="server" ImageUrl="~/Layout/Images/help_16x16.ico" />
                            <div id="pop-up2">
                                <h4>Leave Travel Allowance(LTA) </h4>
                                <p>
                                    Currently LTA amount are fixed as per grade. Tax exemption as per prevailing IT rules.
                                </p>
                                <p>
                                    <b>Employees can restructure the amount as per their requirements.
                                    </b>
                                </p>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Child Education Allowance (CEA)</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblCEAExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txtCEA" runat="server" CssClass="numerictextboxlabel" onblur="javascript:IsNumeric(this,event);" Width="92%">0</asp:TextBox>
                        <asp:Label ID="lblCEA" runat="server" CssClass="numerictextboxlabel" Width="92%">0</asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblCEAMaxCeiling" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label></td>
                    <td class="form-fld-lbl">
                        <div id="1235" class="container">
                            <asp:ImageButton ID="trigger3" runat="server" ImageUrl="~/Layout/Images/help_16x16.ico" />
                            <div id="pop-up3">
                                <h4>Children Education Allowance (CEA) </h4>
                                <p>
                                    Rs. 100 per month per child up to a maximum of two children is exempt from tax. To avail tax exemption you will need to submit details/declaration of children, age, school details & receipts for school fees paid. 
                                </p>
                                <p>
                                    <b>Employees can restructure amount from zero to Rs. 200/- per month.
                                    </b>
                                </p>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Child Hostel Allowance (CHA)</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblCHAExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txtCHA" runat="server" CssClass="numerictextboxlabel" onblur="javascript:IsNumeric(this,event);" Width="92%">0</asp:TextBox>
                        <asp:Label ID="lblCHA" runat="server" CssClass="numerictextboxlabel" Width="92%">0</asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblCHAMaxCeiling" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label></td>
                    <td class="form-fld-lbl">
                        <div id="1236" class="container">
                            <asp:ImageButton ID="trigger8" runat="server" ImageUrl="~/Layout/Images/help_16x16.ico" />
                            <div id="pop-up8">
                                <h4>Children Hostel Allowance (CHA) </h4>
                                <p>
                                    Rs. 300 per month per child up to a maximum of two children is exempt from tax. To avail tax exemption you will need to submit details/declaration of children, age, school details & receipts for hostel fees paid.
                                </p>
                                <p>
                                    <b>Employees can restructure amount from zero to Rs. 600/- per month.
                                    </b>
                                </p>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Medical Allowance(MA)</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblMAExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txtMARestruct" runat="server" CssClass="numerictextboxlabel" onblur1="javascript:IsNumeric(this,event);" Width="92%">0</asp:TextBox>
                        <asp:Label ID="lblMA" runat="server" Width="92%" CssClass="numerictextboxlabel">0</asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblMAMaxCeiling" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label></td>
                    <td class="form-fld-lbl">
                        <div id="1237" class="container">
                            <asp:ImageButton ID="trigger4" Visible="false" runat="server" ImageUrl="~/Layout/Images/help_16x16.ico" Style="width: 16px" />
                            <div id="pop-up4">
                                <h4>Medical Allowance (MA) </h4>
                                <p>
                                    Tax exemption can be availed upto a limit of Rs.1250 per month.
                                </p>
                                <p>
                                    <b>Employees can restructure amount from zero to Rs. 1250/- per month.
                                    </b>
                                </p>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr id="trMobile">
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Mobile Reimbursement</td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblMobileExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:TextBox ID="txtMobile" runat="server" CssClass="numerictextboxlabel" onblur="javascript:IsNumeric(this,event);" Width="92%" ToolTip="New component introduced with ceiling of Rs. 2000/- per month. Restructuring allowed with AA only. Restructuring not applicable for M Grades.">0</asp:TextBox>
                        <asp:Label ID="lblMobile" runat="server" CssClass="numerictextboxlabel" Width="92%">0</asp:Label>
                    </td>
                    <td rowspan="2" style="background-repeat: no-repeat; background-image: url('Layout/Images/GroupBImg.png');">
                        <asp:Label ID="lblGroupBTotalLimit" runat="server" Text="0"  CssClass="numerictextboxlabel"  Width="55px"></asp:Label><br />
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblMobileMaxCeiling" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label></td>
                    <td class="form-fld-lbl">
                        <div id="1238" class="container">
                            <asp:ImageButton ID="trigger5" runat="server" ImageUrl="~/Layout/Images/help_16x16.ico" />
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
                                <%--   <p>
                                    Unclaimed amount under Mobile Reimbursement will be paid quarterly along with salary and taxed.
                                 e.g. if employee claims bill of July and August through EMS but forgets to claim September bill. 
                                 Then unclaimed amount of July, August and September will be paid along with October Salary.
                                </p>--%>
                                <p>
                                    <b>Employees can restructure amount from zero to Rs. 2000/- per month.
                                    </b>
                                </p>
                            </div>
                        </div>
                        <a href="Documents/FAQ - Mobile Reimbursement.pdf" style="font-size: 10px;" target="_blank">Mobile FAQ</a>
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">
                        <asp:Label ID="lblAASuperAnuationOption0" class="form-fld-lbl" Text="Additional Allowance(AA)" runat="server"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblAAExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="txtAA" runat="server" CssClass="numerictextboxlabel" Width="92%">0</asp:Label>
                        <asp:Label ID="lblAA" runat="server" Width="92%" CssClass="numerictextboxlabel">0</asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblAAMaxCeiling" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label></td>
                    <td class="form-fld-lbl">
                        <div id="1239" class="container">
                            <asp:ImageButton ID="trigger7" runat="server" ImageUrl="~/Layout/Images/help_16x16.ico" />
                            <div id="pop-up7">
                                <h4>Additional Allowance (AA) </h4>
                                <p>
                                    This amount can be restructured with Mobile Reimbursement , Meal Card,Superannuation and NPS only. AA is completely taxable.
                                    This amount will be automatically calculated based on the restructuring of other components.
                                </p>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Amt. of Meal Card deducted from AA </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="Label3" runat="server" CssClass="numerictextboxlabel" Width="92%">0</asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblMeamCardDeducFrmAA" runat="server" Width="92%" CssClass="numerictextboxlabel">0</asp:Label>
                    </td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Amt. of NPS deducted from AA </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="Label1" runat="server" CssClass="numerictextboxlabel" Width="92%">0</asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblNPSDeducFrmAA" runat="server" Width="92%" CssClass="numerictextboxlabel">0</asp:Label>
                    </td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl"></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <div id="trSuperanuationMerged" style="width: 100%; background-color: #d3d6ed;">
                            <table style="width: 100%">
                                <tr>
                                    <td class="form-fld-lbl" style="width: 5%"></td>
                                    <td class="form-fld-lbl" style="width: 45%">Amt. added from SN to AA
                                        <%--  <asp:Label ID="lblAASuperAnuationOption" class="form-fld-lbl" Text ="Amt. adjusted from SA to AA" runat="server"></asp:Label>--%>
                                    </td>
                                    <td class="form-fld-lbl" style="background-color: white; width: 10%">
                                        <asp:Label ID="lblAAWithSuperannuationExisting" runat="server" Width="92%" CssClass="numerictextboxlabel" Text="0"></asp:Label>
                                    </td>
                                    <td class="form-fld-lbl" style="background-color: white; width: 10.7%">
                                        <asp:Label ID="lblAAWithSuperannuation" Width="92%" runat="server" CssClass="numerictextboxlabel" Text="0"></asp:Label>
                                    </td>
                                    <td class="form-fld-lbl" style="width: 15.3%;">&nbsp;</td>
                                    <td class="form-fld-lbl" style="width: 15.5%;"></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">Revised AA </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="Label7" runat="server" CssClass="numerictextboxlabel" Width="92%">0</asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblAACalculation" runat="server" CssClass="numerictextboxlabel" Text="0" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="7">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl" style="text-align: right;"><strong>Total[A] per month</strong></td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblGroupATotalExisting" runat="server" Width="92%" CssClass="numerictextboxlabel"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: white;">
                        <asp:Label ID="lblGroupATotalRestruc" runat="server" Width="92%" CssClass="numerictextboxlabel"></asp:Label></td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="7">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl" style="font-family: Verdana; font-size: 12px; color: #800000;">[B]</td>
                    <td class="form-fld-lbl" style="font-family: Verdana; font-size: 12px; color: #800000;">Retirement Benefits</td>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">&nbsp;</td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Provident Fund (PF) </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblPFExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblPFRestruct" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl"></td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Gratuity </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblGratuityExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblGratuityRestruct" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">&nbsp;</td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Superannuation (SN) </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblSNExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblSNRestruct" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl">&nbsp;
                    </td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">
                        <div id="12310" class="container">
                            <asp:ImageButton ID="trigger10" runat="server" ImageUrl="~/Layout/Images/help_16x16.ico" Style="width: 16px" />
                            <div id="pop-up10">
                                <h4>Employer’s Contribution to Superannuation (SN)</h4>

                                <p>
                                    Employees having Superannuation in CTC, can choose to the amount to be 15% of Basic OR 10% of Basic
                                    OR 5% of Basic. Employee will get the option to choose % of Basic as Superannuation amount once
                                    in Financial Year. In case employee chooses SN to be 5% or 10% of Basic, the differential amount 
                                    will get adjusted with Additional Allowance (AA). Employees must note that amount diverted in
                                    AA will increase monthly gross salary but at the same time it will be taxable.
                                </p>


                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                        <div id="trSuperAnnuation" style="width: 100%;">
                            <table style="width: 100%; background-color: #e3e3e3;">
                                <tr>
                                    <td style="width: 5%"></td>
                                    <td style="width: 36%" class="form-fld-lbl">Superanuation Option</td>
                                    <td style="background-color: #FFFFFF; width: 35%">
                                        <asp:DropDownList ID="ddlSuperanuation" runat="server" onchange="OnSuperAnnuationChange();" Width="92%">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblEmpSelectedSuperanuationOption" runat="server" Width="92%"></asp:Label>
                                        <asp:HiddenField ID="hdnSuperanution" runat="server" />
                                    </td>
                                    <td style="text-align: left; width: 25%">
                                        <%-- <a href="Documents/Tax Exemption limit for contribution by employer to Superannuation.pdf" style="font-size:11px;" target="_blank">Superannuation Option</a>--%>
                                        <a href="Documents/FAQ-NPS.pdf" style="font-size: 10px;" target="_blank">FAQ NPS</a>
                                        <br />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                        <div id="trNPS" runat="server" visible="false" style="width: 100%; background-color: #d3d6ed;">
                            <table style="width: 100%">
                                <tr>
                                    <td class="form-fld-lbl" style="width: 5%"></td>
                                    <td class="form-fld-lbl" style="width: 38%">Amt. to be contributed from SN to NPS</td>
                                    <td class="form-fld-lbl" style="background-color: white; width: 8.5%">
                                        <asp:Label ID="lblNPSExisting" runat="server" Width="95%" CssClass="numerictextboxlabel"></asp:Label>
                                    </td>
                                    <td class="form-fld-lbl" style="background-color: white; width: 8.5%">
                                        <asp:Label ID="lblNPS" runat="server" Width="95%" CssClass="numerictextboxlabel"></asp:Label>
                                    </td>

                                    <td class="form-fld-lbl" colspan="2" style="text-align: right; width: 28%">
                                        <%--     <a href="Documents/Existing NPS Account Holders - Process.pdf" style="font-size: 10px;" target="_blank">Existing NPS Holder</a><br />
                                        <a href="Documents/NewHoldersProcess.pdf" style="font-size: 10px" target="_blank">For New NPS</a>--%>
                                        <a href="Documents/NPS%20Account%20Opening%20Process.pdf" style="font-size: 10px" target="_blank">NPS account opening process</a>
                                    </td>
                                    <td>
                                        <div id="12314" class="container">
                                            <asp:ImageButton ID="trigger12" runat="server" ImageUrl="~/Layout/Images/help_16x16.ico" />
                                            <div id="pop-up12">
                                                <h4>Employer’s Contribution to National Pension Scheme (NPS) </h4>
                                                <p>
                                                    If employee chooses to invest in NPS, he/she must follow the process given in “TCE.M9-HR-PA-36A : Compensation Restructuring (CompStruct) – User Manual.pdf”. If NPS account details are not submitted as per process, corresponding amount for that month will be added to AA by default. 
                                                </p>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                        <div id="trNPSNew" style="width: 100%; background-color: #d3d6ed;">
                            <table style="width: 100%">
                                <tr>
                                    <td class="form-fld-lbl" style="width: 5%"></td>
                                    <td class="form-fld-lbl" style="width: 38%">Amt. to be contributed in NPS</td>
                                    <td class="form-fld-lbl" style="background-color: white; width: 8.5%">
                                        <asp:Label ID="lblNPSExistingAmt" runat="server" Width="95%" CssClass="numerictextboxlabel"></asp:Label>
                                    </td>
                                    <td class="form-fld-lbl" style="background-color: white; width: 8.5%">
                                        <asp:TextBox ID="txtNPSNewAmt" runat="server" Width="95%" onblur="javascript:IsNumeric(this,event);" Text="0" CssClass="numerictextboxlabel"></asp:TextBox>
                                        <asp:Label ID="lblNPSNewAmt" runat="server" CssClass="numerictextboxlabel" Width="92%">0</asp:Label>
                                        <asp:HiddenField ID="hiddNPSMaxLimit" runat="server" />
                                        <asp:HiddenField ID="hiddNPSMinLimit" runat="server" />
                                    </td>

                                    <td class="form-fld-lbl" colspan="2" style="text-align: right; width: 28%">
                                        <%-- <a href="Documents/Existing NPS Account Holders - Process.pdf" style="font-size: 10px;" target="_blank">Existing NPS Holder</a><br />
                                        <a href="Documents/NewHoldersProcess.pdf" style="font-size: 10px" target="_blank">For New NPS</a>--%>
                                        <a href="Documents/NPS%20Account%20Opening%20Process.pdf" style="font-size: 10px" target="_blank">NPS account opening process</a>

                                    </td>
                                    <td>
                                        <div id="12324" class="container">
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Layout/Images/help_16x16.ico" />
                                            <div id="pop-up13">
                                                <h4>Employer’s Contribution to National Pension Scheme (NPS) </h4>
                                                <p>
                                                    If employee chooses to invest in NPS, he/she must follow the process given in “TCE.M9-HR-PA-36A : Compensation Restructuring (CompStruct) – User Manual.pdf”. If NPS account details are not submitted as per process, corresponding amount for that month will be added to AA by default. 
                                                </p>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                        <asp:HiddenField ID="hdnBalnceSA" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnIsMealVoucherOpted" runat="server" Value="0" />
                        <asp:LinkButton ID="lnkDummy" runat="server" OnClick="lnkDummy_Click"></asp:LinkButton>
                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
                            PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                            <div class="header">
                                Superannuation
                                <asp:Label ID="lblSuperAnnPerc" runat="server"> </asp:Label>%  
                            </div>
                            <div class="body">
                                <table width="100%">
                                    <colgroup>
                                        <col style="width: 68%" />
                                        <col style="width: 2%" />
                                        <col style="width: 30%" />
                                    </colgroup>
                                    <tr>
                                        <td class="form-fld-lbl">Your old SN amount per month Rs.</td>
                                        <td>:</td>
                                        <td class="form-fld-lbl" style="text-align: center;">
                                            <asp:Label ID="lblSAOLD" runat="server" Text="6075"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form-fld-lbl">Revised SN amount per month Rs.</td>
                                        <td>:</td>
                                        <td class="form-fld-lbl" style="text-align: center;">
                                            <asp:Label ID="lblSARevised" runat="server" Text="2025"></asp:Label>
                                        </td>
                                    </tr>
                                    <%--Commented By Mohan On 27 May 2019--%>
                                    <%-- <tr>
                                        <td colspan="3" class="form-fld-lbl" style="background-color: #e3e3e3; color: black">Balance amount to be diverted to NPS or AA :
                                            <asp:Label ID="lblBalance" runat="server">0</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" class="form-fld-lbl" style="background-color: #e3e3e3; color: black"><span style="font-size: 10px" class="Required">Value in NPS can either be zero or more than Rs. 499/- per month</span></td>
                                    </tr>
                                    <tr>
                                        <td class="form-fld-lbl" style="text-align: left">Transfer to NPS Rs.</td>
                                        <td>:</td>
                                        <td class="form-fld-lbl">
                                            <asp:TextBox Width="100px" ID="txtNPSSA" runat="server" class="numerictextboxlabel" onblur="javascript:IsNumeric(this,event);">0</asp:TextBox>
                                        </td>
                                    </tr>--%>
                                    <%--End By Mohan--%>
                                    <tr>
                                        <td class="form-fld-lbl">Transfer to AA Rs.</td>
                                        <td>:</td>
                                        <td class="form-fld-lbl" style="text-align: center;">
                                            <asp:Label ID="txtAASA" runat="server">0</asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <asp:Button ID="btnApplySAChanges" runat="server" Text="Apply & Submit" BorderStyle="None" BackColor="Maroon" ForeColor="White" Font-Bold="true" Height="20px" OnClick="btnApplySAChanges_Click" />
                                <asp:Button ID="btnHide" runat="server" Text="Close" BorderStyle="None" BackColor="Maroon" ForeColor="White" Font-Bold="true" Height="20px" OnClientClick="return HideModalPopup()" />
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl" style="text-align: right;"><strong>Total[B] per month</strong></td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblGroupBTotalExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblGroupBTotalRestruc" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label></td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="7">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl" style="font-family: Verdana; font-size: 12px; color: #800000;">[C]</td>
                    <td class="form-fld-lbl" style="font-family: Verdana; font-size: 12px; color: #800000;">Other Allowances/ Benefits</td>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">&nbsp;</td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Site Allowance (SA)</td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblSiteAllowaExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblSiteAllowaRestruct" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                </tr>
                <tr>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">City Compensatory Allowance(CCA)</td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblCCAExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblCCARestruct" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>

                    <td class="form-fld-lbl">&nbsp;</td>

                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">ESIC </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblESICExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label><!-- All existing bonus will be treated as Bonus-->
                    </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblESICRestruct" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl"></td>

                    <td class="form-fld-lbl">&nbsp;</td>

                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Bonus </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblBonusExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label><!-- All existing bonus will be treated as Bonus-->
                    </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblBonusRestruct" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl"></td>

                    <td class="form-fld-lbl">&nbsp;</td>

                </tr>

                <tr>
                    <td colspan="7">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl" style="text-align: right;"><strong>Total[C] per month</strong></td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblGroupCTotalExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblGroupCTotalRestruc" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label></td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="7">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl" style="font-family: Verdana; font-size: 12px; color: #800000;">[D]</td>
                    <td class="form-fld-lbl" style="font-family: Verdana; font-size: 12px; color: #800000;">Indirect Benefits </td>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">&nbsp;</td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Meal Card Options (Yes/No)</td>
                    <td class="form-fld-lbl" colspan="4" style="background-color: #dee0ef">
                        <asp:RadioButtonList ID="rdoGetMealVoucher" class="form-fld-lbl" runat="server" OnSelectedIndexChanged="rdoGetMealVoucher_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Value="2200">Yes I want to avail Meal Card of Rs. 2200</asp:ListItem>
                            <asp:ListItem Value="1100">Yes I want to avail Meal Card of Rs. 1100</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">No I do not want to avail Meal Card</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:Label ID="lblGetMealVoucher" runat="server"></asp:Label>
                    </td>
                    <%-- <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>--%>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl" style="vertical-align: middle;">Meal Card</td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblMealCardExisting" runat="server" CssClass="numerictextboxlabel" Width="92%">0</asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblMealCardRestruc" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">
                        <div id="12315" class="container">
                            <asp:ImageButton ID="trigger9" runat="server" ImageUrl="~/Layout/Images/help_16x16.ico" />
                            <div id="pop-up9">
                                <h4>Meal card</h4>
                                <p>
                                    Employees have a choice to opt for electronic meal card for availing tax benefits.
                                </p>
                                <p>This option will be given once in the beginning of the financial year. Once employee chooses to opt for meal card, it will be applicable for the rest of Financial Year.</p>
                                <p>Employees opting for electronic meal card can choose the meal card amount to be –</p>
                                <p>•	Rs. 1100/- per month</p>
                                <p>•	Rs. 2200/- per month</p>
                                <p>Accordingly above amount will be carved out from AA and loaded in the meal card. This amount is exempted from tax as per current IT rules.</p>
                                <p>The meal card will be reloaded at the beginning of every month.</p>
                                <p>The electronic meal cards can be used in the company provided canteen facility or select restaurants.</p>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr id="trMedPre" runat="server">
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Medical Premium</td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblMediPreExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label><!-- All existing bonus will be treated as Bonus-->
                    </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblMediPreNew" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl"></td>

                    <td class="form-fld-lbl">&nbsp;</td>

                </tr>
                <tr>
                    <td colspan="7">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="form-fld-lbl" style="text-align: right"><b>Computed Fixed Pay Total [A+B+C+D] Per Month (Rs.)</b></td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblTotalABCDExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblTotalABCDRestruc" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label></td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="7">
                        <hr />
                    </td>
                </tr>
                <tr style="background-color: #FFEFD5">

                    <td colspan="2" class="form-fld-lbl" style="text-align: right;"><strong>Computed Fixed Pay Per Annum (Rs.)</strong></td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblComputedFixedExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblComputedFixedRestructur" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label></td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="7">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="form-fld-lbl" style="font-family: Verdana; font-size: 12px; color: #800000;">[E]</td>
                    <td class="form-fld-lbl" style="font-family: Verdana; font-size: 12px; color: #800000;">Performance Linked Variable Pay</td>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">&nbsp;</td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Individual Component (IC) per annum </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblICExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblICRestruc" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label></td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                </tr>
                <tr>
                    <td class="form-fld-lbl"></td>
                    <td class="form-fld-lbl">Company Component (CC) per annum </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblCCExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblCCRestruc" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label></td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="7">
                        <hr />
                    </td>
                </tr>
                <tr style="background-color: #FFEFD5">

                    <td colspan="2" class="form-fld-lbl" style="text-align: right"><strong>Total Variable Pay per annum (Rs.)</strong></td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblTotalEExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblTotalERestructur" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label></td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="7">
                        <hr />
                    </td>
                </tr>
                <tr style="background-color: #FFEFD5">
                    <td colspan="2" class="form-fld-lbl" style="text-align: right"><strong>Computed CTC (Fixed +Variable Pay) Per Annum (Rs.)</strong></td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblComputedCTCExisting" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label>
                    </td>
                    <td class="form-fld-lbl" style="background-color: #FFFFFF">
                        <asp:Label ID="lblComputedCTCRestruct" runat="server" CssClass="numerictextboxlabel" Width="92%"></asp:Label></td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td class="form-fld-lbl">&nbsp;</td>
                    <td>
                        <asp:Label Text="2000" Style="display: none;" ID="lblMobileCeilingForEmp" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                        <table style="width: 100%">
                            <tr>
                                <td style="vertical-align: top; background-color: #dee0ef; font-family: Verdana; padding: 10px; font-size: 12px; background-repeat: no-repeat; width: 33%">
                                    <hr />
                                    <span class="form-fld-lbl" style="color: maroon; font-family: Verdana; font-size: 12px">Notes:</span>
                                    <ol type="1">
                                        <ol type="1">
                                            <li>Only HRA, PF, Gratuity and Superannuation (applicable to E6/ A6 and above grades) are linked to the Basic Salary.</li>
                                            <li>Company reserves the right, at its discretion, to change the structure of compensation and the allowance and/or its components within your CTC, at any point of time during the term of employment, in view of any future change in the statutory rules and regulations including the Code on Wages etc. Additional Allowance is an adjusting factor in the remuneration package and may vary due to any restructuring/ revision of allowances/ benefits.</li>
                                            <li>Employees having Performance Bonus (Individual Component of Variable Pay) amount in CTC is inclusive of Bonus, as applicable under Payment of Bonus Act, 1965 and amendments thereof. For others, it is shown separately where annual amount converted into monthly amount for computing monthly pay. It is annually payable on pro-rata basis.</li>
                                            <li>Company’s contribution towards premium for Mediclaim/ Health Insurance Scheme/ Term Life/ Personal Accident coverage is over and above the Computed CTC shown above.
                                            </li>
                                            <li>Tax liability, if any, on all the above payments shall be borne by employee.</li>
                                        </ol>
                                    </ol>
                                </td>
                            </tr>
                        </table>
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
        <asp:HiddenField ID="hdnSuperAnnuationLimit" runat="server" />
        <asp:HiddenField ID="hdnSuperAnnuationUpperLimit" runat="server" />
        <asp:HiddenField ID="hdnFinancialYearPrev" runat="server" />
        <asp:HiddenField ID="hdnFinancialYearCurr" runat="server" />
        <%--Added By Mohan On 29 May 2019--%>
        <asp:HiddenField ID="hdnIsNewEmployee" runat="server" />
        <asp:HiddenField ID="hdnIsSAApplicable" runat="server" />
        <%--End By Mohan--%>
        <asp:Button ID="hdnbtnForRevoke" runat="server" Style="display: none" OnClientClick1="showProgress()" OnClick="hdnbtnForRevoke_Click" CausesValidation="false" />
    </form>
</body>
</html>
