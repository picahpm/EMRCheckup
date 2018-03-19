
$(document).ready(function() {
    $("#txtYesDetailOperation").prop("disabled", true);
    $("#txtAre_you_allergic_othersDetails").prop("disabled", true);
    $("#txtNonSmokingButBeSecondHand").prop("disabled", true);
    $("#txtWhat_type_of_drugs_did_you_used_other").prop("disabled", true);
    $("#txtOtherClassificationofEmployment").prop("disabled", true);
    $("#txtOtherSpecialAssignment").prop("disabled", true);
    $("#txtOthersHazard").prop("disabled", true);
    $("#txtOthersBiologicalHealtHazard").prop("disabled", true);
    $("#txtOtherChemicalhealthhazard").prop("disabled", true);
    $("#txtPPEOtherDetails").prop("disabled", true);
    $("#txtOtherDo_you_needDetailsTaking").prop("disabled", true);
    $("#txtOthers_please_specify").prop("disabled", true);
    $("#txtFather_other_details").prop("disabled", true);
    $("#txtMother_other_details").prop("disabled", true);
    $("#txtSiblings_other_details").prop("disabled", true);
    $("#txtWhat_is_your_favorite_food_Others_details").prop("disabled", true);
    $("#sub_Have_you_ever_had_any_severe_illness").hide();
    $("#underlying_deceases").hide();
    $("#immunity").hide();
    $("#divFinalTable").hide();
    $("#rdoOperationNo").click(function() {
        $("#txtYesDetailOperation").val("").prop("disabled", true);
    }); $("#rdoOperationYes").click(function() {
        $("#txtYesDetailOperation").prop("disabled", false);
    });

    $("#rdoUnderlying_deceases_No").click(function() {
        $("#underlying_deceases").slideUp("slow", function() {
            $("#underlying_deceases input[type=text], #underlying_deceases select, #underlying_deceases textarea").val("");
            $("#underlying_deceases input[type=radio], #underlying_deceases input[type=checkbox]").prop("checked", false);

        });
    });
    $("#rdoUnderlying_deceases_Yes").click(function() {
        $("#underlying_deceases").slideDown("slow");

    });

    $("#rdoVaccination_or_immunity_No").click(function() {
        $("#immunity").slideUp("slow", function() {
            $("#immunity input[type=text], #immunity select, #immunity textarea").val("");
            $("#immunity input[type=radio], #immunity input[type=checkbox]").prop("checked", false);

        });
    });
    $("#rdoVaccination_or_immunity_Yes").click(function() {
        $("#immunity").slideDown("slow");

    });
    $("#rdoWhat_type_of_drugs_did_you_used_Mari").click(function() {
        $("#txtWhat_type_of_drugs_did_you_used_other").val("").prop("disabled", true);
    }); $("#rdoWhat_type_of_drugs_did_you_used_Amp").click(function() {
        $("#txtWhat_type_of_drugs_did_you_used_other").val("").prop("disabled", true);
    }); $("#rdoWhat_type_of_drugs_did_you_used_Volatile").click(function() {
        $("#txtWhat_type_of_drugs_did_you_used_other").val("").prop("disabled", true);
    }); $("#rdoWhat_type_of_drugs_did_you_used_Others").click(function() {
        $("#txtWhat_type_of_drugs_did_you_used_other").prop("disabled", false);
    });


    $("#chkFather_None").click(function() {
        $("#fatherHide").slideToggle("slow", function() {
            $("#fatherHide input[type=text], #fatherHide select, #fatherHide textarea").val("");
            $("#fatherHide input[type=radio], #fatherHide input[type=checkbox]").prop("checked", false);

        });
    });

    $("#chkMother_None").click(function() {
        $("#MotherHide").slideToggle("slow", function() {
            $("#MotherHide input[type=text], #MotherHide select, #MotherHide textarea").val("");
            $("#MotherHide input[type=radio], #MotherHide input[type=checkbox]").prop("checked", false);

        });
    });
    $("#chkSiblings_None").click(function() {
        $("#siblingsHide").slideToggle("slow", function() {
            $("#siblingsHide input[type=text], #siblingsHide select, #siblingsHide textarea").val("");
            $("#siblingsHide input[type=radio], #siblingsHide input[type=checkbox]").prop("checked", false);

        });
    });

    $("#rdoDo_you_want_to_declare_personal_history_No").click(function() {
        $("#divFinalTable").slideUp("slow", function() {
            $("#divFinalTable input[type=text], #divFinalTable select, #divFinalTable textarea").val("");
            $("#divFinalTable input[type=radio], #divFinalTable input[type=checkbox]").prop("checked", false);

        });
    });
    $("#rdoDo_you_want_to_declare_personal_history_Yes").click(function() {
        $("#divFinalTable").slideDown("slow");

    });



    $("#rdoHave_llness_No").click(function() {
        $("#sub_Have_you_ever_had_any_severe_illness").slideUp("slow", function() {
            $("#sub_Have_you_ever_had_any_severe_illness input[type=text], #sub_Have_you_ever_had_any_severe_illness select, #sub_Have_you_ever_had_any_severe_illness textarea").val("");
            $("#sub_Have_you_ever_had_any_severe_illness input[type=radio], #sub_Have_you_ever_had_any_severe_illness input[type=checkbox]").prop("checked", false);

        });
    });
    $("#rdoHave_llness_Yes").click(function() {
        $("#sub_Have_you_ever_had_any_severe_illness").slideDown("slow");

    });

    $("#rdoAre_you_allergic_no").click(function() {
        $("#txtAre_you_allergic_othersDetails").val("").prop("disabled", true);
    }); $("#rdoAre_you_allergic_not_sure").click(function() {
        $("#txtAre_you_allergic_othersDetails").prop("disabled", true);
    }); $("#rdoAre_you_allergic_others").click(function() {
        $("#txtAre_you_allergic_othersDetails").prop("disabled", false);
    });


    $("#rdoNOmedication_regularly").click(function() {
        $("#MedicationTaking").slideUp("slow", function() {
            $("#MedicationTaking input[type=text], #MedicationTaking select, #MedicationTaking textarea").val("");
            $("#MedicationTaking input[type=radio], #MedicationTaking input[type=checkbox]").prop("checked", false);

        });
    });
    $("#rdoYESmedication_regularly").click(function() {
        $("#MedicationTaking").slideDown("slow");

    });
    $("#rdoDo_you_smoke_No").click(function() {
        $("#beforeQuitting").slideUp("slow", function() {
            $("#beforeQuitting input[type=text], #beforeQuitting select, #beforeQuitting textarea").val("");
            $("#beforeQuitting input[type=radio], #beforeQuitting input[type=checkbox]").prop("checked", false);

        });
        $("#ManyCigarettes").slideUp("slow", function() {
            $("#ManyCigarettes input[type=text], #ManyCigarettes select, #ManyCigarettes textarea").val("");
            $("#ManyCigarettes input[type=radio], #ManyCigarettes input[type=checkbox]").prop("checked", false);

        });
        $("#beenSmoking").slideUp("slow", function() {
            $("#beenSmoking input[type=text], #beenSmoking select, #beenSmoking textarea").val("");
            $("#beenSmoking input[type=radio], #beenSmoking input[type=checkbox]").prop("checked", false);

        });
        $("#How_many_cigarettes").slideUp("slow", function() {
            $("#How_many_cigarettes input[type=text], #How_many_cigarettes select, #How_many_cigarettes textarea").val("");
            $("#How_many_cigarettes input[type=radio], #How_many_cigarettes input[type=checkbox]").prop("checked", false);

        });
        $("#About_quit_smoking").slideUp("slow", function() {
            $("#About_quit_smoking input[type=text], #About_quit_smoking select, #About_quit_smoking textarea").val("");
            $("#About_quit_smoking input[type=radio], #About_quit_smoking input[type=checkbox]").prop("checked", false);

        });
    });
    $("#rdoDo_you_smoke_Yes").click(function() {
        $("#beforeQuitting").slideUp("slow", function() {
            $("#beforeQuitting input[type=text], #beforeQuitting select, #beforeQuitting textarea").val("");
            $("#beforeQuitting input[type=radio], #beforeQuitting input[type=checkbox]").prop("checked", false);

        });
        $("#ManyCigarettes").slideUp("slow", function() {
            $("#ManyCigarettes input[type=text], #ManyCigarettes select, #ManyCigarettes textarea").val("");
            $("#ManyCigarettes input[type=radio], #ManyCigarettes input[type=checkbox]").prop("checked", false);

        });
        $("#beenSmoking").slideDown("slow");
        $("#How_many_cigarettes").slideDown("slow");
        $("#About_quit_smoking").slideDown("slow");
    });
    $("#rdoDo_you_smoke_Yes_but").click(function() {

        $("#beforeQuitting").slideDown("slow");
        $("#ManyCigarettes").slideDown("slow");
        $("#beenSmoking").slideUp("slow", function() {
            $("#beenSmoking input[type=text], #beenSmoking select, #beenSmoking textarea").val("");
            $("#beenSmoking input[type=radio], #beenSmoking input[type=checkbox]").prop("checked", false);
        });
        $("#How_many_cigarettes").slideUp("slow", function() {
            $("#How_many_cigarettes input[type=text], #How_many_cigarettes select, #How_many_cigarettes textarea").val("");
            $("#How_many_cigarettes input[type=radio], #How_many_cigarettes input[type=checkbox]").prop("checked", false);
        });
        $("#About_quit_smoking").slideUp("slow", function() {
            $("#About_quit_smoking input[type=text], #About_quit_smoking select, #About_quit_smoking textarea").val("");
            $("#About_quit_smoking input[type=radio], #About_quit_smoking input[type=checkbox]").prop("checked", false);
        });
    });

    $("#rdoHave_you_ever_consumed_alcohol_No").click(function() {

        $("#stop_drinking").slideUp("slow", function() {
            $("#stop_drinking input[type=text], #stop_drinking select, #stop_drinking textarea").val("");
            $("#stop_drinking input[type=radio], #stop_drinking input[type=checkbox]").prop("checked", false);
        });
        $("#you_stopped").slideUp("slow", function() {
            $("#you_stopped input[type=text], #you_stopped select, #you_stopped textarea").val("");
            $("#you_stopped input[type=radio], #you_stopped input[type=checkbox]").prop("checked", false);
        });
        $("#consume_alcohol").slideUp("slow", function() {
            $("#consume_alcohol input[type=text], #consume_alcohol select, #consume_alcohol textarea").val("");
            $("#consume_alcohol input[type=radio], #consume_alcohol input[type=checkbox]").prop("checked", false);
        });
        $("#about_stop_drinking").slideUp("slow", function() {
            $("#about_stop_drinking input[type=text], #about_stop_drinking select, #about_stop_drinking textarea").val("");
            $("#about_stop_drinking input[type=radio], #about_stop_drinking input[type=checkbox]").prop("checked", false);
        });
    });

    $("#rdoHave_you_ever_consumed_alcohol_Yes").click(function() {
        $("#stop_drinking").slideUp("slow", function() {
            $("#stop_drinking input[type=text], #stop_drinking select, #stop_drinking textarea").val("");
            $("#stop_drinking input[type=radio], #stop_drinking input[type=checkbox]").prop("checked", false);
        });
        $("#you_stopped").slideUp("slow", function() {
            $("#you_stopped input[type=text], #you_stopped select, #you_stopped textarea").val("");
            $("#you_stopped input[type=radio], #you_stopped input[type=checkbox]").prop("checked", false);
        });
        $("#consume_alcohol").slideDown("slow");
        $("#about_stop_drinking").slideDown("slow");
    });

    $("#rdoHave_you_ever_consumed_alcohol_Yes_But").click(function() {
        $("#stop_drinking").slideDown("slow");
        $("#you_stopped").slideDown("slow");
        $("#consume_alcohol").slideUp("slow", function() {
            $("#consume_alcohol input[type=text], #consume_alcohol select, #consume_alcohol textarea").val("");
            $("#consume_alcohol input[type=radio], #consume_alcohol input[type=checkbox]").prop("checked", false);
        });
        $("#about_stop_drinking").slideUp("slow", function() {
            $("#about_stop_drinking input[type=text], #about_stop_drinking select, #about_stop_drinking textarea").val("");
            $("#about_stop_drinking input[type=radio], #about_stop_drinking input[type=checkbox]").prop("checked", false);
        });

    });

    $("#rdoHave_you_use_or_tried_any_drugs_No").click(function() {
        $("#did_you_used").slideUp("slow", function() {
            $("#did_you_used input[type=text], #did_you_used select, #did_you_used textarea").val("");
            $("#did_you_used input[type=radio], #did_you_used input[type=checkbox]").prop("checked", false);

        });
    });
    $("#rdoHave_you_use_or_tried_any_drugs_Yes").click(function() {
        $("#did_you_used").slideDown("slow");

    });
    $("#rdoHave_you_use_or_tried_any_drugs_Yes_But").click(function() {
        $("#did_you_used").slideDown("slow");

    });


    $("#rdoDo_you_exercise_play_sports_No").click(function() {
        $("#exercise_duration").slideUp("slow", function() {
            $("#exercise_duration input[type=text], #exercise_duration select, #exercise_duration textarea").val("");
            $("#exercise_duration input[type=radio], #exercise_duration input[type=checkbox]").prop("checked", false);

        });
    });
    $("#rdoDo_you_exercise_play_sports_Less_than_3_times").click(function() {
        $("#exercise_duration").slideDown("slow");

    });
    $("#rdo_you_exercise_play_sports_More_than_3_times").click(function() {
        $("#exercise_duration").slideDown("slow");

    });
    $('#txtMenoDateForm').datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });

    $("#txtVisitDate").datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });
    $("#txtBirthDate").datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });
    $("#txtEmploymentDate").datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });
    $("#txtDetails_Year_row_1").datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });
    $("#txtDetails_Year_row_2").datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });
    $("#txtPeriod_2_1_row_1_1").datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });
    $("#txtPeriod_2_1_row_1_2").datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });
    $("#txtPeriod_2_1_row_2_1").datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });
    $("#txtPeriod_2_1_row_2_2").datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });
    $("#txtPeriod_2_1_row_3_1").datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });
    $("#txtPeriod_2_1_row_3_2").datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });
    $("#txtPeriod_2_1_row_4_1").datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });
    $("#txtPeriod_2_1_row_4_2").datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });
    $("#txtPeriod_2_1_row_5_1").datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });
    $("#txtPeriod_2_1_row_5_2").datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });
    $("#txtPeriod_2_2_row_1_1").datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });
    $("#txtPeriod_2_2_row_1_2").datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });
    $("#txtMenoDateForm").datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });
    $("#txtMenoDateTo").datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });
    $("#txtDDMMYY_1").datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });
    $("#txtDDMMYY_2").datepicker({ dateFormat: 'dd-mm-yy', inline: true
    });
    $("#chkOtherClassificationofEmployment").click(function() { $("#txtOtherClassificationofEmployment").prop("disabled", false); });
    $("#chkOtherSpecialAssignment").click(function() { $("#txtOtherSpecialAssignment").prop("disabled", false); });
    $("#chkOthersHazard").click(function() { $("#txtOthersHazard").prop("disabled", false); });
    $("#chkOtherBiologicalHealtHazard").click(function() { $("#txtOthersBiologicalHealtHazard").prop("disabled", false); });
    $("#chkOtherChemicalhealthhazard").click(function() { $("#txtOtherChemicalhealthhazard").prop("disabled", false); });
    $("#chkPPEOther").click(function() { $("#txtPPEOtherDetails").prop("disabled", false); });
    $("#chkOtherDo_you_needTaking").click(function() { $("#txtOtherDo_you_needDetailsTaking").prop("disabled", false); });
    $("#chkOthers_please_specify").click(function() { $("#txtOthers_please_specify").prop("disabled", false); });
    $("#chkFather_Others").click(function() { $("#txtFather_other_details").prop("disabled", false); });
    $("#chkMother_Others").click(function() { $("#txtMother_other_details").prop("disabled", false); });
    $("#chkSiblings_Others").click(function() { $("#txtSiblings_other_details").prop("disabled", false); });
    $("#chkWhat_is_your_favorite_food_Others").click(function() { $("#txtWhat_is_your_favorite_food_Others_details").prop("disabled", false); });


});

