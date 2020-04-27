function showSessionModal(session) {
  console.log(session);
  // title
  $("div.modal-header>.title").text(session.title);

  // practical info
  moment.locale("nl-be");
  let startDateTime = moment(session["startDateTime"]);
  let endDateTime = moment(session["endDateTime"]);

  $("div.modal-date>.info").text(startDateTime.format("D MMMM"));

  $("div.modal-time>.info").text(
    `${startDateTime.format("h:mm")} - ${endDateTime.format("h:mm")}`
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
    $("div.organiser").text(author["name"]);
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
    if (session['hasStarted']) {
      $("div.register").css("display", "none");
      $("div.attendance").css("display", "flex");
      console.log('hello');
      if (info['HasAttended']) {
        $("span.has-user-attended").html("<i class=\"fas fa-check\"></i> Aanwezig");
      } else {
        $("span.has-user-attended").html("<i class=\"fas fa-times\"></i> Niet Aanwezig");
      }
      $("span.session-attendances").text(`Aantal personen aanwezig: ${session['numberOfAttendances']}`);

    } else {
      $("div.attendance").css("display", "none");
      $("div.register").css("display", "flex");
      if(info['isRegistered']) {
        $("div.register button").text("Uitschrijven");
        // set the click behaviour to unregister here
      } else {
        $("div.register button").text("Inschrijven");
        // set the click behaviour to register here
      }
      
      $("span.session-free-spaces").text(`Nog ${session['availableRegistrationSpots']} vrije plaatsen`);
    }
  }
  // show modal
  $("#sessionModal").modal();
}
