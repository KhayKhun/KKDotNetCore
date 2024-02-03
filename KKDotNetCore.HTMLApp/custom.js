function AlertSuccess(message) {
  Swal.fire({
    title: "Success",
    text: message,
    icon: "success",
  });
}

function ConfirmDelete() {
  return new Promise(function (resolve, reject) {
    Swal.fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Delete",
    }).then((result) => {
      resolve(result.isConfirmed);
    });
  });
}
