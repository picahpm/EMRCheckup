
$(document).ready(function () {
    $("#txtDateFormHH").datepicker({ dateFormat: 'dd/mm/yy', inline: true
    });
    $("#txtDateToHH").datepicker({ dateFormat: 'dd/mm/yy', inline: true
    });
    $("#txtNonSmokingButBeSecondHand").val("").prop("disabled", true);
    $("#txtSpecify").val("").prop("disabled", true);
    $("#txtAmount").val("").prop("disabled", true);
    $("#txtDuration").val("").prop("disabled", true);
    $("#txtQuitAlcohol").val("").prop("disabled", true);
    $("#txtAnnualCheckUpOthers").val("").prop("disabled", true);
    $("#txtPhycho").val("").prop("disabled", true);
    $("#txtCancerType").val("").prop("disabled", true);
    $("#txtOtherMedicalHistory").val("").prop("disabled", true);
    $("#txtCurrentMedicineOthers").val("").prop("disabled", true);
    $("#txtAllergyHave").val("").prop("disabled", true);
    $("#txtPastIllessOrhospitalAdmissionOthers").val("").prop("disabled", true);
    $("#txtPastSurgeryYesOthers").val("").prop("disabled", true);
    $("#txtFatherOthers").val("").prop("disabled", true);
    $("#txtFatherCancer").val("").prop("disabled", true);
    $("#txtMotherOthers").val("").prop("disabled", true);
    $("#txtMotherCancer").val("").prop("disabled", true);
    $("#txtRelativesOthers").val("").prop("disabled", true);
    $("#txtRelativesCancer").val("").prop("disabled", true);
    $("#txtDateFormHH").val("").prop("disabled", true);
    $("#txtDateToHH").val("").prop("disabled", true);
    $("#txtTextRight").val("").prop("disabled", true);
    $("#MedicalHistory").hide();
    $("#CurrentMedicine").hide();
    $("#FatherHide").hide();
    $("#MotherHide").hide();
    $("#RelativesHide").hide();
    $("#rdoNonSmoking").click(function () {
        $("#txtNonSmokingButBeSecondHand").val("").prop("disabled", true);
        $("#txtSpecify").val("").prop("disabled", true);
        $("#txtAmount").val("").prop("disabled", true);
        $("#txtDuration").val("").prop("disabled", true);
    });
    $("#rdoNonSmokingButBeSecondHand").click(function () {
        $("#txtNonSmokingButBeSecondHand").prop("disabled", false);
        $("#txtSpecify").val("").prop("disabled", true);
        $("#txtAmount").val("").prop("disabled", true);
        $("#txtDuration").val("").prop("disabled", true);
    });
    $("#rdoOutSmoking").click(function () {
        $("#txtNonSmokingButBeSecondHand").val("").prop("disabled", true);
        $("#txtAmount").val("").prop("disabled", true);
        $("#txtDuration").val("").prop("disabled", true);
        $("#txtSpecify").prop("disabled", false);
    });
    $("#rdoSmoke").click(function () {
        $("#txtNonSmokingButBeSecondHand").val("").prop("disabled", true);
        $("#txtSpecify").val("").prop("disabled", true);
        $("#txtAmount").prop("disabled", false);
        $("#txtDuration").prop("disabled", false);
    });

    $("#rdoNoneAlcohol").click(function () {
        $("#txtQuitAlcohol").val("").prop("disabled", true);
        $('input[name="SocialDrinkPerMonth"]').prop('checked', false).prop("disabled", true);
    });
    $("#rdoQuitAlcohol").click(function () {
        $("#txtQuitAlcohol").prop("disabled", false);
        $('input[name="SocialDrinkPerMonth"]').prop('checked', false).prop("disabled", true);
    });
    $("#rdoSocialDrink").click(function () {
        $("#txtQuitAlcohol").val("").prop("disabled", true);
        $('input[name="SocialDrinkPerMonth"]').prop("disabled", false);
    });
    $("#rdoDrinkFourPerWeek").click(function () {
        $('input[name="SocialDrinkPerMonth"]').prop('checked', false).prop("disabled", true);
        $("#txtQuitAlcohol").val("").prop("disabled", true);
    });

    $("#rdoAnnualCheckUp").click(function () {
        $("#txtAnnualCheckUpOthers").val("").prop("disabled", true);
    }); $("#rdoAnnualCheckUpOthers").click(function () {
        $("#txtAnnualCheckUpOthers").prop("disabled", false);
    });

    $("#rdoNonePhycho").click(function () {
        $("#txtPhycho").val("").prop("disabled", true);
    }); $("#rdoPhychoOthers").click(function () {
        $("#txtPhycho").prop("disabled", false);
    });

    $("#rdoMedicalHistoryNoDiseases").click(function () {
        $("#MedicalHistory").slideUp("slow", function () {
            $("#MedicalHistory input[type=text], #MedicalHistory select, #MedicalHistory textarea").val("");
            $("#MedicalHistory input[type=radio], #MedicalHistory input[type=checkbox]").prop("checked", false);

        });
    });
    $("#rdoMedicalDiseases").click(function () {
        $("#MedicalHistory").slideDown("slow");

    });
    $("#rdoCurrentMedicineNone").click(function () {
        $("#CurrentMedicine").slideUp("slow", function () {
            $("#CurrentMedicine input[type=text], #CurrentMedicine select, #MedicalHistory textarea").val("");
            $("#CurrentMedicine input[type=radio], #CurrentMedicine input[type=checkbox]").prop("checked", false);
            $("#txtCancerType").prop("disabled", true);
        });
    });
    $("#rdoCurrentMedicineHave").click(function () {
        $("#CurrentMedicine").slideDown("slow");
        $("#CurrentMedicine").prop("disabled", false);
    });

    $("#rdoAllergyNone").click(function () {
        $("#txtAllergyHave").val("").prop("disabled", true);
    });
    $("#rdoAllergyNotsure").click(function () {
        $("#txtAllergyHave").val("").prop("disabled", true);
    });
    $("#rdoAllergyHave").click(function () {
        $("#txtAllergyHave").prop("disabled", false);
    });

    $("#rdoPastIllessOrhospitalAdmissionNo").click(function () {
        $("#txtPastIllessOrhospitalAdmissionOthers").val("").prop("disabled", true);
    }); $("#rdoPastIllessOrhospitalAdmissionYes").click(function () {
        $("#txtPastIllessOrhospitalAdmissionOthers").prop("disabled", false);
    });

    $("#rdoPastSurgeryNo").click(function () {
        $("#txtPastSurgeryYesOthers").val("").prop("disabled", true);
    }); $("#rdoPastSurgeryYes").click(function () {
        $("#txtPastSurgeryYesOthers").prop("disabled", false);
    });
    $("#rdoFatherNodisease").click(function () {
        $("#FatherHide").slideUp("slow", function () {
            $("#FatherHide input[type=text], #FatherHide select, #MedicalHistory textarea").val("");
            $("#FatherHide input[type=radio], #FatherHide input[type=checkbox]").prop("checked", false);
        });
    });
    $("#rdoFatherDisease").click(function () {
        $("#FatherHide").slideDown("slow");
    });
    $("#rdoMotherNodisease").click(function () {
        $("#MotherHide").slideUp("slow", function () {
            $("#MotherHide input[type=text], #MotherHide select, #MedicalHistory textarea").val("");
            $("#MotherHide input[type=radio], #MotherHide input[type=checkbox]").prop("checked", false);
        });
    });
    $("#rdoMotherDisease").click(function () {
        $("#MotherHide").slideDown("slow");
    });

    $("#rdoRelativesNodisease").click(function () {
        $("#RelativesHide").slideUp("slow", function () {
            $("#RelativesHide input[type=text], #RelativesHide select, #MedicalHistory textarea").val("");
            $("#RelativesHide input[type=radio], #RelativesHide input[type=checkbox]").prop("checked", false);
        });
    });
    $("#rdoRelativesDisease").click(function () {
        $("#RelativesHide").slideDown("slow");
    });
    $("#chkMonoPause").click(function () {
        $("#rdoMonoStart").prop("disabled", false).prop("checked", false);
        $("#rdoNotsure").prop("disabled", false).prop("checked", false);
        $("#txtDateFormHH").val("").prop("disabled", true);
        $("#txtDateToHH").val("").prop("disabled", true);
    });
    $("#rdoMonoStart").click(function () {
        $("#chkMonoPause").prop("disabled", false).prop("checked", false); ;
        $("#txtDateFormHH").val("").prop("disabled", false);
        $("#txtDateToHH").val("").prop("disabled", false);
    });
    $("#rdoNotsure").click(function () {
        $("#chkMonoPause").prop("disabled", false).prop("checked", false);
        $("#txtDateFormHH").val("").prop("disabled", true);
        $("#txtDateToHH").val("").prop("disabled", true);
    });
    $("#txtDateFormHH").datepicker({ dateFormat: 'dd-mm-yy', inline: true });
    $("#txtDateToHH").datepicker({ dateFormat: 'dd-mm-yy', inline: true });
        
    $("#chkcancer").click(function () { $("#txtCancerType").val("").prop("disabled", false); });
    $("#chkDiseaseOthers").click(function () { $("#txtOtherMedicalHistory").val("").prop("disabled", false); });
    $("#chkCurrentMedicineOthers").click(function () { $("#txtCurrentMedicineOthers").val("").prop("disabled", false); });
    $("#chkFatherOther").click(function () { $("#txtFatherOthers").val("").prop("disabled", false); });
    $("#chkFatherCancer").click(function () { $("#txtFatherCancer").val("").prop("disabled", false); });
    $("#chkMotherOther").click(function () { $("#txtMotherOthers").val("").prop("disabled", false); });
    $("#chkMotherCancer").click(function () { $("#txtMotherCancer").val("").prop("disabled", false); });
    $("#chkRelativesOther").click(function () { $("#txtRelativesOthers").val("").prop("disabled", false); });
    $("#chkRelativesCancer").click(function () { $("#txtRelativesCancer").val("").prop("disabled", false); });
});
