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
- Hacer funcion Login en Usuario Repository (??)
- Poder tener tareas asignadas a mas usuarios (?)
- Que no se puedan crear usuarios tableros y tareas ya existentes


---

## Se puede agregar:
Ofrecer colores y poder agregar colores en tareas

---

## Consultar:
- Problema del selected en los formularios de editar tarea y usuario (estado y rol) (basically cuando toco editar, no se carga el dato, se pone el marcado en el selected)
    - (solucion momentanea, quitar el selected del editar usuario)
    - (segunda solucion momentanea, poner el selected en el primer valor)
- Cuando intente usar el index de tarea para mostrarlas por tablero no me dejaba, por eso tuve que crear otro index de tareas en una view en tableros.
