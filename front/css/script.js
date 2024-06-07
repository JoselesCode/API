const form = document.querySelector('form');

form.addEventListener('submit', (event) => {
  event.preventDefault();

  const nombre = document.querySelector('#nombre').value;
  const asunto = document.querySelector('#asunto').value;
  const email = document.querySelector('#email').value;
  const mensaje = document.querySelector('#mensaje').value;

  // Validar los datos

  if (!nombre || !asunto || !email || !mensaje) {
    alert('Todos los campos son obligatorios');
    return;
  }

  // Enviar los datos a un servidor

  // Mostrar un mensaje de éxito o error

  alert('El mensaje se ha enviado correctamente');

  // Limpiar el formulario

  form.reset();
});