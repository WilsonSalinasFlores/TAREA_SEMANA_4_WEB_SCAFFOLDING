// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var guardarProfesor = async () => {
    var nombre = document.getElementById("nuevoNombre").value;
    var especialidad = document.getElementById("nuevoEspecialidad").value;
    if (!nombre || !especialidad) {
        alert("Por favor, complete todos los campos.");
        return;
    }
    var profesor = {
        nombre: nombre,
        especialidad: especialidad,
        id: 0,
        eliminado: false,
        creado: new Date().toISOString(),
    };
    console.log("Profesor guardado:", profesor);
    await fetch('/api/ProfesorApi', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(profesor)
    });
    // Cierra el modal de forma programática
    var myModal = bootstrap.Modal.getInstance(document.getElementById('nuevoProfesorModal'));
    myModal.hide();

    // Recarga la página para mostrar el nuevo profesor
    location.reload();
}

