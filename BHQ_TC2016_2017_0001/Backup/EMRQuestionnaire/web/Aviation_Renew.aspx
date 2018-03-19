<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Aviation_Renew.aspx.cs" Inherits="EMRQuestionnaire.web.Aviation_Renew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<link href="/css/FontsThai/fontsthaisans_neueregular.css" rel="stylesheet" type="text/css" />
<link href="/css/maincss.css" rel="stylesheet" type="text/css" />
<link rel="shortcut icon" href="../images/quiz-games-300x300.png" />

<link href="/datePicker/jquery-ui.css" rel="stylesheet" type="text/css" />

<script src="/datePicker/external/jquery/jquery.js" type="text/javascript"></script>
<script src="/datePicker/jquery-ui.js" type="text/javascript"></script>

<script src="/scripts/aviation_renew.js" type="text/javascript"></script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="headAviationInitial" runat="server">
    <title>Application for Renewal Medial Certificate</title>
    <style type="text/css">
        .style1
        {
            height: 319px;
        }
        </style>

    <script type="text/javascript" language="javascript">
        function rdLicenseType_CheckedChanged(clic) {
          
            if (clic == "rdOther") {
                            document.getElementById('txtOtherLicense').disabled = false;                            
                        }
                        else {

                            document.getElementById('txtOtherLicense').disabled = true;
                            document.getElementById('txtOtherLicense').value = "";
                        }
                    }
         function rdResultMedical_CheckedChanged(clic) {

             if (clic == "rdMedicalYes") {
                 document.getElementById('txtMedicalIfYes').disabled = false;
                 document.getElementById('txtMedicalQuantity').disabled = false;
                 document.getElementById('txtMedicalReason').disabled = false;
                        }
                        else {

                            document.getElementById('txtMedicalIfYes').disabled = true;
                            document.getElementById('txtMedicalQuantity').disabled = true;
                            document.getElementById('txtMedicalReason').disabled = true;
                            document.getElementById('txtMedicalIfYes').value = "";
                            document.getElementById('txtMedicalQuantity').value = "";
                            document.getElementById('txtMedicalReason').value = "";
                        }
                    }
    </script>
</head>
<body>
    <table border ="0" cellpadding ="0" cellspacing="0" width="100%"  > <tr> 
        <td  align ="center" class="style1">
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
                <table id="Table1">
                    <tr>
                        <td  style="width:50%" class="trPath">
                            <table  border ="0" cellpadding ="0" cellspacing="0"><tr>
                                <td style="width:150px"><asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>
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
                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
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
                    <asp:Label ID="lblRecommendation" runat="server" Text=""></asp:Label>
                </div>  
                <table id="Patient" >
                    <tr>
                        <td style="width:100%; padding-left:10px;" align="left">
                            <asp:Label ID="lblRecommendationRule" runat="server" Text=""></asp:Label>                            
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%" align="center">
                            <table id="PatientInfo" width="100%" cellspacing="0" >
                                <tr><td style="width:100%;"><table id="PlaceNameNationality" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width:250px;" align="left" valign="top" >
                                            <asp:Label ID="lblPlace" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="left" valign="middle">
                                            <asp:TextBox ID="txtplace" runat="server" Width="300" class="inputbox"></asp:TextBox>
                                        </td>
                                        <td style="width:auto;" align="left" valign="top">
                                            <asp:Panel ID="plGender" runat="server" Font-Size="Small" BorderStyle="Solid" 
                                                BorderColor="#0099FF" BorderWidth="1" Width="285px">
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
                                        <td style="width:250px;" align="left" valign="middle">
                                            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                        </td>                                    
                                        <td style="width:auto;" align="left" valign="middle">
                                            <asp:TextBox ID="txtname_th" runat="server" Width="300" class="inputbox"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="lblDOB" runat="server" Text=""></asp:Label>
                                            &nbsp;<asp:TextBox ID="txtDOB" runat="server" Width="180px" class="inputbox"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width:250px;" align="left" valign="middle">
                                            <asp:Label ID="lblNationality" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="left" valign="middle">
                                            <asp:TextBox ID="txtnation_th" runat="server" Width="300" class="inputbox"></asp:TextBox>
                                        </td>
                                        <td style="width:auto;" align="left" valign="middle">
                                            <asp:Label ID="lblAge" runat="server" Text=""></asp:Label>
                                            &nbsp;<asp:TextBox ID="txtAgeYear" runat="server" Width="100" class="inputbox"></asp:TextBox>&nbsp;<asp:Label ID="lblAgeYear" runat="server" Text=""></asp:Label>&nbsp;<asp:TextBox ID="txtAgeMonth" runat="server" Width="100" class="inputbox"></asp:TextBox>&nbsp;<asp:Label ID="lblAgeMonth" runat="server" Text=""></asp:Label></td>
                                    </tr>                                    
                                </table></td></tr>                                
                                <tr><td style="width:100%;"><table id="Address" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top" style="width:250px;" rowspan="2">
                                            <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="left" valign="top" >
                                            <asp:Panel ID="plAddress" runat="server" Width="100%" Font-Size="Small"> 
                                                <asp:RadioButton ID="rdAddress_S" runat="server" GroupName="Address"/>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="rdAddress_C" runat="server" GroupName="Address"/>
                                            </asp:Panel>
                                        </td>                                    
                                    </tr>
                                    <tr>
                                        <td style="width:auto;" align="left" valign="top" >
                                            <asp:TextBox ID="txtAddressChanged" runat="server" Width="600px" 
                                                class="inputbox" Height="60px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                
                                <tr><td style="width:100%;"><table id="Telephone" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Label ID="lblTel" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="left" valign="top" >
                                            <asp:TextBox ID="txtTel" runat="server" Width="300px" class="inputbox"></asp:TextBox>
                                        </td>
                                    </tr>                                    
                                </table></td></tr>
                                <tr><td style="width:100%;"><table id="TOL" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Label ID="lblLicenseType" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:auto;"><table id="LicenseType" width="100%" 
                                            cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left" valign="top">
                                                    <asp:RadioButton ID="rdATPL" runat="server" Font-Size="Small"
                                                        onclick="rdLicenseType_CheckedChanged(this.id);" GroupName="LicenseType" 
                                                       /> 
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:RadioButton ID="rdCPL" Font-Size="Small" runat="server"  onclick="rdLicenseType_CheckedChanged(this.id);"
                                                         GroupName="LicenseType" 
                                                         />
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:RadioButton ID="rdPPL" Font-Size="Small" runat="server"  onclick="rdLicenseType_CheckedChanged(this.id);"
                                                         GroupName="LicenseType" 
                                                         />
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:RadioButton ID="rdStudent" Font-Size="Small" runat="server"  onclick="rdLicenseType_CheckedChanged(this.id);"
                                                         GroupName="LicenseType" 
                                                         />
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:RadioButton ID="rdATC" Font-Size="Small" runat="server"  onclick="rdLicenseType_CheckedChanged(this.id);"
                                                         GroupName="LicenseType" 
                                                        />
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:RadioButton ID="rdPE" Font-Size="Small" runat="server"  onclick="rdLicenseType_CheckedChanged(this.id);"
                                                        GroupName="LicenseType" 
                                                        />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top" colspan="6">
                                                    <asp:RadioButton ID="rdOther" runat="server" Font-Size="Small"  onclick="rdLicenseType_CheckedChanged(this.id);"
                                                        GroupName="LicenseType"  />
                                                    &nbsp;<asp:TextBox ID="txtOtherLicense" runat="server" Width="400px" 
                                                        class="inputbox"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table></td>
                                    </tr>                                    
                                </table></td></tr>
                                <tr><td style="width:100%;"><table id="LicenseNo" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Label ID="lblLicenseNo" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="left" valign="top" colspan="7">
                                            <asp:TextBox ID="txtLicenseNo" runat="server" Width="300px" class="inputbox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:250px;" align="left" valign="bottom">
                                            <asp:Label ID="lblFlyTime" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="left" valign="bottom">
                                            <asp:Label ID="lblForceInputNum1" Font-Size="Small" ForeColor="Red" runat="server" Text=""></asp:Label>
                                            <br /><asp:TextBox ID="txtFlyTime" runat="server" Width="100px" class="inputbox"></asp:TextBox>
                                        </td>
                                        <td style="width:auto;" align="left" valign="bottom">
                                            <asp:Label ID="lblFlyTimeHour" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="right" valign="bottom" >
                                            <asp:Label ID="lblLast6Month" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="left" valign="bottom">
                                            <asp:Label ID="lblForceInputNum2" Font-Size="Small" ForeColor="Red" runat="server" Text=""></asp:Label>
                                            <br /><asp:TextBox ID="txtLast6Month" runat="server" Width="100px" class="inputbox"></asp:TextBox>
                                        </td>
                                        <td style="width:auto;" align="right" valign="bottom" >
                                            <asp:Label ID="lblFlyTimeHou2" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="right" valign="bottom" >
                                            <asp:Label ID="lblACType" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="left" valign="bottom">
                                            <asp:TextBox ID="txtACType" runat="server" Width="150px" class="inputbox"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table></td></tr>  
                                <tr><td style="width:100%;"><table id="LastMedicalExam" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Label ID="lblLastMedicalExam" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="left" valign="top" colspan="7">
                                            <asp:TextBox ID="txtLastMedicalExam" runat="server" Width="300px" class="inputbox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:250px;" align="left" valign="middle">
                                            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="left" valign="top" >
                                            <asp:Panel ID="plLastMedicalExam" runat="server" Width="100%" Font-Size="Small">
                                                <asp:RadioButton ID="rdLastMedicalExam_F" runat="server" GroupName="LastMedicalExam"/>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="rdLastMedicalExam_U" runat="server" GroupName="LastMedicalExam"/>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="rdLastMedicalExam_L" runat="server" GroupName="LastMedicalExam"/>
                                            </asp:Panel>
                                        </td> 
                                    </tr>
                                </table></td></tr> 
                                <tr><td style="width:100%;"><table id="CurrentlyMedical" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top" colspan="6">
                                            <asp:Label ID="lblCurrentlyMedical" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:250px;" align="left" valign="top" rowspan="3">
                                            <asp:Label ID="lblResultMedical" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="left" valign="top" colspan="5">
                                            <asp:RadioButton ID="rdMedicalNo" runat="server" Font-Size="Small" GroupName="ResultMedical" 
                                                onclick="rdResultMedical_CheckedChanged(this.id);" />                                                 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:auto;" align="left" valign="top">
                                            <asp:RadioButton ID="rdMedicalYes" runat="server" Font-Size="Small" GroupName="ResultMedical" 
                                                onclick="rdResultMedical_CheckedChanged(this.id);" />
                                        </td>
                                        <td style="width:auto;" align="right" valign="top">
                                            <asp:Label ID="lblMedicalIfYes" runat="server" Text="" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="left" valign="top" colspan="3">
                                            <asp:TextBox ID="txtMedicalIfYes" runat="server" Width="390px" class="inputbox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:auto;" align="right" valign="top" colspan="2">
                                            <asp:Label ID="lblQuantity" runat="server" Text="" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="left" valign="top">
                                            <asp:TextBox ID="txtMedicalQuantity" runat="server" Width="70px" 
                                                class="inputbox"></asp:TextBox>
                                        </td>
                                        <td style="width:auto;" align="right" valign="top">
                                            <asp:Label ID="lblReason" runat="server" Text="" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td style="width:auto;" align="left" valign="top">
                                            <asp:TextBox ID="txtMedicalReason" runat="server" Width="200px" 
                                                class="inputbox"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table></td></tr>                                                                                     
                            </table>
                        </td>
                    </tr>                    
                    <tr>
                        <td style="width:100%;" align="left">
                            <div id="divMedicalHistory" class="divBarGroup">
                                <asp:Label ID="lblMedicalHistory" runat="server" Text=""></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%; padding-left:10px;" align="left">                            
                            <asp:Label ID="lblMedicalHistoryInfo" runat="server" Text=""></asp:Label>                            
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%" align="center">
                            <table id="MedicalHistory" width="100%" cellspacing="0" border="1">
                                <tr><td style="width:100%;" ><table id="MainQuestion14" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion14" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion14" runat="server" Width="100%" Font-Size="Small">
                                                <asp:RadioButton ID="rdMainQuestion14_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion14"/>
                                                <asp:RadioButton ID="rdMainQuestion14_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion14"/>
                                                <div id="divQuestion14Yes">
                                                <asp:TextBox ID="txtQuestion14Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>   
                                                </div>                                                 
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion15" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion15" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion15" runat="server" Width="100%" Font-Size="Small">
                                                <asp:RadioButton ID="rdMainQuestion15_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion15"/>
                                                <asp:RadioButton ID="rdMainQuestion15_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion15"/>
                                                <div id="divQuestion15Yes">
                                                <asp:TextBox ID="txtQuestion15Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
                                <tr><td style="width:100%;" ><table id="MainQuestion16" width="100%" 
                                        cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMainQuestion16" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width:250px;" align="left" valign="top">
                                            <asp:Panel ID="plMainQuestion16" runat="server" Width="100%" Font-Size="Small">
                                                <asp:RadioButton ID="rdMainQuestion16_no" runat="server" Width="120px"
                                                    GroupName="MainQuestion16"/>
                                                <asp:RadioButton ID="rdMainQuestion16_yes" runat="server" Width="120px"
                                                    GroupName="MainQuestion16"/>
                                                <div id="divQuestion16Yes">
                                                <asp:TextBox ID="txtQuestion16Yes" runat="server" Width="240px" class="inputbox"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table></td></tr>
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
                                                <asp:TextBox ID="txtQuestion18Yes" runat="server" Width="240px" class="inputbox" ></asp:TextBox>
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
            alls only)
            </div>   
            </div>      
            </div>
        </div>
        </form>
    </td></tr>
    </table>
</body>
</html>
