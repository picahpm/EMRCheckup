<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="MainPage.aspx.cs" Inherits="EMRQuestionnaire.web.MainPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<link href="../css/FontsThai/fontsthaisans_neueregular.css" rel="stylesheet" type="text/css" />
<link href="../css/maincss.css" rel="stylesheet" type="text/css" />
<link rel="shortcut icon" href="../images/quiz-games-300x300.png" />
<link href="../datePicker/jquery-ui.css" rel="stylesheet" type="text/css" />
<script src="../datePicker/external/jquery/jquery.js" type="text/javascript"></script>
<script src="../datePicker/jquery-ui.js" type="text/javascript"></script>
<script src="../scripts/main_Page.js" type="text/javascript"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="headMainPage" runat="server">
    <title>Occupational Medicine Checkup Record </title>
    <style type="text/css">
        .style1
        {
            /* background: #ecf2f6; */
	    background: #FFFFFF;
            }
        .style2
        {
            /* background: #ecf2f6; */
	    background: #FFFFFF;
            height: 94px;
        }
    </style>
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
        <div id="information">
            <div id="Personal" class="divBarMainTopic" >
                <asp:Label ID="Lblperson" runat="server" Text=""></asp:Label>
            </div>
            <div id="divLoadData" style="float: right; margin-top: -20px;">
                <asp:Button ID="btnLoadData" runat="server" Text="Load Data" Width="100" Height="20"
                    class="buttonRec" OnClick="btnLoadData_Click" />
            </div>
            <br />
            <div id="groupPatientInformation" class="divBarGroup">
                General Informational
            </div>
            <table id="Patient">
                <tr>
                    <td  style="width:50%" class="trPath">
                       <table  border ="0" cellpadding ="0" cellspacing="0"><tr>
                       <td style="width:150px"><asp:Label ID="lblName" runat="server" Text=""></asp:Label></td>
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
                       <td> <asp:TextBox ID="txtBirthDate" runat="server" class="inputbox"></asp:TextBox>
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
                       </td><td>
                        <asp:TextBox ID="txtHN" runat="server" class="inputbox" Enabled="false"></asp:TextBox></td>
                    </tr></table>
                    </td>
                    <td class="trPath">
                      <table  border ="0" cellpadding ="0" cellspacing="0"><tr>
                       <td style="width:150px">
                        <asp:Label ID="lblAge" runat="server" Text=""></asp:Label>
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
            <br />
        </div>
        <div id="groupInformations" class="divBarGroup">
            <asp:Label ID="lblGeneralInformation" runat="server" Text="" class="subGroupLeft"></asp:Label>
        </div>
        <table id="Informations">
            <tr>
                <td class="trPath" >
                    <div id="generalInformation">
                        <asp:Label ID="lblEmploymentDate" runat="server" Text=""></asp:Label>
                        <div style="margin-left: 168px; margin-top: -20px;">
                            <asp:TextBox ID="txtEmploymentDate" runat="server" class="inputbox"></asp:TextBox>
                        </div>
                        <br />
                        <asp:Label ID="lblEmployer" runat="server" Text=""></asp:Label>
                        <div style="margin-left: 168px; margin-top: -20px;">
                            <asp:TextBox ID="txtEmployer" runat="server" Width="300" class="inputbox"></asp:TextBox></div>
                        <br />
                        <asp:Label ID="lblWorkLocation" runat="server" Text=""></asp:Label>
                        <div style="margin-left: 168px; margin-top: -20px;">
                            <asp:TextBox ID="txtWorkLocation" runat="server" Width="200" class="inputbox"></asp:TextBox>
                        </div>
                        <br />
                    </div>
                </td>
                <td class="trPath">
                    <br />
                    <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                    <div style="margin-left: 168px; margin-top: -20px;">
                        <asp:TextBox ID="txtEmail" runat="server" Width="250" class="inputbox" AutoCompleteType="Email"></asp:TextBox>
                    </div>
                    <br />
                    
                    <asp:Label ID="lblFunctional" runat="server" Text=""></asp:Label>
                  <div style="margin-left: 350px; margin-top: -20px;">
                    <asp:TextBox ID="txtFunctional" runat="server" class="inputbox"></asp:TextBox><br />
                      </div><br />
                    <asp:Label ID="lblCurrent" runat="server" Text=""></asp:Label>
                    <div style="margin-left: 168px; margin-top: -20px;">
                        <asp:TextBox ID="txtCurrent" runat="server" Width="300" class="inputbox"></asp:TextBox></div>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <div id="divInformation">
            <div id="grpWorkinghistory" class="divBarMainTopic">
                <asp:Label ID="lblgrpWorkingHistory" runat="server" Text=""></asp:Label>
            </div>
            <br />
            <div id="divGroupHistory" class="divBarGroup">
                <asp:Label ID="lblHeadWorking" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="workinghistory">
                <tr>
                    <th class="centerTextAlign">
                        <asp:Label ID="lblCurrentEmp_2_1" runat="server" Text=""></asp:Label>
                    </th>
                    <th class="centerTextAlign">
                        <asp:Label ID="lblClass_2_1" runat="server" Text=""></asp:Label>
                    </th>
                    <th class="centerTextAlign">
                        <asp:Label ID="lblType_2_1" runat="server" Text=""></asp:Label>
                    </th>
                    <th class="centerTextAlign" style="width: 210px;">
                        <asp:Label ID="lblPeriod_2_1" runat="server" Text=""></asp:Label>
                    </th>
                    <th class="centerTextAlign">
                        <asp:Label ID="lblRelated_2_1" runat="server" Text=""></asp:Label>
                    </th>
                    <th class="centerTextAlign">
                        <asp:Label ID="lblDoYou_2_1" runat="server" Text=""></asp:Label>
                    </th>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtCurrentEmp_2_1_row_1" runat="server" class="inputbox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtClass_2_1_row_1" runat="server" class="inputbox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtType_2_1_row_1" runat="server" class="inputbox"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtPeriod_2_1_row_1_1" runat="server" class="inputbox" Width="90"></asp:TextBox>
                        -
                        <asp:TextBox ID="txtPeriod_2_1_row_1_2" runat="server" class="inputbox" Width="90"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtRelated_2_1_row_1" runat="server" class="inputbox" Width="135"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDoYou_2_1_row_1" runat="server" class="inputbox" Width="135"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtCurrentEmp_2_1_row_2" runat="server" class="inputbox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtClass_2_1_row_2" runat="server" class="inputbox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtType_2_1_row_2" runat="server" class="inputbox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPeriod_2_1_row_2_1" runat="server" class="inputbox" Width="90"></asp:TextBox>
                        -
                        <asp:TextBox ID="txtPeriod_2_1_row_2_2" runat="server" class="inputbox" Width="90"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRelated_2_1_row_2" runat="server" class="inputbox" Width="135"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDoYou_2_1_row_2" runat="server" class="inputbox" Width="135"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtCurrentEmp_2_1_row_3" runat="server" class="inputbox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtClass_2_1_row_3" runat="server" class="inputbox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtType_2_1_row_3" runat="server" class="inputbox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPeriod_2_1_row_3_1" runat="server" class="inputbox" Width="90"></asp:TextBox>
                        -
                        <asp:TextBox ID="txtPeriod_2_1_row_3_2" runat="server" class="inputbox" Width="90"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRelated_2_1_row_3" runat="server" class="inputbox" Width="135"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDoYou_2_1_row_3" runat="server" class="inputbox" Width="135"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtCurrentEmp_2_1_row_4" runat="server" class="inputbox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtClass_2_1_row_4" runat="server" class="inputbox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtType_2_1_row_4" runat="server" class="inputbox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPeriod_2_1_row_4_1" runat="server" class="inputbox" Width="90"></asp:TextBox>
                        -
                        <asp:TextBox ID="txtPeriod_2_1_row_4_2" runat="server" class="inputbox" Width="90"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRelated_2_1_row_4" runat="server" class="inputbox" Width="135"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDoYou_2_1_row_4" runat="server" class="inputbox" Width="135"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtCurrentEmp_2_1_row_5" runat="server" class="inputbox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtClass_2_1_row_5" runat="server" class="inputbox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtType_2_1_row_5" runat="server" class="inputbox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPeriod_2_1_row_5_1" runat="server" class="inputbox" Width="90"></asp:TextBox>
                        -
                        <asp:TextBox ID="txtPeriod_2_1_row_5_2" runat="server" class="inputbox" Width="90"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRelated_2_1_row_5" runat="server" class="inputbox" Width="135"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDoYou_2_1_row_5" runat="server" class="inputbox" Width="135"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <div id="divGroupCurrent" class="divBarGroup">
                <asp:Label ID="lblHeadWorkingCurrent" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblCurrentEmployer">
                <tr>
                    <th class="centerTextAlign">
                        <asp:Label ID="lblCurrentEmp_2_2" runat="server" Text=""></asp:Label>
                    </th>
                    <th class="centerTextAlign">
                        <asp:Label ID="lblClass_2_2" runat="server" Text=""></asp:Label>
                    </th>
                    <th class="centerTextAlign">
                        <asp:Label ID="lblType_2_2" runat="server" Text=""></asp:Label>
                    </th>
                    <th class="centerTextAlign" style="width: 210px;">
                        <asp:Label ID="lblPeriod_2_2" runat="server" Text=""></asp:Label>
                    </th>
                    <th class="centerTextAlign" style="width: 155px;">
                        <asp:Label ID="lblRelated_2_2" runat="server" Text=""></asp:Label>
                    </th>
                    <th class="centerTextAlign">
                        <asp:Label ID="lblDoYou_2_2" runat="server" Text=""></asp:Label>
                    </th>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtCurrentEmp_2_2_row_1" runat="server" class="inputbox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtClass_2_2_row_1" runat="server" class="inputbox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtType_2_2_row_1" runat="server" class="inputbox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPeriod_2_2_row_1_1" runat="server" class="inputbox" Width="90"></asp:TextBox>
                        -
                        <asp:TextBox ID="txtPeriod_2_2_row_1_2" runat="server" class="inputbox" Width="90"></asp:TextBox>
                    </td>
                    <td class="greyTd">
                    </td>
                    <td class="greyTd">
                    </td>
                </tr>
            </table>
            <br />
            <div id="ClassificationofEmployment" class="divBarGroup">
                <asp:Label ID="lblClassification" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div>
                <table id="tbClassificationofEmployment">
                    <tr>
                        <td class="trPath">
                            <asp:CheckBox ID="chkOil" runat="server" /><br />
                            <asp:CheckBox ID="chkNon" runat="server" /><br />
                            <asp:CheckBox ID="chkMan" runat="server" /><br />
                            <asp:CheckBox ID="chkBasicMetals" runat="server" /><br />
                            <asp:CheckBox ID="chkText" runat="server" /><br />
                            <asp:CheckBox ID="chkMetals" runat="server" /><br />
                            <asp:CheckBox ID="chkForrest" runat="server" /><br />
                            <asp:CheckBox ID="chkOtherClassificationofEmployment" runat="server" OnCheckedChanged="chkOtherClassificationofEmployment_CheckedChanged" />
                            <asp:TextBox ID="txtOtherClassificationofEmployment" Width="300" runat="server" class="inputboxOthers"></asp:TextBox>
                        </td>
                        <td class="trPath">
                            <div id="ClassificationofEmploymentRight">
                                <asp:CheckBox ID="chkMotor" runat="server" /><br />
                                <asp:CheckBox ID="chkPaper" runat="server" /><br />
                                <asp:CheckBox ID="chkPublic" runat="server" /><br />
                                <asp:CheckBox ID="chkChemecal" runat="server" /><br />
                                <asp:CheckBox ID="h_01" runat="server" Style="display: none" /><br />
                                <asp:CheckBox ID="h_02" runat="server" Style="display: none" /><br />
                                <asp:CheckBox ID="h_03" runat="server" Style="display: none" /><br />
                                <asp:CheckBox ID="h_04" runat="server" Style="display: none" /><br />
                                <asp:CheckBox ID="h_05" runat="server" Style="display: none" /><br />
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            <div id="typeofwork" class="divBarGroup">
                <asp:Label ID="lblTypeOfWork" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblTypeOfWork">
                <tr>
                    <td class="trPath">
                        <asp:RadioButton ID="rdoOffice" runat="server" GroupName="typeofwork" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoOnShore" runat="server" GroupName="typeofwork" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoOffShore" runat="server" GroupName="typeofwork" />
                    </td>
                    <td class="trPath">
                    </td>
                </tr>
            </table>
            <br />
            <div id="specialAssignment" class="divBarGroup">
                <asp:Label ID="lblSpecialAssignment" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div  class="height130">
                <table id="tblSpecialAssignment">
                    <tr>
                        <td class="trPath">
                            <asp:CheckBox ID="chkFire" runat="server" /><br />
                            <asp:CheckBox ID="chkCon" runat="server" /><br />
                            <asp:CheckBox ID="chkProfes" runat="server" /><br />
                            <asp:CheckBox ID="chkLab" runat="server" /><br />
                            <asp:CheckBox ID="chkOtherSpecialAssignment" runat="server" />
                            <asp:TextBox ID="txtOtherSpecialAssignment" Width="300" runat="server" class="inputboxOthers"></asp:TextBox>
                        </td>
                        <td class="trPath">
                            <div id="SpecialAssignmentRight">
                                <asp:CheckBox ID="chkCrane" runat="server" /><br />
                                <asp:CheckBox ID="chkPainter" runat="server" /><br />
                                <asp:CheckBox ID="chkCarter" runat="server" /><br />
                                <asp:CheckBox ID="h_06" runat="server" Style="display: none" /><br />
                                <asp:CheckBox ID="h_07" runat="server" Style="display: none" /><br />
                                <asp:CheckBox ID="h_08" runat="server" Style="display: none" /><br />
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            <div id="PhysicalHealthHazard" class="divBarGroup">
                <asp:Label ID="lblPhysicalHealthHazard" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div class="height110">
                <table id="tblWorkrelatedHealthHazard">
                    <tr>
                        <td class="trPath">
                            <asp:CheckBox ID="chkNophysical" runat="server" /><br />
                            <asp:CheckBox ID="chkLight" runat="server" /><br />
                            <asp:CheckBox ID="chkCold" runat="server" /><br />
                            <asp:CheckBox ID="chkOthersHazard" runat="server" />
                            <asp:TextBox ID="txtOthersHazard" Width="300" runat="server" class="inputboxOthers"></asp:TextBox>
                        </td>
                        <td class="trPath">
                            <div id="WorkrelatedHealthHazardRight">
                                <asp:CheckBox ID="chkNoise" runat="server" /><br />
                                <asp:CheckBox ID="chkRadia" runat="server" /><br />
                                <asp:CheckBox ID="chkHeat" runat="server" /><br />
                                <asp:CheckBox ID="h_09" runat="server" Style="display: none" /><br />
                                <asp:CheckBox ID="h_10" runat="server" Style="display: none" /><br />
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            <div id="Biologicalhealthhazard" class="divBarGroup">
                <asp:Label ID="lblBiologicalHealtHazard" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div class="height110">
                <table id="tblBiologicalHealtHazard">
                    <tr>
                        <td class="trPath">
                            <asp:CheckBox ID="chkNobiological" runat="server" /><br />
                            <asp:CheckBox ID="chkAnimal" runat="server" /><br />
                            <asp:CheckBox ID="chkBlood" runat="server" /><br />
                            <asp:CheckBox ID="chkOtherBiologicalHealtHazard" runat="server" />
                            <asp:TextBox ID="txtOthersBiologicalHealtHazard" Width="300" runat="server" class="inputboxOthers"></asp:TextBox>
                        </td>
                        <td class="trPath">
                            <asp:CheckBox ID="chkBacteria" runat="server" /><br />
                            <asp:CheckBox ID="chkFungus" runat="server" /><br />
                            <asp:CheckBox ID="chkVirus" runat="server" /><br />
                            <asp:CheckBox ID="h_11" runat="server" Style="display: none" /><br />
                            <asp:CheckBox ID="h_12" runat="server" Style="display: none" /><br />
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            <div id="Chemicalhealthhazard" class="divBarGroup">
                <asp:Label ID="lblChemicalHealthHazard" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div  class="height150">
                <table id="tblChemicalhealthhazard">
                    <tr>
                        <td class="trPath">
                            <asp:CheckBox ID="chkNoChemecal" runat="server" /><br />
                            <asp:CheckBox ID="chkOrganic" runat="server" /><br />
                            <asp:CheckBox ID="chkGas" runat="server" /><br />
                            <asp:CheckBox ID="chkHeavy" runat="server" /><br />
                            <asp:CheckBox ID="chkDust" runat="server" /><br />
                            <asp:CheckBox ID="chkOtherChemicalhealthhazard" runat="server" />
                            <asp:TextBox ID="txtOtherChemicalhealthhazard" Width="300" runat="server" class="inputboxOthers"></asp:TextBox>
                        </td>
                        <td class="trPath">
                            <asp:CheckBox ID="chkAcid" runat="server" /><br />
                            <asp:CheckBox ID="chkMetalFume" runat="server" /><br />
                            <asp:CheckBox ID="chkHerb" runat="server" /><br />
                            <asp:CheckBox ID="chkPowder" runat="server" /><br />
                            <asp:CheckBox ID="h_13" runat="server" Style="display: none" /><br />
                            <asp:CheckBox ID="h_14" runat="server" Style="display: none" /><br />
                            <asp:CheckBox ID="h_15" runat="server" Style="display: none" /><br />
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            <div id="Psychological" class="divBarGroup">
                <asp:Label ID="lblPsychological" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblPsychological">
                <tr>
                    <td class="trPath">
                        <asp:RadioButton ID="rdoNoPsychological" runat="server" GroupName="Psychological" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoYesPsychological" runat="server" GroupName="Psychological" />   <asp:TextBox ID="TxtOtherPsychological" Width="300" runat="server" class="inputboxOthers"></asp:TextBox>
                    </td>
                    <td class="trPath">
                    </td>
                </tr>
            </table>
            <br />
            <div id="Ergonomic" class="divBarGroup">
                <asp:Label ID="lblErgonomic" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblErgonomic">
                <tr>
                    <td class="trPath">
                        <asp:CheckBox ID="chkNoErgonomic" runat="server" /><br />
                        <asp:CheckBox ID="chkPoor" runat="server" /><br />
                        <asp:CheckBox ID="chkInapp" runat="server" />
                        <br />
                        <asp:CheckBox ID="chkRepeat" runat="server" />
                    </td>
                    <td class="trPath">
                    </td>
                </tr>
            </table>
            <br />
            <div id="DoyouuseanyPersonal" class="divBarGroup">
                <asp:Label ID="lblDoYouUseAnyPersonal" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div class="height130">
                <table id="tblDoYouUseAnyPersonal">
                    <tr>
                        <td class="trPath">
                            <asp:CheckBox ID="chkNoPPE" runat="server" /><br />
                            <asp:CheckBox ID="chkEarplug" runat="server" /><br />
                            <asp:CheckBox ID="chkSafetyGlass" runat="server" /><br />
                            <asp:CheckBox ID="chkHelmet" runat="server" />
                            <br />
                            <asp:CheckBox ID="chkPPEOther" runat="server" />
                            <asp:TextBox ID="txtPPEOtherDetails" Width="300" runat="server" class="inputboxOthers"></asp:TextBox>
                        </td>
                        <td class="trPath">
                            <asp:CheckBox ID="chkSafetyShoe" runat="server" /><br />
                            <asp:CheckBox ID="chkGlove" runat="server" /><br />
                            <asp:CheckBox ID="chkCoverall" runat="server" /><br />
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            <div id="grpPersonalIllness" class="divBarMainTopic">
                <asp:Label ID="lblgrpPersonalIllness" runat="server" Text=""></asp:Label>
            </div>
            <br />
            <div id="Do_you_need_to_take_any_medication_regularly" class="divBarGroup">
                <asp:Label ID="lblDo_you_need_to_take_any_medication_regularly" runat="server" Text=""
                    class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblDo_you_need_to_take_any_medication_regularly">
                <tr>
                    <td class="trPath">
                        <asp:RadioButton ID="rdoNOmedication_regularly" runat="server" GroupName="do_you_need_to_take_any_medication_regularly" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoYESmedication_regularly" runat="server" GroupName="do_you_need_to_take_any_medication_regularly" />
                    </td>
                    <td class="trPath">
                    </td>
                </tr>
            </table>
            <br />
            <div id="Please_select_the_medication" class="divBarGroup">
                <asp:Label ID="lblPlease_select_the_medication" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div id="MedicationTaking">
                <table id="tblPlease_select_the_medication">
                    <tr id="tr3_2">
                        <td class="trPath">
                            <asp:CheckBox ID="chkHeartTaking" runat="server" /><br />
                            <asp:CheckBox ID="chkHighBloodPressureTaking" runat="server" /><br />
                            <asp:CheckBox ID="chkHighBloodLipidsTaking" runat="server" /><br />
                            <asp:CheckBox ID="chkBloodThinnerTaking" runat="server" /><br />
                            <asp:CheckBox ID="chkDiabetesMedicationTaking" runat="server" /><br />
                            <asp:CheckBox ID="chkOtherDo_you_needTaking" runat="server" />
                            <asp:TextBox ID="txtOtherDo_you_needDetailsTaking" Width="300" runat="server"></asp:TextBox>
                        </td>
                        <td class="trPath">
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="Are_you_allergic" class="divBarGroup">
                <asp:Label ID="lblAre_you_allergic" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblAre_you_allergic">
                <tr>
                    <td class="trPath">
                        <asp:RadioButton ID="rdoAre_you_allergic_no" runat="server" GroupName="Are_you_allergic" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoAre_you_allergic_not_sure" runat="server" GroupName="Are_you_allergic" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoAre_you_allergic_others" runat="server" GroupName="Are_you_allergic" />
                        <asp:TextBox ID="txtAre_you_allergic_othersDetails" Width="300" runat="server"></asp:TextBox>
                    </td>
                    <td class="trPath">
                    </td>
                </tr>
            </table>
            <br />
            <div id="Have_you_ever_had_any_severe_illness" class="divBarGroup">
                <asp:Label ID="lblHave_llness" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblHave_you_ever_had_any_severe_illness">
                <tr>
                    <td class="trPath">
                        <asp:RadioButton ID="rdoHave_llness_No" runat="server" GroupName="severe_illness" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoHave_llness_Yes" runat="server" GroupName="severe_illness" />
                    </td>
                    <td class="trPath">
                    </td>
                </tr>
            </table>
            <div id="sub_Have_you_ever_had_any_severe_illness">
                <table id="tbl_sub_Have_you_ever_had_any_severe_illness">
                    <tr>
                        <th>
                            <asp:Label ID="lblDetails" runat="server" Text=""></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblYears" runat="server" Text=""></asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDetails_Illness_row_1" runat="server" Width="880px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDetails_Year_row_1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDetails_Illness_row_2" runat="server" Width="880"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDetails_Year_row_2" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="Have_you_ever_had_an_operation" class="divBarGroup">
                <asp:Label ID="lblHave_you_ever_had_an_operation" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblHave_you_ever_had_an_operation">
                <tr>
                    <td class="trPath">
                        <asp:RadioButton ID="rdoOperationNo" runat="server" GroupName="operation" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoOperationYes" runat="server" GroupName="operation" />
                        <asp:TextBox ID="txtYesDetailOperation" runat="server" Width="300"></asp:TextBox>
                    </td>
                    <td class="trPath">
                    </td>
                </tr>
            </table>
            <br />
            <div id="Do_you_have_any_underlying_deceases" class="divBarGroup">
                <asp:Label ID="lblDo_you_have_any_underlying_deceases" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblDo_you_have_any_underlying_deceases">
                <tr>
                    <td class="trPath">
                        <asp:RadioButton ID="rdoUnderlying_deceases_No" runat="server" GroupName="3_6" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoUnderlying_deceases_Yes" runat="server" GroupName="3_6" />
                        <br />
                        <br />
                    </td>
                    <td class="trPath">
                    </td>
                </tr>
            </table>
            <div id="underlying_deceases" style="margin-top: -10px;">
                <table>
                    <tr>
                        <td class="trPath">
                            <div id="Do_you_have_any_underlying_deceasesLeft">
                                <asp:CheckBox ID="chkSLE" runat="server" /><br />
                                <asp:CheckBox ID="chkCancer" runat="server" /><br />
                                <asp:CheckBox ID="chkDiabets" runat="server" /><br />
                                <asp:CheckBox ID="chkUnderlying_deceases_Asthma" runat="server" /><br />
                                <asp:CheckBox ID="chkPeptic_Ulcer" runat="server" /><br />
                                <asp:CheckBox ID="chkEpile" runat="server" /><br />
                                <asp:CheckBox ID="chkHigh_blood_pressure_Hypertension" runat="server" /><br />
                                <asp:CheckBox ID="chkChronic" runat="server" />
                                <br />
                                <asp:CheckBox ID="chkOthers_please_specify" runat="server" />
                                <asp:TextBox ID="txtOthers_please_specify" runat="server" Width="200"></asp:TextBox>
                            </div>
                        </td>
                        <td class="trPath">
                            <div id="Do_you_have_any_underlying_deceasesRight" style="margin-top: -50px;">
                                <asp:CheckBox ID="chkAnemia" runat="server" /><br />
                                <asp:CheckBox ID="chkLung_emphysema" runat="server" /><br />
                                <asp:CheckBox ID="chkCardiovascular" runat="server" /><br />
                                <asp:CheckBox ID="chkKidney_disease" runat="server" /><br />
                                <asp:CheckBox ID="chkHepatitis" runat="server" /><br />
                                <asp:CheckBox ID="chkHigh_blood_lipids_Hyperlipidemia" runat="server" />
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            <div id="Have_you_ever_had_any_vaccination_or_immunitys" class="divBarGroup">
                <asp:Label ID="lblHave_you_ever_had_any_vaccination_or_immunity" runat="server" Text=""
                    class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblHave_you_ever_had_any_vaccination_or_immunity">
                <tr>
                    <td class="trPath">
                        <asp:RadioButton ID="rdoVaccination_or_immunity_No" runat="server" GroupName="vaccination_or_immunity" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoVaccination_or_immunity_Yes" runat="server" GroupName="vaccination_or_immunity" />
                        <br />
                    </td>
                    <td class="trPath">
                    </td>
                </tr>
            </table>
            <div id="immunity">
                <table>
                    <tr>
                        <td class="trPath">
                            <div id="div3_7">
                                <asp:CheckBox ID="chkJE" runat="server" />
                                <br />
                                <asp:CheckBox ID="chkChickenpox" runat="server" />
                                <br />
                                <asp:CheckBox ID="chkInfluenza" runat="server" />
                                <br />
                                <asp:CheckBox ID="chkHepatitisA" runat="server" />
                                <br />
                                <asp:CheckBox ID="chkYellowFever" runat="server" />
                            </div>
                        </td>
                        <td>
                            <div id="Have_you_ever_had_any_vaccination_or_immunity">
                                <asp:CheckBox ID="chkMeningococcal" runat="server" />
                                <br />
                                <asp:CheckBox ID="chkHepatitisB" runat="server" />
                                <br />
                                <asp:CheckBox ID="chkTetanus" runat="server" />
                                <br />
                                <asp:CheckBox ID="chkTyphoid" runat="server" />
                            </div>
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="Do_you_smoke" class="divBarGroup">
                <asp:Label ID="lblDo_you_smoke" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblDo_you_smoke">
                <tr>
                    <td class="trPath">
                        <asp:RadioButton ID="rdoDo_you_smoke_No" runat="server" GroupName="Do_you_smoke" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoDo_you_smoke_Yes" runat="server" GroupName="Do_you_smoke" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoDo_you_smoke_Yes_but" runat="server" GroupName="Do_you_smoke" />
                    </td>
                    <td class="trPath">
                    </td>
                </tr>
            </table>
            <br />
            <div id="Smoke_before_quitting" class="divBarGroup">
                <asp:Label ID="lblSmoke_before_quitting" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div id="beforeQuitting">
                <table id="tblSmoke_before_quitting">
                    <tr id="tr3_9">
                        <td class="trPath">
                            <asp:RadioButton ID="rdoSmoke_before_quitting_0_5" runat="server" GroupName="Smoke_before_quitting" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoSmoke_before_quitting_6_10" runat="server" GroupName="Smoke_before_quitting" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoSmoke_before_quitting_over_10" runat="server" GroupName="Smoke_before_quitting" />
                        </td>
                        <td class="trPath">
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="Many_cigarettes" class="divBarGroup">
                <asp:Label ID="lblMany_cigarettes" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div id="ManyCigarettes">
                <table id="tblMany_cigarettes">
                    <tr id="tr3_10">
                        <td class="trPath">
                            <asp:RadioButton ID="rdoMany_cigarettes_less_5" runat="server" GroupName="Many_cigarettes" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoMany_cigarettes_5_10" runat="server" GroupName="Many_cigarettes" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoMany_cigarettes_over_10" runat="server" GroupName="Many_cigarettes" />
                        </td>
                        <td class="trPath">
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="How_long_have_you_been_smoking" class="divBarGroup">
                <asp:Label ID="lblHow_long_have_you_been_smoking" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div id="beenSmoking">
                <table id="tblHow_long_have_you_been_smoking">
                    <tr id="tr3_11">
                        <td class="trPath">
                            <asp:RadioButton ID="rdoHow_long_have_you_been_smoking_0_5" runat="server" GroupName="How_long_have_you_been_smoking" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoHow_long_have_you_been_smoking_6_10" runat="server" GroupName="How_long_have_you_been_smoking" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoHow_long_have_you_been_smoking_over_10" runat="server" GroupName="How_long_have_you_been_smoking" />
                        </td>
                        <td class="trPath">
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="How_many_cigarettes_do_you_smoke_in_a_day" class="divBarGroup">
                <asp:Label ID="lblHow_many_cigarettes_do_you_smoke_in_a_day" runat="server" Text=""
                    class="subGroupLeft"></asp:Label>
            </div>
            <div id="How_many_cigarettes">
                <table id="tblHow_many_cigarettes">
                    <tr id="tr3_12">
                        <td class="trPath">
                            <asp:RadioButton ID="rdoHow_many_cigarettes_do_you_smoke_in_a_day_less_5" runat="server"
                                GroupName="smoke_in_a_day" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoHow_many_cigarettes_do_you_smoke_in_a_day_5_10" runat="server"
                                GroupName="smoke_in_a_day" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoHow_many_cigarettes_do_you_smoke_in_a_day_over_10" runat="server"
                                GroupName="smoke_in_a_day" />
                        </td>
                        <td class="trPath">
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="Have_you_ever_thinking_about_quit_smoking" class="divBarGroup">
                <asp:Label ID="lblHave_you_ever_thinking_about_quit_smoking" runat="server" Text=""
                    class="subGroupLeft"></asp:Label>
            </div>
            <div id="About_quit_smoking">
                <table id="tblHave_you_ever_thinking_about_quit_smoking">
                    <tr id="tr3_13">
                        <td class="trPath">
                            <asp:RadioButton ID="rdoHave_you_ever_thinking_about_quit_smoking_No" runat="server"
                                GroupName="Have_you_ever_thinking_about_quit_smoking" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoHave_you_ever_thinking_about_quit_smoking_Yes" runat="server"
                                GroupName="Have_you_ever_thinking_about_quit_smoking" />
                        </td>
                        <td class="trPath">
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="Have_you_ever_consumed_alcohol" class="divBarGroup">
                <asp:Label ID="lblHave_you_ever_consumed_alcohol" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblHave_you_ever_consumed_alcohol">
                <tr id="tr3_14">
                    <td class="trPath">
                        <asp:RadioButton ID="rdoHave_you_ever_consumed_alcohol_No" runat="server" GroupName="Have_you_ever_consumed_alcohol" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoHave_you_ever_consumed_alcohol_Yes" runat="server" GroupName="Have_you_ever_consumed_alcohol" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoHave_you_ever_consumed_alcohol_Yes_But" runat="server" GroupName="Have_you_ever_consumed_alcohol" />
                    </td>
                    <td class="trPath">
                    </td>
                </tr>
            </table>
            <br />
            <div id="How_long_did_you_drink_alcohol_before_stop_drinking" class="divBarGroup">
                <asp:Label ID="lblHow_long_did_you_drink_alcohol_before_stop_drinking" runat="server"
                    Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div id="stop_drinking">
                <table id="tblHow_long_did_you_drink_alcohol_before_stop_drinking">
                    <tr id="tr3_15">
                        <td class="trPath">
                            <asp:RadioButton ID="rdoHow_long_did_you_drink_alcohol_before_stop_drinking_0_5"
                                runat="server" GroupName="How_long_did_you_drink_alcohol_before_stop_drinking" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoHow_long_did_you_drink_alcohol_before_stop_drinking_6_10"
                                runat="server" GroupName="How_long_did_you_drink_alcohol_before_stop_drinking" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoHow_long_did_you_drink_alcohol_before_stop_drinking_over_10"
                                runat="server" GroupName="How_long_did_you_drink_alcohol_before_stop_drinking" />
                        </td>
                        <td class="trPath">
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="How_often_did_you_drink_before_you_stopped" class="divBarGroup">
                <asp:Label ID="lblHow_often_did_you_drink_before_you_stopped" runat="server" Text=""
                    class="subGroupLeft"></asp:Label>
            </div>
            <div id="you_stopped">
                <table id="tblHow_often_did_you_drink_before_you_stopped">
                    <tr id="tr3_16">
                        <td class="trPath">
                            <asp:RadioButton ID="rdoHow_often_did_you_drink_before_you_stopped_1" runat="server"
                                GroupName="How_often_did_you_drink_before_you_stopped" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoHow_often_did_you_drink_before_you_stopped_2" runat="server"
                                GroupName="How_often_did_you_drink_before_you_stopped" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoHow_often_did_you_drink_before_you_stopped_3" runat="server"
                                GroupName="How_often_did_you_drink_before_you_stopped" />
                        </td>
                        <td class="trPath">
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="How_often_do_you_consume_alcohol" class="divBarGroup">
                <asp:Label ID="lblHow_often_do_you_consume_alcohol" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div id="consume_alcohol">
                <table id="tblHow_often_do_you_consume_alcohol">
                    <tr id="tr3_17">
                        <td class="trPath">
                            <asp:RadioButton ID="rdoHow_often_do_you_consume_alcohol_Less_than_1_time" runat="server"
                                GroupName="How_often_do_you_consume_alcohol" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoHow_often_do_you_consume_alcohol_1_time_week" runat="server"
                                GroupName="How_often_do_you_consume_alcohol" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoHow_often_do_you_consume_alcohol_2_3_week" runat="server"
                                GroupName="How_often_do_you_consume_alcohol" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoHow_often_do_you_consume_alcohol_more_than_3_week" runat="server"
                                GroupName="How_often_do_you_consume_alcohol" />
                        </td>
                        <td class="trPath">
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="Have_you_ever_think_about_stop_drinking" class="divBarGroup">
                <asp:Label ID="lbltblHave_you_ever_think_about_stop_drinking" runat="server" Text=""
                    class="subGroupLeft"></asp:Label>
            </div>
            <div id="about_stop_drinking">
                <table id="tblHave_you_ever_think_about_stop_drinking">
                    <tr id="tr3_18">
                        <td class="trPath">
                            <asp:RadioButton ID="rdoHave_you_ever_think_about_stop_drinking_No" runat="server"
                                GroupName="Have_you_ever_think_about_stop_drinking" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoHave_you_ever_think_about_stop_drinking_Yes" runat="server"
                                GroupName="Have_you_ever_think_about_stop_drinking" />
                        </td>
                        <td class="trPath">
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="Have_you_use_or_tried_any_drugs" class="divBarGroup">
                <asp:Label ID="lblHave_you_use_or_tried_any_drugs" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div id="tried_any_drugs">
                <table id="tblHave_you_use_or_tried_any_drugs">
                    <tr id="tr3_19">
                        <td class="trPath">
                            <asp:RadioButton ID="rdoHave_you_use_or_tried_any_drugs_No" runat="server" GroupName="Have_you_use_or_tried_any_drugs" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoHave_you_use_or_tried_any_drugs_Yes" runat="server" GroupName="Have_you_use_or_tried_any_drugs" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoHave_you_use_or_tried_any_drugs_Yes_But" runat="server" GroupName="Have_you_use_or_tried_any_drugs" />
                        </td>
                        <td class="trPath">
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="What_type_of_drugs_did_you_used" class="divBarGroup">
                <asp:Label ID="lblWhat_type_of_drugs_did_you_used" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div id="did_you_used">
                <table id="tblWhat_type_of_drugs_did_you_used">
                    <tr id="tr3_20">
                        <td class="trPath">
                            <asp:RadioButton ID="rdoWhat_type_of_drugs_did_you_used_Mari" runat="server" GroupName="What_type_of_drugs_did_you_used" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoWhat_type_of_drugs_did_you_used_Amp" runat="server" GroupName="What_type_of_drugs_did_you_used" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoWhat_type_of_drugs_did_you_used_Volatile" runat="server"
                                GroupName="What_type_of_drugs_did_you_used" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoWhat_type_of_drugs_did_you_used_Others" runat="server" GroupName="What_type_of_drugs_did_you_used" />
                            <asp:TextBox ID="txtWhat_type_of_drugs_did_you_used_other" runat="server" Width="300"></asp:TextBox>
                            <br />
                        </td>
                        <td class="trPath">
                        </td>
                    </tr>
                </table>
            </div>
            <div id="grpFamilyIllness" class="divBarMainTopic">
                <asp:Label ID="lblgrpFamilyIllness" runat="server" Text=""></asp:Label>
            </div>
            <br />
            <%-- 4.1 --%>
            <div id="Father" class="divBarGroup">
                <asp:Label ID="lblFather" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div>
                <table id="tblFather">
                    <tr>
                        <td class="trPath">
                            <asp:CheckBox ID="chkFather_None" runat="server" />
                        </td>
                        <td class="trPath">
                        </td>
                    </tr>
                </table>
                <div id="fatherHide">
                    <table>
                        <tr id="divFather">
                            <td class="trPath">
                                <asp:CheckBox ID="chkFather_Anemia" runat="server" /><br />
                                <asp:CheckBox ID="chkFather_Cancer" runat="server" /><br />
                                <asp:CheckBox ID="chkFather_Diabetes" runat="server" /><br />
                                <asp:CheckBox ID="chkFather_Asthma" runat="server" /><br />
                                <asp:CheckBox ID="chkFather_Others" runat="server" />
                                <asp:TextBox ID="txtFather_other_details" runat="server" Width="300"></asp:TextBox>
                            </td>
                            <td class="trPath">
                                <asp:CheckBox ID="chkfatherHigh_blood_pressure" runat="server" /><br />
                                <asp:CheckBox ID="chkFather_Allergy" runat="server" /><br />
                                <asp:CheckBox ID="chkFatherCardiovascular" runat="server" /><br />
                                <asp:CheckBox ID="chkFather_Tuberculosis" runat="server" />
                                <asp:CheckBox ID="h_16" runat="server" Style="display: none" /><br />
                                <asp:CheckBox ID="h_17" runat="server" Style="display: none" /><br />
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
            </div>
            <div id="Mother" class="divBarGroup">
                <asp:Label ID="lblMother" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div>
                <table id="tblMother">
                    <tr>
                        <td class="trPath">
                            <asp:CheckBox ID="chkMother_None" runat="server" />
                        </td>
                        <td class="trPath">
                        </td>
                    </tr>
                </table>
                <div id="MotherHide">
                    <table>
                        <tr id="divMother">
                            <td class="trPath">
                                <asp:CheckBox ID="chkMother_Anemia" runat="server" /><br />
                                <asp:CheckBox ID="chkMother_Cancer" runat="server" /><br />
                                <asp:CheckBox ID="chkMother_Diabetes" runat="server" /><br />
                                <asp:CheckBox ID="chkMother_Asthma" runat="server" /><br />
                                <asp:CheckBox ID="chkMother_Others" runat="server" />
                                <asp:TextBox ID="txtMother_other_details" runat="server" Width="300"></asp:TextBox><br />
                            </td>
                            <td class="trPath">
                                <asp:CheckBox ID="chkMotherHigh_blood_pressure" runat="server" /><br />
                                <asp:CheckBox ID="chkMother_Allergy" runat="server" /><br />
                                <asp:CheckBox ID="chkMotherCardiovascular" runat="server" /><br />
                                <asp:CheckBox ID="chkMother_Tuberculosis" runat="server" />
                                <asp:CheckBox ID="h_18" runat="server" Style="display: none" /><br />
                                <asp:CheckBox ID="h_19" runat="server" Style="display: none" /><br />
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
            </div>
            <div id="Siblings" class="divBarGroup">
                <asp:Label ID="lblSiblings" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div>
                <table id="tblSiblings">
                    <tr>
                        <td class="trPath">
                            <asp:CheckBox ID="chkSiblings_None" runat="server" />
                        </td>
                        <td class="trPath">
                        </td>
                    </tr>
                </table>
                <div id="siblingsHide">
                    <table>
                        <tr id="divSiblings">
                            <td class="trPath">
                                <asp:CheckBox ID="chkSiblings_Anemia" runat="server" /><br />
                                <asp:CheckBox ID="chkSiblings_Cancer" runat="server" /><br />
                                <asp:CheckBox ID="chkSiblings_Diabetes" runat="server" /><br />
                                <asp:CheckBox ID="chkSiblings_Asthma" runat="server" /><br />
                                <asp:CheckBox ID="chkSiblings_Others" runat="server" />
                                <asp:TextBox ID="txtSiblings_other_details" runat="server" Width="300"></asp:TextBox>
                            </td>
                            <td class="trPath">
                                <asp:CheckBox ID="chkSiblingsHigh_blood_pressure" runat="server" /><br />
                                <asp:CheckBox ID="chkSiblings_Allergy" runat="server" /><br />
                                <asp:CheckBox ID="chkSiblingsCardiovascular" runat="server" /><br />
                                <asp:CheckBox ID="chkSiblings_Tuberculosis" runat="server" />
                                <asp:CheckBox ID="h_20" runat="server" Style="display: none" /><br />
                                <asp:CheckBox ID="h_21" runat="server" Style="display: none" /><br />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="grpOtherHealthIssues" class="divBarMainTopic">
                <asp:Label ID="lblgrpOtherHealthIssues" runat="server" Text=""></asp:Label>
            </div>
            <br />
            <div id="What_is_your_favorite_food" class="divBarGroup">
                <asp:Label ID="lblWhat_is_your_favorite_food" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div class="height130">
                <table id="tblWhat_is_your_favorite_food">
                    <tr>
                        <td class="trPath">
                            <asp:CheckBox ID="chkWhat_is_your_favorite_food_Rice" runat="server" /><br />
                            <asp:CheckBox ID="chkWhat_is_your_favorite_food_Vegetable" runat="server" /><br />
                            <asp:CheckBox ID="chkWhat_is_your_favorite_food_Deep" runat="server" /><br />
                            <asp:CheckBox ID="chkWhat_is_your_favorite_food_Snack" runat="server" /><br />
                            <asp:CheckBox ID="chkWhat_is_your_favorite_food_Others" runat="server" />
                            <asp:TextBox ID="txtWhat_is_your_favorite_food_Others_details" runat="server" Width="300"></asp:TextBox>
                        </td>
                        <td class="trPath">
                            <asp:CheckBox ID="chkWhat_is_your_favorite_food_Fast_Food" runat="server" /><br />
                            <asp:CheckBox ID="chkWhat_is_your_favorite_food_Fish_Lean" runat="server" /><br />
                            <asp:CheckBox ID="chkWhat_is_your_favorite_food_Instant" runat="server" />
                            <asp:CheckBox ID="h_22" runat="server" Style="display: none" /><br />
                            <asp:CheckBox ID="h_23" runat="server" Style="display: none" /><br />
                            <asp:CheckBox ID="h_24" runat="server" Style="display: none" /><br />
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            <br />
            <%-- 5.2 --%>
            <div id="Do_you_exercise_play_sports" class="divBarGroup">
                <asp:Label ID="lblDo_you_exercise_play_sports" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblDo_you_exercise_play_sports">
                <tr>
                    <td class="trPath">
                        <asp:RadioButton ID="rdoDo_you_exercise_play_sports_No" runat="server" GroupName="Do_you_exercise_play_sports" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoDo_you_exercise_play_sports_Less_than_3_times" runat="server"
                            GroupName="Do_you_exercise_play_sports" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdo_you_exercise_play_sports_More_than_3_times" runat="server"
                            GroupName="Do_you_exercise_play_sports" />
                    </td>
                    <td class="trPath">
                    </td>
                </tr>
            </table>
            <br />
            <div id="What_is_your_exercise_duration" class="divBarGroup">
                <asp:Label ID="lblWhat_is_your_exercise_duration" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <div id="exercise_duration">
                <table id="tblWhat_is_your_exercise_duration">
                    <tr id="tr5_3">
                        <td class="trPath">
                            <asp:RadioButton ID="rdoWhat_is_your_exercise_duration_Less_than_30" runat="server"
                                GroupName="What_is_your_exercise_duration" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rdoWhat_is_your_exercise_duration_Over_than_30" runat="server"
                                GroupName="What_is_your_exercise_duration" />
                        </td>
                        <td class="trPath">
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="Do_you_want_to_declare_personal_history" class="divBarGroup">
                <asp:Label ID="lblDo_you_want_to_declare_personal_history" runat="server" Text=""
                    class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblDo_you_want_to_declare_personal_history">
                <tr>
                    <td class="trPath">
                        <asp:RadioButton ID="rdoDo_you_want_to_declare_personal_history_No" GroupName="5_3_1"
                            runat="server" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoDo_you_want_to_declare_personal_history_Yes" GroupName="5_3_1"
                            runat="server" />
                        <br />
                        <br />
                    </td>
                    <td class="trPath">
                    </td>
                </tr>
            </table>
            <div id="divFinalTable" >
                <table id="final_table" border="0" cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col span="7" style="width: 53pt" width="70" />
                    </colgroup>
                    <tr style="height: 34.2pt; text-align: center;">
                        <td height="118" rowspan="3" width="70" style="background-color: #91c5d4; text-align: center;">
                            <asp:Label ID="lblDDMM" runat="server" Text=""></asp:Label>
                            &nbsp;
                        </td>
                        <td rowspan="3" width="70" style="background-color: #91c5d4; text-align: center;">
                            <asp:Label ID="lblPartOF" runat="server" Text=""></asp:Label>
                            &nbsp;
                        </td>
                        <td rowspan="3" width="70" style="background-color: #91c5d4; text-align: center;">
                            <asp:Label ID="lblCause" runat="server" Text=""></asp:Label>
                            &nbsp;
                        </td>
                        <td colspan="4" width="280" style="background-color: #91c5d4; text-align: center;">
                            <asp:Label ID="lblServerity" runat="server" Text=""></asp:Label>
                            &nbsp;
                        </td>
                    </tr>
                    <tr style="height: 25.2pt; text-align: center;">
                        <td height="72" rowspan="2" style="background-color: #91c5d4; text-align: center;">
                            <asp:Label ID="lblDisabled" runat="server" Text=""></asp:Label>
                            &nbsp;
                        </td>
                        <td rowspan="2" style="background-color: #91c5d4; text-align: center;">
                            <asp:Label ID="lblLossof" runat="server" Text=""></asp:Label>
                            &nbsp;
                        </td>
                        <td colspan="2" style="background-color: #91c5d4; text-align: center;">
                            <asp:Label ID="lblTemporary" runat="server" Text=""></asp:Label>
                            &nbsp;
                        </td>
                    </tr>
                    <tr style="height: 28.2pt; text-align: center;">
                        <td height="38" style="background-color: #91c5d4; text-align: center;">
                            <asp:Label ID="lblLess_than_3" runat="server" Text=""></asp:Label>
                            &nbsp;
                        </td>
                        <td style="background-color: #91c5d4; text-align: center;">
                            <asp:Label ID="lblMore_than_3" runat="server" Text=""></asp:Label>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                           <asp:TextBox ID="txtDDMMYY_1" runat="server" Width="130" class="inputbox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtInjury_1" runat="server" Width="130" class="inputbox"></asp:TextBox>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtCause_of_injury_1" runat="server" Width="130" class="inputbox"></asp:TextBox>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtDisabled_1" runat="server" Width="130" class="inputbox"></asp:TextBox>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtLimbs_1" runat="server" Width="130" class="inputbox"></asp:TextBox>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtLessThan_1" runat="server" Width="130" class="inputbox"></asp:TextBox>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtMoreThan_1" runat="server" Width="130" class="inputbox"></asp:TextBox>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        
                         <td>
                            <asp:TextBox ID="txtDDMMYY_2" runat="server" Width="130" class="inputbox"></asp:TextBox>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtInjury_2" runat="server" Width="130" class="inputbox"></asp:TextBox>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtCause_of_injury_2" runat="server" Width="130" class="inputbox"></asp:TextBox>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtDisabled_2" runat="server" Width="130" class="inputbox"></asp:TextBox>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtLimbs_2" runat="server" Width="130" class="inputbox"></asp:TextBox>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtLessThan_2" runat="server" Width="130" class="inputbox"></asp:TextBox>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtMoreThan_2" runat="server" Width="130" class="inputbox"></asp:TextBox>&nbsp;
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            <br />
            <div id="menstrual_periods" class="divBarGroup">
                <asp:Label ID="lblMenstrual_periods" runat="server" Text="" class="subGroupLeft"></asp:Label>
            </div>
            <table id="tblMenstrual_periods">
                <tr>
                    <td class="trPath">
                        <asp:RadioButton ID="rdoMenstrual_periods_yes" runat="server" GroupName="Menstrual_periods" />
                        <br />
                        <br />
                        <asp:RadioButton ID="rdoMenstrual" runat="server" GroupName="Menstrual_periods" />
                    </td>
                    <td class="trPath">
                    </td>
                </tr>
            </table>
            <br />
            <div id="woman">
                <div id="For_women" class="divBarGroup">
                    <asp:Label ID="lblFor_women" runat="server" Text="" class="subGroupLeft"></asp:Label>
                </div>
                <table id="tblwoman">
                    <tr>
                        <td>
                            <asp:Label ID="lblMenoFirstDate" runat="server" Text=""></asp:Label>
                            <asp:TextBox ID="txtMenoDateForm" runat="server" class="inputboxOthers" Width="80"></asp:TextBox>
                            <asp:Label ID="lblMenoToDate" runat="server" Text=""></asp:Label>
                            <asp:TextBox ID="txtMenoDateTo" runat="server" class="inputboxOthers" Width="80"></asp:TextBox>
                            <br />
                            <asp:Label ID="lblCharacteristic" runat="server" Text=""></asp:Label>
                            <asp:RadioButton ID="rdoCharacteristicNormal" runat="server" GroupName="Character" />
                            <asp:Label ID="lblCharacteristicNomal" runat="server" Text=""></asp:Label>
                            <asp:RadioButton ID="rdoCharacteristicABNormal" runat="server" GroupName="Character" />
                            <asp:Label ID="lblCharacteristicAbnomal" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblPreg" runat="server" Text=""></asp:Label>
                            <asp:RadioButton ID="rdoPreNo" runat="server" GroupName="Character" />
                            <asp:RadioButton ID="rdoPregnacy" runat="server" GroupName="Character" />
                            <asp:RadioButton ID="rdoPregSuspect" runat="server" GroupName="Character" />
                            <br />
                            <asp:Label ID="lbldelivered" runat="server" Text=""></asp:Label>
                            <asp:RadioButton ID="rdoDelivered_Yes" runat="server" GroupName="Delivered" />
                            <asp:RadioButton ID="rdodelivered_No" runat="server" GroupName="Delivered" />
                            <asp:RadioButton ID="rdodelivered_not_sure" runat="server" GroupName="Delivered" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblperiod_is_over_7_days" runat="server" Text=""></asp:Label>
                            <br />
                            <asp:Label ID="lblperiod_is_less_7_days" runat="server" Text=""></asp:Label>
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="btnSave_Data" style="float: right;">
                <asp:Button ID="btnSave" runat="server" Text="Submit Medicine Checkup Record" Width="250"
                    class="buttonRec" OnClick="btnSave_Click" />
            </div>
            <div id="btnSave_Draft" style="float: left;">
                <asp:Button ID="btnSaveDraft" runat="server" Text="Save Draft Medicine Checkup Record"
                    Width="250" class="buttonRec" OnClick="btnSaveDraft_Click" />
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
    </form>
    </td></tr></table>
</body>
</html>
