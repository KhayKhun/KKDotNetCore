function EditEvent(title, type) {
  return new Promise(function (resolve, reject) {
    Swal.fire({
      title: title,
      input: "text",
      inputAttributes: {
        autocapitalize: "off",
      },
      showCancelButton: true,
      showDenyButton: type === "edit",
      denyButtonText: "Delete event",
      confirmButtonText: "Save",
      showLoaderOnConfirm: true,
      preConfirm: async (title) => {
        return title;
      },
      allowOutsideClick: () => !Swal.isLoading(),
    }).then((result) => {
      if (result.isConfirmed) {
        resolve({
          isConfirmed: result.isConfirmed,
          event: result.value,
        });
      }
      if (result.isDenied) {
        resolve({
          isDenied: result.isDenied,
          event: result.value,
        });
      }
    });
  });
}

function alertSuccess(event, type) {
  if (type === "add") {
    Swal.fire({
      title: `Added event "${event}".`,
      icon: "success",
    });
  }
  if (type === "edit") {
    Swal.fire({
      title: `Edited event title to "${event}".`,
      icon: "success",
    });
  }
  if (type === "delete") {
    Swal.fire({
      title: `Deleted an event`,
      icon: "success",
    });
  }
}