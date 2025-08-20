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

var cargarUnProfesorModal = async (id) => {
    var response = await fetch(`/api/ProfesorApi/${id}`);
    var profesor = await response.json();
    document.getElementById("editarBoton").setAttribute("onclick", `editarProfesor(${profesor.id})`);
    document.getElementById("editarNombre").value = profesor.nombre;
    document.getElementById("editarEspecialidad").value = profesor.especialidad;
}

var editarProfesor = async (id) => {
    var nombre = document.getElementById("editarNombre").value;
    var especialidad = document.getElementById("editarEspecialidad").value;
    if (!nombre || !especialidad) {
        alert("Por favor, complete todos los campos.");
        return;
    }
    var response = await fetch(`/api/ProfesorApi/${id}`);
    var profesorAnterior = await response.json();

    var profesor = {
        nombre: nombre,
        especialidad: especialidad,
        id: id,
        eliminado: profesorAnterior.eliminado,
        creado: profesorAnterior.creado,
        actualizado: new Date().toISOString()
    };
    console.log("Profesor editado:", profesor);
    await fetch(`api/ProfesorApi/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(profesor)
    });
    // Cierra el modal de forma programática
    var myModal = bootstrap.Modal.getInstance(document.getElementById('editarProfesorModal'));
    myModal.hide();

    // Recarga la página para mostrar el profesor editado
    location.reload();
}

var cargarEliminarProfesorModal = async (id) => {
    var response = await fetch(`/api/ProfesorApi/${id}`);
    var profesor = await response.json();
    document.getElementById("eliminarBoton").setAttribute("onclick", `eliminarProfesor(${profesor.id})`);
    document.getElementById("eliminarTitulo").innerText = `Eliminar ${profesor.nombre}`;
}

var eliminarProfesor = async (id) => {
    var response = await fetch(`/api/ProfesorApi/${id}`);
    var profesorAnterior = await response.json();

    var profesor = {
        nombre: profesorAnterior.nombre,
        especialidad: profesorAnterior.especialidad,
        id: id,
        eliminado: true,
        creado: profesorAnterior.creado,
        actualizado: new Date().toISOString()
    };
    console.log("Profesor editado:", profesor);
    await fetch(`api/ProfesorApi/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(profesor)
    });
    // Cierra el modal de forma programática
    var myModal = bootstrap.Modal.getInstance(document.getElementById('eliminarProfesorModal'));
    myModal.hide();

    // Recarga la página para mostrar el profesor editado
    location.reload();
}
