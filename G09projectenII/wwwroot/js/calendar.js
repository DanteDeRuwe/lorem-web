/*
 * DISCLAIMER
 * This js file is not the most pretty file in existance.
 * It is also not what was intended for the project.
 *
 * We just wanted a better UX by showing a modal when the user clicks the event in the calendar.
 * The calendar is fullcalendar, and generates the html from js. We could therefore not access the html with MVC.
 * We did however make GET requests and DTO's to update this modal dynamically.
 *
 * If you would like to see how we did it in the MVC-way: check the session details page (Session views and SessionController)
 */




const loadLegend = () => {
    let html = `
            <!--Legend-->
            <div id="calendar-legend" class="d-none d-md-block">
                <span class="legend-item"><span class="text-primary">&bull;</span>Aangemaakt</span>
                <span class="legend-item"><span class="text-success">&bull;</span>Open</span>
                <span class="legend-item"><span class="text-warning">&bull;</span>Gesloten</span>
                <span class="legend-item"><span class="text-danger">&bull;</span>Afgelopen</span>
            </div>`;

    $(".fc-center").html(html);
}

function loadModal(sessionId) {
    $.ajax({
        type: "GET",
        url: "/Calendar/GetSessionBy?id=" + sessionId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: showSessionModal,
        error: function () {
            alert('Server Error');
        }
    });
}


function showSessionModal(session) {
    // title
    $("div.modal-header>.title").text(session.title);

    // practical info
    moment.locale("nl-be");
    let startDateTime = moment(session["startDateTime"]);
    let endDateTime = moment(session["endDateTime"]);

    $("div.modal-date>.info").text(startDateTime.format("dd D MMMM"));

    $("div.modal-time>.info").text(
        `${startDateTime.format("H:mm")} - ${endDateTime.format("H:mm")}`
    );

    $("div.modal-location>.info").text(session["location"]);

    // description
    if (session.description) {
        $("div.modal-description>.description").text(session.description);
        $("div.modal-description>.empty-description").empty();
    } else {
        $("div.modal-description>.empty-description").text(
            "Geen beschrijving beschikbaar."
        );
        $("div.modal-description>.description").empty();
    }

    // organiser and speaker
    $("div.modal-organiser>.info").text(session['organiser']['name']);
    if (session['speakername']) {
        $("div.modal-speaker").css("display", "inline-block");
        $("div.modal-speaker>.info").text(session['speakername']);

        $("div.modal-practical-info > div").removeClass("col-12 col-sm-4");
        $("div.modal-practical-info > div").addClass("col-12 col-sm-6 col-lg-3");

    } else {
        $("div.modal-speaker").css("display", "none");

        $("div.modal-practical-info > div").removeClass("col-12 col-sm-6 col-lg-3");
        $("div.modal-practical-info > div").addClass("col-12 col-sm-4");
    }

    // announcement
    let announcement = session["mostRecentAnnouncement"];
    if (announcement) {
        $("div.modal-announcement").css("display", "block");

        let author = announcement["author"];
        let announcementDate = moment(announcement["timestamp"]);

        $("img.profile-picture").attr("src", author["profilepicpath"]);
        $("div.organiser>.name").text(author["name"]);
        $("div.announcement-date").text(announcementDate.format("DD/MM/YY"));
        $("div.announcement-text").text(announcement['text']);
    } else {
        $("div.modal-announcement").css("display", "none");
    }

    $.ajax({
        type: "GET",
        url: "/Calendar/GetRegistrationStatus?sessionId=" + session['id'],
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: fillFooter,
        error: function () {
            alert('Server Error');
        }
    });

    function fillFooter(info) {
        //See more
        $("button.see-more-btn").click(() => location.href = "/Session/" + session["id"]);

        if (!info) {
            if (session['isOpen']) {
                $(".not-logged-in-register").css("visibility", "");
                $("span.not-logged-in-session-free-spaces").text(`(nog ${session['availableRegistrationSpots']} vrije plaatsen)`);
            } else {
                $(".not-logged-in-register").css("visibility", "hidden");
            }
            return;
        }

        // Register
        $(".not-logged-in-register").css("visibility", "hidden");
        if (session['hasStarted']) {
            $("div.register").css("display", "none");
            $("div.attendance").css("display", "block");
            $("div.modal-attendances").css("display", "block");
            if (info['hasAttended']) {
                $("span.has-user-attended").html("<i class=\"fas fa-check\"></i> Aanwezig")
                    .addClass("badge-attended")
                    .removeClass("badge-not-attended");
            } else {
                $("span.has-user-attended").html("<i class=\"fas fa-times\"></i> Niet Aanwezig")
                    .addClass("badge-not-attended")
                    .removeClass("badge-attended");
            }
            $(".modal-attendances>.info").text(`${session['numberOfAttendees']}/${session['numberOfRegistrees']} ingeschrevenen aanwezig`);

        } else {
            $("div.attendance").css("display", "none");
            $("div.modal-attendances").css("display", "none");
            $("div.register").css("display", "flex");
            if (info['isRegistered']) {
                $("div.register button").text("Uitschrijven");
                $("span.session-free-spaces").css("display", "none");
            } else {
                if (!session["isOpen"]) {
                    $("div.register button").prop('disabled', true);
                    $("div.register button").text(`Inschrijven nog niet mogelijk`);
                    $("span.session-free-spaces").css("display", "none");
                } else {
                    $("span.session-free-spaces").css("display", "block");
                    if(session['availableRegistrationSpots'] >= 0) {
                        $("div.register button").prop('disabled', false);
                    }
                    $("div.register button").text("Inschrijven");
                    $("span.session-free-spaces").text(`Nog ${session['availableRegistrationSpots']} vrije plaatsen`);
                }
            }

            $("div.register button").unbind('click');
            $("div.register button").click(toggleRegistration)

            function toggleRegistration() {
                $.ajax({
                    type: "POST",
                    url: "/Session/ToggleRegistration?sessionId=" + session['id'],
                });
                $("#sessionModal .close").click();
            }
        }
    }

    // show modal
    $("#sessionModal").modal();

}