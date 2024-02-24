function Run(){
  const jsonStr = localStorage.getItem('products');

  if(jsonStr === null){
    fetch("products.json")
      .then((response) => {
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        return response.json();
      })
      .then((products) => {
        localStorage.setItem("products", JSON.stringify(products));
        Run();
      })
      .catch((error) => {
        console.error("There was a problem with the fetch operation:", error);
      });
  }else{
    const storedProducts = JSON.parse(jsonStr);
    const addedProducts = storedProducts.filter(p => p.isInCart === true);

    $(".item-count").html(addedProducts.length || 0);
    $(".cost-count").html(
      addedProducts.reduce(
        (accumulator, currentValue) => accumulator + currentValue.price,
        0 // start up accumulator value
      )
    );

    let allProductsString = "";
    let addedProductsString = "";

    storedProducts.map((product) => {
      allProductsString += `<li>
            <img class="lg" src="${product.image}"/>
            <h3 class="title">${product.title}</h3>
            <p class="price">$ ${product.price}</p>

            <button class="${product.isInCart ? "btn btn-danger" : "btn btn-success"}" onCLick="toggleItem(${product.id})"> ${
        product.isInCart ? "Remove" : "Add to cart"
      } </button>
        </li>`;
    });

    addedProducts.map((product) => {
      let titleMaxLength = 16;
      addedProductsString += `<li>
            <div class="content-in-cart">
            <img class="sm" src="${product.image}"/>
            <h3 class="title">${
              product.title.length >= titleMaxLength
                ? product.title.substring(0, titleMaxLength) + "..."
                : product.title
            }</h3>
            <p class="price">$ ${product.price}</p>
            </div>

            <button class="btn btn-danger btn-remove" onCLick="toggleItem(${
              product.id
            })">Cancel</button>
        </li>`;
    });

    $(".item-list").html(allProductsString);
    $(".added-list").html(addedProductsString);
  }
}


function toggleItem(id) {

  const products = JSON.parse(localStorage.getItem("products"));

  const newProducts = products.map((product) => {
    if (product.id === id) return { ...product, isInCart: !product.isInCart };
    return product;
  });

  localStorage.setItem("products", JSON.stringify(newProducts));
  Run();
}

Run();