// Write your Javascript code.
$(document).ready(function()
{
    $(".deleteactivity").on("click", function()
    {
        var antiForgery = $("[name='__RequestVerificationToken']").attr("value");

        $.post("/Activity/Delete", { activityId: $(this).attr("data-activity"), __RequestVerificationToken: antiForgery })
            .done(function(data)
            {
                location.reload();
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
        $.each($("input[name='chosenDays[]']:checked"), function () {
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

$('.btn-group').on('hide.bs.dropdown', function (e)
{
    return false;
});