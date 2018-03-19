<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Questionaire_Health_History.aspx.cs"
    Inherits="EMRQuestionnaire.web.Questionaire_Health_History" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="../css/FontsThai/fontsthaisans_neueregular.css" rel="stylesheet" type="text/css" />
<link href="../css/maincss.css" rel="stylesheet" type="text/css" />
<link href="../datePicker/jquery-ui.css" rel="stylesheet" type="text/css" />
<script src="../datePicker/external/jquery/jquery.js" type="text/javascript"></script>
<script src="../datePicker/jquery-ui.js" type="text/javascript"></script>
<link rel="shortcut icon" href="../images/quiz-games-300x300.png" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="headMainPage" runat="server">
    <title>Occupational Medicine Checkup Record </title>
</head>
<body>
<table border ="0" cellpadding ="0" cellspacing="0" width="100%"  > <tr> <td  align ="center">
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
           <span style="vertical-align:bottom;"> <asp:ImageButton ID="imgThai" 
                 runat="server" ImageUrl="~/images/Thailand.png" OnClick="imgThai_Click"  
                 Height="36px" />
            <asp:ImageButton ID="imgEnglish" runat="server" 
                 ImageUrl="~/images/United-Kingdom.png" OnClick="imgEnglish_Click" 
                 Height="36px"/></span>
          </div> 
        </div>
        <div id="grpWorkinghistory" class="divBarMainTopic" style="margin-top: -19px;">
            <asp:Label ID="lblgrpWorkingHistory" runat="server" Text=""></asp:Label></div>
        <div id="divLoadData" style="float: right; margin-top: -20px;">
            <asp:Button ID="btnLoadData" runat="server" Text="Load Data" Width="100" Height="20"
                class="buttonRec" OnClick="btnLoadData_Click" />
        </div>
        <br />
        <div id="information">
            <div id="groupPatientInformation" class="divBarGroup">
                <asp:Label ID="lblSmoking" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div id="detailsSmoke">
                <table id="tblSmoke">
                    <tr>
                        <td class="trPath">
                            <asp:RadioButton ID="rdoNonSmoking" runat="server" GroupName="Smoking" value="1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rdoNonSmokingButBeSecondHand" runat="server" GroupName="Smoking"
                                value="2" />
                            <div style="margin-left: 310px; margin-top: -15px;">
                                <asp:Label ID="lblNonFor" runat="server" Text=""></asp:Label>
                            </div>
                            <div style="margin-left: 345px; margin-top: -20px;">
                                <asp:TextBox ID="txtNonSmokingButBeSecondHand" runat="server" class="txtTextRight"></asp:TextBox>&nbsp;<asp:Label
                                    ID="lblYearNonSmoking" runat="server" Text=""></asp:Label></div>
                            <br />
                            <asp:RadioButton ID="rdoOutSmoking" runat="server" GroupName="Smoking" value="3" />
                            <div style="margin-left: 290px; margin-top: -15px;">
                                <asp:Label ID="lblSpecify" runat="server"></asp:Label>
                            </div>
                            <div style="margin-left: 345px; margin-top: -20px;">
                                <asp:TextBox ID="txtSpecify" runat="server" class="txtTextRight"></asp:TextBox>&nbsp;<asp:Label
                                    ID="lblSpecifyYear" runat="server" Text=""></asp:Label></div>
                            <br />
                            <asp:RadioButton ID="rdoSmoke" runat="server" GroupName="Smoking" value="4" />
                            <div style="margin-left: 290px; margin-top: -15px;">
                                <asp:Label ID="lblAmount" runat="server"></asp:Label>
                            </div>
                            <div style="margin-left: 345px; margin-top: -20px;">
                                <asp:TextBox ID="txtAmount" runat="server" class="txtTextRight"></asp:TextBox>&nbsp;
                                <asp:Label ID="lblAmountPerDay" runat="server" Text=""></asp:Label>
                            </div>
                            <br />
                            <div style="margin-left: 290px;">
                                <asp:Label ID="lblDuration" runat="server"></asp:Label>
                            </div>
                            <div style="margin-left: 345px; margin-top: -20px;">
                                <asp:TextBox ID="txtDuration" runat="server" class="txtTextRight"></asp:TextBox>&nbsp;
                                <asp:Label ID="lblDurationYear" runat="server" Text=""></asp:Label>
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            <div id="grpAlcohol" class="divBarGroup">
                <asp:Label ID="lblAlcohol" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div id="detailsAlcohol">
                <table id="tblAlgohol">
                    <tr>
                        <td>
                            <asp:RadioButton ID="rdoNoneAlcohol" runat="server" GroupName="Alcohol" value="1" /><br />
                            <br />
                            <asp:RadioButton ID="rdoQuitAlcohol" runat="server" GroupName="Alcohol" value="2" />
                            <asp:TextBox ID="txtQuitAlcohol" runat="server" class="txtTextRight"></asp:TextBox>
                            <asp:Label ID="lblQuitAlcoholYear" runat="server" Text=""></asp:Label><br />
                            <br />
                            <asp:RadioButton ID="rdoSocialDrink" runat="server" GroupName="Alcohol" value="3" />
                            <div id="socialPerMonth" class="socialPerMonth">
                                <asp:RadioButton ID="rdoSocialDrinkOnePerMonth" runat="server" GroupName="SocialDrinkPerMonth"
                                    value="1" />
                                <asp:RadioButton ID="rdoSocialDrinkTwoPerMonth" runat="server" GroupName="SocialDrinkPerMonth"
                                    value="2" />
                                <asp:RadioButton ID="rdoSocialDrinkThreePerMonth" runat="server" GroupName="SocialDrinkPerMonth"
                                    value="3" />
                            </div>
                            <br />
                            <asp:RadioButton ID="rdoDrinkFourPerWeek" runat="server" value="4" GroupName="Alcohol" />
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            <div id="divExercise" class="divBarGroup">
                <asp:Label ID="lblExercise" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div id="divExerciseDetails">
                <table id="tblExercise">
                    <tr>
                        <td>
                            <asp:RadioButton ID="rdoNoneExercise" runat="server" GroupName="grpExercise" value="1" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoLessThanHoursExercise" runat="server" GroupName="grpExercise"
                                value="2" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoRegularExercise" runat="server" GroupName="grpExercise" value="3" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divPresentIlless" class="divBarMainTopic">
                <asp:Label ID="lblGrpPresentIlless" runat="server" Text=""></asp:Label>
            </div>
            <br />
            <div id="divPresentConcern" class="divBarGroup">
                <asp:Label ID="lblPresentConcern" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblAnnualCheckUp">
                <tr>
                    <td>
                        <asp:RadioButton ID="rdoAnnualCheckUp" runat="server" GroupName="grpAnnualCheckUp"
                            value="1" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoAnnualCheckUpOthers" runat="server" GroupName="grpAnnualCheckUp"
                            value="2" />
                        <asp:TextBox ID="txtAnnualCheckUpOthers" runat="server" class="inputboxOthers"></asp:TextBox>
                        <br />
                    </td>
                </tr>
            </table>
            <br />
            <div id="divPsycho" class="divBarGroup">
                <asp:Label ID="lblPhycho" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblPhycho">
                <tr>
                    <td>
                        <asp:RadioButton ID="rdoNonePhycho" runat="server" GroupName="grpPhycho" value="1" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoPhychoOthers" runat="server" GroupName="grpPhycho" value="2" />
                        <asp:TextBox ID="txtPhycho" runat="server" class="inputboxOthers"></asp:TextBox>
                        <br />
                    </td>
                </tr>
            </table>
            <br />
            <div id="divMedicalHistory" class="divBarGroup">
                <asp:Label ID="lblMedicalHistory" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblMedicalHistory">
                <tr>
                    <td class="trPath">
                        <asp:RadioButton ID="rdoMedicalHistoryNoDiseases" runat="server" GroupName="grpMedicalDiseases"
                            value="1" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoMedicalDiseases" runat="server" GroupName="grpMedicalDiseases"
                            value="2" />
                        <br />
                    </td>
                    <td class="trPath">
                    </td>
                </tr>
            </table>
            <div id="MedicalHistory" style="margin-top: -5px;">
                <table>
                    <tr>
                        <td class="trPath">
                            <asp:CheckBox ID="chkPerTension" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkHeartDisease" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkDiabetes" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkCoronary" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkDysipidemia" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkCperipheral" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkGout" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkAbddimal" runat="server" />
                        </td>
                        <td class="trPath">
                            <asp:CheckBox ID="chkPulmonaryDisease" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkParalysis" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkPulmonaryTB" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkStroke" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkKidney" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkEpilesy" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkRespiratory" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkAsthma" runat="server" />
                            <asp:CheckBox ID="chkEmphysema" runat="server" />
                            <asp:CheckBox ID="chkChoronic" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="trPath">
                            <asp:CheckBox ID="chkSis" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkAllergy" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkcancer" runat="server" />
                            <asp:TextBox ID="txtCancerType" runat="server" class="inputboxOthers" />
                            <br />
                            <asp:CheckBox ID="chkPeptic" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkDiseaseOthers" runat="server" />
                            <asp:TextBox ID="txtOtherMedicalHistory" runat="server" class="inputboxOthers" />
                        </td>
                        <td class="trPath">
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="divCurrentMedicine" class="divBarGroup">
                <asp:Label ID="lblCurrentMedicine" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblCurrentMedicine">
                <tr>
                    <td class="trPath">
                        <asp:RadioButton ID="rdoCurrentMedicineNone" runat="server" GroupName="grpCurrentMedicine"
                            value="1" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoCurrentMedicineHave" runat="server" GroupName="grpCurrentMedicine"
                            value="2" />
                        <br />
                    </td>
                    <td class="trPath">
                    </td>
                </tr>
            </table>
            <div id="CurrentMedicine" style="margin-top: -5px;">
                <table>
                    <tr>
                        <td class="trPath">
                            <asp:CheckBox ID="chkCurrentForDiabetes" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkCurrentForHypertension" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkCurrentHyperlipidemia" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkCurrentMedicineOthers" runat="server" />
                            <asp:TextBox ID="txtCurrentMedicineOthers" runat="server" class="inputboxOthers" />
                        </td>
                        <td class="trPath">
                            <asp:CheckBox ID="chkCurrentForcadiovascular" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkCurrentDyslip" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkCurrentHormone" runat="server" />
                            <br />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="divAllergy" class="divBarGroup">
                <asp:Label ID="lblAllergy" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblAllergy">
                <tr>
                    <td>
                        <asp:RadioButton ID="rdoAllergyNone" runat="server" GroupName="grpAllergy" value="1" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoAllergyNotsure" runat="server" GroupName="grpAllergy" value="2" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoAllergyHave" runat="server" GroupName="grpAllergy" value="3" />
                        <asp:TextBox ID="txtAllergyHave" runat="server" class="inputboxOthers" />
                        <br />
                    </td>
                </tr>
            </table>
            <div id="PastIlless" class="divBarMainTopic">
                <asp:Label ID="lblPastIllness" runat="server" Text=""></asp:Label>
            </div>
            <br />
            <div id="PastIllessOrhospitalAdmission" class="divBarGroup">
                <asp:Label ID="lblPastIllessOrhospitalAdmission" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblPastIllessOrhospitalAdmission">
                <tr>
                    <td>
                        <asp:RadioButton ID="rdoPastIllessOrhospitalAdmissionNo" runat="server" GroupName="grpPastIllessOrhospitalAdmission"
                            value="1" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoPastIllessOrhospitalAdmissionYes" runat="server" GroupName="grpPastIllessOrhospitalAdmission"
                            value="2" />
                        <asp:TextBox ID="txtPastIllessOrhospitalAdmissionOthers" runat="server" class="inputboxOthers" />
                        <br />
                    </td>
                </tr>
            </table>
            <br />
            <div id="PastSurgery" class="divBarGroup">
                <asp:Label ID="lblPastSurgery" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblPastSurgery">
                <tr>
                    <td>
                        <asp:RadioButton ID="rdoPastSurgeryNo" runat="server" GroupName="grpPastSurgery"
                            value="1" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoPastSurgeryYes" runat="server" GroupName="grpPastSurgery"
                            value="2" />
                        <asp:TextBox ID="txtPastSurgeryYesOthers" runat="server" class="inputboxOthers" />
                        <br />
                    </td>
                </tr>
            </table>
            <div id="VaccinationInformation" class="divBarMainTopic">
                <asp:Label ID="lblVaccinationInformation" runat="server" Text=""></asp:Label>
            </div>
            <br />
            <div id="HepatitisB" class="divBarGroup">
                <asp:Label ID="lblHepatitisB" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblHepatitisB">
                <tr>
                    <td>
                        <asp:RadioButton ID="rdoHepatitisBYes" runat="server" GroupName="grpHepatitisB" value="1" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoHepatitisBNo" runat="server" GroupName="grpHepatitisB" value="2" />
                        <br />
                    </td>
                </tr>
            </table>
            <br />
            <div id="HepatitisA" class="divBarGroup">
                <asp:Label ID="lblHepatitisA" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblHepatitisA">
                <tr>
                    <td>
                        <asp:RadioButton ID="rdoHepatitisAYes" runat="server" GroupName="grpHepatitisA" value="1" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoHepatitisANo" runat="server" GroupName="grpHepatitisA" value="2" />
                        <br />
                    </td>
                </tr>
            </table>
            <br />
            <div id="Tetanusvaccine" class="divBarGroup">
                <asp:Label ID="lblTetanusvaccine" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblTetanusvaccine">
                <tr>
                    <td>
                        <asp:RadioButton ID="rdoTetanusvaccineLessthanTenYear" runat="server" GroupName="grpTetanusvaccine"
                            value="1" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoTetanusvaccineMorethanTenYear" runat="server" GroupName="grpTetanusvaccine"
                            value="2" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoTetanusvaccineNorecieve" runat="server" GroupName="grpTetanusvaccine"
                            value="3" />
                        <br />
                    </td>
                </tr>
            </table>
            <div id="FamilyHistory" class="divBarMainTopic">
                <asp:Label ID="lblFamilyHistory" runat="server" Text=""></asp:Label>
            </div>
            <br />
            <div id="Father" class="divBarGroup">
                <asp:Label ID="lblFather" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblFather">
                <tr>
                    <td class="trPath">
                        <asp:RadioButton ID="rdoFatherNodisease" runat="server" GroupName="grpFatherDisease"
                            value="1" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoFatherDisease" runat="server" GroupName="grpFatherDisease"
                            value="2" />
                        <br />
                    </td>
                    <td class="trPath">
                    </td>
                </tr>
            </table>
            <div id="FatherHide" style="margin-top: -5px;">
                <table>
                    <tr>
                        <td class="trPath">
                            <asp:CheckBox ID="chkFatherHypertension" runat="server" /><br />
                            <asp:CheckBox ID="chkFatherDiabetes" runat="server" /><br />
                            <asp:CheckBox ID="chkFatherDyslip" runat="server" /><br />
                            <asp:CheckBox ID="chkFatherGout" runat="server" /><br />
                            <asp:CheckBox ID="chkFatherPulmonary" runat="server" /><br />
                            <asp:CheckBox ID="chkFatherTB" runat="server" /><br />
                            <asp:CheckBox ID="chkFatherPepticUlcer" runat="server" /><br />
                            <asp:CheckBox ID="chkFatherAllergy" runat="server" /><br />
                            <asp:CheckBox ID="chkFatherHeartDisease" runat="server" /><br />
                            <asp:CheckBox ID="chkFatherOther" runat="server" />
                            <asp:TextBox ID="txtFatherOthers" runat="server" class="inputboxOthers" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="trPath">
                            <asp:CheckBox ID="chkFatherCoronary" runat="server" />
                            <br />
                            <asp:RadioButton ID="rdoFatherBeforeAge" runat="server" GroupName="grpFather" value="1" />
                            <br />
                            <asp:RadioButton ID="rdoFatherAfterAge" runat="server" GroupName="grpFather" value="2" />
                            <br />
                            <asp:RadioButton ID="rdoFatherNotSure" runat="server" GroupName="grpFather" value="3" />
                            <br />
                            <asp:CheckBox ID="chkFatherParalysis" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkFatherStroke" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkFatherAsthma" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkFatherCancer" runat="server" />
                            <asp:TextBox ID="txtFatherCancer" runat="server" class="inputboxOthers"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="Mother" class="divBarGroup">
                <asp:Label ID="lblMother" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblMother">
                <tr>
                    <td class="trPath">
                        <asp:RadioButton ID="rdoMotherNodisease" runat="server" GroupName="grpMotherDisease"
                            value="1" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoMotherDisease" runat="server" GroupName="grpMotherDisease"
                            value="2" />
                        <br />
                    </td>
                    <td class="trPath">
                    </td>
                </tr>
            </table>
            <div id="MotherHide" style="margin-top: -5px;">
                <table>
                    <tr>
                        <td class="trPath">
                            <asp:CheckBox ID="chkMotherHypertension" runat="server" /><br />
                            <asp:CheckBox ID="chkMotherDiabetes" runat="server" /><br />
                            <asp:CheckBox ID="chkMotherDyslip" runat="server" /><br />
                            <asp:CheckBox ID="chkMotherGout" runat="server" /><br />
                            <asp:CheckBox ID="chkMotherPulmonary" runat="server" /><br />
                            <asp:CheckBox ID="chkMotherTB" runat="server" /><br />
                            <asp:CheckBox ID="chkMotherPepticUlcer" runat="server" /><br />
                            <asp:CheckBox ID="chkMotherAllergy" runat="server" /><br />
                            <asp:CheckBox ID="chkMotherHeartDisease" runat="server" /><br />
                            <asp:CheckBox ID="chkMotherOther" runat="server" />
                            <asp:TextBox ID="txtMotherOthers" runat="server" class="inputboxOthers"></asp:TextBox>
                        </td>
                        <td class="trPath">
                            <asp:CheckBox ID="chkMotherCoronary" runat="server" /><br />
                            <asp:RadioButton ID="rdoMotherBeforeAge" runat="server" GroupName="grpMother" value="1" />
                            <br />
                            <asp:RadioButton ID="rdoMotherAfterAge" runat="server" GroupName="grpMother" value="2" />
                            <br />
                            <asp:RadioButton ID="rdoMotherNotSure" runat="server" GroupName="grpMother" value="3" />
                            <br />
                            <asp:CheckBox ID="chkMotherParalysis" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkMotherStroke" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkMotherAsthma" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkMotherCancer" runat="server" />
                            <asp:TextBox ID="txtMotherCancer" runat="server" class="inputboxOthers"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="Relatives" class="divBarGroup">
                <asp:Label ID="lblRelatives" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblRelatives">
                <tr>
                    <td class="trPath">
                        <asp:RadioButton ID="rdoRelativesNodisease" runat="server" GroupName="grpRelativesDisease"
                            value="1" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoRelativesDisease" runat="server" GroupName="grpRelativesDisease"
                            value="2" />
                        <br />
                    </td>
                    <td class="trPath">
                    </td>
                </tr>
            </table>
            <div id="RelativesHide" style="margin-top: -5px;">
                <table>
                    <tr>
                        <td class="trPath">
                            <asp:CheckBox ID="chkRelativesHypertension" runat="server" /><br />
                            <asp:CheckBox ID="chkRelativesDiabetes" runat="server" /><br />
                            <asp:CheckBox ID="chkRelativesDyslip" runat="server" /><br />
                            <asp:CheckBox ID="chkRelativesGout" runat="server" /><br />
                            <asp:CheckBox ID="chkRelativesPulmonary" runat="server" /><br />
                            <asp:CheckBox ID="chkRelativesTB" runat="server" /><br />
                            <asp:CheckBox ID="chkRelativesPepticUlcer" runat="server" /><br />
                            <asp:CheckBox ID="chkRelativesAllergy" runat="server" /><br />
                            <asp:CheckBox ID="chkRelativesHeartDisease" runat="server" /><br />
                            <asp:CheckBox ID="chkRelativesOther" runat="server" />
                            <asp:TextBox ID="txtRelativesOthers" runat="server" class="inputboxOthers"></asp:TextBox>
                        </td>
                        <td class="trPath">
                            <asp:CheckBox ID="chkRelativesCoronary" runat="server" /><br />
                            <asp:CheckBox ID="chkWomenRelativesBeforeAge" runat="server" />
                            <br />
                            &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkWomenRelativesAfterAge" runat="server" />
                            <br />
                            &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkWomenRelativesNotSure" runat="server" />
                            <br />
                            &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkMenRelativesBeforeAge" runat="server" />
                            <br />
                            &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkMenRelativesAfterAge" runat="server" />
                            &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkMenRelativesNotSure" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkRelativesParalysis" runat="server" /><br />
                            <asp:CheckBox ID="chkRelativesStroke" runat="server" /><br />
                            <asp:CheckBox ID="chkRelativesAsthma" runat="server" /><br />
                            <asp:CheckBox ID="chkRelativesCancer" runat="server" />
                            <asp:TextBox ID="txtRelativesCancer" runat="server" class="inputboxOthers"></asp:TextBox>
                            <br />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="FamilyOthers" class="divBarGroup">
                <asp:Label ID="lblFamilyOthers" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblFamilyOthers">
                <tr>
                    <td>
                        <asp:TextBox ID="txtFamilyOthers" TextMode="multiline" Columns="50" Rows="5" runat="server"
                            class="Others"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div id="forwoman" class="divBarMainTopic">
                <asp:Label ID="lblForwoman" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <br />
            <div id="FirstDate" class="divBarGroup">
                <asp:Label ID="lblWomanFirstDate" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblWoman">
                <tr>
                    <td>
                        <asp:CheckBox ID="chkMonoPause" runat="server" /><br />
                        <br />
                        <asp:RadioButton ID="rdoMonoStart" runat="server" GroupName="menStart" />
                        <asp:Label ID="lblDateFoirm" runat="server" Text="" class="subGroupLeft"></asp:Label>
                        <asp:TextBox ID="txtDateFormHH" runat="server" class="txtTextRight" Style="width: 80px;"></asp:TextBox>
                        <asp:Label ID="lblDateTo" runat="server" Text="" class="subGroupLeft"></asp:Label>
                        <asp:TextBox ID="txtDateToHH" runat="server" class="txtTextRight" Style="width: 80px;"></asp:TextBox>
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoNotsure" runat="server" GroupName="menStart" />
                    </td>
                </tr>
            </table>
            <br />
            <div id="characteristic" class="divBarGroup">
                <asp:Label ID="lblCharacteristic" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblCharacteristic">
                <tr>
                    <td>
                        <asp:RadioButton ID="rdoCharacteristicNormal" runat="server" GroupName="characteristic"
                            value="1" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoCharacteristicAbNormal" runat="server" GroupName="characteristic"
                            value="2" />
                    </td>
                </tr>
            </table>
            <br />
            <div id="Pregnancy" class="divBarGroup">
                <asp:Label ID="lblPregnancy" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblPregnancy">
                <tr>
                    <td>
                        <asp:RadioButton ID="rdoPregnancyNo" runat="server" GroupName="Pregnancy" value="1" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoPregnancy" runat="server" GroupName="Pregnancy" value="2" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoPregnancySuspectPregnancy" runat="server" GroupName="Pregnancy"
                            value="3" />
                    </td>
                </tr>
            </table>
            <br />
            <div id="HasDelivere" class="divBarGroup">
                <asp:Label ID="lblHasDelivere" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblHasDelivere">
                <tr>
                    <td>
                        <asp:RadioButton ID="rdoHasDelivereYes" runat="server" GroupName="HasDelivere" value="1" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoHasDelivereNo" runat="server" GroupName="HasDelivere" value="2" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoHasDelivereNotSure" runat="server" GroupName="HasDelivere"
                            value="3" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNote_RowOne" runat="server" Text="" class="subGroupLeft"></asp:Label>
                        <br />
                        <asp:Label ID="lblNote_RowTwo" runat="server" Text="" class="subGroupLeft"></asp:Label>
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
            <br />
            <div style="float: right;">
                <asp:Button ID="saveQuestionaireHealthHistory" runat="server" Width="250" class="buttonRec"
                    Text="Submit Questionaire Health History" OnClick="saveQuestionaireHealthHistory_Click" />
            </div>
            <br />
            <br />
            <br />
            <div align="center">
                Bangkok Hospital, 2 Soi Soonvijai 7, New Petchaburi Rd., Bangkok, Thailand 10310
                Tel.(+66) 2310-3000, 1719 (local mobile calls only) +66) 2310-3000, 1719 (local
                mobile calls only)
            </div>
        </div>
    </div>
    </form>
        </td></tr></table>
</body>
</html>
