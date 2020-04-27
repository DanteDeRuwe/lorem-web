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
    $("div.modal-speaker>").css("display", "inline-block");
    $("div.modal-speaker>.info").text(session['speakername']);
  } else {
    $("div.modal-speaker>").css("display", "none");
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
  
  // available spots
  $("span.session-free-spaces").text(`Nog ${session['availableRegistrationSpots']} vrije plaatsen`);

  // show modal
  $("#sessionModal").modal();
}
