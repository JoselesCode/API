//var nombre = document.getElementById('nombre');
var Email = document.getElementById('Email');
var password = document.getElementById('Password');
var error = document.getElementById('error');


function enviarFormulario(){
    console.log('Enviando formulario...');


    var mensajesError = [];


    //if(nombre.value === null  || nombre.value === ''){
    //    mensajesError.push('Nombre')
    //}
    if(Email.value === null  || Email.value === ''){
        mensajesError.push('Email')
    }
    if(password.value === null  || password.value === ''){
        mensajesError.push('Contraseña')
    }

    if(mensajesError.length > 0){
        error.innerHTML = "Por favor ingresa los siguientes datos: " + mensajesError.join(', ');
        return false;
    }

    return false
}
