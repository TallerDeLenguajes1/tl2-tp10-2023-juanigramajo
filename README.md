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
- Cualquier usuario puede crear tablero
    - (Un usuario operario puede crear tableros y tareas propias, y puede asignar tareas propias a ajenas
    tableros pueden hacer todos
    y solamente pueden crearlos para uno, es decir, no se puede crear un tablero y asignarle el tablero a otro usuario
    no importa si se es operario o admin)
- Advertir al usuario de los errores del try catch\
- Para crear una tarea, el IdTablero es FOREIGN KEY asi que si no esta el tablero dara un error, dar a corregir

---

## Se puede agregar:

---

## Consultar:
- Problema del selected en los formularios de editar tarea y usuario (estado y rol) (basically cuando toco editar, no se carga el dato, se pone el marcado en el selected)
    - (solucion momentanea, quitar el selected del editar usuario)
    - (segunda solucion momentanea, poner el selected en el primer valor)



### Para Proyecto Final únicamente.
Implemente la funcionalidad de asignar usuarios a tareas. El funcionamiento debería ser el siguiente:
- c. El usuario logueado debe poder asignar un usuario a las tareas de las que es propietario.
- d. El usuario logueado debería poder ver en la lista de tableros, además de los tableros que le pertenecen, todos los tableros donde tenga tareas asignadas. Los permisos del usuario logueado para tableros que no le pertenecen son:
- Tableros: Solo lectura
- Tareas no asignadas: Solo lectura.
- Tareas asignadas: Lectura y modificación, pero únicamente para modificar el estado de la tarea.