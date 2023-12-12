# tl2-tp10-2023-juanigramajo

## Explicación:

### 1. Pestañas del navbar
- Sin estar loggeado, únicamente puede acceder a crear un usuario o a login, todas las demás redireccionan a loggearse.
- Al crear un usuario únicamente se puede asignar el rol de un operador.

### 2. Permisos de sesión
- Un admin:
    - Puede crear usuarios con cualquier rol.
    - Puede editar y eliminar usuarios (no puede eliminar su propio usuario ni bajar su propio rango de rol).
    - Puede crear, editar y eliminar tableros.
    - Puede crear, editar y eliminar tareas.
- Un operador:
    - No puede editar ni eliminar usuarios, solo verlos.
    - No puede ver tableros que no le hayan sido asignados.
    - Puede crear un tablero.
    - No puede ver tareas que no le hayan sido asignadas.
    - Puede crear tareas.
    - Puede editar y eliminar tareas únicamente si le fueron asignadas.

---


## Corregir:
- En vez de escribir las funciones isLogger e isAdmin en cada controller usar un helper, en una clase estática.
- Que pide required en formularios cuando no esta solicitado en el viewmodel
- Hacer funcion Login en Usuario Repository
- Si el loggeado es admin, debería poder crear usuarios con rol admin. (VIEW DE CREAR USUARIO)
- Cualquier usuario puede crear tablero
    - (Un usuario operario puede crear tableros y tareas propias, y puede asignar tareas propias a ajenas
    tableros pueden hacer todos
    y solamente pueden crearlos para uno, es decir, no se puede crear un tablero y asignarle el tablero a otro usuario
    no importa si se es operario o admin)
- Advertir al usuario de los errores del try catch

---

## Preguntas de funcionalidades a resolver (no importante o necesario):
- Si no esta loggeado y creo un usuario, login con ese usuario?

---

## Se puede agregar:
- Si está loguedo ocultar botón login
- Cerrar sesión

---

## Consultar:
- Problema del selected en los formularios de editar tarea y usuario (estado y rol) (basically cuando toco editar, no se carga el dato, se pone el marcado en el selected)
    - (solucion momentanea, quitar el selected del editar usuario)
    - (segunda solucion momentanea, poner el selected en el primer valor)
- Por qué en los views, en @model, cuando trabajo con ViewModels tengo que mandarle una ruta específica y cuando eran clases normales no hacía falta
    - Ejemplo: @model Tarea
    - Ejemplo: @model tl2_tp10_2023_juanigramajo.ViewModels.Tareas.ListarTareasViewModel