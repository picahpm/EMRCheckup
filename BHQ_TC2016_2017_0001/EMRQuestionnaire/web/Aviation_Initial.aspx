<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Aviation_Initial.aspx.cs" Inherits="EMRQuestionnaire.web.Aviation_Initial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<link href="/css/FontsThai/fontsthaisans_neueregular.css" rel="stylesheet" type="text/css" />
<link href="/css/maincss.css" rel="stylesheet" type="text/css" />
<link rel="shortcut icon" href="../images/quiz-games-300x300.png" />

<link href="/datePicker/jquery-ui.css" rel="stylesheet" type="text/css" />

<script src="/datePicker/external/jquery/jquery.js" type="text/javascript"></script>
<script src="/datePicker/jquery-ui.js" type="text/javascript"></script>

<script src="/scripts/aviation_initial.js" type="text/javascript"></script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="headAviationInitial" runat="server">
    <title>Application for Medial Certificate</title>
    <script type="text/javascript" language="javascript">
        function rdLicenseType_CheckedChanged(clic) {

            if (clic == "rdoOther") {
                document.getElementById('txtOtherLicenseType').disabled = false;
            }
            else {

                document.getElementById('txtOtherLicenseType').disabled = true;
                document.getElementById('txtOtherLicenseType').value = "";
            }
        }
        function rdSmoking_CheckedChanged(clic) {
            if (clic == "rdoSmokeNever") {
                document.getElementById('txtSmokeStopSince').disabled = true;
                document.getElementById('txtSmokeStopSince').value = "";
                document.getElementById('txtSmokeTobaccoType').disabled = true;
                document.getElementById('txtSmokeTobaccoType').value = "";
                document.getElementById('txtSmokeAmount').disabled = true;
                document.getElementById('txtSmokeAmount').value = "";
            }
            else if (clic == "rdoSmokeStopSince") {
                document.getElementById('txtSmokeStopSince').disabled = false;
                document.getElementById('txtSmokeTobaccoType').disabled = true;
                document.getElementById('txtSmokeTobaccoType').value = "";
                document.getElementById('txtSmokeAmount').disabled = true;
                document.getElementById('txtSmokeAmount').value = "";
            }
            else if (clic == "rdoSmokeYes") {
                document.getElementById('txtSmokeStopSince').disabled = true;
                document.getElementById('txtSmokeStopSince').value = "";
                document.getElementById('txtSmokeTobaccoType').disabled = false;
                document.getElementById('txtSmokeAmount').disabled = false;
            }
        }
        function chkAircraftType_CheckedChanged(clic) {

            if (clic == "chkOther") {
                document.getElementById('txtOtherAircraft').disabled = false;
            }
            else {

                document.getElementById('txtOtherAircraft').disabled = true;
                document.getElementById('txtOtherAircraft').value = "";
            }
        }
        function rdMedication_CheckedChanged(clic) {

            if (clic == "rdMedicationYes") {
                document.getElementById('txtMedicationIfYes').disabled = false;
                document.getElementById('txtMedicationQuantity').disabled = false;
                document.getElementById('txtMedicationReason').disabled = false;
                document.getElementById('txtMedicationReason').disabled = false;
            }
            else {

                document.getElementById('txtMedicationIfYes').disabled = true;
                document.getElementById('txtMedicationQuantity').disabled = true;
                document.getElementById('txtMedicationStartDate').disabled = true;
                document.getElementById('txtMedicationReason').disabled = true;

                document.getElementById('txtMedicationIfYes').value = "";
                document.getElementById('txtMedicationQuantity').value = "";
                document.getElementById('txtMedicationStartDate').value = "";
                document.getElementById('txtMedicationReason').value = "";
            }
        }
    </script>    
</head>
<body>
    <table border ="0" cellpadding ="0" cellspacing="0" width="100%"  > <tr> 
        <td  align ="center">
        <form id="general" runat="server">
        <div id="Content" class="Content">
            <div id="dash" class="header">
                <div id="left" style="float:left;"> 
                    <img src="../images/header_logo.jpg" />  
                </div>
                <div id="right" 
                    style="float:right;width:50%; height:22px; vertical-align:bottom;" 
                    align="right">
                    &nbsp;                      
                </div>
                <div id="Div1" 
                    style="float:right;width:50%; height:30px; vertical-align:bottom;" 
                    align="right">
                    <span style="vertical-align:bottom;"> 
                    <asp:ImageButton ID="imgThai" 
                        runat="server" ImageUrl="~/images/Thailand.png"  
                        Height="36px" onclick="imgThai_Click" />
                    <asp:ImageButton ID="imgEnglish" runat="server" 
                        ImageUrl="~/images/United-Kingdom.png"
                        Height="36px" onclick="imgEnglish_Click"/></span>
                </div> 
            </div>
            <div id="information">
                <div id="Personal" class="divBarMainTopic" >
                    <asp:Label ID="Lblperson" runat="server" Text=""></asp:Label>
                </div>
                <div id="divLoadData" style="float: right; margin-top: -20px;">
                    <asp:Button ID="btnLoadData" runat="server" Text="Load Data" Width="100" Height="20"
                        class="buttonRec" onclick="btnLoadData_Click" />
                </div>
                <br />
                <div id="divGeneralInfo" class="divBarGroup">
                    General Informational
                </div>
                <table id="Patient">
                    <tr>
                        <td  style="width:50%" class="trPath">
                            <table  border ="0" cellpadding ="0" cellspacing="0"><tr>
                                <td style="width:150px"><asp:Label ID="lblName" runat="server"></asp:Label></td>
                                <td><asp:TextBox ID="txtName" runat="server" class="inputbox" Width="270" Enabled="false"></asp:TextBox>  
                                    <asp:Label ID="lblPhysician" runat="server" Visible="False"></asp:Label><asp:TextBox ID="txtRoom" runat="server" class="inputbox" Visible="False" Width="35px"></asp:TextBox>
                                    <asp:Label ID="lblRoom" runat="server" Visible="False"></asp:Label><asp:TextBox ID="txtDepartment" runat="server" class="inputbox" Visible="False" Width="34px"></asp:TextBox>
                                    <asp:Label ID="lblDepartment" runat="server" Visible="False"></asp:Label><asp:TextBox ID="txtPhysician" runat="server" class="inputbox" Width="44px" Visible="False"></asp:TextBox>
                                </td>
                            </tr></table>
                       
                        </td>
                        <td  style="width:50%">
                        <table><tr>
                            <td style="width:150px" class="trPath">
                                <asp:Label ID="lblBirthDate" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="left"><asp:TextBox ID="txtBirthDate" runat="server" class="inputbox"></asp:TextBox>
                                <asp:Label ID="lblAllergies" runat="server" Visible="False"></asp:Label><asp:TextBox ID="txtAllergies" runat="server" class="inputbox" Width="64px" Enabled="false" Visible="False"></asp:TextBox>                      
                   
                            </td>
                        </tr></table>                        
                        </td>
                    </tr>
                    <tr>
                        <td class="trPath">
                            <table  border ="0" cellpadding ="0" cellspacing="0"><tr>
                            <td style="width:150px">
                                <asp:Label ID="lblHN" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHN" runat="server" class="inputbox" Enabled="false"></asp:TextBox></td>
                            </tr></table>
                        </td>
                        <td class="trPath">
                            <table  border ="0" cellpadding ="0" cellspacing="0"><tr>
                            <td style="width:150px">
                                <asp:Label ID="lblAge" runat="server"></asp:Label>
                            </td><td>
                                <asp:TextBox ID="txtAge" runat="server" class="inputbox"></asp:TextBox></td>
                            </tr></table>
                        </td>
                    </tr>
                    <tr>
                        <td class="trPath">
                            <table  border ="0" cellpadding ="0" cellspacing="0"><tr>
                            <td style="width:150px">
                                <asp:Label ID="lblVisitDate" runat="server" Text=""></asp:Label>
                            </td><td>
                                <asp:TextBox ID="txtVisitDate" runat="server" class="inputbox"></asp:TextBox></td>
                            </tr></table>
                        </td>
                        <td class="trPath">
                            <table  border ="0" cellpadding ="0" cellspacing="0">
                            <tr>
                                <td style="width:150px">
                                    <asp:Label ID="lblSex" runat="server" Text=""></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtSex" runat="server" class="inputbox" AutoCompleteType="Gender"></asp:TextBox>
                                </td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div id="divPatientInformation">
                <div id="groupPatientInformation" class="divBarGroup">
                    <asp:Label ID="lblRecommendation" runat="server"></asp:Label>
                </div>  
                <table id="PatientHistory" >
                    <tr>
                        <td style="width:100%; padding-left:10px;" align="left">
                            <asp:Label ID="lblRecommendationRule" runat="server"></asp:Label>                            
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%" align="center">
                            <table id="PatientInfo" width="100%" cellspacing="0" >
                                <tr><td style="width:100%;"><table id="Place" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width:300px;" align="left" valign="top" >
                                            <asp:Label ID="lblPlace" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="left" valign="middle" colspan="4">
                                            <asp:TextBox ID="txtplace" runat="server" Width="580" class="inputbox"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;"><table id="Name" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width:300px;" align="left" valign="middle">
                                            <asp:Label ID="lblName_th" runat="server"></asp:Label>
                                        </td>                                    
                                        <td style="width:auto;" align="left" valign="middle">
                                            <asp:TextBox ID="txtname_th" runat="server" Width="200" class="inputbox"></asp:TextBox>
                                        </td>
                                        <td style="width:auto;" align="right" valign="middle">
                                            <asp:Label ID="lblNationality_th" runat="server"></asp:Label>&nbsp;
                                        </td>
                                        <td style="width:auto;" align="left" valign="middle">
                                            <asp:TextBox ID="txtnation_th" runat="server" Width="200" class="inputbox"></asp:TextBox>
                                        </td>
                                        <td style="width:auto;" align="left" valign="top" >
                                            <asp:Panel ID="plGender" runat="server" Width="200" Font-Size="Small" BorderStyle="Solid" BorderColor="#0099FF" BorderWidth="1">
                                               &nbsp;&nbsp;<asp:Label ID="lblGender" runat="server" Text=""></asp:Label>
                                                <br />
                                                &nbsp;
                                                <asp:RadioButton ID="rdGender_M" runat="server" GroupName="Gender" />
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="rdGender_F" runat="server" GroupName="Gender" />
                                            </asp:Panel>
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td align="left" valign="top" style="width:300px;">
                                            <asp:Label ID="lblDOB_th" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle" >
                                            <asp:TextBox ID="txtDOB" runat="server" Width="200" class="inputbox"></asp:TextBox>
                                        </td>
                                        <td align="right" valign="middle">
                                            <asp:Label ID="lblAge_th" runat="server"></asp:Label>&nbsp;
                                        </td>
                                        <td align="left" valign="middle" colspan="2">
                                            <asp:TextBox ID="txtAgeYear" runat="server" Width="100" class="inputbox"></asp:TextBox>&nbsp;<asp:Label 
                                                ID="lblAgeYear_th" runat="server"></asp:Label><asp:TextBox ID="txtAgeMonth" runat="server" Width="100" class="inputbox"></asp:TextBox>&nbsp;<asp:Label 
                                                ID="lblAgeMonth_th" runat="server"></asp:Label></td>                                   
                                    </tr>                                    
                                    <tr>
                                        <td align="left" valign="top" style="width:300px;">
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblMarital" runat="server"></asp:Label></td>
                                        <td style="width:auto;" align="left" valign="top" colspan="4">
                                            <asp:Panel ID="plMarital" runat="server" Width="100%" Font-Size="Small">
                                                <asp:RadioButton ID="rdMarital_S" runat="server" GroupName="Marital"/>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="rdMarital_M" runat="server" GroupName="Marital"/>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="rdMarital_D" runat="server" GroupName="Marital"/>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="rdMarital_P" runat="server" GroupName="Marital"/>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="rdMarital_W" runat="server" GroupName="Marital"/>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;"><table id="License" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="left" valign="top" style="width:300px;" rowspan="2">
                                        <asp:Label ID="lblTOL" runat="server"></asp:Label>
                                    </td>
                                    <td style="width:auto;" align="left" valign="top" >
                                        <asp:Panel ID="plTOL" runat="server" Width="100%" Font-Size="Small">
                                            <asp:RadioButton ID="rdTOL_N" runat="server" GroupName="TOL"/>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rdTOL_R" runat="server" GroupName="TOL"/>
                                        </asp:Panel>
                                    </td>                                    
                                </tr>
                                <tr><td style="width:auto;" ><table id="LicenseType" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width:30%; padding-left:10px;" align="left" valign="top">
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rdoATPL" runat="server" GroupName="LicenseType" 
                                                Font-Size="Small" onclick="rdLicenseType_CheckedChanged(this.id);"/>
                                        </td>
                                        <td style="width:30%;" align="left" valign="top" >
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rdoPPL" runat="server" GroupName="LicenseType" 
                                                Font-Size="Small" onclick="rdLicenseType_CheckedChanged(this.id);"/>
                                        </td>
                                        <td style="width:40%;" align="left" valign="top" >
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rdoATC" runat="server" GroupName="LicenseType" 
                                                Font-Size="Small" onclick="rdLicenseType_CheckedChanged(this.id);"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:30%; padding-left:10px;" align="left" valign="top" >
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rdoCPL" runat="server" GroupName="LicenseType" 
                                                Font-Size="Small" onclick="rdLicenseType_CheckedChanged(this.id);"/>
                                        </td>
                                        <td style="width:30%;" align="left" valign="top" >
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rdoPE" runat="server" GroupName="LicenseType" 
                                                Font-Size="Small" onclick="rdLicenseType_CheckedChanged(this.id);"/>
                                        </td>
                                        <td style="width:40%;" align="left" valign="top" >
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rdoStudent" runat="server" GroupName="LicenseType" 
                                                Font-Size="Small" onclick="rdLicenseType_CheckedChanged(this.id);"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:100%; padding-left:10px;" align="left" valign="top" colspan="3">
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rdoOther" runat="server" GroupName="LicenseType" 
                                                Font-Size="Small" onclick="rdLicenseType_CheckedChanged(this.id);"/>
                                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtOtherLicenseType" runat="server" Width="550" class="inputbox"></asp:TextBox></td>
                                    </tr>
                                    </table></td></tr>                              
                                <tr>
                                    <td align="left" valign="top" style="width:300px;">
                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblLicenseNo" runat="server"></asp:Label></td>
                                    <td style="width:auto;" align="left" valign="top">
                                        <asp:TextBox ID="txtLicenseNo" runat="server" Width="350px" class="inputbox"></asp:TextBox>
                                    </td>
                                </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;"><table id="Address" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top" style="width:300px;">
                                            <asp:Label ID="lblAddress_th" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtAddress_th" runat="server" Width="350px" class="inputbox" 
                                                Height="45px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td align="right" valign="top" >
                                            <asp:Label ID="lblOccupation_th" runat="server"></asp:Label>&nbsp;
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtOccupation_th" runat="server" Width="200px" 
                                                class="inputbox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" style="width:300px;">
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTel_th" runat="server"></asp:Label></td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtTel_th" runat="server" Width="250px" class="inputbox"></asp:TextBox>
                                        </td>
                                        <td align="right" valign="top" >
                                            <asp:Label ID="lblCompany_th" runat="server"></asp:Label>&nbsp;
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtCompany_th" runat="server" Width="200px" class="inputbox"></asp:TextBox>
                                        </td>
                                    </tr>                                    
                                </table></td></tr>
                                <tr><td style="width:100%;"><table id="CompanyAddress" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top" style="width:300px;">
                                            <asp:Label ID="lblComAddress_th" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtComAddress_th" runat="server" Width="350px" class="inputbox" 
                                                Height="45px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblContactAdd" runat="server"></asp:Label><br /><br />
                                            <asp:Panel ID="plContactAdd" runat="server" Width="100%" Font-Size="Small"> 
                                                &nbsp;
                                                <asp:RadioButton ID="rdContactAdd_R" runat="server" GroupName="Contact" />
                                                &nbsp;&nbsp;
                                                <asp:RadioButton ID="rdContactAdd_C" runat="server" GroupName="Contact" />
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="bottom" style="width:300px;">
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblComTel_th" runat="server"></asp:Label></td>
                                        <td align="left" valign="bottom">
                                            <asp:TextBox ID="txtComTel_th" runat="server" Width="250px" class="inputbox"></asp:TextBox>
                                        </td>  
                                        <td align="left" valign="top"><table id="ContactPerson" width="100%" 
                                                cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left" valign="top">
                                                    <asp:Label ID="lblContactPerson" runat="server"></asp:Label><br />
                                                    <asp:TextBox ID="txtContactPerson" runat="server" Width="300px" 
                                                        class="inputbox"></asp:TextBox><br /></td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top">
                                                    <asp:Label ID="lblContactTel" runat="server"></asp:Label>
                                                    &nbsp;<asp:TextBox ID="txtContactTel" runat="server" Width="225px" 
                                                        class="inputbox"></asp:TextBox></td>
                                            </tr>
                                        </table></td>
                                    </tr>                                    
                                </table></td></tr>
                                <tr><td style="width:100%;"><table id="HaveBeenExam" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top" colspan="6">
                                            <asp:Label ID="lblHaveBeenExam" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="bottom" style="width:230px;">
                                            
                                        </td>
                                        <td align="left" valign="bottom">                                            
                                            <asp:Panel ID="plHaveBeenExam" runat="server" Width="100%" Font-Size="Small">  
                                                <asp:RadioButton ID="rdHaveBeenExam_No" runat="server" GroupName="HaveBeenExam"/><br />
                                                <asp:RadioButton ID="rdHaveBeenExam_Yes" runat="server" GroupName="HaveBeenExam"/>
                                            </asp:Panel>
                                        </td>
                                        <td align="right" valign="bottom">
                                            <asp:Label ID="lblIfYesExam" runat="server" Font-Size="Small"></asp:Label>&nbsp;
                                        </td>
                                        <td align="left" valign="bottom">
                                            <asp:TextBox ID="txtExamIfYes" runat="server" Width="200px" class="inputbox"></asp:TextBox>
                                        </td>
                                        <td align="right" valign="bottom">
                                            <asp:Label ID="lblDateHaveExam" runat="server" Font-Size="Small"></asp:Label>&nbsp;
                                        </td>
                                        <td align="left" valign="bottom">
                                            <asp:TextBox ID="txtDateHaveExam" runat="server" Width="100px" class="inputbox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle" style="width:230px;">
                                            &nbsp;&nbsp;&nbsp;<asp:Label ID="lblWereYouDeclared" runat="server"></asp:Label></td>                                        
                                        <td align="left" valign="middle" colspan="5">                                            
                                            <asp:Panel ID="plWereYouDeclared" runat="server" Width="100%" Font-Size="Small"> 
                                                <asp:RadioButton ID="rdWereYouDeclared_F" runat="server" GroupName="WereYouDeclared"/>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="rdWereYouDeclared_U" runat="server" GroupName="WereYouDeclared"/>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;"><table id="HasMedical" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top" colspan="4">
                                            <asp:Label ID="lblHasMedical" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="bottom" style="width:230px;">
                                            
                                        </td>
                                        <td align="left" valign="top">                                            
                                            <asp:Panel ID="plHasMedical" runat="server" Width="100%" Font-Size="Small"> 
                                                <asp:RadioButton ID="rdHasMedical_No" runat="server" GroupName="HasMedical"/>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="rdHasMedical_Yes" runat="server" GroupName="HasMedical"/>
                                            </asp:Panel>
                                        </td> 
                                        <td align="right" valign="middle">
                                            <asp:Label ID="lblIfYesMedical" runat="server" Font-Size="Small"></asp:Label>&nbsp;
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="txtIfYesMedical" runat="server" Width="200px" class="inputbox"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;"><table id="FlyTime" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="bottom" style="width:230px;">
                                            <asp:Label ID="lblFlyTime" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="left" valign="bottom">
                                            <asp:Label ID="lblForceInputNum1" Font-Size="Small" ForeColor="Red" 
                                                runat="server"></asp:Label>
                                            <br /><asp:TextBox ID="txtFlyTime" runat="server" Width="200px" class="inputbox"></asp:TextBox>
                                        </td>
                                        <td style="width:auto;" align="right" valign="bottom" >
                                            <asp:Label ID="lblLast6Month" runat="server"></asp:Label>&nbsp;
                                        </td>
                                        <td style="width:auto;" align="left" valign="bottom">
                                            <asp:Label ID="lblForceInputNum2" Font-Size="Small" ForeColor="Red" 
                                                runat="server"></asp:Label>
                                            <br /><asp:TextBox ID="txtLast6Month" runat="server" Width="200px" class="inputbox"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;"><table id="AircraftPresently" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top" style="width:350px;" rowspan="2">
                                            <asp:Label ID="lblAircraftPresently" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="left" valign="top">
                                            <asp:TextBox ID="txtAircraftName" runat="server" Width="655px" 
                                                class="inputbox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr><td style="width:auto;" ><table id="AircraftPresentlyItem" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="width:30%;" align="left" valign="middle">
                                                <asp:CheckBox ID="chkJet" runat="server" Font-Size="Small" onclick="chkAircraftType_CheckedChanged(this.id);"/>
                                            </td>
                                            <td style="width:30%;" align="left" valign="middle" >
                                                <asp:CheckBox ID="chkTurboProp" runat="server" Font-Size="Small" onclick="chkAircraftType_CheckedChanged(this.id);"/>
                                            </td>
                                            <td style="width:40%;" align="left" valign="middle" >
                                                <asp:CheckBox ID="chkHelicopter" runat="server" Font-Size="Small" onclick="chkAircraftType_CheckedChanged(this.id);"/>                                            
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:30%;" align="left" valign="middle">
                                                <asp:CheckBox ID="chkPiston" runat="server" Font-Size="Small" onclick="chkAircraftType_CheckedChanged(this.id);"/>
                                            </td>
                                            <td align="left" valign="middle" colspan="2">
                                                <asp:CheckBox ID="chkOther" runat="server" Font-Size="Small" onclick="chkAircraftType_CheckedChanged(this.id);"/>
                                                &nbsp;<asp:TextBox ID="txtOtherAircraft" runat="server" Width="355px" 
                                                    class="inputbox"></asp:TextBox></td>                                                                                                
                                        </tr>
                                    </table></td></tr>
                                    <tr>
                                        <td align="left" valign="bottom" style="width:350px;">
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPresentlyFlyingStatus" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:auto;" ><table id="PresentlyFlyingStatus" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width:30%;" align="left" valign="top">
                                                    <asp:CheckBox ID="chkSinglePilot" runat="server" Font-Size="Small"/>
                                                </td>
                                                <td style="width:auto;" align="left" valign="top" >
                                                    <asp:CheckBox ID="chkMultiPilot" runat="server" Font-Size="Small"/>
                                                </td>
                                            </tr>
                                        </table></td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="Smoking" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top" style="width:350px;" rowspan="2">
                                            <asp:Label ID="lblSmoke" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="left" valign="middle">
                                            <asp:RadioButton ID="rdoSmokeNever" runat="server" GroupName="SmokeTobacco" 
                                                Font-Size="Small" onclick="rdSmoking_CheckedChanged(this.id);"/>
                                        </td>
                                        <td style="width:auto;" align="left" valign="middle">
                                             <asp:RadioButton ID="rdoSmokeStopSince" runat="server" GroupName="SmokeTobacco" 
                                                Font-Size="Small" onclick="rdSmoking_CheckedChanged(this.id);"/>
                                        </td>
                                        <td style="width:auto;" align="left" valign="middle" colspan="3">
                                            
                                            <asp:TextBox ID="txtSmokeStopSince" runat="server" Width="255px" 
                                                class="inputbox"></asp:TextBox></td>  
                                    </tr>   
                                    <tr>
                                        <td style="width:auto;" align="left" valign="middle">
                                            <asp:RadioButton ID="rdoSmokeYes" runat="server" GroupName="SmokeTobacco" 
                                                Font-Size="Small" onclick="rdSmoking_CheckedChanged(this.id);"/>
                                        </td>
                                        <td style="width:auto;" align="right" valign="middle" >
                                            <asp:Label ID="lblSmokeTobaccoType" runat="server" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td style="width:auto; padding-left:5px;" align="left" valign="top">
                                            <asp:TextBox ID="txtSmokeTobaccoType" runat="server" Width="200px" class="inputbox"></asp:TextBox>
                                        </td>
                                        <td style="width:auto;" align="left" valign="middle">
                                            <asp:Label ID="lblSmokeAmount" runat="server" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td style="width:auto; padding-left:5px;" align="left" valign="top">
                                            <asp:TextBox ID="txtSmokeAmount" runat="server" Width="100px" class="inputbox"></asp:TextBox>
                                        </td>
                                    </tr>                                 
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="CurrentMedication" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top" style="width:350px;" rowspan="3">
                                            <asp:Label ID="lblCurrentlyMedication" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="left" valign="middle" colspan="7">
                                            <asp:RadioButton ID="rdMedicationNo" runat="server" 
                                                GroupName="CurrentlyMedication" Font-Size="Small" onclick="rdMedication_CheckedChanged(this.id);"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:auto;" align="left" valign="middle">
                                            <asp:RadioButton ID="rdMedicationYes" runat="server" 
                                                GroupName="CurrentlyMedication" Font-Size="Small" onclick="rdMedication_CheckedChanged(this.id);"/>
                                        </td>
                                        <td align="left" valign="top" style="padding-left:10px;" colspan="6">
                                            <asp:Label ID="lblMedicationIfYes" runat="server" Font-Size="Small"></asp:Label>
                                            &nbsp;<asp:TextBox ID="txtMedicationIfYes" runat="server" Width="200px" 
                                                class="inputbox"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="middle" colspan="2">
                                            <asp:Label ID="lblMedicationQuantity" runat="server" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td style="width:auto; padding-left:5px;" align="left" valign="top">
                                            <asp:TextBox ID="txtMedicationQuantity" runat="server" Width="50px" 
                                                class="inputbox"></asp:TextBox>
                                        </td>
                                        <td align="right" valign="middle">
                                            <asp:Label ID="lblMedicationStartDate" runat="server" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td style="width:auto; padding-left:5px;" align="left" valign="top">
                                            <asp:TextBox ID="txtMedicationStartDate" runat="server" Width="70px" class="inputbox"></asp:TextBox>
                                        </td>
                                        <td align="right" valign="middle">
                                            <asp:Label ID="lblMedicationReason" runat="server" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td style="width:auto; padding-left:5px;" align="left" valign="top">
                                            <asp:TextBox ID="txtMedicationReason" runat="server" Width="130px" class="inputbox"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table></td></tr> 
                                <tr><td style="width:100%;" ><table id="AlcohalState" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="middle" style="width:350px">
                                            <asp:Label ID="lblAlcohal" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="left" valign="middle">
                                            <asp:TextBox ID="txtAlcohal" runat="server" Width="400px" class="inputbox"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table></td></tr>  
                                <tr><td style="width:100%;" ><table id="Exercise" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="lblExercise" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="middle">
                                            <asp:Panel ID="plExercise" runat="server" Width="100%" Font-Size="Small">                                            
                                                <asp:RadioButton ID="rdExercise_No" runat="server" Width="120px"
                                                    GroupName="Exercise"/>                                                
                                                <asp:RadioButton ID="rdExercise_Yes" runat="server" Width="120px"
                                                    GroupName="Exercise"/>                                                
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>                                                             
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%;" align="left">
                            <div id="divMedicalHistory" class="divBarGroup">
                                <asp:Label ID="lblMedicalHistory" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%; padding-left:10px;" align="left">                            
                            <asp:Label ID="lblMedicalHistoryInfo" runat="server"></asp:Label>                            
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%" align="center">
                            <table id="MedicalHistory" width="100%" cellspacing="0" border="1">
                                <tr><td style="width:100%;" ><table id="MainQuestion17" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion17" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion17" runat="server" Width="100%" Font-Size="Small">                                            
                                                <asp:RadioButton ID="rdMainQuestion17_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion17"/>
                                                <asp:RadioButton ID="rdMainQuestion17_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion17"/>
                                                <div id="divQuestion17Yes">
                                                <asp:TextBox ID="txtQuestion17Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion18" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion18" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion18" runat="server" Width="100%" Font-Size="Small">                                            
                                                <asp:RadioButton ID="rdMainQuestion18_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion18"/>                                                
                                                <asp:RadioButton ID="rdMainQuestion18_yes" runat="server" Width="120px" 
                                                    GroupName="MainQuestion18"/>
                                                <div id="divQuestion18Yes">
                                                <asp:TextBox ID="txtQuestion18Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion19" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion19" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion19" runat="server" Width="100%" Font-Size="Small">                                            
                                                <asp:RadioButton ID="rdMainQuestion19_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion19"/>
                                                <asp:RadioButton ID="rdMainQuestion19_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion19"/>
                                                <div id="divQuestion19Yes">
                                                <asp:TextBox ID="txtQuestion19Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion20" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion20" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion20" runat="server" Width="100%" Font-Size="Small"> 
                                                <asp:RadioButton ID="rdMainQuestion20_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion20"/>
                                                <asp:RadioButton ID="rdMainQuestion20_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion20"/>
                                                <div id="divQuestion20Yes">
                                                <asp:TextBox ID="txtQuestion20Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion21" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion21" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion21" runat="server" Width="100%" Font-Size="Small"> 
                                                <asp:RadioButton ID="rdMainQuestion21_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion21"/>
                                                <asp:RadioButton ID="rdMainQuestion21_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion21"/>
                                                <div id="divQuestion21Yes">
                                                <asp:TextBox ID="txtQuestion21Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion22" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion22" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion22" runat="server" Width="100%" Font-Size="Small">  
                                                <asp:RadioButton ID="rdMainQuestion22_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion22"/>
                                                <asp:RadioButton ID="rdMainQuestion22_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion22"/>
                                                <div id="divQuestion22Yes">
                                                <asp:TextBox ID="txtQuestion22Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion23" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion23" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion23" runat="server" Width="100%" Font-Size="Small">
                                                <asp:RadioButton ID="rdMainQuestion23_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion23"/>
                                                <asp:RadioButton ID="rdMainQuestion23_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion23"/>
                                                <div id="divQuestion23Yes">
                                                <asp:TextBox ID="txtQuestion23Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion24" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion24" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion24" runat="server" Width="100%" Font-Size="Small"> 
                                                <asp:RadioButton ID="rdMainQuestion24_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion24"/>
                                                <asp:RadioButton ID="rdMainQuestion24_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion24"/>
                                                <div id="divQuestion24Yes">
                                                <asp:TextBox ID="txtQuestion24Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion25" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion25" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion25" runat="server" Width="100%" Font-Size="Small">  
                                                <asp:RadioButton ID="rdMainQuestion25_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion25"/>
                                                <asp:RadioButton ID="rdMainQuestion25_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion25"/>
                                                <div id="divQuestion25Yes">
                                                <asp:TextBox ID="txtQuestion25Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion26" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion26" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion26" runat="server" Width="100%" Font-Size="Small">
                                                <asp:RadioButton ID="rdMainQuestion26_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion26"/>
                                                <asp:RadioButton ID="rdMainQuestion26_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion26"/>
                                                <div id="divQuestion26Yes">
                                                <asp:TextBox ID="txtQuestion26Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion27" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion27" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion27" runat="server" Width="100%" Font-Size="Small">
                                                <asp:RadioButton ID="rdMainQuestion27_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion27"/>
                                                <asp:RadioButton ID="rdMainQuestion27_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion27"/>
                                                <div id="divQuestion27Yes">
                                                <asp:TextBox ID="txtQuestion27Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion28" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion28" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion28" runat="server" Width="100%" Font-Size="Small">
                                                <asp:RadioButton ID="rdMainQuestion28_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion28"/>
                                                <asp:RadioButton ID="rdMainQuestion28_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion28"/>
                                                <div id="divQuestion28Yes">
                                                <asp:TextBox ID="txtQuestion28Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion29" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion29" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion29" runat="server" Width="100%" Font-Size="Small">
                                                <asp:RadioButton ID="rdMainQuestion29_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion29"/>
                                                <asp:RadioButton ID="rdMainQuestion29_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion29"/>
                                                <div id="divQuestion29Yes">
                                                <asp:TextBox ID="txtQuestion29Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion30" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion30" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion30" runat="server" Width="100%" Font-Size="Small"> 
                                                <asp:RadioButton ID="rdMainQuestion30_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion30"/>
                                                <asp:RadioButton ID="rdMainQuestion30_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion30"/>
                                                <div id="divQuestion30Yes">
                                                <asp:TextBox ID="txtQuestion30Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion31" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion31" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion31" runat="server" Width="100%" Font-Size="Small">
                                                <asp:RadioButton ID="rdMainQuestion31_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion31"/>
                                                <asp:RadioButton ID="rdMainQuestion31_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion31"/>
                                                <div id="divQuestion31Yes">
                                                <asp:TextBox ID="txtQuestion31Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion32" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion32" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion32" runat="server" Width="100%" Font-Size="Small">
                                                <asp:RadioButton ID="rdMainQuestion32_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion32"/>
                                                <asp:RadioButton ID="rdMainQuestion32_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion32"/>
                                                <div id="divQuestion32Yes">
                                                <asp:TextBox ID="txtQuestion32Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion33" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion33" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion33" runat="server" Width="100%" Font-Size="Small">
                                                <asp:RadioButton ID="rdMainQuestion33_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion33"/>
                                                <asp:RadioButton ID="rdMainQuestion33_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion33"/>
                                                <div id="divQuestion33Yes">
                                                <asp:TextBox ID="txtQuestion33Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion34" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion34" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion34" runat="server" Width="100%" Font-Size="Small">
                                                <asp:RadioButton ID="rdMainQuestion34_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion34"/>
                                                <asp:RadioButton ID="rdMainQuestion34_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion34"/>
                                                <div id="divQuestion34Yes">
                                                <asp:TextBox ID="txtQuestion34Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion35" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion35" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion35" runat="server" Width="100%" Font-Size="Small"> 
                                                <asp:RadioButton ID="rdMainQuestion35_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion35"/>
                                                <asp:RadioButton ID="rdMainQuestion35_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion35"/>
                                                <div id="divQuestion35Yes">
                                                <asp:TextBox ID="txtQuestion35Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion36" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion36" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion36" runat="server" Width="100%" Font-Size="Small"> 
                                                <asp:RadioButton ID="rdMainQuestion36_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion36"/>
                                                <asp:RadioButton ID="rdMainQuestion36_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion36"/>
                                                <div id="divQuestion36Yes">
                                                <asp:TextBox ID="txtQuestion36Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion37" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion37" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion37" runat="server" Width="100%" Font-Size="Small">
                                                <asp:RadioButton ID="rdMainQuestion37_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion37"/>
                                                <asp:RadioButton ID="rdMainQuestion37_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion37"/>
                                                <div id="divQuestion37Yes">
                                                <asp:TextBox ID="txtQuestion37Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion38" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion38" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion38" runat="server" Width="100%" Font-Size="Small">
                                                <asp:RadioButton ID="rdMainQuestion38_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion38"/>
                                                <asp:RadioButton ID="rdMainQuestion38_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion38"/>
                                                <div id="divQuestion38Yes">
                                                <asp:TextBox ID="txtQuestion38Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion39" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion39" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion39" runat="server" Width="100%" Font-Size="Small"> 
                                                <asp:RadioButton ID="rdMainQuestion39_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion39"/>
                                                <asp:RadioButton ID="rdMainQuestion39_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion39"/>
                                                <div id="divQuestion39Yes">
                                                <asp:TextBox ID="txtQuestion39Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion40" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion40" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion40" runat="server" Width="100%" Font-Size="Small"> 
                                                <asp:RadioButton ID="rdMainQuestion40_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion40"/>
                                                <asp:RadioButton ID="rdMainQuestion40_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion40"/>
                                                <div id="divQuestion40Yes">
                                                <asp:TextBox ID="txtQuestion40Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion41" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion41" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion41" runat="server" Width="100%" Font-Size="Small">
                                                <asp:RadioButton ID="rdMainQuestion41_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion41"/>
                                                <asp:RadioButton ID="rdMainQuestion41_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion41"/>
                                                <div id="divQuestion41Yes">
                                                <asp:TextBox ID="txtQuestion41Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion42" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion42" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion42" runat="server" Width="100%" Font-Size="Small">
                                                <asp:RadioButton ID="rdMainQuestion42_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion42"/>
                                                <asp:RadioButton ID="rdMainQuestion42_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion42"/>
                                                <div id="divQuestion42Yes">
                                                <asp:TextBox ID="txtQuestion42Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion43" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top" colspan="3">
                                            <asp:Label ID="lblMainQuestion43" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:auto;" align="center" valign="top">
                                            <asp:CheckBox ID="chkMainQuestion43Diabete" runat="server" />
                                        </td>
                                        <td style="width:auto;" align="center" valign="top">
                                            <asp:CheckBox ID="chkMainQuestion43Cardiovascular" runat="server" />
                                        </td>
                                        <td style="width:auto;" align="center" valign="top">
                                            <asp:CheckBox ID="chkMainQuestion43Mental" runat="server" />
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion44" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion44" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion44" runat="server" Width="100%" Font-Size="Small"> 
                                                <asp:RadioButton ID="rdMainQuestion44_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion44"/>
                                                <asp:RadioButton ID="rdMainQuestion44_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion44"/>
                                                <div id="divQuestion44Yes">
                                                <asp:TextBox ID="txtQuestion44Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>                                        
                            </table>
                        </td>
                     </tr>                        
                     <tr><td style="width:100%" align="center">
                         <table id="Remark" width="100%" cellspacing="0">
                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblRemark" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtRemark" runat="server" Width="990px" class="inputbox" 
                                        Height="100px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                    </table></td></tr>
                </table>              
            <br /> 
            <div id="btnSave_Data" style="float: right;">
                <asp:Button ID="btnSave" runat="server" Text="Submit Medicine Checkup Record" Width="250"
                    class="buttonRec" onclick="btnSave_Click" />
            </div>
            <div id="btnSave_Draft" style="float: left;">
                <asp:Button ID="btnSaveDraft" runat="server" Text="Save Draft Medicine Checkup Record"
                    Width="250" class="buttonRec" onclick="btnSaveDraft_Click" />
            </div>
            <br />
            <br />
            <br />
            <div align="center">
                Bangkok Hospital, 2 Soi Soonvijai 7, New Petchaburi Rd., Bangkok, Thailand 10310
                Tel.(+66) 2310-3000, 1719 (local mobile calls only)
          </div> 
          </div>  
          </div>                
    </div>
    </form>
    </td>
    </tr>
    </table>
            
</body>
</html>
