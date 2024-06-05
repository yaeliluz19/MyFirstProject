let categoryIdArr = [];

const getAllProducts = async () => {

    const responsePost = await fetch('api/products', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });
    const dataPost = await responsePost.json();
    showProducts(dataPost);
    document.getElementById("minPrice").value = dataPost[0].price
    document.getElementById("maxPrice").value = dataPost[dataPost.length - 1].price
}

const showProducts = (data) => {
    const productArr = JSON.parse(sessionStorage.getItem('Basket')) || []
    let sumProducts = 0;
    productArr.forEach(p => { sumProducts += p.quantity })
    document.getElementById("ItemsCountText").textContent = sumProducts;

    const template = document.getElementById("temp-card");
    data.forEach((product) => {
        const card = template.content.cloneNode(true)
        card.querySelector('.h1').textContent = product.productName
        card.querySelector('.price').textContent = product.price
        card.querySelector('.description').textContent = product.description
        card.querySelector('img').src = '../Img/' + product.imageUrl
        card.querySelector('button').addEventListener("click", () => { addToBasket(product) })
     
        document.getElementById("PoductList").appendChild(card)
    }
    )
}

//categories function
const filterProducts = async () =>  {
    const search = document.getElementById("nameSearch").value;
    const minPrice = document.getElementById("minPrice").value;
    const maxPrice = document.getElementById("maxPrice").value;
    const categoryId = categoryIdArr;
    let c = ''
    categoryIdArr.forEach(e => c += `&categories=${e}`)
    const responsePost = await fetch(`api/products?minPrice=${minPrice}&maxPrice=${maxPrice}&description=${search}${c}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });
   
    const dataPost = await responsePost.json();
    document.getElementById("PoductList").replaceChildren();
    showProducts(dataPost);
}


const showCategory = (data) => {


    const template = document.getElementById("temp-category");
    data.forEach(category => {
        const card = template.content.cloneNode(true)

        card.querySelector('.opt').id = category.categoryId;
        card.querySelector('.opt').value = category.categoryName;
        //card.querySelector('label').for = category.CategoryName
        card.querySelector('.OptionName').textContent = category.categoryName;
        //card.querySelector('.Count').textContent = product.category.length
        card.querySelector('.opt').addEventListener("change", (event) => { filterCategory(event, category) })

        document.getElementById("CategoryList").appendChild(card)
    }
    )
}

const filterCategory = async (event, category) => {

    if (event.target.checked) {
        categoryIdArr.push(category.categoryId)
    }
    else {
        categoryIdArr.splice(categoryIdArr.indexOf(category.categoryId),1)
    }
    filterProducts();
}

const getAllCategories = async () => {

    const responsePost = await fetch('api/Categories', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });
    const dataPost = await responsePost.json();
    showCategory(dataPost);
}
    getAllCategories();
    getAllProducts();

const addToBasket = (product) => {

    const productArr = JSON.parse(sessionStorage.getItem('Basket')) || []
    const index = productArr.findIndex(p => p.productId == product.productId)
    if (index != -1) {
        productArr[index].quantity += 1
    }
    else {
        product.quantity = 1
        productArr.push(product)
    }
    sessionStorage.setItem('Basket', JSON.stringify(productArr));

    let sumProducts = 0;
    productArr.forEach(p => { sumProducts += p.quantity })
    document.getElementById("ItemsCountText").textContent = sumProducts;
    
}