const getAllProducts = async () => {
  
    const products = JSON.parse(sessionStorage.getItem('Basket'));
    showProducts(products);
}


const showProducts = (data) => {


    const template = document.getElementById("temp-row");
    let sum = 0, count = 0;
    
    data.forEach(product => {
        sum += product.quantity;
        count += product.price * product.quantity
        const card = template.content.cloneNode(true)
        card.querySelector('img').src = '../Img/' + product.imageUrl
        card.querySelector('.price').innerText = product.quantity * product.price
        card.querySelector('.descriptionColumn').innerText = product.description
        card.querySelector('.availabilityColumn').textContent = product.quantity
        card.querySelector(".DeleteButton").addEventListener('click', () => { product.quantity = 1, removeFromBasket(product) });
        card.querySelector(".minus").addEventListener('click', () => { removeFromBasket(product) });
        card.querySelector(".plus").addEventListener('click', () => { addToBasket(product) });
        document.getElementById("itemList").appendChild(card)
    })
    
    document.getElementById('itemCount').innerHTML = sum;
    document.getElementById('totalAmount').innerHTML = count;
}
addToBasket = (product) => {
    const products = JSON.parse(sessionStorage.getItem('Basket'))
    const index = products.findIndex(p => p.productId == product.productId);
    products[index].quantity += 1
    sessionStorage.setItem('Basket', JSON.stringify(products))
    document.getElementById("itemList").replaceChildren();
    showProducts(products);
}
const removeFromBasket = (product) => {
    const products = JSON.parse(sessionStorage.getItem('Basket'));
    const index = products.findIndex(p => p.productId == product.productId)
    if (product.quantity == 1) {
        products.splice(index, 1)
    }
    else {
        products[index].quantity -= 1
    }
    sessionStorage.setItem('Basket', JSON.stringify(products));
    document.getElementById("itemList").replaceChildren();
    showProducts(products) 
}

const placeOrder = async() => {
    let orderItems = []
    const products = JSON.parse(sessionStorage.getItem('Basket'));
    if (!products)
    {
        alert("No products in basket!")
        window.location.href = 'Products.html'
    }

    products.forEach(e => orderItems.push({ "ProductId": e.productId, "ProductName": e.productName, "Quantity": e.quantity }))
    const orderData = {

        "OrderDate": new Date(),
        "OrderSum": parseInt(document.getElementById("totalAmount").innerHTML),
        "UserId": JSON.parse(sessionStorage.getItem('user')).userId,
        "OrderItems": orderItems
    }
    const responseOrder = await fetch('api/Orders', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(orderData)
    });
    

    if (responseOrder) {
        sessionStorage.removeItem("Basket")
        alert("the order is success")
        window.location.href = 'Products.html'
    }
    else {
        window.location.href = 'ShoppingBag.html'

    }
}
getAllProducts()