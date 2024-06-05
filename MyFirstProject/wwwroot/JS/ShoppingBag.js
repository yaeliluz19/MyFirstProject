const getAllProducts = async () => {
  
    const products = JSON.parse(sessionStorage.getItem('Basket'));
    showProducts(products);
}


const showProducts = (data) => {


    const template = document.getElementById("temp-row");
    let sum = 0, count = 0;
    
    data.forEach(p => { sum += p.quantity; count += p.price * p.quantity })
    document.getElementById('itemCount').textContent = sum;
    document.getElementById('totalAmount').textContent = count;

    data.forEach((product) => {

        const card = template.content.cloneNode(true)
        card.querySelector('.image').src = '../Img/' + product.imageUrl
        card.querySelector('.price').innerText = product.quantity * product.price
        card.querySelector('.descriptionColumn').innerText = product.description
        //card.querySelector('h3').textContent = product.productName
        //card.querySelector('.itemNumber').textContent = product.price
        //card.querySelector('.availabilityColumn').innerText = product.description
        card.querySelector('.availabilityColumn').textContent = product.quantity
        card.querySelector('.DeleteButton').addEventListener("click", () => { removeFromBasket(product) })
        document.getElementById("itemList").appendChild(card)
    }
    )
}
/*const drawSelectedProducts = (products) => {

    const template = document.getElementById('temp-row');

    let sum = 0,count=0;

    
    products.forEach(p => { sum += p.quantity; count += p.price *p.quantity })
   
    //document.getElementById('itemCount').textContent = sum;
   // document.getElementById('totalAmount').textContent = count;

    products.forEach(item => {

        const row = template.content.cloneNode(true);
        row.querySelector(".price").innerText = item.price * item.quantity;
        row.querySelector(".image").src = '../Images/' + item.imageUrl;
        row.querySelector(".descriptionColumn").innerText = item.description;
        row.querySelector(".quantity").innerText = item.quantity
        row.querySelector(".DeleteButton").addEventListener('click', () => { item.quantity = 1; removeFromBasket(item) });
        
        
        row.querySelector(".plus").addEventListener('click', () => { addToBasket(item) });
        row.querySelector(".minus").addEventListener('click', () => { removeFromBasket(item) });

        document.getElementById("itemList").appendChild(row);
    });
}*/



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

    products.forEach(e => orderItems.push({ "ProductName": e.ProductName, "Quantity": e.Quantity }))
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
/*const userData = {

        Password: document.getElementById("password3").value,
        Email: document.getElementById("username3").value
    }
    const responseUser = await fetch('api/User/Login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData)
    });
   
    const dataUser = await responseUser;
    
    
    if (dataUser) {
        sessionStorage.setItem("user", JSON.stringify(await dataUser.json()))
        window.location.href = 'HomePage.html'
       }
    else {
       window.location.href='AddUser.html'
    } */

getAllProducts()