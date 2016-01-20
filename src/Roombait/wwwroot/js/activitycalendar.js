$(document).ready(function () {

    // page is now ready, initialize the calendar...

    $("#calendar").fullCalendar({
        // put your options and callbacks here
        header: {
            left:   "title",
            center: "",
            right:  "today prev,next"
        },

        aspectRatio: 2,

        events: "/Activity/Data/" + activityId,

        eventRender: function(event, element) {
            $(element).tooltip({
                title: event.memo,
                placement: "left"
            });
        }
    });

});