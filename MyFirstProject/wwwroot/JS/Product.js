let categoryIdArr = [];

// Fetch all products from the API and display them
const getAllProducts = async () => {
    const response = await fetch('api/products', {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
    });
    const data = await response.json();
    showProducts(data);
    document.getElementById("minPrice").value = data[0].price;
    document.getElementById("maxPrice").value = data[data.length - 1].price;
};

// Display products on the page
const showProducts = (products) => {
    const basket = JSON.parse(sessionStorage.getItem('Basket')) || [];
    const itemCount = basket.reduce((sum, product) => sum + product.quantity, 0);
    document.getElementById("ItemsCountText").textContent = itemCount;

    const template = document.getElementById("temp-card");
    const productList = document.getElementById("PoductList");
    productList.innerHTML = ''; // Clear existing products

    products.forEach((product) => {
        const card = template.content.cloneNode(true);
        card.querySelector('.h1').textContent = product.productName;
        card.querySelector('.price').textContent = product.price;
        card.querySelector('.description').textContent = product.description;
        card.querySelector('img').src = '../Img/' + product.imageUrl;
        card.querySelector('button').addEventListener("click", () => addToBasket(product));
        productList.appendChild(card);
    });
};

// Filter products based on search criteria and selected categories
const filterProducts = async () => {
    const search = document.getElementById("nameSearch").value;
    const minPrice = document.getElementById("minPrice").value;
    const maxPrice = document.getElementById("maxPrice").value;
    const categories = categoryIdArr.map(id => `&categories=${id}`).join('');

    const response = await fetch(`api/products?minPrice=${minPrice}&maxPrice=${maxPrice}&description=${search}${categories}`, {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
    });
    const data = await response.json();
    showProducts(data);
};

// Display categories for filtering
const showCategory = (categories) => {
    const template = document.getElementById("temp-category");
    const categoryList = document.getElementById("CategoryList");

    categories.forEach(category => {
        const option = template.content.cloneNode(true);
        option.querySelector('.opt').id = category.categoryId;
        option.querySelector('.opt').value = category.categoryName;
        option.querySelector('.OptionName').textContent = category.categoryName;
        option.querySelector('.opt').addEventListener("change", (event) => filterCategory(event, category));
        categoryList.appendChild(option);
    });
};

// Handle category selection and filtering
const filterCategory = (event, category) => {
    if (event.target.checked) {
        categoryIdArr.push(category.categoryId);
    } else {
        const index = categoryIdArr.indexOf(category.categoryId);
        if (index > -1) categoryIdArr.splice(index, 1);
    }
    filterProducts();
};

// Fetch and display all categories from the API
const getAllCategories = async () => {
    const response = await fetch('api/Categories', {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
    });
    const data = await response.json();
    showCategory(data);
};

// Add a product to the basket and update the basket count
const addToBasket = (product) => {
    const basket = JSON.parse(sessionStorage.getItem('Basket')) || [];
    const index = basket.findIndex(p => p.productId === product.productId);

    if (index !== -1) {
        basket[index].quantity += 1;
    } else {
        product.quantity = 1;
        basket.push(product);
    }

    sessionStorage.setItem('Basket', JSON.stringify(basket));
    const itemCount = basket.reduce((sum, p) => sum + p.quantity, 0);
    document.getElementById("ItemsCountText").textContent = itemCount;
};

// Initialize by fetching categories and products
getAllCategories();
getAllProducts();