# tl2-tp10-2023-juanigramajo

---

## Corregir:
- En vez de escribir las funciones isLogger e isAdmin en cada controller usar un helper, en una clase estática.
- Si el usuario es administrador, no puede cambiar su propio rol a operador NO DEBE AUTOMODIFICARSE
- Un usuario no debería poder autoeliminar su usuario
- El nombre del usuario unicamente se muestra cuando recien se loguea (por el viewBag), cuando abro el home de otro lado no se muestra

---

## Preguntas de funcionalidades a resolver (no importante o necesario):
- Si no esta loggeado y creo un usuario, login con ese usuario?

---

## Se puede agregar:
- Cerrar sesión

---

## Consultar:
- Problema del selected en los formularios de editar tarea y usuario (estado y rol)
    - (solucion momentanea, quitar el selected del editar usuario)
    - (segunda solucion momentanea, poner el selected en el primer valor)
- LoginController/loggearUsuario sobre almacenar en sesión la contraseña
- Por qué en los views, en @model, cuando trabajo con ViewModels tengo que mandarle una ruta específica y cuando eran clases normales no hacía falta
    - Ejemplo: @model Tarea
    - Ejemplo: @model tl2_tp10_2023_juanigramajo.ViewModels.Tareas.ListarTareasViewModel
- Sobre el viewBag, como se implementaría con otro viewModel