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

  // announcement
  let announcement = session["mostRecentAnnouncement"];
  if (announcement) {
    let author = announcement["author"];
    $("img.profile-picture").attr("src", author["profilepicpath"]);
    $("div.organiser").text(author["name"]);
    let announcementDate = moment(announcement["timestamp"]);
    $("div.announcement-date").text(announcementDate.format("DD/MM/YY"));
  }

  // show modal
  $("#sessionModal").modal();
}
