# tl2-tp10-2023-juanigramajo

---

## Corregir:
- En vez de escribir las funciones isLogger e isAdmin en cada controller usar un helper, en una clase estática.
- Que pide required en formularios cuando no esta solicitado en el viewmodel

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
- Por que un model de una vista depende del otro (ejemplo, el error que me salía cuando de un index que recibia un tipo de dato quería pasar a otro index que recibia otro tipo de dato)
- TableroController/Index() sobre la clase con los valores de inicio de sesion, debo hacerla cada vez que la uso y no parece ser eficaz ese método
- Sobre inyección de dependencia