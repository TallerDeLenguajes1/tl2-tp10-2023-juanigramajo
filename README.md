<!-- # tl2-tp10-2023-juanigramajo -->

# Proyecto final - Taller de Lenguajes II

---

- Gramajo Juan Ignacio
- Licenciatura en Informática

---

## Informe:

### 1. Pestañas
- Sin estar loggeado, únicamente puede acceder a crear un usuario o a iniciar sesión, todas las demás redireccionan a loggearse.
- Si no tiene usuario, al crearse uno únicamente se puede asignar el rol de un operador.

### 2. Permisos de sesión
- Un admin:
    - Puede crear usuarios con cualquier rol.
    - Puede editar y eliminar usuarios (no puede eliminar su propio usuario ni bajar su propio rango de rol).
    - Puede crear, editar y eliminar tableros.
    - Puede crear, editar y eliminar tareas.
- Un operador:
    - No puede editar ni eliminar usuarios.
    - No puede editar ni eliminar tareas que no le fueron asignados.
    - No puede editar ni eliminar tableros que no le fueron asignados.
    - Puede crear un tablero y tareas.

### 3. Visualizaciones de tableros
Los tableros estan organizados en 3 secciones
- Mis tableros: Donde puede ver los tableros creados por el usuario.
- Mis tareas en otros tableros: Tableros que no fueron creados por el usuario pero donde tiene tareas asignadas.
- Explorar más tableros: Tableros que no fueron creados por el usuario y no tiene tareas en él.

### 4. Visualizaciones de tareas
Las tareas estan organizadas en 2 secciones por tableros
- Mis tareas en el tablero: Las cuales puede editar o eliminar.
- Listado de otras tareas en el tablero: Las cuales solo puede ver.


---




<!-- ## Corregir:
- En vez de escribir las funciones isLogger e isAdmin en cada controller usar un helper, en una clase estática.
- Que pide required en formularios cuando no esta solicitado en el viewmodel
- Hacer funcion Login en Usuario Repository (??)
- Poder tener tareas asignadas a mas usuarios (?)
- Que no se puedan crear tableros y tareas ya existentes.
- Cuando elimino un usuario, el tablero y tareas deben tener idUsuario en -9999


---

## Se puede agregar:
Ofrecer colores y poder agregar colores en tareas

---

## Consultar:
- Problema del selected en los formularios de editar tarea y usuario (estado y rol) (basically cuando toco editar, no se carga el dato, se pone el marcado en el selected)
    - (solucion momentanea, quitar el selected del editar usuario)
    - (segunda solucion momentanea, poner el selected en el primer valor)
- Cuando intente usar el index de tarea para mostrarlas por tablero no me dejaba, por eso tuve que crear otro index de tareas en una view en tableros.
 -->