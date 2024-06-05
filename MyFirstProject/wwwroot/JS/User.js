const Register = async () => {

    const postData = {

        FirstName:document.getElementById("firstname2").value,
        LastName:document.getElementById("lastname2").value,
        Password:document.getElementById("password2").value,
        Email:document.getElementById("username2").value
    }
    
    const responseUser = await fetch('api/User/register', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(postData)
    });
    if (responseUser.status == 400) {
        alert("this password is not valid")
    }
    else { 
    const dataPost = await responseUser.json();
    if (!dataPost) {
        window.location.href = "AddUser.html"
    }
    else {
        window.location.href = "login.html";
    }
}}

const Login = async () => {

    
    const userData = {

        Password: document.getElementById("password3").value,
        Email: document.getElementById("username3").value
    }
    console.log(userData)
    const responseUser = await fetch('api/User/Login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData)
    });
   
    const dataUser = await responseUser;
    console.log(dataUser)
    
    if (dataUser) {
        sessionStorage.setItem("user", JSON.stringify(await dataUser.json()))
        window.location.href = 'Products.html'
       }
    else {
       window.location.href='AddUser.html'
    }
}
const Update = async () => {

    const userData = {

        FirstName: document.getElementById("firstname1").value,
        LastName: document.getElementById("lastname1").value,
        Password: document.getElementById("password1").value,
        Email: document.getElementById("username1").value
    }
    const id = JSON.parse(sessionStorage.getItem("user")).userId
    const responseUser = await fetch(`api/User/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData)
    });
    
    if (!responseUser.ok) {
        alert("this password is not valid")
        window.location.href='Login.html'
    }
    else {
        alert("User update!!!!!!!!!!!!")
        window.location.href = 'Login.html'
    }
}

const CheckPassword = async () => {

     Password= document.getElementById("Password").value

    const responsePost = await fetch('api/User/CheckPassword', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(Password)
    });
    const dataPost = await responsePost.json();
    let color = ''
    document.getElementById("progress").setAttribute("value", dataPost)
    if (dataPost <= 1)
        color = '#ff0000'
    else {
        if (dataPost <= 3)
            color = 'blue'
        else
            color = '#4cff00'
    }
    document.getElementById("progress").style.setProperty("accent-color", color)
    document.getElementById("strentgh").innerHTML = "strength: " + dataPost;
 }
