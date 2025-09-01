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
//////////////////////////////////////////////CURSO////////////////////////////////////////////

var guardarCurso = async () => {
    var nombre = document.getElementById("nuevoNombre").value;
    var paralelo = document.getElementById("nuevoParalelo").value;
    var seccion = document.getElementById("nuevoSeccion").value;
    var periodo = document.getElementById("nuevoPeriodo").value;
    if (!nombre || !paralelo || !seccion || !periodo) {
        alert("Por favor, complete todos los campos.");
        return;
    }
    var curso = {
        nombre: nombre,
        paralelo: paralelo,
        seccion: seccion,
        periodo: periodo,
        id: 0,
        eliminado: false,
        creado: new Date().toISOString(),
    };
    console.log("Curso guardado:", curso);
    await fetch('/api/CursoApi', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(curso)
    });
    // Cierra el modal de forma programática
    var myModal = bootstrap.Modal.getInstance(document.getElementById('nuevoCursoModal'));
    myModal.hide();

    // Recarga la página para mostrar el nuevo curso
    location.reload();
}


var cargarUnCursoModal = async (id) => {
    var response = await fetch(`/api/CursoApi/${id}`);
    var curso = await response.json();
    document.getElementById("editarBoton").setAttribute("onclick", `editarCurso(${curso.id})`);
    document.getElementById("editarNombre").value = curso.nombre;
    document.getElementById("editarParalelo").value = curso.paralelo;
    document.getElementById("editarSeccion").value = curso.seccion;
    document.getElementById("editarPeriodo").value = curso.periodo;
}

var editarCurso = async (id) => {
    var nombre = document.getElementById("editarNombre").value;
    var paralelo = document.getElementById("editarParalelo").value;
    var seccion = document.getElementById("editarSeccion").value;
    var periodo = document.getElementById("editarPeriodo").value;

    if (!nombre || !paralelo || !seccion || !periodo) {
        alert("Por favor, complete todos los campos.");
        return;
    }

    var curso = {
        nombre: nombre,
        paralelo: paralelo,
        seccion: seccion,
        periodo: periodo,
        id: id,
        eliminado: false,
        creado: new Date().toISOString(),
        actualizado: new Date().toISOString()
    };

    console.log("Curso editado:", curso);
    await fetch(`/api/CursoApi/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(curso)
    });

    // Cierra el modal de forma programática
    var myModal = bootstrap.Modal.getInstance(document.getElementById('editarCursoModal'));
    myModal.hide();

    // Recarga la página para mostrar el curso editado
    location.reload();
}

var cargarEliminarCursoModal = async (id) => {
    var response = await fetch(`/api/CursoApi/${id}`);
    var curso = await response.json();
    document.getElementById("eliminarBoton").setAttribute("onclick", `eliminarCurso(${curso.id})`);
    document.getElementById("eliminarTitulo").innerText = `Eliminar ${curso.nombre} "${curso.paralelo}" ${curso.seccion} - ${curso.periodo}`;
}

var eliminarCurso = async (id) => {
    var response = await fetch(`/api/CursoApi/${id}`);
    var cursoAnterior = await response.json();

    var curso = {
        nombre: cursoAnterior.nombre,
        paralelo: cursoAnterior.paralelo,
        seccion: cursoAnterior.seccion,
        periodo: cursoAnterior.periodo,
        id: id,
        eliminado: true,
        creado: cursoAnterior.creado,
        actualizado: new Date().toISOString()
    };
    console.log("Curso editado:", curso);
    await fetch(`api/CursoApi/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(curso)
    });
    // Cierra el modal de forma programática
    var myModal = bootstrap.Modal.getInstance(document.getElementById('eliminarCursoModal'));
    myModal.hide();

    // Recarga la página para mostrar el curso eliminado
    location.reload();
}

//////////////////////////////////////////////Materia////////////////////////////////////////////
var CargarSelectsNuevaMateriaModal = async () => {
    await cargarSelectProfesores(-1, "nuevo");
    await cargarSelectCursos(-1, "nuevo");
}

var cargarSelectsModal = async (id) => {
    var response = await fetch(`/api/MateriaApi/${id}`);
    var materia = await response.json();

    // Cargar los select de profesores y cursos
    await cargarSelectProfesores(materia.profesorId, "editar");
    await cargarSelectCursos(materia.cursoId, "editar");

    // Establecer los valores seleccionados
 }
var cargarSelectProfesores = async (selectedId, modal) => {
    var response = await fetch('/api/ProfesorApi');
    var profesores = await response.json();

    var select = document.getElementById(`${modal}Profesor`);
    select.innerHTML = '';

    profesores.forEach(profesor => {
        var option = document.createElement("option");
        option.value = profesor.id;
        option.text = profesor.nombre ;
        if (profesor.id === selectedId) {
            option.selected = true;
        }
        select.appendChild(option);
    });
}

var cargarSelectCursos = async (selectedId, modal) => {
    var response = await fetch('/api/CursoApi');
    var cursos = await response.json();

    var select =  document.getElementById(`${modal}Curso`);
    select.innerHTML = '';

    cursos.forEach(curso => {
        var option = document.createElement("option");
        option.value = curso.id;
        option.text = curso.nombre + " " + curso.paralelo + " " + curso.seccion + " - " + curso.periodo;
        if (curso.id === selectedId) {
            option.selected = true;
        }
        select.appendChild(option);
    });
}


var guardarMateria = async () => {
    var materia = {
        nombre: document.getElementById("nuevoNombre").value,
        profesorId: document.getElementById("nuevoProfesor").value,
        cursoId: document.getElementById("nuevoCurso").value,
        eliminado: false,
        creado: new Date().toISOString(),
        actualizado: new Date().toISOString()

    };
    if (materia.nombre.trim()=="" ) {
        alert("Por favor, complete todos los campos.");
        return;
    }
    await fetch('/api/MateriaApi', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(materia)
    });

    // Cierra el modal de forma programática
    var myModal = bootstrap.Modal.getInstance(document.getElementById('nuevaMateriaModal'));
    myModal.hide();

    // Recarga la página para mostrar la nueva materia
    location.reload();
}

var cargarUnaMateriaModal = async (id) => {

    var response = await fetch(`/api/MateriaApi/${id}`);
    var materia = await response.json();

    await cargarSelectProfesores(materia.profesorId, "editar");
    await cargarSelectCursos(materia.cursoId, "editar");

    // Establecer los valores en los campos del modal
    document.getElementById("editarBoton").setAttribute("onclick", `editarMateria(${materia.id})`);
    document.getElementById("editarNombre").value = materia.nombre;
    document.getElementById("editarProfesor").value = materia.profesorId;
    document.getElementById("editarCurso").value = materia.cursoId;

}

var editarMateria = async (id) => {

    var materia = {
        nombre: document.getElementById("editarNombre").value,
        profesorId: document.getElementById("editarProfesor").value,
        cursoId: document.getElementById("editarCurso").value
    };
    if (materia.nombre.trim()=="" ) {
        alert("Por favor, complete todos los campos.");
        return;
    }
    await fetch(`/api/MateriaApi/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(materia)
    });

    // Cierra el modal de forma programática
    var myModal = bootstrap.Modal.getInstance(document.getElementById('editarMateriaModal'));
    myModal.hide();

    // Recarga la página para mostrar la materia editada
    location.reload();
}

var cargarEliminarMateriaModal = async (id) => {
    var response = await fetch(`/api/MateriaApi/${id}`);
    var materia = await response.json();

    document.getElementById("eliminarBoton").setAttribute("onclick", `eliminarMateria(${materia.id})`);
    document.getElementById("eliminarTitulo").innerText = `Eliminar ${materia.nombre}`;
}

var eliminarMateria = async (id) => {
    await fetch(`/api/MateriaApi/${id}`, {
        method: 'DELETE'
    });

    // Cierra el modal de forma programática
    var myModal = bootstrap.Modal.getInstance(document.getElementById('eliminarMateriaModal'));
    myModal.hide();

    // Recarga la página para mostrar la materia eliminada
    location.reload();
}


///////////////////////////Calificaciones////////////////////////////////////////////
var cargarPeriodos = async () => {
    var response = await fetch(`/Api/CursoApi/periodo/`);
    var periodos = await response.json();

    var select = document.getElementById("periodo");
    select.innerHTML = '';

    var option = document.createElement("option");
    option.value = "";
    option.text = "-- Seleccione un periodo --";
    select.appendChild(option);
    select.addEventListener("change", function() {
        cargarCalificaciones();
    });
    periodos.forEach(periodo => {
        var option = document.createElement("option");
        option.value = periodo;
        option.text = periodo;
        select.appendChild(option);
    });
}

var cargarCalificaciones = async () => {
    var response = await fetch(`api/CalificacionApi/periodo/${document.getElementById("periodo").value}`);
    var calificaciones = await response.json();

    var tbody = document.getElementById("calificaciones");
    tbody.innerHTML = '';
    console.log(calificaciones);
    calificaciones.forEach(calificacion => {
        var tr = document.createElement("tr");
        tr.innerHTML = `
            <td>${calificacion.curso}</td>
            <td>${calificacion.profesor}</td>
            <td>${calificacion.materia}</td>
            <td>${calificacion.nEstudiantes}</td>
        `;
        tbody.appendChild(tr);
    });
}

var cargarCurso = async () => {
    var response = await fetch(`/api/CursoApi`);
    var cursos = await response.json();

    var select = document.getElementById("SelectCursoId");
    select.innerHTML = '';

    var option = document.createElement("option");
    option.value = "";
    option.text = "-- Seleccione un curso --";
    select.appendChild(option);
    cursos.forEach(curso => {
        var option = document.createElement("option");
        option.value = curso.id;
        option.text = curso.nombre+" "+curso.paralelo+" "+curso.seccion+" - "+curso.periodo;
        select.appendChild(option);
    });
}

var cargarProfesores = async () => {
    var response = await fetch(`/api/ProfesorApi`);
    var profesores = await response.json();

    var select = document.getElementById("SelectProfesorId");
    select.innerHTML = '';

    var option = document.createElement("option");
    option.value = "";
    option.text = "-- Seleccione un profesor --";
    select.appendChild(option);
    profesores.forEach(profesor => {
        var option = document.createElement("option");
        option.value = profesor.id;
        option.text = profesor.nombre;
        select.appendChild(option);
    });
}

var cargarMateriasPorProfesorYCurso = async () => {
    var profesorId = document.getElementById("SelectProfesorId").value;
    var cursoId = document.getElementById("SelectCursoId").value;
    if ( profesorId>0 && cursoId>0) {  
        var response = await fetch(`/api/MateriaApi/ObtenerMaterias`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ Nombre: "", ProfesorId: profesorId, CursoId: cursoId })
        });

        if (response.ok) {
            var materias = await response.json();
            var select = document.getElementById("SelectMateriasId");
            select.innerHTML = '';

            var option = document.createElement("option");
            option.value = "";
            option.text = "-- Seleccione una materia --";
            select.appendChild(option);
            materias.forEach(materia => {
                var option = document.createElement("option");
                option.value = materia.id;
                option.text = materia.nombre;
                select.appendChild(option);
            });
        }
    }
}

var cargarCalificacionesNuevas = async () => {
    const CursoId = document.getElementById("SelectCursoId").value;
    const ProfesorId = document.getElementById("SelectProfesorId").value;
    const MateriaId = document.getElementById("SelectMateriasId").value;

    if (CursoId && ProfesorId && MateriaId) {
        var response = await fetch(`/api/CalificacionApi/obtenerCalificaciones`,
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    CursoId: CursoId,
                    ProfesorId: ProfesorId,
                    MateriaId: MateriaId
                })
            });

        const calificaciones = await response.json();

        var tbody = document.getElementById("calificacionesEstudiantes");
        tbody.innerHTML = '';
        console.log(calificaciones);
        calificaciones.forEach(calificacion => {
            var tr = document.createElement("tr");
            tr.id = `${calificacion.id}`;
            tr.innerHTML = `
                <td>${calificacion.estudiante.nombre} ${calificacion.estudiante.apellido}</td>
                <td>${calificacion.n1}</td>
                <td>${calificacion.n2}</td>
                <td>${calificacion.n3}</td>
                <td>${calificacion.promedio}</td>
                <td>${calificacion.observacion}</td>
                <td>
                    <button class="btn btn-warning" data-bs-toggle="modal" data-bs-target="#editarCalificacionModal" onclick="CargarSelectsEditarCalificacionModal(${calificacion.id})">Editar</button>
                </td>
            `;
            tbody.appendChild(tr);
        });
    }
}

var CargarSelectsEditarCalificacionModal = async (id) => {
    const response = await fetch(`/api/CalificacionApi/${id}`);
    const calificacion = await response.json();

    document.getElementById("editarNombre").value = `${calificacion.estudiante.nombre} ${calificacion.estudiante.apellido}`;
    document.getElementById("editarN1").value = calificacion.n1;
    document.getElementById("editarN2").value = calificacion.n2;
    document.getElementById("editarN3").value = calificacion.n3;
    document.getElementById("editarObservacion").value = calificacion.observacion;
    document.getElementById("editarBoton").setAttribute("onclick", `editarCalificacion(${calificacion.id})`);
}

var editarCalificacion = async (id) => {
    var n1 = parseFloat(document.getElementById("editarN1").value);
    var n2 = parseFloat(document.getElementById("editarN2").value);
    var n3 = parseFloat(document.getElementById("editarN3").value);
    var observacion = document.getElementById("editarObservacion").value;

    if (isNaN(n1) || isNaN(n2) || isNaN(n3)) {
        alert("Por favor, ingrese valores numéricos válidos para las notas.");
        return;
    }

    if (n1 < 0 || n2 < 0 || n3 < 0) {
        alert("Las notas no pueden ser menores a cero.");
        return;
    }

    if (n1 > 10 || n2 > 10 || n3 > 10) {
        alert("Las notas no pueden ser mayores a diez.");
        return;
    }

    const payload = {
        id: id,
        n1: n1,
        n2: n2,
        n3: n3,
        promedio: (n1 + n2 + n3) / 3,
        CursoId: document.getElementById("SelectCursoId").value,
        MateriaId: document.getElementById("SelectMateriasId").value,
        ProfesorId: document.getElementById("SelectProfesorId").value,
        observacion: observacion
    };
    console.log(payload);
    const response = await fetch(`/api/CalificacionApi/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(payload)
    });

    if (response.ok) {
        // La calificación se editó correctamente
        alert("Calificación editada correctamente");
        // Cerrar el modal
        var modal = bootstrap.Modal.getInstance(document.getElementById("editarCalificacionModal"));
        modal.hide();
        // Recargar las calificaciones
        cargarCalificacionesNuevas();
    } else {
        alert("Error al editar la calificación");
    }
}