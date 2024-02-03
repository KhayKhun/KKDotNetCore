const calendar = $("#calendar").calendarGC({
  dayBegin: 0,

  // add events
  onclickDate: function (e, data) {
    EditEvent("Add new event", "add").then((res) => {
      if (res.isConfirmed) {
        const newEvent = {
          date: data.datejs,
          eventName: res.event,
          className: `event ${uuidv4()}`,
          onclick: function (e, data) {
            EditEvent("Edit event title to...", "edit").then((res) => {
              if (res.isConfirmed) {
                // parse
                let events = [];
                const jsonStr = localStorage.getItem("events");
                if (jsonStr != null) {
                  events = JSON.parse(jsonStr);
                }
                events.map((singleEvent) => {
                  if (data.className == singleEvent.className) {
                    singleEvent.eventName = res.event;
                  }
                });
                // stringify
                localStorage.setItem("events", JSON.stringify(events));

                refresh();
                alertSuccess(res.event, "edit");
              }
              if (res.isDenied) {
                // parse
                let events = [];
                const jsonStr = localStorage.getItem("events");
                if (jsonStr != null) {
                  events = JSON.parse(jsonStr);
                }
                const filteredEvent = events.filter((singleEvent) => {
                  return data.className != singleEvent.className;
                });
                // stringify
                localStorage.setItem("events", JSON.stringify(filteredEvent));

                refresh();
                alertSuccess(res.event, "delete");
              }
            });
          },
          dateColor: "#29ab30",
        };

        let events = [];
        const jsonStr = localStorage.getItem("events");
        if (jsonStr != null) {
          events = JSON.parse(jsonStr);
        }
        newEvent.onclick = newEvent.onclick.toString();
        events.push(newEvent);
        localStorage.setItem("events", JSON.stringify(events));
        refresh();
        alertSuccess(res.event, "add");
      }
    });
  },
});

function refresh() {
  let events = [];
  const jsonStr = localStorage.getItem("events");
  if (jsonStr != null) {
    events = JSON.parse(jsonStr);
  }
  events.map((e) => {
    e.date = new Date(e.date);
    const fc = new Function("return " + e.onclick)();
    e.onclick = fc;
  });

  calendar.setEvents(events);
  $(".prev").click();
}

function uuidv4() {
  return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, (c) =>
    (
      c ^
      (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (c / 4)))
    ).toString(16)
  );
}

refresh();
