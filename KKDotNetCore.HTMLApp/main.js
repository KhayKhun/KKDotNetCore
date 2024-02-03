const _tblName = "Tbl_name";
let _editItemId = null;

$("#btnSave").click(function () {
  if (_editItemId == null) {
    CreateItem();
  } else {
    UpdateItem();
  }
});

$("#btnCheck").click(function () {
  console.log(_editItemId);
});

function ReadItem() {
  if (localStorage.getItem(_tblName) == null) return;

  const jsonStr = localStorage.getItem(_tblName);
  const lst = JSON.parse(jsonStr);
  let trRows = "";
  let count = 0;
  lst.forEach((i) => {
    trRows += `
             <tr >
      
      <td ><button onclick="EditItem('${
        i.id
      }')">Edit</button><button onclick="DeleteItem('${
      i.id
    }')">Delete</button></td>
      <td>${++count}</td>
      <td>${i.item}</td>
    </tr>

        `;
  });

  $("#tblBody").html(trRows);
}

function CreateItem() {
  const name = $("#inputItem").val();
  console.log(name);
  if (name == "" || name == null) return;

  let lst = [];
  if (localStorage.getItem(_tblName) != null) {
    lst = JSON.parse(localStorage.getItem(_tblName));
  }
  lst.push({
    id: uuidv4(),
    item: name,
  });

  localStorage.setItem(_tblName, JSON.stringify(lst));

  $("#inputItem").val("");
  $("#inputItem").focus("");
  _editItemId = null;

  AlertSuccess("Saved item");
  ReadItem();
}

function EditItem(id) {
  if (localStorage.getItem(_tblName) == null) return;

  const jsonStr = localStorage.getItem(_tblName);
  const lst = JSON.parse(jsonStr);

  const result = lst.filter((x) => x.id == id);
  if (result.length == 0) {
    alert("no item found");
    return;
  }
  const item = result[0];
  _editItemId = item.id;

  $("#inputItem").val(item.item);
  console.log(_editItemId);
}

function DeleteItem(id) {
  if (localStorage.getItem(_tblName) == null) return;
  const jsonStr = localStorage.getItem(_tblName);
  const lst = JSON.parse(jsonStr);

  const index = lst.findIndex((x) => {
    return x.id == id;
  });
  console.log(lst[index].item);
  ConfirmDelete().then((isConfirmed) => {
    if (isConfirmed) {
      const result = lst.filter((x) => x.id != id);

      localStorage.setItem(_tblName, JSON.stringify(result));
      AlertSuccess('Deleted item');
      ReadItem();
    }
  });
}

function UpdateItem() {
  const name = $("#inputItem").val();
  if (name == "" || name == null) return;

  let lst = [];
  if (localStorage.getItem(_tblName) != null) {
    lst = JSON.parse(localStorage.getItem(_tblName));
  }
  console.log(lst);
  const index = lst.findIndex((x) => {
    console.log(x.id, _editItemId);
    return x.id == _editItemId;
  });
  lst[index].item = name;

  localStorage.setItem(_tblName, JSON.stringify(lst));

  $("#inputItem").val("");
  $("#inputItem").focus("");
  _editItemId = null;

  ReadItem();
}

function uuidv4() {
  return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, (c) =>
    (
      c ^
      (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (c / 4)))
    ).toString(16)
  );
}

ReadItem();
