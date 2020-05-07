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
    error: function() { alert('Server Error'); }
  });
  
  function fillFooter(info) {
    //See more
    $("button.see-more-btn").click(() => location.href = "/Session/" + session["id"]);

    // Register
    if (session['hasStarted']) {
      $("div.register").css("display", "none");
      $("div.attendance").css("display", "block");
      if (info['hasAttended']) {
        $("span.has-user-attended").html("<i class=\"fas fa-check\"></i> Aanwezig")
            .addClass("badge-attended")
            .removeClass("badge-not-attended");
      } else {
        $("span.has-user-attended").html("<i class=\"fas fa-times\"></i> Niet Aanwezig")
            .addClass("badge-not-attended")
            .removeClass("badge-attended");
      }
      $("span.session-attendances").text(`# Aanwezig: ${session['numberOfAttendees']}`);

    } else {
      $("div.attendance").css("display", "none");
      $("div.register").css("display", "flex");
      if(info['isRegistered']) {
        $("div.register button").text("Uitschrijven");
        // set the click behaviour to unregister here
      } else {
          if (!session["isOpen"]) {
              $("div.register button").prop('disabled', true);
              $("div.register button").text(`Inschrijven nog niet mogelijk`);
              $("span.session-free-spaces").css("display", "none");
          } else {
              $("span.session-free-spaces").css("display", "block");
              $("div.register button").prop('disabled', false);
              $("div.register button").text("Inschrijven");
              $("span.session-free-spaces").text(`Nog ${session['availableRegistrationSpots']} vrije plaatsen`);
          }
        // set the click behaviour to register here
      }
      
      
    }
  }
  // show modal
  $("#sessionModal").modal();
}
