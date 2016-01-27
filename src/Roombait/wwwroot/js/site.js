// Write your Javascript code.
$(document).ready(function()
{
    $(".deleteactivity").on("click", function()
    {
        toastr.options.closeButton = true;
        toastr.options.preventDuplicates = true;

        toastr.warning("<button id='confirmdelete' class='btn btn-danger' data-activity='" + $(this).attr("data-activity") + "'>Yes, delete it</button>",
            "Are you sure you want to delete this action?");

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

    $('#chosenDays').multiselect();

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
        $.each($("option[name='chosenDays[]']:selected"), function () {
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
