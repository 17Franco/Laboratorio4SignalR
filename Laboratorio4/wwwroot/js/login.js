
const formulario = document.getElementById("loginForm");
const cartelError = document.getElementById("Error");
const cartelEmail = document.getElementById("mensajeVerificacion");

//creo conexion 
const conexion = new signalR.HubConnectionBuilder()
    .withUrl("/loginHub")
    .build();

//se ejecuta cuando se entra a la url
conexion.on("VerificacionOk", () => {
    window.location.href = "/Home/Inicio";
});

//inicio conexion
const conexionLista = conexion.start();

formulario.addEventListener("submit", async (evento) => {
    // Evitar el envío tradicional, que recargaría la página.
    evento.preventDefault();
    
    // Leer lo que escribió el usuario.
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;
    
    await conexionLista;
    
    //Datos que voy a enviar a validadLogin
    const datos = new URLSearchParams({
        email: email,
        password: password,
        connectionId: conexion.connectionId
    });
    
    const respuesta = await fetch("/Login/ValidaLogin", {
        method: "POST",
        body: datos
    });

    const resultado = await respuesta.json();

    //Segun respuesta del controller muestro cartel de envio de email o error
    if (resultado.valido) {
        console.log("Credenciales correctas. Falta confirmar el enlace.");
        cartelEmail.classList.remove("d-none");
    } else {
        console.log("Email o contraseña incorrectos.");
        cartelEmail.classList.add("d-none");
        cartelError.classList.remove("d-none");
        
        setTimeout(() =>{
            cartelError.classList.add   ("d-none");
        },3000)
    }
    
});