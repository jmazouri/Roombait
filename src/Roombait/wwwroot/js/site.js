$(document).ready(function()
{
    var newPerformanceId = "";

    $(".deleteactivity").on("click", function()
    {
        toastr.options.closeButton = true;
        toastr.options.preventDuplicates = true;

        toastr.warning("<button id='confirmdelete' class='btn btn-danger' data-activity='" + $(this).attr("data-activity") + "'>Yes, delete it</button>",
            "Are you sure you want to delete this action?");

        event.preventDefault();
    });

    $(document).on("click", ".performactivity", function ()
    {
        var activityId = $(this).attr("data-activity");
        newPerformanceId = activityId;

        event.preventDefault();
    });

    $(document).on("click", ".activityperform", function ()
    {
        var antiForgery = $("[name='__RequestVerificationToken']").attr("value");

        $.post("/Activity/Perform", { activityId: newPerformanceId, memo: $("#PerformActivityMemo").val(), dateOverride: $("#performActivityDateOverride").val(), __RequestVerificationToken: antiForgery })
            .done(function (data)
            {
                toastr.success("Successfully performed activity.");
            })
            .fail(function (data)
            {
                toastr.error('There was an error in performing the activity.');
            });

        event.preventDefault();
    });

    $(document).on("click", "#confirmdelete", function () {
        var antiForgery = $("[name='__RequestVerificationToken']").attr("value");
        var activityId = $(this).attr("data-activity");

        $.post("/Activity/Delete", { activityId: activityId, __RequestVerificationToken: antiForgery })
            .done(function (data)
            {
                $(".activitybox[data-activity='" + activityId + "']").fadeOut();
            })
            .fail(function (data)
            {
                toastr.error('Could not delete activity: you are not the owner of the residence.');
            });

        event.preventDefault();
    });

    $(".residencecreate").on("click", function ()
    {
        var residenceName = $("#NewResidenceName").val();

        if (!residenceName)
        {
            return;
        }

        var antiForgery = $("[name='__RequestVerificationToken']").attr("value");

        $.post("/Residence/Create", { name: residenceName, __RequestVerificationToken: antiForgery })
            .done(function(data)
            {
                location.reload();
            });
    });

    $(".activitycreate").on("click", function () {
        var activityName = $("#NewActivityName").val();

        if (!activityName) {
            return;
        }

        var antiForgery = $("[name='__RequestVerificationToken']").attr("value");

        var values = new Array();
        $.each($("select[name='chosenDays[]'] > option:selected"), function () {
            values.push($(this).val());
        });

        $.post("/Activity/Create",
        {
            name: activityName,
            __RequestVerificationToken: antiForgery,
            AssociatedResidence: residenceId,
            DaysPerformedList: values
        })
        .done(function (data)
        {
            location.reload();
        });
    });

});

$('.submitbtn').on('click', function ()
{
    location.reload();
});

$('.keepopen').on('hide.bs.dropdown', function (e)
{
    return false;
});
